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
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDate("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("FromCurrencyId", "fromcurrencyid", select, request);
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 

            /*
            if (not IncludeHistory)
            begin
               qryRates.sql.add(' and   c.exchangedate = (select max(exchangedate) from currencyrateview c2 with (nolock) where c2.fromcurrencyid = c.fromcurrencyid and c2.tocurrencyid = c.tocurrencyid)');
            end;
            */

        }
        //------------------------------------------------------------------------------------ 
    }
}
