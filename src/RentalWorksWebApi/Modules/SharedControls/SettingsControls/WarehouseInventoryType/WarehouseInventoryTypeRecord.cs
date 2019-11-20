using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.WarehouseInventoryType
{
    [FwSqlTable("departmentwarehouse")]
    public class WarehouseInventoryTypeRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalbarcoderangeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RentalBarCodeRangeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesbarcoderangeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SalesBarCodeRangeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}