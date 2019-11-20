using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.HomeControls.PhysicalInventoryQuantityInventory
{
    [FwSqlTable("physicalitem")]
    public class PhysicalInventoryQuantityInventoryRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalitemid", modeltype: FwDataTypes.Integer, sqltype: "int", isPrimaryKey: true)]
        public int? PhysicalInventoryItemId { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PhysicalInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignorid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string ConsignorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtysession", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? SessionQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "recountstatus", modeltype: FwDataTypes.Text, sqltype: "char")]
        public string RecountStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastorderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string LastOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string UserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "recountqty", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? RecountQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "recountusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RecountUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "msg", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Message { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text, sqltype: "char")]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prescanned", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ISPreScanned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 40)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "counted", modeltype: FwDataTypes.Boolean, sqltype: "varchar")]
        public bool? IsCounted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countedspaceid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CountedSpaceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentspaceid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string CurrentSpaceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "recountcounted", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ISRecountCounted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weight", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? Weight { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "length", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? Length { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "recountweight", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? RecountWeight { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "recountlength", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? RecountLength { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgserial", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 40)]
        public string SerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
