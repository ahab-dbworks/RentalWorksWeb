using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Inventory.InventoryAdjustment
{
    [FwSqlTable("inventadj")]
    public class InventoryAdjustmentRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjid", isPrimaryKey: true, modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InventoryAdjustmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldqty", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? OldQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "moduserid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ModifiedByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newqty", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? NewQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reference", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string Reference { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invadjid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string InventoryAdjustmentReasonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjustdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime", required: true)]
        public string AdjustmentDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 3)]
        public decimal? NewUnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjusttime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string AdjustmentTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 3)]
        public decimal? OldUnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjustmenttype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 4, required: true)]
        public string AdjustmentType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "averagecostadjustment", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 3)]
        public decimal? AverageCostAdjustment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "onhandadjustment", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? OnHandAdjustment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 3)]
        public decimal? UnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "averageadjustment", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 3)]
        public decimal? AverageAdjustment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PhysicalInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
