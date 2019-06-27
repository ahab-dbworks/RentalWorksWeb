using FwStandard.BusinessLogic;
using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.Modules.Administrator.AlertCondition;
using FwStandard.Modules.Administrator.AlertWebUsers;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace FwStandard.Modules.Administrator.Alert
{
    public static class AlertFunc
    {
        public static List<AlertLogic> alerts;   // this would be a singular static list of all Alerts in the system
                                                 // Phase 2, we will convert this List into a Dictionary to make it even faster
                                                 //-------------------------------------------------------------------------------------------------------        
        public static async void RefreshAlerts(FwApplicationConfig appConfig)
        {
            //this method would get called once on start up  (\lib\Fw\src\FwCore\Api\FwStartup.cs\ConfigureServices)
            // this method would also need to get called in the AfterSave method of AlertLogic.  Search for OnAfterSaveDuplicateRule for an example to copy

            // basically port/copy your FwBusinessLogic.refreshAlerts method here.
            BrowseRequest browseRequest = new BrowseRequest();
            browseRequest.module = "Alert";
            AlertLogic l = new AlertLogic();
            l.AppConfig = appConfig;
            alerts = l.SelectAsync<AlertLogic>(browseRequest).Result;

        }
        //-------------------------------------------------------------------------------------------------------        
        public static async void ProcessAlerts(FwApplicationConfig appConfig, FwUserSession userSession, string moduleName, object oldObject, object newObject, TDataRecordSaveMode saveMode, FwCustomValues _Custom)
        {
            if (alerts == null)
            {
                RefreshAlerts(appConfig);
            }
            if (alerts != null)
            {
                List<AlertLogic> alertList = new List<AlertLogic>();

                foreach (AlertLogic alert in alerts)
                {
                    if (alert.ModuleName.ToString().Equals(moduleName))
                    {
                        alertList.Add(alert);
                    }
                }
                if (alertList.Count > 0)
                {
                    Type type = oldObject.GetType();
                    PropertyInfo[] propertyInfo;
                    propertyInfo = type.GetProperties();
                    FwBusinessLogic l2 = (FwBusinessLogic)Activator.CreateInstance(type);
                    l2.AppConfig = appConfig;

                    foreach (AlertLogic alert in alertList)
                    {
                        string alertId = alert.AlertId;
                        AlertConditionLogic acLoader = new AlertConditionLogic();
                        acLoader.SetDependencies(appConfig, userSession);
                        acLoader.AlertId = alertId;
                        BrowseRequest request = new BrowseRequest();
                        IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                        uniqueIds.Add("AlertId", alertId);
                        request.uniqueids = uniqueIds;
                        List<AlertConditionLogic> alertConditions = acLoader.SelectAsync<AlertConditionLogic>(request).Result;

                        bool conditionsMet = true;
                        foreach (AlertConditionLogic condition in alertConditions)
                        {
                            if (!conditionsMet)
                            {
                                break;
                            }
                            string acFieldName = condition.FieldName;
                            string acCondition = condition.Condition;
                            string acValue = condition.Value;

                            bool propertyFound = false;
                            if (!propertyFound)
                            {
                                foreach (PropertyInfo property in propertyInfo)
                                {
                                    if (property.Name.Equals(acFieldName))
                                    {
                                        propertyFound = true;
                                        object value = newObject.GetType().GetProperty(property.Name).GetValue(newObject);
                                        object valueToCompare = null;

                                        if (value != null)
                                        {
                                            valueToCompare = value;
                                        }
                                        else
                                        {
                                            if (saveMode == TDataRecordSaveMode.smUpdate)
                                            {
                                                valueToCompare = oldObject.GetType().GetProperty(property.Name).GetValue(oldObject);
                                            }
                                        }

                                        switch (acCondition)
                                        {
                                            case "CONTAINS":
                                                conditionsMet = valueToCompare.ToString().Contains(acValue);
                                                break;
                                            case "STARTSWITH":
                                                conditionsMet = valueToCompare.ToString().StartsWith(acValue);
                                                break;
                                            case "ENDSWITH":
                                                conditionsMet = valueToCompare.ToString().EndsWith(acValue);
                                                break;
                                            case "EQUALS":
                                                conditionsMet = string.Equals(valueToCompare.ToString(), acValue);
                                                break;
                                            case "DOESNOTCONTAIN":
                                                conditionsMet = !valueToCompare.ToString().Contains(acValue);
                                                break;
                                            case "DOESNOTEQUAL":
                                                conditionsMet = !string.Equals(valueToCompare.ToString(), acValue);
                                                break;
                                            case "CHANGEDBY":
                                                //conditionsMet =
                                                break;
                                        }
                                        break;
                                    }
                                }
                            }

                            if (!propertyFound)  // property not found, check Custom Fields
                            {
                                //LoadCustomFields();

                                foreach (FwCustomField customField in _Custom.CustomFields)
                                {
                                    if (customField.FieldName.Equals(acFieldName))
                                    {
                                        propertyFound = true;
                                        object value = null;
                                        object valueToCompare = null;
                                        for (int customFieldIndex = 0; customFieldIndex < _Custom.Count; customFieldIndex++)
                                        {
                                            if (_Custom[customFieldIndex].FieldName.Equals(customField.FieldName))
                                            {
                                                value = _Custom[customFieldIndex].FieldValue;
                                                break;
                                            }
                                        }

                                        if (value != null)
                                        {
                                            valueToCompare = value;
                                        }
                                        else
                                        {
                                            if (saveMode == TDataRecordSaveMode.smUpdate)
                                            {
                                                bool b = l2.LoadAsync<Type>().Result;
                                                var databaseValue = "";
                                                for (int customFieldIndex = 0; customFieldIndex < l2._Custom.Count; customFieldIndex++)
                                                {
                                                    if (l2._Custom[customFieldIndex].FieldName.Equals(customField.FieldName))
                                                    {
                                                        databaseValue = l2._Custom[customFieldIndex].FieldValue;
                                                    }
                                                }
                                                valueToCompare = databaseValue;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        }

                        if (conditionsMet)
                        {
                            string alertSubject = alert.AlertSubject;
                            string alertBody = alert.AlertBody;
                            var pattern = @"(?<=\[)[^[]*(?=\])";
                            var subjectMatches = Regex.Matches(alertSubject, pattern);
                            var bodyMatches = Regex.Matches(alertBody, pattern);

                            foreach (Match sm in subjectMatches)
                            {
                                string field = sm.ToString();
                                object value = null;
                                foreach (PropertyInfo property in propertyInfo)
                                {
                                    if (property.Name.Equals(field))
                                    {
                                        value = newObject.GetType().GetProperty(field).GetValue(newObject);

                                        if (value != null)
                                        {
                                            alertSubject = alertSubject.Replace("[" + field + "]", value.ToString());
                                        }
                                        else if (saveMode == TDataRecordSaveMode.smUpdate)
                                        {
                                            value = oldObject.GetType().GetProperty(field).GetValue(oldObject);
                                            alertSubject = alertSubject.Replace("[" + field + "]", value.ToString());
                                        }
                                        break;
                                    }
                                }

                            }

                            foreach (Match bm in bodyMatches)
                            {
                                string field = bm.ToString();
                                object value = null;
                                foreach (PropertyInfo property in propertyInfo)
                                {
                                    if (property.Name.Equals(field))
                                    {
                                        value = newObject.GetType().GetProperty(field).GetValue(newObject);

                                        if (value != null)
                                        {
                                            alertBody = alertBody.Replace("[" + field + "]", value.ToString());
                                        }
                                        else if (saveMode == TDataRecordSaveMode.smUpdate)
                                        {
                                            value = oldObject.GetType().GetProperty(field).GetValue(oldObject);
                                            alertBody = alertBody.Replace("[" + field + "]", value.ToString());
                                        }
                                        break;
                                    }
                                }
                            }

                            AlertWebUsersLogic awuLoader = new AlertWebUsersLogic();
                            awuLoader.SetDependencies(appConfig, userSession);
                            awuLoader.AlertId = alertId;
                            BrowseRequest awuRequest = new BrowseRequest();
                            awuRequest.uniqueids = uniqueIds;
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
                            bool sent = await FwBusinessLogic.SendEmailAsync(from, to, alertSubject, alertBody, appConfig);
                        }
                    }
                }
            }
            return;
        }
        //-------------------------------------------------------------------------------------------------------        

        // since we are having trouble with Email, can you please add logic to log the alert to a new "webalertlog" table (hotfix 155 required for this)?
        // Add Record, Logic, Controller classes for "webalertlog"
        // add a new "History" tab on the Alert form. And a grid to display the webalertlog data - newest on top

    }
}
