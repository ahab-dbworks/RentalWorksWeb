using System.Collections.Concurrent;
using System.Collections.Generic;

namespace FwStandard.Models
{
    public class FwApplicationConfig
    {
        public string PublicBaseUrl { get; set; } = string.Empty;
        public string VirtualDirectory { get; set; } = string.Empty;
        public SqlServerConfig DatabaseSettings { get; set; }
        public SqlServerConfig DataWarehouseDatabaseSettings { get; set; }
        public FwJwtIssuerOptions JwtIssuerOptions { get; set; }
        public DebuggingConfig Debugging { get; set; } = new DebuggingConfig();
        public bool EnableAvailabilityService { get; set; } = true;
        public string ApplicationPool { get; set; } = string.Empty;  // this field is deprecated. But I am leaving the actual field here for a while to prevent errors on startup for sites that still have this value defined.
        //the following fields are used by the System Update tool to determine where to apply the updates
        public string ApiApplicationPool { get; set; } = string.Empty;  // (previously called "ApplicationPool")
        public string WebApplicationPool { get; set; } = string.Empty;
        public string ApiPath { get; set; } = string.Empty;
        public string WebPath { get; set; } = string.Empty;
        public HostedServices HostedServices { get; set; } = new HostedServices();
        public WebAppConfig WebApp { get; set; } = new WebAppConfig();
        public string WebRequestPath { get; set; } = null;
        public string MobileRequestPath { get; set; } = null;
        public MobileAppConfig MobileApp { get; set; } = new MobileAppConfig();
        //public bool EnableHttpsRedirect { get; set; } = false;
        //public List<RewriteRulesConfig> RewriteRules { get; set; }
    }

    public class HostedServices : ConcurrentDictionary<string, HostedService>
    {
        public bool IsServiceEnabled(string serviceName)
        {
            bool isServiceEnabled = false;
            HostedService service = null;
            if (this.ContainsKey(serviceName) && this.TryGetValue(serviceName, out service))
            {
                isServiceEnabled = service.Enabled;
            }
            return isServiceEnabled;
        }

        public bool LogSql(string serviceName)
        {
            bool logSql = true;
            HostedService service = null;
            if (this.ContainsKey(serviceName) && this.TryGetValue(serviceName, out service))
            {
                logSql = service.LogSql;
            }
            return logSql;
        }
    }

    public class HostedService
    {
        public bool Enabled { get; set; } = false;
        public bool LogSql { get; set; } = false;
    }

    public class SqlServerConfig
    {
        public string ConnectionString { get; set; } = string.Empty;
        public int QueryTimeout { get; set; } = 30;
        public int ReportTimeout { get; set; } = 3600; // 1 hour
    }

    public class DebuggingConfig
    {
        public bool LogSql { get; set; } = false;
        public bool LogSqlContext { get; set; } = true;
    }

    public class WebAppConfig
    {
        public string appbaseurl { get; set; } = string.Empty;
        public string fwvirtualdirectory { get; set; } = string.Empty;
        public string appvirtualdirectory { get; set; } = string.Empty;
        public bool debugMode { get; set; } = false;
        public bool designMode { get; set; } = false;
        public int ajaxTimeoutSeconds { get; set; } = 300;
        public string version { get; set; } = string.Empty;
        public string apiurl { get; set; } = string.Empty;
        public bool defaultPeek { get; set; } = false;
        public int photoQuality { get; set; } = 100;
        public int photoWidth { get; set; } = 1024;
        public int photoHeight { get; set; } = 1024;
        public bool customLogin { get; set; } = false;
        public string client { get; set; } = string.Empty;
        public bool allCaps { get; set; } = false;
        //public bool OktaEnabled { get; set; } = false;
        //public FwOktaSignInConfig oktaSignIn { get; set; } = new FwOktaSignInConfig
        //public string oktaApiUrl = string.Empty;
    }

        public class MobileAppConfig
    {
        public string appbaseurl { get; set; } = string.Empty;
        public string fwvirtualdirectory { get; set; } = string.Empty;
        public string appvirtualdirectory { get; set; } = string.Empty;
        public bool debugMode { get; set; } = false;
        public bool designMode { get; set; } = false;
        public bool demoMode { get; set; } = false;
        public bool devMode { get; set; } = false;
        public string demoEmail { get; set; } = "qsdemo@dbworks.com";
        public string demoPassword { get; set; } = "QSDEMO";
        public int ajaxTimeoutSeconds { get; set; } = 20;
        public string version { get; set; } = string.Empty;
        public string apiurl { get; set; } = string.Empty;
        public int photoWidth { get; set; } = 1024;
        public int photoHeight { get; set; } = 1024;
        public int photoQuality { get; set; } = 100;
        public int iPodPhotoQuality { get; set; } = 100;
        public int iPhonePhotoQuality { get; set; } = 100;
        public bool defaultPeek { get; set; } = false;
        public QuikInConfig quikIn { get; set; } = new QuikInConfig();
        public bool allowDisableBarcodeFieldInStaging { get; set; } = true;
    }

    public class QuikInConfig
    {
        public bool enableQuikInSessionSearch { get; set; } = false;
        public bool enableSessionInItemSearch { get; set; } = true;
        public bool enableCancelItem { get; set; } = true;

    }

    public class RewriteRulesConfig
    {
        public string Regex { get; set; } = string.Empty;
        public string Replacement { get; set; } = string.Empty;
        public bool SkipRemainingRules { get; set; } = false;
    }

    //public class OktaSignInConfig
    //{
    //    renderEl = null;
    //    hasTokensInUrl = null;
    //    authClient = null;
    //    signIn = null;
    //    public string baseUrl { get; set; } = "";
    //    public string clientId { get; set; } = "";
    //    public string redirectUri { get; set; } = "";
    //    authParams = {
    //        issuer: "",
    //        responseType: ['token', 'id_token'],
    //        display: 'page'
    //    }
    //}
}
