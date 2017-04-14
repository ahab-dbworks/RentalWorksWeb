using System;
using Fw.Json.ValueTypes;
using System.Collections.Generic;
using Fw.Json.Utilities;
using System.Text;

namespace Fw.Json.SqlServer.Entities
{
    public class FwWebUserSettings
    {
        //---------------------------------------------------------------------------------------------
        public FwWebUserSettingsSettings Settings {get; private set;}
        public class FwWebUserSettingsSettings
        {
            //---------------------------------------------------------------------------------------------
            private Dictionary<string,string> dictSettings;
            //---------------------------------------------------------------------------------------------
            public string BrowseDefaultRows 
            {
                get 
                {
                    string result;

                    result = "15"; //Value if not defined
                    if (dictSettings.ContainsKey("browsedefaultrows"))
                    {
                        try { result = dictSettings["browsedefaultrows"]; } catch {}
                    }

                    return result;
                }
                set
                {
                    dictSettings["browsedefaultrows"] = value.ToString();
                }
            }
            //---------------------------------------------------------------------------------------------
            public string ApplicationTheme
            {
                get 
                {
                    string result;

                    result = "theme-default"; //Value if not defined
                    if (dictSettings.ContainsKey("applicationtheme"))
                    {
                        try { result = dictSettings["applicationtheme"]; } catch {}
                    }

                    return result;
                }
                set
                {
                    dictSettings["applicationtheme"] = value.ToString();
                }
            }
            //---------------------------------------------------------------------------------------------
            public FwWebUserSettingsSettings(string xml)
            {
                dictSettings = FwXml.GetDictionary(xml);
            }
            //---------------------------------------------------------------------------------------------
            public FwWebUserSettingsSettings()
            {
                dictSettings = new Dictionary<string,string>();
            }
            //---------------------------------------------------------------------------------------------
            public void Save(FwSqlConnection conn, string webusersid)
            {
                StringBuilder sb;
                string settings;

                sb = new StringBuilder();
                sb.Append("<settings>");
                foreach(KeyValuePair<string,string> item in dictSettings)
                {
                    sb.Append("<");
                    sb.Append(item.Key);
                    sb.Append(">");
                    sb.Append(item.Value);
                    sb.Append("</");
                    sb.Append(item.Key);
                    sb.Append(">");
                }
                sb.Append("</settings>");
                settings = sb.ToString();
                using (FwSqlCommand qry = new FwSqlCommand(conn))
                {
                    qry.Add("update webusers");
                    qry.Add("set settings = @settings");
                    qry.Add("where webusersid = @webusersid");
                    qry.AddParameter("@settings", settings);
                    qry.AddParameter("@webusersid", webusersid);
                    qry.ExecuteNonQuery();
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public void LoadSettings(string xml)
        {
            if (!string.IsNullOrEmpty(xml))
            {
                this.Settings = new FwWebUserSettingsSettings(xml);
            }
            else
            {
                this.Settings = new FwWebUserSettingsSettings();
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}