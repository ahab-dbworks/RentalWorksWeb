using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using WebApi.Data;
namespace WebApi.Modules.Warehouse.Activity
{
    [FwSqlTable("activityview")]
    public class ActivityLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activityid", modeltype: FwDataTypes.Integer, identity: true, isPrimaryKey: true)]
        public int? ActivityId { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytypeid", modeltype: FwDataTypes.Integer)]
        public int? ActivityTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytype", modeltype: FwDataTypes.Text)]
        public string ActivityType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytypedesc", modeltype: FwDataTypes.Text)]
        public string ActivityTypeDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytypecolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ActivityTypeColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytypetextcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ActivityTypeTextColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitystatusid", modeltype: FwDataTypes.Integer)]
        public int? ActivityStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitystatus", modeltype: FwDataTypes.Text)]
        public string ActivityStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitystatusdesc", modeltype: FwDataTypes.Text)]
        public string ActivityStatusDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitystatuscolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ActivityStatusColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitystatustextcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ActivityStatusTextColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderstatus", modeltype: FwDataTypes.Text)]
        public string OrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text)]
        public string OrderTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypedesc", modeltype: FwDataTypes.Text)]
        public string OrderTypeDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assignedtousersid", modeltype: FwDataTypes.Text)]
        public string AssignedToUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assignedtousername", modeltype: FwDataTypes.Text)]
        public string AssignedToUserName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitydatetime", modeltype: FwDataTypes.DateTime)]
        public DateTime ActivityDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitydate", modeltype: FwDataTypes.Date)]
        public string ActivityDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytime", modeltype: FwDataTypes.Text)]
        public string ActivityTime { get; set; }
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
            select.AddWhere("activitydatetime is not null");
        }
        //------------------------------------------------------------------------------------ 
    }
}
