using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.HomeControls.BarCodeHolding
{
    [FwSqlTable("barcodeholding")]
    public class BarCodeHoldingRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcodeholdingid", modeltype: FwDataTypes.Integer, sqltype: "numeric", isPrimaryKey: true, identity: true)]
        public int? BarCodeHoldingId { get; set; } 
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 40)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inspectionno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 12)]
        public string InspectionNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inspectionvendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InspectionVendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string ManufactureDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "printqty", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? PrintQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "serialno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 40)]
        public string SerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SessionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertranid", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? OrderTranId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "internalchar", modeltype: FwDataTypes.Text, sqltype: "char")]
        public string InternalChar { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivecontractid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ReceiveContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returncontractid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ReturnContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rfid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string RfId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
