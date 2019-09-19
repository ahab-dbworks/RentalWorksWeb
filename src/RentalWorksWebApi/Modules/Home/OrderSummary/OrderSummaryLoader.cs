using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using System.Reflection;
using System.Threading.Tasks;
using WebApi.Data;
using WebLibrary;

namespace WebApi.Modules.Home.OrderSummary
{
    public class OrderSummaryLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "totaltype", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string TotalType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalprice", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalPrice { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentaldisc", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalDiscount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalcost", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalCost { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalprofit", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalProfit { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalmarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalMarkup { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalmargin", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalMargin { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalsub", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalSubTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentaltax", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentaltotal", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salesprice", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesPrice { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salesdisc", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesDiscount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salescost", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesCost { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salesprofit", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesProfit { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salesmarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesMarkup { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salesmargin", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesMargin { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salessub", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesSubTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salestax", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salestotal", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "partsprice", modeltype: FwDataTypes.Decimal)]
        public decimal? PartsPrice { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "partsdisc", modeltype: FwDataTypes.Decimal)]
        public decimal? PartsDiscount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "partscost", modeltype: FwDataTypes.Decimal)]
        public decimal? PartsCost { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "partsprofit", modeltype: FwDataTypes.Decimal)]
        public decimal? PartsProfit { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "partsmarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? PartsMarkup { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "partsmargin", modeltype: FwDataTypes.Decimal)]
        public decimal? PartsMargin { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "partssub", modeltype: FwDataTypes.Decimal)]
        public decimal? PartsSubTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "partstax", modeltype: FwDataTypes.Decimal)]
        public decimal? PartsTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "partstotal", modeltype: FwDataTypes.Decimal)]
        public decimal? PartsTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "spaceprice", modeltype: FwDataTypes.Decimal)]
        public decimal? FacilitiesPrice { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "spacedisc", modeltype: FwDataTypes.Decimal)]
        public decimal? FacilitiesDiscount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "spacecost", modeltype: FwDataTypes.Decimal)]
        public decimal? FacilitiesCost { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "spaceprofit", modeltype: FwDataTypes.Decimal)]
        public decimal? FacilitiesProfit { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "spacemarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? FacilitiesMarkup { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "spacemargin", modeltype: FwDataTypes.Decimal)]
        public decimal? FacilitiesMargin { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "spacesub", modeltype: FwDataTypes.Decimal)]
        public decimal? FacilitiesSubTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "spacetax", modeltype: FwDataTypes.Decimal)]
        public decimal? FacilitiesTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "spacetotal", modeltype: FwDataTypes.Decimal)]
        public decimal? FacilitiesTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehicleprice", modeltype: FwDataTypes.Decimal)]
        public decimal? TransportationPrice { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehicledisc", modeltype: FwDataTypes.Decimal)]
        public decimal? TransportationDiscount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclecost", modeltype: FwDataTypes.Decimal)]
        public decimal? TransportationCost { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehicleprofit", modeltype: FwDataTypes.Decimal)]
        public decimal? TransportationProfit { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclemarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? TransportationMarkup { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclemargin", modeltype: FwDataTypes.Decimal)]
        public decimal? TransportationMargin { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclesub", modeltype: FwDataTypes.Decimal)]
        public decimal? TransportationSubTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehicletax", modeltype: FwDataTypes.Decimal)]
        public decimal? TransportationTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehicletotal", modeltype: FwDataTypes.Decimal)]
        public decimal? TransportationTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "laborprice", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborPrice { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "labordisc", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborDiscount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "laborcost", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborCost { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "laborprofit", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborProfit { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "labormarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborMarkup { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "labormargin", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborMargin { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "laborsub", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborSubTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "labortax", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "labortotal", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "miscprice", modeltype: FwDataTypes.Decimal)]
        public decimal? MiscPrice { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "miscdisc", modeltype: FwDataTypes.Decimal)]
        public decimal? MiscDiscount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "misccost", modeltype: FwDataTypes.Decimal)]
        public decimal? MiscCost { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "miscprofit", modeltype: FwDataTypes.Decimal)]
        public decimal? MiscProfit { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "miscmarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? MiscMarkup { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "miscmargin", modeltype: FwDataTypes.Decimal)]
        public decimal? MiscMargin { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "miscsub", modeltype: FwDataTypes.Decimal)]
        public decimal? MiscSubTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "misctax", modeltype: FwDataTypes.Decimal)]
        public decimal? MiscTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "misctotal", modeltype: FwDataTypes.Decimal)]
        public decimal? MiscTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalsaleprice", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalSalePrice { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalsaledisc", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalSaleDiscount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalsalecost", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalSaleCost { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalsaleprofit", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalSaleProfit { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalsalemarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalSaleMarkup { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalsalemargin", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalSaleMargin { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalsalesub", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalSaleSubTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalsaletax", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalSaleTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalsaletotal", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalSaleTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "totalprice", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalPrice { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "totaldisc", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalDiscount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "totalcost", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalCost { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "totalprofit", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalProfit { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "totalmarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalMarkup { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "totalmargin", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalMargin { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "totalsub", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalSubTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "totaltax", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "total", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxcost", modeltype: FwDataTypes.Decimal)]
        public decimal? TaxCost { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "replacement", modeltype: FwDataTypes.Decimal)]
        public decimal? ReplacementCostTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "value", modeltype: FwDataTypes.Decimal)]
        public decimal? ValueTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ownedreplacement", modeltype: FwDataTypes.Decimal)]
        public decimal? ReplacementCostOwned { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ownedvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? ValueOwned { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subreplacement", modeltype: FwDataTypes.Decimal)]
        public decimal? ReplacementCostSubs { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? ValueSubs { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weightlbs", modeltype: FwDataTypes.Integer)]
        public int? WeightPounds { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weightoz", modeltype: FwDataTypes.Integer)]
        public int? WeightOunces { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weightkg", modeltype: FwDataTypes.Integer)]
        public int? WeightKilograms { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "weightgr", modeltype: FwDataTypes.Integer)]
        public int? WeightGrams { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "caseweightlbs", modeltype: FwDataTypes.Integer)]
        public int? WeightInCasePounds { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "caseweightoz", modeltype: FwDataTypes.Integer)]
        public int? WeightInCaseOunces { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "caseweightkg", modeltype: FwDataTypes.Integer)]
        public int? WeightInCaseKilograms { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "caseweightgr", modeltype: FwDataTypes.Integer)]
        public int? WeightInCaseGrams { get; set; }
        //------------------------------------------------------------------------------------
        public override async Task<dynamic> GetAsync<T>(FwCustomFields customFields = null, FwSqlConnection conn = null)
        {
            if (string.IsNullOrEmpty(OrderId))
            {
                throw new Exception("OrderId not supplied.");
            }
            else
            {
                if (string.IsNullOrEmpty(TotalType))
                {
                    TotalType = RwConstants.TOTAL_TYPE_PERIOD;
                }
                if (conn == null)
                {
                    conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString);
                }
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Clear();
                    qry.Add("exec getordersummaryasresultset @orderid = @orderid, @totaltype = @totaltype");
                    qry.AddParameter("@orderid", OrderId);
                    qry.AddParameter("@totaltype", TotalType);
                    dynamic records = await qry.SelectAsync<OrderSummaryLoader>(/*true, */customFields);
                    dynamic record = null;
                    if (records.Count > 0)
                    {
                        record = records[0];
                    }
                    return record;
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
