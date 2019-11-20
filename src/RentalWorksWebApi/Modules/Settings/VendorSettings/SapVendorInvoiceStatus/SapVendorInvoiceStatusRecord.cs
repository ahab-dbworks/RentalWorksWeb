using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.VendorSettings.SapVendorInvoiceStatus
{
    [FwSqlTable("sapvendorinvoicestatus")]
    public class SapVendorInvoiceStatusRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sapvendorinvoicestatusid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string SapVendorInvoiceStatusId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sapvendorinvoicestatus", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 40, required: true)]
        public string SapVendorInvoiceStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorinvoicestatus", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string VendorInvoiceStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sapstatus", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string SapStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}