using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.Currency
{
    [FwSqlTable("currency")]
    public class CurrencyRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string CurrencyId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currency", modeltype: FwDataTypes.Text, maxlength: 50, required: true)]
        public string Currency { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "code", modeltype: FwDataTypes.Text, maxlength: 10, required: true)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencysymbol", modeltype: FwDataTypes.Text, maxlength: 1, required: true)]
        public string CurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
