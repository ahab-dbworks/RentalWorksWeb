using FwStandard.DataLayer;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using System.Reflection;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Modules.Home.OrderSummary
{
    [FwSqlTable("dealorder")]
    public class OrderSummaryLoader : AppDataLoadRecord
    {
        //private string orderId;
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalprice", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalPrice { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentaldisc", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalDiscount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentalcost", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalCost{ get; set; }
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
        [FwSqlDataField(column: "spacetotal", modeltype: FwDataTypes.Decimal)]
        public decimal? FacilitiesTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehicleprice", modeltype: FwDataTypes.Decimal)]
        public decimal? TransporationPrice { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehicledisc", modeltype: FwDataTypes.Decimal)]
        public decimal? TransporationDiscount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclecost", modeltype: FwDataTypes.Decimal)]
        public decimal? TransporationCost { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehicleprofit", modeltype: FwDataTypes.Decimal)]
        public decimal? TransporationProfit { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclemarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? TransporationMarkup { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclemargin", modeltype: FwDataTypes.Decimal)]
        public decimal? TransporationMargin { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclesub", modeltype: FwDataTypes.Decimal)]
        public decimal? TransporationSubTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehicletotal", modeltype: FwDataTypes.Decimal)]
        public decimal? TransporationTotal { get; set; }
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
        [FwSqlDataField(column: "rentalsaletotal", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalSaleTotal { get; set; }
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
        [FwSqlDataField(column: "replacement", modeltype: FwDataTypes.Decimal)]
        public decimal? ReplacementCostSubs { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "value", modeltype: FwDataTypes.Decimal)]
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
        //protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        //{
        //    select.Add("exec getordersummaryasresultset '" + OrderId + "'");
        //    //base.SetBaseSelectQuery(select, qry, customFields, request);
        //    //select.AddWhere("ordertype = '" + RwConstants.ORDER_TYPE_ORDER + "'");
        //    //addFilterToSelect("WarehouseId", "warehouseid", select, request);
        //}
        ////------------------------------------------------------------------------------------    

        //jh 01/29/2018 note: I don't really want to override GetASync this way.  This is a hack until we can figure out a clean way to parse the "exec stored_procedure_name" syntax" using teh FwSqlSelect object
        public override async Task<dynamic> GetAsync<T>(FwCustomFields customFields = null)
        {
            //if (AllPrimaryKeysHaveValues)
            if (PrimaryKeyCount > 0)
            {
                if (AllPrimaryKeysHaveValues)
                {
                    using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
                    {
                        FwSqlSelect select = new FwSqlSelect();
                        using (FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout))
                        {
                            //SetBaseSelectQuery(select, qry, customFields);
                            //select.Add("exec getordersummaryasresultset @orderid");
                            //select.AddParameter("@orderid", OrderId);
                            qry.Clear();
                            qry.Add("exec getordersummaryasresultset '" + OrderId + "'");

                            MethodInfo method = typeof(FwSqlCommand).GetMethod("SelectAsync");
                            MethodInfo generic = method.MakeGenericMethod(this.GetType());
                            object openAndCloseConnection = true;
                            dynamic result = generic.Invoke(qry, new object[] { openAndCloseConnection, customFields });
                            dynamic records = await result;
                            dynamic record = null;
                            if (records.Count > 0)
                            {
                                record = records[0];
                            }
                            return record;
                        }
                    }
                }
                else
                {
                    throw new Exception("One or more Primary Key values are missing on " + GetType().ToString() + ".GetAsync");
                }
            }
            else
            {
                throw new Exception("No Primary Keys have been defined on " + GetType().ToString());
            }
        }
        //------------------------------------------------------------------------------------

    }
}
