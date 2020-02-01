using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using WebApi.Data;
namespace WebApi.Modules.Warehouse.Activity
{
    [FwSqlTable("activity")]
    public class ActivityRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activityid", modeltype: FwDataTypes.Integer, sqltype: "int", identity: true, isPrimaryKey: true)]
        public int? ActivityId { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitytypeid", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? ActivityTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assignedtousersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string AssignedToUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitydatetime", modeltype: FwDataTypes.DateTime, sqltype: "datetime")]
        public DateTime? ActivityDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activitystatusid", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? ActivityStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalqty", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? TotalQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "completeqty", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? CompleteQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "completepct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 6, scale: 3)]
        public decimal? CompletePercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
