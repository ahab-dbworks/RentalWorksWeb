using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;

namespace WebApi.Modules.HomeControls.Pricing
{
    [FwSqlTable("dbo.funcmasterwh2(@masterid, @warehouseid, @warehouseid, @currencyid, 'T', 'F')")]  // last 2 params are @getrates and @getcounts
    public class PricingLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", isPrimaryKey: true, modeltype: FwDataTypes.Text)]
        public string MasterId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid",  modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cost", modeltype: FwDataTypes.Decimal)]
        public decimal? Cost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultcost", modeltype: FwDataTypes.Decimal)]
        public decimal? DefaultCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price", modeltype: FwDataTypes.Decimal)]
        public decimal? Price { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "markup", modeltype: FwDataTypes.Decimal)]
        public decimal? MarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "retail", modeltype: FwDataTypes.Decimal)]
        public decimal? Retail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hourlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? HourlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hourlycost", modeltype: FwDataTypes.Decimal)]
        public decimal? HourlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hourlymarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? HourlyMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? DailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailycost", modeltype: FwDataTypes.Decimal)]
        public decimal? DailyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailymarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? DailyMarkupPercent { get; set; }
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
        [FwSqlDataField(column: "weeklycost", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklymarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlycost", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlymarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyMarkupPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "maxdiscount", modeltype: FwDataTypes.Decimal)]
        public decimal? MaximumDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hastieredcost", modeltype: FwDataTypes.Boolean)]
        public bool? HasTieredCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "restockfee", modeltype: FwDataTypes.Decimal)]
        public decimal? RestockingFee { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "restockpercent", modeltype: FwDataTypes.Decimal)]
        public decimal? RestockingPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;

            if ((MasterId == null) || (MasterId.Equals(string.Empty)))
            {
                if ((request != null) && (request.uniqueids != null))
                {
                    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                    if (uniqueIds.ContainsKey("MasterId"))
                    {
                        MasterId = uniqueIds["MasterId"].ToString();
                    }
                }
            }


            if ((WarehouseId == null) || (WarehouseId.Equals(string.Empty)))
            {
                if ((request != null) && (request.uniqueids != null))
                {
                    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                    if (uniqueIds.ContainsKey("WarehouseId"))
                    {
                        WarehouseId = uniqueIds["WarehouseId"].ToString();
                    }
                }
            }

            if ((CurrencyId == null) || (CurrencyId.Equals(string.Empty)))
            {
                if ((request != null) && (request.uniqueids != null))
                {
                    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                    if (uniqueIds.ContainsKey("CurrencyId"))
                    {
                        CurrencyId = uniqueIds["CurrencyId"].ToString();
                    }
                }
            }

            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();

            if (MasterId == null)
            {
                MasterId = "";
            }
            if (WarehouseId == null)
            {
                WarehouseId = "";
            }
            if (CurrencyId == null)
            {
                CurrencyId = "";
            }

            select.AddParameter("@masterid", MasterId);
            select.AddParameter("@warehouseid", WarehouseId);
            select.AddParameter("@currencyid", CurrencyId);

        }
        //------------------------------------------------------------------------------------ 

    }
}