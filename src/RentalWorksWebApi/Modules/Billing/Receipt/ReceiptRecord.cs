using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Billing.Receipt
{
    [FwSqlTable("ar")]
    public class ReceiptRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        public ReceiptRecord()
        {
            InsteadOfDelete += OnInsteadOfDelete;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "arid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string ReceiptId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string PaymentTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ardate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string ReceiptDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pmtamt", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? PaymentAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "checkno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10, required: true)]
        public string CheckNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text, sqltype: "char")]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "crcno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string CreditCardNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crcexpdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string CreditCardExpirationDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crcname", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string CreditCardName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pmtmemo", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string PaymentMemo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "appliedbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string AppliedById { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "overpaymentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OverPaymentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nsfarid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InsufficientFundsReceiptId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentby", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string PaymentBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ModifiedById { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string InputDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "moddate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string ModifiedDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "externalid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string ExternalId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        public void OnInsteadOfDelete(object sender, InsteadOfDataRecordDeleteEventArgs e)
        {
            e.Success = ReceiptFunc.DeleteReceipt(AppConfig, UserSession, ReceiptId).Result;
        }
        //------------------------------------------------------------------------------------
    }
}
