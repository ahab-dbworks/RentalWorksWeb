using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Warehouse.Activity
{
    [FwSqlTable("activityview")]
    public class ActivityLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activityid", modeltype: FwDataTypes.Integer, isPrimaryKey: true)]
        public int? ActivityId { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytypeid", modeltype: FwDataTypes.Integer)]
        public int? ActivityTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytype", modeltype: FwDataTypes.Text)]
        public string ActivityType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitystatusid", modeltype: FwDataTypes.Integer)]
        public int? ActivityStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitystatus", modeltype: FwDataTypes.Text)]
        public string ActivityStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assignedtousersid", modeltype: FwDataTypes.Text)]
        public string AssignedToUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitydatetime", modeltype: FwDataTypes.Date)]
        public string ActivityDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalqty", modeltype: FwDataTypes.Integer)]
        public int? TotalQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "completeqty", modeltype: FwDataTypes.Integer)]
        public int? CompleteQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "completepct", modeltype: FwDataTypes.Decimal)]
        public decimal? CompletePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("OrderId", "orderid", select, request); 
            addFilterToSelect("ActivityTypeId", "activitytypeid", select, request); 
            addFilterToSelect("AssignedToUserId", "assignedtousersid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
