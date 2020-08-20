using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Settings.CurrencyExchangeRate
{
    [FwSqlTable("currencyrate")]
    public class CurrencyExchangeRateRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyrateid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string CurrencyExchangeRateId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exchangedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string AsOfDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromcurrencyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string FromCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "importdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string ImportDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 17, scale: 10)]
        public decimal? ExchangeRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tocurrencyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string ToCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
