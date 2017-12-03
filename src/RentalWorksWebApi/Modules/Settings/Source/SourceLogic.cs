using FwStandard.BusinessLogic.Attributes;
using FwStandard.SqlServer;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.Source
{
    public class SourceLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        SourceRecord source = new SourceRecord();
        public SourceLogic()
        {
            dataRecords.Add(source);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string SourceId { get { return source.SourceId; } set { source.SourceId = value; } }
        public string Source { get { return source.Source; } set { source.Source = value; } }
        public string SourceType { get { return source.SourceType; } set { source.SourceType = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Description { get { return source.Description; } set { source.Description = value; } }
        public string Path { get { return source.Path; } set { source.Path = value; } }
        public string FileName { get { return source.FileName; } set { source.FileName = value; } }
        public string FtpHost { get { return source.FtpHost; } set { source.FtpHost = value; } }
        public int? FtpPort { get { return source.FtpPort; } set { source.FtpPort = value; } }
        public string FtpUserName { get { return source.FtpUserName; } set { source.FtpUserName = value; } }
        public string FtpPassword { get { return source.FtpPassword; } set { source.FtpPassword = value; } }
        public string FtpPath { get { return source.FtpPath; } set { source.FtpPath = value; } }
        public string FtpFileName { get { return source.FtpFileName; } set { source.FtpFileName = value; } }
        public bool? FtpArchive { get { return source.FtpArchive; } set { source.FtpArchive = value; } }
        public string FtpArchivePath { get { return source.FtpArchivePath; } set { source.FtpArchivePath = value; } }
        public bool? FtpSsl { get { return source.FtpSsl; } set { source.FtpSsl = value; } }
        public string TemporaryFtpFileName { get { return source.TemporaryFtpFileName; } set { source.TemporaryFtpFileName = value; } }
        public bool? Disabled { get { return source.Disabled; } set { source.Disabled = value; } }
        public bool? SystemSource { get { return source.SystemSource; } set { source.SystemSource = value; } }
        public string TemporarySoapPath { get { return source.TemporarySoapPath; } set { source.TemporarySoapPath = value; } }
        public string WebServiceUserName { get { return source.WebServiceUserName; } set { source.WebServiceUserName = value; } }
        public string WebServicePassword { get { return source.WebServicePassword; } set { source.WebServicePassword = value; } }
        public bool? UseProxy { get { return source.UseProxy; } set { source.UseProxy = value; } }
        public string Proxy { get { return source.Proxy; } set { source.Proxy = value; } }
        public int? ProxyPort { get { return source.ProxyPort; } set { source.ProxyPort = value; } }
        public string ProxyUserName { get { return source.ProxyUserName; } set { source.ProxyUserName = value; } }
        public string ProxyPassword { get { return source.ProxyPassword; } set { source.ProxyPassword = value; } }
        public string DateStamp { get { return source.DateStamp; } set { source.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }
}

