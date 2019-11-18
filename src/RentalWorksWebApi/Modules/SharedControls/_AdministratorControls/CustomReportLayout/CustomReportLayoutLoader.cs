using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Administrator.CustomReportLayout
{
    [FwSqlTable("webreportlayoutview")]
    public class CustomReportLayoutLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webreportlayoutid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string CustomReportLayoutId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "basereport", modeltype: FwDataTypes.Text)]
        public string BaseReport { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webusersid", modeltype: FwDataTypes.Text)]
        public string WebUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "username", modeltype: FwDataTypes.Text)]
        public string UserName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "html", modeltype: FwDataTypes.Text)]
        public string Html { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "active", modeltype: FwDataTypes.Boolean)]
        public bool? Active { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assignto", modeltype: FwDataTypes.Text)]
        public string AssignTo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("BaseReport", "basereport", select, request); 
            addFilterToSelect("WebUserId", "webusersid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
