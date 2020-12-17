using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.CreditCardSettings.CreditCardPaymentType

{
    [FwSqlTable("creditcardpaytype")]
    public class CreditCardPaymentTypeRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditcardpaytypeid", modeltype: FwDataTypes.Integer, sqltype: "int", isPrimaryKey: true)]
        public int? CreditCardPayTypeId { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chargepaytypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ChargePaymentTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "refundpaytypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RefundPaymentTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
