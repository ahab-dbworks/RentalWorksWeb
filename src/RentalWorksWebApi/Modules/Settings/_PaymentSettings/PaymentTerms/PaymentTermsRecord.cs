using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace RRentalWorksWebApi.Modules.Settings.PaymentTerms
{
    [FwSqlTable("payterms")]
    public class PaymentTermsRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytermsid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string PaymentTermsId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "payterms", modeltype: FwDataTypes.Text, maxlength: 12, required: true)]
        public string PaymentTerms { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "invmessage", modeltype: FwDataTypes.Text, maxlength: 60)]
        public string InvoiceMessage { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "days", modeltype: FwDataTypes.Integer)]
        public int? DueInDays { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "cod", modeltype: FwDataTypes.Boolean)]
        public bool? COD { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytermscode", modeltype: FwDataTypes.Text, maxlength: 10)]
        public string PaymentTermsCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
