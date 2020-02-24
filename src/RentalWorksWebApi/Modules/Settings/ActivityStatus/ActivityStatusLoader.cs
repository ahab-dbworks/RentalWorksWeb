using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Settings.ActivityStatus
{
    [FwSqlTable("activitystatusview")]
    public class ActivityStatusLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitystatusid", modeltype: FwDataTypes.Integer, isPrimaryKey: true)]
        public int? ActivityStatusId { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitystatus", modeltype: FwDataTypes.Text)]
        public string ActivityStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rename", modeltype: FwDataTypes.Text)]
        public string Rename { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "descriptiondisplay", modeltype: FwDataTypes.Text)]
        public string ActivityStatusDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "systemstatus", modeltype: FwDataTypes.Boolean)]
        public bool? IsSystemStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytypeid", modeltype: FwDataTypes.Integer)]
        public int? ActivityTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytype", modeltype: FwDataTypes.Text)]
        public string ActivityType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.OleToHtmlColor)]
        public string Color { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "textcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string TextColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Integer)]
        public int? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("ActivityTypeId", "activitytypeid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
