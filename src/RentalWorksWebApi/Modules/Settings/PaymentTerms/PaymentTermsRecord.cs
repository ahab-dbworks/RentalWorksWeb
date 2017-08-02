using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RRentalWorksWebApi.Modules.Settings.PaymentTerms
{
    [FwSqlTable("payterms")]
    public class PaymentTermsRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "paytermsid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string PaymentTermsId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "payterms", dataType: FwDataTypes.Text, length: 12)]
        public string PaymentTerms { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "invmessage", dataType: FwDataTypes.Text, length: 60)]
        public string InvoiceMessage { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "days", dataType: FwDataTypes.Integer)]
        public int DueInDays { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "cod", dataType: FwDataTypes.Boolean)]
        public bool COD { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "paytermscode", dataType: FwDataTypes.Text, length: 10)]
        public string PaymentTermsCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
