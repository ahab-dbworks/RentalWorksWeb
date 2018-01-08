using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.WarehouseDepartment
{
    [FwSqlTable("departmentwarehouse")]
    public class WarehouseDepartmentRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requesttoid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RequestToId { get; set; }
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