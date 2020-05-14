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
}
