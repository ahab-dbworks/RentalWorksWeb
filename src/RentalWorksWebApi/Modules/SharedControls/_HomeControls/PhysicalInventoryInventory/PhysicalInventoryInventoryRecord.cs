using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.PhysicalInventoryInventory
{
    [FwSqlTable("physicalmaster")]
    public class PhysicalInventoryInventoryRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "id", modeltype: FwDataTypes.Integer, sqltype: "int", isPrimaryKey: true, identity: true)]
        public int? Id { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string PhysicalInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "recount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsRecount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "aisleloc", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 4)]
        public string AisleLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shelfloc", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 4)]
        public string ShelfLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "neginv", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? IsNegativeInventory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "added", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? QuantityAdded { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retired", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? QuantityRetired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newpurchasecost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? NewUnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastpurchasecost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 3)]
        public decimal? LastPurchaseUnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentspaceid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string CurrentSpaceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignorid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string ConsignorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "owned", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? QuantityOwned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 3)]
        public decimal? UnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
