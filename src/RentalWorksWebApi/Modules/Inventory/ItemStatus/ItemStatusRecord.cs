using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Inventory.ItemStatus
{
    [FwSqlTable("rentalitemstatus")]
    public class ItemStatusRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", isPrimaryKey: true, modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalstatusid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InventoryStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outorderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OutOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "note", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Note { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "origwarehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OriginalWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
