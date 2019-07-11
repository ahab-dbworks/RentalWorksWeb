using FwStandard.BusinessLogic;
using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.Modules.Administrator.AlertCondition;
using FwStandard.Modules.Administrator.AlertWebUsers;
using FwStandard.Modules.Administrator.WebAlertLog;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace FwStandard.Modules.Administrator.Alert
{

    public static class AlertFunc
    {

        public class Alert
        {
            public AlertLogic alert = new AlertLogic();
            public List<AlertConditionLogic> conditions = new List<AlertConditionLogic>();
            public List<AlertWebUsersLogic> recipients = new List<AlertWebUsersLogic>();
        }

        public class AlertEmailResponse
        {
            public bool Success { get; set; } = false;
            public string ErrorMessage { get; set; } = "";
        }

        public static List<Alert> Alerts = new List<Alert>();   // singular static list of all Alerts in the entire system.  #jhtodo: switch to Dictionary of modules with Dictionary of alerts for speed

        //temporary individual class fields.  shoudl move EmailSettingsLogic to Fw for this
        private static string emailAccountName = "";
        private static string emailAccountPassword = "";
        private static string emailAuthType = "";
        private static string emailHost = "";
        private static string emailDomain = "";
        private static int emailPort = 0;

        public const string ALERT_CONDITION_CONTAINS = "CONTAINS";
        public const string ALERT_CONDITION_STARTS_WITH = "STARTSWITH";
        public const string ALERT_CONDITION_ENDS_WITH = "ENDSWITH";
        public const string ALERT_CONDITION_EQUALS = "EQUALS";
        public const string ALERT_CONDITION_DOES_NOT_CONTAIN = "DOESNOTCONTAIN";
        public const string ALERT_CONDITION_DOES_NOT_EQUAL = "DOESNOTEQUAL";
        public const string ALERT_CONDITION_CHANGED_BY = "CHANGEDBY";

        //-------------------------------------------------------------------------------------------------------        
        public static void RefreshAlerts(FwApplicationConfig appConfig)
        {
            //this method gets called once on system start up, and whenever an AlertLogic is saved or deleted

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select top 1 accountname, accountpassword, authtype, host, port ");
                    qry.Add(" from  emailreportcontrol with (nolock)                         ");
                    FwJsonDataTable dt = qry.QueryToFwJsonTableAsync().Result;
                    emailAccountName = dt.Rows[0][dt.GetColumnNo("accountname")].ToString().Trim();
                    emailAccountPassword = dt.Rows[0][dt.GetColumnNo("accountpassword")].ToString().Trim();
                    emailAuthType = dt.Rows[0][dt.GetColumnNo("authtype")].ToString().Trim();
                    emailHost = dt.Rows[0][dt.GetColumnNo("host")].ToString().Trim();
                    emailDomain = ""; //??
                    emailPort = FwConvert.ToInt32(dt.Rows[0][dt.GetColumnNo("port")].ToString().Trim());
                }
            }

            BrowseRequest alertBrowseRequest = new BrowseRequest();
            AlertLogic l = new AlertLogic();
            l.AppConfig = appConfig;
            List<AlertLogic> alerts = l.SelectAsync<AlertLogic>(alertBrowseRequest).Result;

            List<Alert> newAlerts = new List<Alert>();

            foreach (AlertLogic alert in alerts)
            {
                Alert a = new Alert();
                a.alert = alert;

                BrowseRequest conditionBrowseRequest = new BrowseRequest();
                conditionBrowseRequest.uniqueids = new Dictionary<string, object>();
                conditionBrowseRequest.uniqueids.Add("AlertId", alert.AlertId);

                AlertConditionLogic ac = new AlertConditionLogic();
                ac.AppConfig = appConfig;
                a.conditions = ac.SelectAsync<AlertConditionLogic>(conditionBrowseRequest).Result;


                BrowseRequest recipientBrowseRequest = new BrowseRequest();
                recipientBrowseRequest.uniqueids = new Dictionary<string, object>();
                recipientBrowseRequest.uniqueids.Add("AlertId", alert.AlertId);

                AlertWebUsersLogic ar = new AlertWebUsersLogic();
                ar.AppConfig = appConfig;
                a.recipients = ar.SelectAsync<AlertWebUsersLogic>(recipientBrowseRequest).Result;

                newAlerts.Add(a);
            }

            lock (Alerts)
            {
                Alerts.Clear();
                Alerts.AddRange(newAlerts);
            }
        }
        //-------------------------------------------------------------------------------------------------------        
        public static async void ProcessAlerts(FwApplicationConfig appConfig, FwUserSession userSession, string moduleName, FwBusinessLogic oldObject, FwBusinessLogic newObject, TDataRecordSaveMode saveMode)
        {
            if (Alerts.Count > 0)
            {
                if ((oldObject == null) || (oldObject.GetType().Equals(newObject.GetType())))  // newObject and oldObject must be the same type.  or original can be null, which means we are Inserting a new record
                {
                    List<Alert> moduleAlerts = new List<Alert>();

                    //iterate through the global alerts and get the ones that are for this module //#jhtodo: optimize with Dictionary
                    foreach (Alert alert in Alerts)
                    {
                        if (alert.alert.ModuleName.Equals(moduleName))
                        {
                            moduleAlerts.Add(alert);
                        }
                    }

                    if (moduleAlerts.Count > 0)
                    {
                        PropertyInfo[] propertyInfo = newObject.GetType().GetProperties();

                        foreach (Alert alert in moduleAlerts)
                        {
                            bool allConditionsMet = true;
                            foreach (AlertConditionLogic condition in alert.conditions)
                            {
                                bool thisConditionMet = false;
                                object newValue = GetFwBusiessLogicPropertyByName(propertyInfo, condition.FieldName, oldObject, newObject);

                                if (newValue != null)
                                {
                                    string conditionValue = condition.Value;
                                    switch (condition.Condition)
                                    {
                                        case ALERT_CONDITION_CONTAINS:
                                            thisConditionMet = newValue.ToString().Contains(conditionValue);
                                            break;
                                        case ALERT_CONDITION_STARTS_WITH:
                                            thisConditionMet = newValue.ToString().StartsWith(conditionValue);
                                            break;
                                        case ALERT_CONDITION_ENDS_WITH:
                                            thisConditionMet = newValue.ToString().EndsWith(conditionValue);
                                            break;
                                        case ALERT_CONDITION_EQUALS:
                                            thisConditionMet = newValue.ToString().Equals(conditionValue);
                                            break;
                                        case ALERT_CONDITION_DOES_NOT_CONTAIN:
                                            thisConditionMet = !newValue.ToString().Contains(conditionValue);
                                            break;
                                        case ALERT_CONDITION_DOES_NOT_EQUAL:
                                            thisConditionMet = !newValue.ToString().Equals(conditionValue);
                                            break;
                                        case ALERT_CONDITION_CHANGED_BY:
                                            //thisConditionMet =  //#jhtodo
                                            break;
                                    }
                                }

                                if (!thisConditionMet)
                                {
                                    allConditionsMet = false;
                                    break;
                                }
                            }

                            if (allConditionsMet)
                            {
                                string alertSubject = alert.alert.AlertSubject;
                                string alertBody = alert.alert.AlertBody;
                                var pattern = @"(?<=\[)[^[]*(?=\])";
                                var subjectMatches = Regex.Matches(alertSubject, pattern);
                                var bodyMatches = Regex.Matches(alertBody, pattern);

                                foreach (Match sm in subjectMatches)
                                {
                                    string field = sm.ToString();
                                    object value = GetFwBusiessLogicPropertyByName(propertyInfo, field, oldObject, newObject);

                                    if (value != null)
                                    {
                                        alertSubject = alertSubject.Replace("[" + field + "]", value.ToString());
                                    }
                                }

                                foreach (Match bm in bodyMatches)
                                {
                                    string field = bm.ToString();
                                    object value = GetFwBusiessLogicPropertyByName(propertyInfo, field, oldObject, newObject);

                                    if (value != null)
                                    {
                                        alertBody = alertBody.Replace("[" + field + "]", value.ToString());
                                    }
                                }

                                List<string> toEmails = new List<string>();
                                foreach (AlertWebUsersLogic user in alert.recipients)
                                {
                                    toEmails.Add(user.Email);
                                }

                                //#jhtodo: need to add a global sending account for this. Alerts should not come from specific user emails.
                                //string from = "RentalWorksWeb@rentalworks.com";
                                string from;
                                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                                {
                                    from = FwSqlCommand.GetStringDataAsync(conn, appConfig.DatabaseSettings.QueryTimeout, "webusersview", "webusersid", userSession.WebUsersId, "email").Result;
                                }
                                string to = String.Join(";", toEmails);


                                WebAlertLogLogic log = new WebAlertLogLogic();
                                log.SetDependencies(appConfig, userSession);
                                log.AlertId = alert.alert.AlertId;
                                log.AlertSubject = alertSubject;
                                log.AlertBody = alertBody;
                                log.AlertFrom = from;
                                log.AlertTo = to;
                                log.Status = "PENDING";
                                log.CreateDateTime = DateTime.Now;
                                await log.SaveAsync(null);

                                AlertEmailResponse emailResponse = await SendEmailAsync(from, to, alertSubject, alertBody);

                                if (emailResponse.Success)
                                {
                                    log.Status = "SENT";
                                }
                                else
                                {
                                    log.Status = "FAILED";
                                    log.ErrorMessage = emailResponse.ErrorMessage;
                                }
                                await log.SaveAsync(null);
                            }
                        }
                    }
                }
            }
            return;
        }
        //-------------------------------------------------------------------------------------------------------        
        private static object GetFwBusiessLogicPropertyByName(PropertyInfo[] propertyInfo, string propertyName, FwBusinessLogic oldObject, FwBusinessLogic newObject)
        {
            object oldValue = null;
            object newValue = null;
            bool propertyFound = false;

            if (!propertyFound)
            {
                foreach (PropertyInfo property in propertyInfo)
                {
                    if (property.Name.Equals(propertyName))
                    {
                        propertyFound = true;
                        newValue = newObject.GetType().GetProperty(property.Name).GetValue(newObject);

                        if (oldObject != null)
                        {
                            oldValue = oldObject.GetType().GetProperty(property.Name).GetValue(oldObject);
                        }

                        if (newValue == null)
                        {
                            newValue = oldValue;
                        }

                        break;
                    }
                }
            }

            if (!propertyFound)  // property not found, check Custom Fields
            {
                FwCustomFields customFields = (oldObject != null ? oldObject._Custom.CustomFields : newObject._Custom.CustomFields);
                foreach (FwCustomField customField in customFields)
                {
                    if (customField.FieldName.Equals(propertyName))
                    {
                        propertyFound = true;
                        if (oldObject != null)
                        {
                            for (int customFieldIndex = 0; customFieldIndex < oldObject._Custom.Count; customFieldIndex++)
                            {
                                if (oldObject._Custom[customFieldIndex].FieldName.Equals(customField.FieldName))
                                {
                                    oldValue = oldObject._Custom[customFieldIndex].FieldValue;
                                    break;
                                }
                            }
                        }

                        for (int customFieldIndex = 0; customFieldIndex < newObject._Custom.Count; customFieldIndex++)
                        {
                            if (newObject._Custom[customFieldIndex].FieldName.Equals(customField.FieldName))
                            {
                                newValue = newObject._Custom[customFieldIndex].FieldValue;
                                break;
                            }
                        }

                        if (newValue == null)
                        {
                            newValue = oldValue;
                        }
                        break;
                    }
                }
            }
            return newValue;
        }
        //------------------------------------------------------------------------------------
        private static async Task<AlertEmailResponse> SendEmailAsync(string from, string to, string subject, string body)
        {
            AlertEmailResponse response = new AlertEmailResponse();
            try
            {
                var message = new MailMessage(from, to, subject, body);
                message.IsBodyHtml = true;
                var client = new SmtpClient(emailHost, emailPort);
                client.Credentials = new NetworkCredential(emailAccountName, emailAccountPassword, emailDomain);
                await client.SendMailAsync(message);
                response.Success = true;
            }
            //catch (SmtpException ex)
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }
        //------------------------------------------------------------------------------------ 
    }
}
