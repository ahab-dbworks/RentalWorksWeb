using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.SourceSettings.Source
{
    [FwSqlTable("source")]
    public class SourceRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "sourceid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string SourceId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "source", modeltype: FwDataTypes.Text, maxlength: 100)]
        public string Source { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "sourcetype", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string SourceType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, maxlength: 30, required: true)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "path", modeltype: FwDataTypes.Text, maxlength: 100)]
        public string Path { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "filename", modeltype: FwDataTypes.Text, maxlength: 30)]
        public string FileName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ftphost", modeltype: FwDataTypes.Text, maxlength: 50)]
        public string FtpHost { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ftpport", modeltype: FwDataTypes.Integer)]
        public int? FtpPort { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ftpusername", modeltype: FwDataTypes.Text, maxlength: 50)]
        public string FtpUserName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ftppassword", modeltype: FwDataTypes.Text, maxlength: 50)]
        public string FtpPassword { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ftppath", modeltype: FwDataTypes.Text, maxlength: 100)]
        public string FtpPath { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ftpfilename", modeltype: FwDataTypes.Text, maxlength: 100)]
        public string FtpFileName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ftparchive", modeltype: FwDataTypes.Boolean)]
        public bool? FtpArchive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ftparchivepath", modeltype: FwDataTypes.Text, maxlength: 100)]
        public string FtpArchivePath { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "tmpftpfilename", modeltype: FwDataTypes.Text, maxlength: 100)]
        public string TemporaryFtpFileName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ftpssl", modeltype: FwDataTypes.Boolean)]
        public bool? FtpSsl { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "disabled", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "systemsource", modeltype: FwDataTypes.Boolean)]
        public bool? SystemSource { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "tmpsoappath", modeltype: FwDataTypes.Text, maxlength: 100)]
        public string TemporarySoapPath { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "webserviceusername", modeltype: FwDataTypes.Text, maxlength: 50)]
        public string WebServiceUserName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "webservicepassword", modeltype: FwDataTypes.Text, maxlength: 50)]
        public string WebServicePassword { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "useproxy", modeltype: FwDataTypes.Boolean)]
        public bool? UseProxy { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "proxy", modeltype: FwDataTypes.Text, maxlength: 50)]
        public string Proxy { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "proxyport", modeltype: FwDataTypes.Integer)]
        public int? ProxyPort { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "proxyusername", modeltype: FwDataTypes.Text, maxlength: 50)]
        public string ProxyUserName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "proxypassword", modeltype: FwDataTypes.Text, maxlength: 50)]
        public string ProxyPassword { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

