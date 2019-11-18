using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.GlManual
{
    [FwSqlTable("gl")]
    public class GlManualRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "glid", modeltype: FwDataTypes.Integer, sqltype: "int", isPrimaryKey: true, identity: true)]
        public int? Id { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "internalchar", modeltype: FwDataTypes.Text, sqltype: "char", isPrimaryKey: true, isPrimaryKeyOptional: true)]
        public string InternalChar { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceitemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InvoiceItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "arid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ReceiptId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorinvoiceid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string VendorInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorinvoiceitemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string VendorInvoiceItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PaymentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manual", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsManual { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "gltype", modeltype: FwDataTypes.Text, sqltype: "varchar")]
        public string GlType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groupheading", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 60)]
        public string GroupHeading { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "debitorderby", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? DebitOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditorderby", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? CreditOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "debitglaccountid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DebitGlAccountId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditglaccountid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CreditGlAccountId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "gldate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string GlDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "amount", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 18, scale: 6)]
        public decimal? Amount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
