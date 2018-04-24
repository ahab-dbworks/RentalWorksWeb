using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Xml.Serialization;

namespace Fw.Json.ValueTypes
{
    [XmlRoot("ApplicationConfig")]
    public class FwApplicationConfig
    {
        [XmlArray("Sites")]
        [XmlArrayItem("Site")]
        public List<FwApplicationConfig_Site> Sites {get;set;}

        [XmlIgnore()]
        public Dictionary<string, FwApplicationConfig_Site> SiteFinder;
        
        public static FwApplicationConfig Current = null;
        //---------------------------------------------------------------------------------------------
        public FwApplicationConfig()
        {
            this.Sites = new List<FwApplicationConfig_Site>();
        }
        //---------------------------------------------------------------------------------------------
        public static FwApplicationConfig_Site CurrentSite
        {
            get
            {
                FwApplicationConfig cfg;
                FwApplicationConfig_Site site;
                object obj;
                string applicationPath;
                //FileSystemWatcher watcher;

                obj = FwApplicationConfig.Current;
                if (obj == null)
                {
                    FwApplicationConfig.Load();
                    cfg = (FwApplicationConfig)FwApplicationConfig.Current;
                    cfg.SiteFinder = new Dictionary<string,FwApplicationConfig_Site>();
                    for(int i = 0; i < cfg.Sites.Count; i++)
                    {
                        for(int j = 0; j < cfg.Sites[i].Urls.Count; j++)
                        {
                            cfg.SiteFinder[cfg.Sites[i].Urls[j]] = cfg.Sites[i];
                        }
                        cfg.Sites[i].DatabaseConnectionFinder = new Dictionary<FwDatabases,FwApplicationConfig_DatabaseConnection>();
                        for(int j = 0; j < cfg.Sites[i].DatabaseConnections.Count; j++)
                        {
                            FwDatabases key = (FwDatabases)Enum.Parse(typeof(FwDatabases), cfg.Sites[i].DatabaseConnections[j].Name);
                            cfg.Sites[i].DatabaseConnectionFinder[key] = cfg.Sites[i].DatabaseConnections[j];

                            if (string.IsNullOrEmpty(cfg.Sites[i].DatabaseConnections[j].ConnectionString))
                            {
                                cfg.Sites[i].DatabaseConnections[j].ConnectionString = string.Format("Server={0};Database={1};User Id={2};Password={3};Persist Security Info=True;Connect Timeout={4};Max Pool Size={5};Workstation Id={6};Packet Size=4096;",
                                    cfg.Sites[i].DatabaseConnections[j].Server
                                  , cfg.Sites[i].DatabaseConnections[j].Database
                                  , (cfg.Sites[i].DatabaseConnections[j].Encrypted) ? FwCryptography.AjaxDecrypt2(cfg.Sites[i].DatabaseConnections[j].User)     : cfg.Sites[i].DatabaseConnections[j].User
                                  , (cfg.Sites[i].DatabaseConnections[j].Encrypted) ? FwCryptography.AjaxDecrypt2(cfg.Sites[i].DatabaseConnections[j].Password) : cfg.Sites[i].DatabaseConnections[j].Password
                                  , cfg.Sites[i].DatabaseConnections[j].ConnectionTimeout
                                  , cfg.Sites[i].DatabaseConnections[j].MaxPoolSize
                                  , System.Net.Dns.GetHostName().ToUpper() + " Mobile Web");
                            }
                        }
                    }
                    //watcher = new FileSystemWatcher(HttpContext.Current.Server.MapPath("~/"), "Application.config");
                    //watcher.Changed += new FileSystemEventHandler(watcher_Changed);
                    //watcher.EnableRaisingEvents = true;
                    //HttpContext.Current.Application["ApplicationConfig_watcher"] = watcher;
                }
                else
                {
                    cfg = (FwApplicationConfig)obj;
                }
                if (cfg.Sites.Count == 1)
                {
                    site = cfg.Sites[0];
                }
                else
                {
                    applicationPath = FwJsonService.GetApplicationPath();
                    site = null;
                    if ((site == null) && (cfg.Sites.Count == 1))
                    {
                        if (cfg.Sites[0].Urls.Count == 0)
                        {
                            site = cfg.Sites[0];
                        }
                    }
                    if ((site == null) && cfg.SiteFinder.ContainsKey(applicationPath))
                    {
                        site = cfg.SiteFinder[applicationPath];
                    }
                    if (site == null)
                    {
                        throw new Exception("None of the Sites in Application.config have been configured to respond to the url you are requesting.");
                    }
                }

                return site;
            }
        }
        //---------------------------------------------------------------------------------------------
        private static void  watcher_Changed(object sender, FileSystemEventArgs e)
        {
 	        System.Web.HttpRuntime.UnloadAppDomain();
            //FwApplicationConfig.Load();
        }
        //---------------------------------------------------------------------------------------------
        public static void Load()
        {
            FwApplicationConfig cfg;
            string pathApplicationConfig;
            
            pathApplicationConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Application.config");
            cfg = FwXml.DeserializeFile<FwApplicationConfig>(pathApplicationConfig);
            FwApplicationConfig.Current = cfg;
        }
        //---------------------------------------------------------------------------------------------
    }

