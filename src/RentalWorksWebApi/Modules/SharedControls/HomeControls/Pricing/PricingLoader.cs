using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Collections.Generic;
using WebApi.Data;
namespace WebApi.Modules.HomeControls.Pricing
{
    [FwSqlTable("dbo.funcmasterwhratesweb(@masterid, @userswarehouseid, @filterwarehouseid, @currencyid)")]
    /*
        @masterid = InventoryId
        @userswarehouseid = User's WarehouseId
        @filterwarehouseid = Only show data from this specific warehouse
        @currencyid = Leave blank to show prices in each warehouse's local currency
                      Provide a valid CurrencyId here to see rates for a specific Currency
                      Provide the value of "ALL" to get rates for all active currencies
     */
    public class PricingLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public PricingLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Text)]
        public string RateId { get { return InventoryId; } set { InventoryId = value; } }  // RateId and InventoryId are interchangeable
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcurrencyid", modeltype: FwDataTypes.Text)]
        public string WarehouseDefaultCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencysymbol", modeltype: FwDataTypes.Text)]
        public string CurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencydefined", modeltype: FwDataTypes.Boolean)]
        public bool? IsCurrencyDefined { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultpurchasecurrencyid", modeltype: FwDataTypes.Text)]
        public string DefaultPurchaseCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "convertfromcurrencyid", modeltype: FwDataTypes.Text)]
        public string ConvertFromCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hourlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? HourlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? DailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week2rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week2Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week3rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week3Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week4rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week4Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week5rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week5Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "seriesrate", modeltype: FwDataTypes.Decimal)]
        public decimal? SeriesRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "year2leaserate", modeltype: FwDataTypes.Decimal)]
        public decimal? Year2LeaseRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "year3leaserate", modeltype: FwDataTypes.Decimal)]
        public decimal? Year3LeaseRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "year4leaserate", modeltype: FwDataTypes.Decimal)]
        public decimal? Year4LeaseRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hourlymarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? HourlyMarkup { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailymarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? DailyMarkup { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklymarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyMarkup { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlymarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyMarkup { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retail", modeltype: FwDataTypes.Decimal)]
        public decimal? Retail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price", modeltype: FwDataTypes.Decimal)]
        public decimal? Price { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cost", modeltype: FwDataTypes.Decimal)]
        public decimal? Cost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hourlycost", modeltype: FwDataTypes.Decimal)]
        public decimal? HourlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailycost", modeltype: FwDataTypes.Decimal)]
        public decimal? DailyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklycost", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlycost", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultcost", modeltype: FwDataTypes.Decimal)]
        public decimal? DefaultCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultcostconverted", modeltype: FwDataTypes.Decimal)]
        public decimal? DefaultCostConverted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "markup", modeltype: FwDataTypes.Decimal)]
        public decimal? Markup { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "markupcostbasis", modeltype: FwDataTypes.Decimal)]
        public decimal? MarkupCostBasis { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "class", modeltype: FwDataTypes.Text)]
        public string Classification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "maxdiscount", modeltype: FwDataTypes.Decimal)]
        public decimal? MaximumDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "maxdw", modeltype: FwDataTypes.Decimal)]
        public decimal? MaxDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mindw", modeltype: FwDataTypes.Decimal)]
        public decimal? MinDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailableFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultrentalrates", modeltype: FwDataTypes.Boolean)]
        public bool? DefaultRentalRates { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdailyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? DefaultDailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultweeklyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? DefaultWeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hours", modeltype: FwDataTypes.Text)]
        public string Hours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hastieredcost", modeltype: FwDataTypes.Boolean)]
        public bool? HasTieredCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcost", modeltype: FwDataTypes.Decimal)]
        public decimal? ReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manifestvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcostconverted", modeltype: FwDataTypes.Decimal)]
        public decimal? ReplacementCostConverted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manifestvalueconverted", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitCostConverted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "markupreplacementcost", modeltype: FwDataTypes.Boolean)]
        public bool? MarkupReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcostmarkuppct", modeltype: FwDataTypes.Decimal)]
        public decimal? ReplacementCostMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "restockfee", modeltype: FwDataTypes.Decimal)]
        public decimal? RestockingFee { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "restockpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? RestockingPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "calculatemonthlyrate", modeltype: FwDataTypes.Boolean)]
        public bool? CalculateMonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "calculateseriesweeklyrate", modeltype: FwDataTypes.Boolean)]
        public bool? CalculateSeriesWeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "calculate2yearleaseweeklyrate", modeltype: FwDataTypes.Boolean)]
        public bool? Calculate2YearLeaseWeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "calculate3yearleaseweeklyrate", modeltype: FwDataTypes.Boolean)]
        public bool? Calculate3YearLeaseWeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Integer)]
        public int? WarehouseOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            string inventoryOrRateId = InventoryId;
            string filterWarehouseId = WarehouseId;
            string userWarehouseId = string.Empty;
            string currencyId = CurrencyId;

            if (string.IsNullOrEmpty(inventoryOrRateId))
            {
                inventoryOrRateId = GetUniqueIdAsString("InventoryId", request);
            }
            if (string.IsNullOrEmpty(inventoryOrRateId))
            {
                inventoryOrRateId = GetUniqueIdAsString("RateId", request);
            }

            if (string.IsNullOrEmpty(filterWarehouseId))
            {
                filterWarehouseId = GetUniqueIdAsString("WarehouseId", request);
            }

            if (string.IsNullOrEmpty(currencyId))
            {
                currencyId = GetUniqueIdAsString("CurrencyId", request);
                // note, special value "ALL" is acceptable here
            }

            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();

            if (inventoryOrRateId == null)
            {
                inventoryOrRateId = "";
            }
            if (filterWarehouseId == null)
            {
                filterWarehouseId = "";
            }
            if (currencyId == null)
            {
                currencyId = "";
            }

            select.AddParameter("@masterid", inventoryOrRateId);
            select.AddParameter("@userswarehouseid", userWarehouseId);
            select.AddParameter("@filterwarehouseid", filterWarehouseId);
            select.AddParameter("@currencyid", currencyId);
        }
        //------------------------------------------------------------------------------------ 
        public void OnAfterBrowse(object sender, AfterBrowseEventArgs e)
        {
            if (e.DataTable != null)
            {
                FwJsonDataTable dt = e.DataTable;
                if (dt.Rows.Count > 0)
                {
                    foreach (List<object> row in dt.Rows)
                    {
                        row[dt.GetColumnNo("RateId")] = row[dt.GetColumnNo("InventoryId")].ToString();
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
