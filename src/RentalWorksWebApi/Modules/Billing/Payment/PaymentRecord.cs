using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.Payment
{
    [FwSqlTable("payment")]
    public class PaymentRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string PaymentId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentdate", modeltype: FwDataTypes.Date, sqltype: "datetime", required: true)]
        public string PaymentDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string PaymentTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "checkno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20, required: true)]
        public string CheckNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "amount", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? Amount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "appliedbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string AppliedById { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ModifiedById { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pmtmemo", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string PaymentMemo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 2, required: true)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string InputDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "moddate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string ModifiedDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentdocno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string PaymentDocumentNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "accountid", modeltype: FwDataTypes.Integer, sqltype: "int", required: true)]
        public int? BankAccountId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
