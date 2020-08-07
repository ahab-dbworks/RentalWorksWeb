using FwStandard.Models;
using FwStandard.SqlServer;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FwStandard.Mobile
{
    public class FwWebUserSettings
    {
        SqlServerConfig DbConfig;
        //---------------------------------------------------------------------------------------------
        public FwWebUserSettings(SqlServerConfig dbConfig)
        {
            this.DbConfig = dbConfig;
        }
        //---------------------------------------------------------------------------------------------
        public FwWebUserSettingsSettings Settings {get; private set;}
        public class FwWebUserSettingsSettings
        {
            SqlServerConfig DbConfig;
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

                    result = "theme-material"; //Value if not defined
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
            public FwWebUserSettingsSettings(SqlServerConfig dbConfig, string xml)
            {
                this.DbConfig = dbConfig;
                dictSettings = FwXml.GetDictionary(xml);
            }
            //---------------------------------------------------------------------------------------------
            public FwWebUserSettingsSettings(SqlServerConfig dbConfig)
            {
                this.DbConfig = dbConfig;
                dictSettings = new Dictionary<string,string>();
            }
            //---------------------------------------------------------------------------------------------
            public async Task SaveAsync(FwSqlConnection conn, string webusersid)
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
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.DbConfig.QueryTimeout))
                {
                    qry.Add("update webusers");
                    qry.Add("set settings = @settings");
                    qry.Add("where webusersid = @webusersid");
                    qry.AddParameter("@settings", settings);
                    qry.AddParameter("@webusersid", webusersid);
                    await qry.ExecuteNonQueryAsync();
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public void LoadSettings(string xml)
        {
            if (!string.IsNullOrEmpty(xml))
            {
                this.Settings = new FwWebUserSettingsSettings(this.DbConfig, xml);
            }
            else
            {
                this.Settings = new FwWebUserSettingsSettings(this.DbConfig);
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}