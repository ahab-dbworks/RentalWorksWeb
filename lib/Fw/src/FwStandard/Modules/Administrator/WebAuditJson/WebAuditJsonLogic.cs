using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;

namespace FwStandard.Modules.Administrator.WebAuditJson
{
    public class WebAuditJsonLogic : FwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WebAuditJsonRecord webAuditJson = new WebAuditJsonRecord();
        WebAuditJsonLoader webAuditJsonLoader = new WebAuditJsonLoader();
        public WebAuditJsonLogic()
        {
            dataRecords.Add(webAuditJson);
            dataLoader = webAuditJsonLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public int? WebAuditId { get { return webAuditJson.WebAuditId; } set { webAuditJson.WebAuditId = value; } }
        public string UniqueId1 { get { return webAuditJson.UniqueId1; } set { webAuditJson.UniqueId1 = value; } }
        public string UniqueId2 { get { return webAuditJson.UniqueId2; } set { webAuditJson.UniqueId2 = value; } }
        public string UniqueId3 { get { return webAuditJson.UniqueId3; } set { webAuditJson.UniqueId3 = value; } }
        public string WebUserId { get { return webAuditJson.WebUserId; } set { webAuditJson.WebUserId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string UserName { get; set; }
        public string Json { get { return webAuditJson.Json; } set { webAuditJson.Json = value; } }
        public string DateStamp { get { return webAuditJson.DateStamp; } set { webAuditJson.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
