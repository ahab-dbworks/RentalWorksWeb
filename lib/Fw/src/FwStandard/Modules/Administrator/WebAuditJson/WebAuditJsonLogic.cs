using FwStandard.AppManager;
using FwStandard.BusinessLogic;

namespace FwStandard.Modules.Administrator.WebAuditJson
{
    [FwLogic(Id: "u23vHQtLKL4")]
    public class WebAuditJsonLogic : FwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WebAuditJsonRecord webAuditJson = new WebAuditJsonRecord();
        WebAuditJsonLoader webAuditJsonLoader = new WebAuditJsonLoader();
        public WebAuditJsonLogic()
        {
            dataRecords.Add(webAuditJson);
            dataLoader = webAuditJsonLoader;
            HasAudit = false;
            ReloadOnSave = false;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "rrFclGnuvha", IsPrimaryKey: true)]
        public int? WebAuditId { get { return webAuditJson.WebAuditId; } set { webAuditJson.WebAuditId = value; } }

        [FwLogicProperty(Id: "zAf1ZR1gjEJ")]
        public string ModuleName { get { return webAuditJson.ModuleName; } set { webAuditJson.ModuleName = value; } }

        [FwLogicProperty(Id: "5aW28iQ7UDJ8H")]
        public string Title { get { return webAuditJson.Title; } set { webAuditJson.Title = value; } }

        [FwLogicProperty(Id: "QWFBMDjegDh")]
        public string UniqueId1 { get { return webAuditJson.UniqueId1; } set { webAuditJson.UniqueId1 = value; } }

        [FwLogicProperty(Id: "QrhJJaHm3b0")]
        public string UniqueId2 { get { return webAuditJson.UniqueId2; } set { webAuditJson.UniqueId2 = value; } }

        [FwLogicProperty(Id: "gf5vzXDBzzp")]
        public string UniqueId3 { get { return webAuditJson.UniqueId3; } set { webAuditJson.UniqueId3 = value; } }

        [FwLogicProperty(Id: "OXQjBqJ1A6Q")]
        public string WebUserId { get { return webAuditJson.WebUserId; } set { webAuditJson.WebUserId = value; } }

        [FwLogicProperty(Id: "4OTFpTKCw2K", IsReadOnly: true)]
        public string UserName { get; set; }

        [FwLogicProperty(Id: "B6FmnXydBoe")]
        public string Json { get { return webAuditJson.Json; } set { webAuditJson.Json = value; } }

        [FwLogicProperty(Id: "1VVjEc4XDYJ")]
        public string DateStamp { get { return webAuditJson.DateStamp; } set { webAuditJson.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
