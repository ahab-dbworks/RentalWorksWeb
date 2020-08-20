using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Settings.CurrencyExchangeRate
{
    [FwSqlTable("currencyrateview")]
    public class CurrencyExchangeRateLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyrateid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string CurrencyExchangeRateId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exchangedate", modeltype: FwDataTypes.Date)]
        public string AsOfDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromcurrencyid", modeltype: FwDataTypes.Text)]
        public string FromCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "importdate", modeltype: FwDataTypes.Date)]
        public string ImportDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rate", modeltype: FwDataTypes.Decimal)]
        public decimal? ExchangeRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tocurrencyid", modeltype: FwDataTypes.Text)]
        public string ToCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromcurrencycode", modeltype: FwDataTypes.Text)]
        public string FromCurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromcurrency", modeltype: FwDataTypes.Text)]
        public string FromCurrency { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tocurrencycode", modeltype: FwDataTypes.Text)]
        public string ToCurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tocurrency", modeltype: FwDataTypes.Text)]
        public string ToCurrency { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("FromCurrencyId", "fromcurrencyid", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
