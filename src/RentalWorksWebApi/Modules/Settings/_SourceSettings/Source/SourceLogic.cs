using FwStandard.AppManager;
using FwStandard.SqlServer;
using WebApi.Logic;

namespace WebApi.Modules.Settings.SourceSettings.Source
{
    [FwLogic(Id:"hCeEranysDNMc")]
    public class SourceLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        SourceRecord source = new SourceRecord();
        public SourceLogic()
        {
            dataRecords.Add(source);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"IvEbBH7O1uIiQ", IsPrimaryKey:true)]
        public string SourceId { get { return source.SourceId; } set { source.SourceId = value; } }

        [FwLogicProperty(Id:"Bxw7xPFQfkx2")]
        public string Source { get { return source.Source; } set { source.Source = value; } }

        [FwLogicProperty(Id:"wvypA7tL9M3S")]
        public string SourceType { get { return source.SourceType; } set { source.SourceType = value; } }

        [FwLogicProperty(Id:"LDPWv7TH0hE9L", IsRecordTitle:true)]
        public string Description { get { return source.Description; } set { source.Description = value; } }

        [FwLogicProperty(Id:"BzuBg4Zp0nl0")]
        public string Path { get { return source.Path; } set { source.Path = value; } }

        [FwLogicProperty(Id:"zhsj1FE1TKGQ")]
        public string FileName { get { return source.FileName; } set { source.FileName = value; } }

        [FwLogicProperty(Id:"jdmQ0E5sYPGS")]
        public string FtpHost { get { return source.FtpHost; } set { source.FtpHost = value; } }

        [FwLogicProperty(Id:"36iDeQNGwp1r")]
        public int? FtpPort { get { return source.FtpPort; } set { source.FtpPort = value; } }

        [FwLogicProperty(Id:"1l9kKt61kTUb")]
        public string FtpUserName { get { return source.FtpUserName; } set { source.FtpUserName = value; } }

        [FwLogicProperty(Id:"D9UO3EeggQsc")]
        public string FtpPassword { get { return source.FtpPassword; } set { source.FtpPassword = value; } }

        [FwLogicProperty(Id:"L9mjJTVSVWzx")]
        public string FtpPath { get { return source.FtpPath; } set { source.FtpPath = value; } }

        [FwLogicProperty(Id:"jgAN4zg31whY")]
        public string FtpFileName { get { return source.FtpFileName; } set { source.FtpFileName = value; } }

        [FwLogicProperty(Id:"aV82UiUE4MdE")]
        public bool? FtpArchive { get { return source.FtpArchive; } set { source.FtpArchive = value; } }

        [FwLogicProperty(Id:"3KL3woxx1ye8")]
        public string FtpArchivePath { get { return source.FtpArchivePath; } set { source.FtpArchivePath = value; } }

        [FwLogicProperty(Id:"fMb0U3k2Hpme")]
        public bool? FtpSsl { get { return source.FtpSsl; } set { source.FtpSsl = value; } }

        [FwLogicProperty(Id:"N9EhcJfJL3UQ")]
        public string TemporaryFtpFileName { get { return source.TemporaryFtpFileName; } set { source.TemporaryFtpFileName = value; } }

        [FwLogicProperty(Id:"wJTUrlCDZpWB")]
        public bool? Inactive { get { return source.Inactive; } set { source.Inactive = value; } }

        [FwLogicProperty(Id:"U0ISABi18vUX")]
        public bool? SystemSource { get { return source.SystemSource; } set { source.SystemSource = value; } }

        [FwLogicProperty(Id:"rZPngxYrEsr7")]
        public string TemporarySoapPath { get { return source.TemporarySoapPath; } set { source.TemporarySoapPath = value; } }

        [FwLogicProperty(Id:"5LF8zwglStpe")]
        public string WebServiceUserName { get { return source.WebServiceUserName; } set { source.WebServiceUserName = value; } }

        [FwLogicProperty(Id:"c1yXzi2qOZZ9")]
        public string WebServicePassword { get { return source.WebServicePassword; } set { source.WebServicePassword = value; } }

        [FwLogicProperty(Id:"l1ma7t4EtNto")]
        public bool? UseProxy { get { return source.UseProxy; } set { source.UseProxy = value; } }

        [FwLogicProperty(Id:"LFFSqutGQuwJ")]
        public string Proxy { get { return source.Proxy; } set { source.Proxy = value; } }

        [FwLogicProperty(Id:"3US5B44bntgL")]
        public int? ProxyPort { get { return source.ProxyPort; } set { source.ProxyPort = value; } }

        [FwLogicProperty(Id:"3ZZ5YCaKiKSJ")]
        public string ProxyUserName { get { return source.ProxyUserName; } set { source.ProxyUserName = value; } }

        [FwLogicProperty(Id:"UnwvzSKyqLVa")]
        public string ProxyPassword { get { return source.ProxyPassword; } set { source.ProxyPassword = value; } }

        [FwLogicProperty(Id:"ABvwLTf6rKJD")]
        public string DateStamp { get { return source.DateStamp; } set { source.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }
}

