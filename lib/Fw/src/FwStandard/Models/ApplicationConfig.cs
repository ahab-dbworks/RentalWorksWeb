namespace FwStandard.Models
{
    public class ApplicationConfig
    {
        public SqlServerConfig DatabaseSettings { get; set; }
    }

    public class SqlServerConfig
    {
        public string ConnectionString { get; set; } = string.Empty;
        public int QueryTimeout { get; set; } = 30;
        public int ReportTimeout { get; set; } = 3600; // 1 hour
    }
}