    [XmlRoot("Dev")]
    public class Dev
    {
        [XmlElement("Debug")]
        public bool Debug {get;set;} = false;
        public bool UseElementCertUrl {get;set;} = false;
    }

    [XmlRoot("Site")]
    public class FwApplicationConfig_Site
    {
        [XmlAttribute("Name")]
        public string Name {get;set;} = string.Empty;

        [XmlElement("Dev")]
        public Dev Dev {get;set;} = new Dev();

        [XmlArray("Urls")]
        [XmlArrayItem("Url")]
        public List<string> Urls {get;set;} = new List<string>();
            
        [XmlArray("DatabaseConnections")]
        [XmlArrayItem("DatabaseConnection")]
        public List<FwApplicationConfig_DatabaseConnection> DatabaseConnections {get;set;} = new List<FwApplicationConfig_DatabaseConnection>();

        [XmlIgnore]
        public Dictionary<FwDatabases, FwApplicationConfig_DatabaseConnection> DatabaseConnectionFinder {get;set;} = new Dictionary<FwDatabases, FwApplicationConfig_DatabaseConnection>();

        [XmlElement("APISettings")]
        public FwApplicationConfig_APISettings APISettings {get;set;} = new FwApplicationConfig_APISettings();

        [XmlElement("ApplicationSettings")]
        public FwApplicationConfig_ApplicationSettings ApplicationSettings {get;set;} = new FwApplicationConfig_ApplicationSettings();

        [XmlArray("ApiAccessTokens")]
        [XmlArrayItem("Token")]
        public List<string> ApiAccessTokens {get;set;} = new List<string>();
    }

    [XmlRoot("DatabaseConnection")]
    public class FwApplicationConfig_DatabaseConnection
    {
        [XmlAttribute("Name")]
        public string Name {get;set;} = string.Empty;

        //[XmlAttribute("Enabled")]
        //public bool Enabled {get;set;} = true;
        
        [XmlElement("Server")]
        public string Server {get;set;} = string.Empty;
                
        [XmlElement("Database")]
        public string Database {get;set;} = string.Empty;
                
        [XmlElement("User")]
        public string User {get;set;} = "dbworks";
                
        [XmlElement("Password")]
        public string Password {get;set;} = "db2424";
                
        [XmlElement("ConnectionTimeout")]
        public int ConnectionTimeout {get;set;} = 15;
                
        [XmlElement("QueryTimeout")]
        public int QueryTimeout {get;set;} = 180; // 3 min

        [XmlElement("ReportQueryTimeout")]
        public int ReportQueryTimeout {get;set;} = 7200; // 2 hours
                
        [XmlElement("MaxPoolSize")]
        public int MaxPoolSize {get;set;} = 100;

        [XmlElement("ConnectionString")]
        public string ConnectionString {get;set;} = string.Empty;

        [XmlElement("Encrypted")]
        public bool Encrypted {get;set;} = false;
    }

    [XmlRoot("APISettings")]
    public class FwApplicationConfig_APISettings
    {
        [XmlElement("AuthUsername")]
        public string AuthUsername {get;set;} = string.Empty;

        [XmlElement("AuthPassword")]
        public string AuthPassword {get;set;} = string.Empty;
    }

    [XmlRoot("ApplicationSettings")]
    public class FwApplicationConfig_ApplicationSettings
    {
        [XmlElement("ForceUpperCase")]
        public bool ForceUpperCase {get;set;} = false;
    }
}
