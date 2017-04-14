using System;
using Fw.Json.ValueTypes;
using System.Collections.Generic;
using Fw.Json.Utilities;
using System.Text;

namespace Fw.Json.SqlServer.Entities
{
    public class FwControl
    {
        public string ControlId {get;set;}
        public string Company {get;set;}
        public string System {get;set;}
        public int MaxRows {get;set;}
        public string ImagePath {get;set;}
        public FwDateTime DateStamp {get;set;}
        public string DbVersion {get;set;}
        public Guid RowGuid {get;set;}
        public string Guid {get;set;}
        //---------------------------------------------------------------------------------------------
        public FwControlSettings Settings {get; private set;}
        public class FwControlSettings
        {
            //---------------------------------------------------------------------------------------------
            private Dictionary<string,string> dictSettings;
            //---------------------------------------------------------------------------------------------
            public bool RequireMinLengthPassword 
            {
                get 
                {
                    bool result;

                    result = false;
                    if (dictSettings.ContainsKey("requireminlengthpassword"))
                    {
                        try { result = FwConvert.ToBoolean(dictSettings["requireminlengthpassword"]); } catch {}
                    }

                    return result;
                }
                set
                {
                    dictSettings["requireminlengthpassword"] = (value) ? "T" : "F";
                }
            }
            //---------------------------------------------------------------------------------------------
            public int MinLengthPassword 
            {
                get 
                {
                    int result;

                    result = 1;
                    if (dictSettings.ContainsKey("minlengthpassword"))
                    {
                        try { result = FwConvert.ToInt32(dictSettings["minlengthpassword"]); } catch {}
                    }

                    return result;
                }
                set
                {
                    dictSettings["minlengthpassword"] = value.ToString();
                }
            }
            //---------------------------------------------------------------------------------------------
            public bool RequireDigitInPassword 
            {
                get 
                {
                    bool result;

                    result = false;
                    if (dictSettings.ContainsKey("requiredigitinpassword"))
                    {
                        try { result = FwConvert.ToBoolean(dictSettings["requiredigitinpassword"]); } catch {}
                    }

                    return result;
                }
                set
                {
                    dictSettings["requiredigitinpassword"] = (value) ? "T" : "F";
                }
            }
            //---------------------------------------------------------------------------------------------
            public bool RequireSymbolInPassword 
            {
                get 
                {
                    bool result;

                    result = false;
                    if (dictSettings.ContainsKey("requiresymbolinpassword"))
                    {
                        try { result = FwConvert.ToBoolean(dictSettings["requiresymbolinpassword"]); } catch {}
                    }

                    return result;
                }
                set
                {
                    dictSettings["requiresymbolinpassword"] = (value) ? "T" : "F";
                }
            }
            //---------------------------------------------------------------------------------------------
            public bool AutoLogoutUser 
            {
                get 
                {
                    bool result;

                    result = false;
                    if (dictSettings.ContainsKey("autologoutuser"))
                    {
                        try { result = FwConvert.ToBoolean(dictSettings["autologoutuser"]); } catch {}
                    }

                    return result;
                }
                set
                {
                    dictSettings["autologoutuser"] = (value) ? "T" : "F";
                }
            }
            //---------------------------------------------------------------------------------------------
            public int AutoLogoutMinutes 
            {
                get 
                {
                    int result;

                    result = 0;
                    if (dictSettings.ContainsKey("autologoutminutes"))
                    {
                        try { result = FwConvert.ToInt32(dictSettings["autologoutminutes"]); } catch {}
                    }

                    return result;
                }
                set
                {
                    dictSettings["autologoutminutes"] = value.ToString();
                }
            }
            //---------------------------------------------------------------------------------------------
            public bool LockUserAfterThreeFailedAttempts 
            {
                get 
                {
                    bool result;

                    result = false;
                    if (dictSettings.ContainsKey("lockuserafterthreefailedattempts"))
                    {
                        try { result = FwConvert.ToBoolean(dictSettings["lockuserafterthreefailedattempts"]); } catch {}
                    }

                    return result;
                }
                set
                {
                    dictSettings["lockuserafterthreefailedattempts"] = (value) ? "T" : "F";
                }
            }
            //---------------------------------------------------------------------------------------------
            public FwControlSettings(string xml)
            {
                dictSettings = FwXml.GetDictionary(xml);
            }
            //---------------------------------------------------------------------------------------------
            public void Save(FwSqlConnection conn)
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
                    qry.Add("update control");
                    qry.Add("set settings = @settings");
                    qry.Add("where controlid = 1");
                    qry.AddParameter("@settings", settings);
                    qry.ExecuteNonQuery();
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public void LoadSettings(string xml)
        {
            this.Settings = new FwControlSettings(xml);
        }
        //---------------------------------------------------------------------------------------------
    }
}