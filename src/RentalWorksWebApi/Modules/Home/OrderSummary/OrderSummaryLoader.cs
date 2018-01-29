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
