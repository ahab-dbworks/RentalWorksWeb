using FwStandard.BusinessLogic;
using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.Modules.Administrator.AlertCondition;
using FwStandard.Modules.Administrator.AlertWebUsers;
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
        public static List<AlertLogic> alerts = new List<AlertLogic>();   // singular static list of all Alerts in the entire system.  #jhtodo: switch to Dictionary of modules with Dictionary of alerts for speed

        //-------------------------------------------------------------------------------------------------------        
        public static void RefreshAlerts(FwApplicationConfig appConfig)
        {
            //this method gets called once on system start up, and whenever an AlertLogic is saved or deleted

            BrowseRequest browseRequest = new BrowseRequest();
            browseRequest.module = "Alert";
            AlertLogic l = new AlertLogic();
            l.AppConfig = appConfig;
            lock (alerts)
            {
                alerts = l.SelectAsync<AlertLogic>(browseRequest).Result;
            }

        }
        //-------------------------------------------------------------------------------------------------------        
        public static async void ProcessAlerts(FwApplicationConfig appConfig, FwUserSession userSession, string moduleName, FwBusinessLogic oldObject, FwBusinessLogic newObject, TDataRecordSaveMode saveMode)
        {
            if ((oldObject == null) || (oldObject.GetType().Equals(newObject.GetType())))  // newObject and oldObject must be the same type.  or original can be null, which means we are Inserting a new record
            {
                if ((alerts == null) || (alerts.Count == 0))
                {
                    // this should really not happen, but added here just in case
                    RefreshAlerts(appConfig);
                }
                if (alerts != null)
                {
                    List<AlertLogic> alertList = new List<AlertLogic>();

                    //iterate through the global alerts and get the ones that are for this module
                    foreach (AlertLogic alert in alerts)
                    {
                        if (alert.ModuleName.Equals(moduleName))
                        {
                            alertList.Add(alert);
                        }
                    }

                    if (alertList.Count > 0)
                    {
                        PropertyInfo[] propertyInfo = newObject.GetType().GetProperties();

                        foreach (AlertLogic alert in alertList)
                        {
                            BrowseRequest request = new BrowseRequest();
                            request.uniqueids = new Dictionary<string, object>();
                            request.uniqueids.Add("AlertId", alert.AlertId);

                            AlertConditionLogic ac = new AlertConditionLogic();
                            ac.SetDependencies(appConfig, userSession);
                            List<AlertConditionLogic> alertConditions = await ac.SelectAsync<AlertConditionLogic>(request);

                            bool allConditionsMet = true;
                            foreach (AlertConditionLogic condition in alertConditions)
                            {
                                bool thisConditionMet = false;
                                string acFieldName = condition.FieldName;
                                string acCondition = condition.Condition;
                                string acValue = condition.Value;
                                object newValue = GetFwBusiessLogicPropertyByName(propertyInfo, acFieldName, oldObject, newObject);

                                if (newValue != null)
                                {
                                    switch (acCondition)
                                    {
                                        case "CONTAINS":
                                            thisConditionMet = newValue.ToString().Contains(acValue);
                                            break;
                                        case "STARTSWITH":
                                            thisConditionMet = newValue.ToString().StartsWith(acValue);
                                            break;
                                        case "ENDSWITH":
                                            thisConditionMet = newValue.ToString().EndsWith(acValue);
                                            break;
                                        case "EQUALS":
                                            thisConditionMet = newValue.ToString().Equals(acValue);
                                            break;
                                        case "DOESNOTCONTAIN":
                                            thisConditionMet = !newValue.ToString().Contains(acValue);
                                            break;
                                        case "DOESNOTEQUAL":
                                            thisConditionMet = !newValue.ToString().Equals(acValue);
                                            break;
                                        case "CHANGEDBY":
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
                                string alertSubject = alert.AlertSubject;
                                string alertBody = alert.AlertBody;
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

                                BrowseRequest awuRequest = new BrowseRequest();
                                awuRequest.uniqueids = new Dictionary<string, object>();
                                awuRequest.uniqueids.Add("AlertId", alert.AlertId);

                                AlertWebUsersLogic awuLoader = new AlertWebUsersLogic();
                                awuLoader.SetDependencies(appConfig, userSession);
                                List<AlertWebUsersLogic> alertWebUsers = awuLoader.SelectAsync<AlertWebUsersLogic>(awuRequest).Result;

                                List<string> toEmails = new List<string>();
                                foreach (AlertWebUsersLogic user in alertWebUsers)
                                {
                                    toEmails.Add(user.Email);
                                }

                                string from;
                                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                                {
                                    from = FwSqlCommand.GetStringDataAsync(conn, appConfig.DatabaseSettings.QueryTimeout, "webusersview", "webusersid", userSession.WebUsersId, "email").Result;
                                }
                                string to = String.Join(";", toEmails);
                                // add to web alert log w/ status of pending, then update status in try/catch block
                                bool sent = await SendEmailAsync(from, to, alertSubject, alertBody, appConfig);
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



        private static async Task<bool> SendEmailAsync(string from, string to, string subject, string body, FwApplicationConfig appConfig)
        {
            var message = new MailMessage(from, to, subject, body);
            message.IsBodyHtml = true;
            string accountname = string.Empty, accountpassword = string.Empty, authtype = string.Empty, host = string.Empty, domain = "";
            int port = 25;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select top 1 *");
                    qry.Add("from emailreportcontrol with (nolock)");
                    await qry.ExecuteAsync();
                    accountname = qry.GetField("accountname").ToString().TrimEnd();
                    accountpassword = qry.GetField("accountpassword").ToString().TrimEnd();
                    authtype = qry.GetField("authtype").ToString().TrimEnd();
                    host = qry.GetField("host").ToString().TrimEnd();
                    port = qry.GetField("port").ToInt32();
                }
            }
            var client = new SmtpClient(host, port);
            client.Credentials = new NetworkCredential(accountname, accountpassword, domain);
            try
            {
                await client.SendMailAsync(message);
            }
            catch (SmtpException ex) { }
            return true;
        }
        //------------------------------------------------------------------------------------ 

        // since we are having trouble with Email, can you please add logic to log the alert to a new "webalertlog" table (hotfix 155 required for this)?
        // Add Record, Logic, Controller classes for "webalertlog"
        // add a new "History" tab on the Alert form. And a grid to display the webalertlog data - newest on top

    }
}
