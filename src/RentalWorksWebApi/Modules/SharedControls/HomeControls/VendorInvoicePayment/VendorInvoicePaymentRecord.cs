using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.HomeControls.VendorInvoicePayment
{
    [FwSqlTable("vendorinvoicepayment")]
    public class VendorInvoicePaymentRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "id", modeltype: FwDataTypes.Integer, identity: true, isPrimaryKey: true)]
        public int? VendorInvoicePaymentId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorinvoiceid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string VendorInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string PaymentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "amount", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? Amount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
