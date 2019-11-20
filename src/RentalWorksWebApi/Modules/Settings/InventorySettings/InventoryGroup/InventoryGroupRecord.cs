using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.InventorySettings.InventoryGroup
{
    [FwSqlTable("inventorygroup")]
    public class InventoryGroupRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorygroupid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string InventoryGroupId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorygroup", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string InventoryGroup { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text, sqltype: "char", required: true)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "includeconsigned", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IncludeConsigned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "includeowned", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IncludeOwned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ranka", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? RankA { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rankb", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? RankB { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rankc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? RankC { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rankd", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? RankD { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "aisle", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string AisleLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shelf", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string ShelfLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}