using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
namespace WebApi.Modules.Reports.CreditsOnAccountReport
{
    [FwSqlTable("creditsonaccountwebview")]
    public class CreditsOnAccountReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totaldepdep", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalDepletingDeposit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalcredit", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalCredit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalover", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalOverpayment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totaldeposit", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalDeposit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalapplied", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalApplied { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalrefunded", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalRefunded { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remaining", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Remaining { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(CreditsOnAccountReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
				select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();

                    select.AddWhereIn("locationid", request.OfficeLocationId);
                    select.AddWhereIn("customerid", request.CustomerId);
                    select.AddWhereIn("dealid", request.DealId);

                    if (request.OnlyRemaining.GetValueOrDefault(false))
                    {
                        select.AddWhere("remaining > 0");
                    }
                    select.AddOrderBy("location,customer,deal");

                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }

            if (request.IncludeSubHeadingsAndSubTotals)
            {
                dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;
                string[] totalFields = new string[] { "TotalDepletingDeposit", "TotalCredit", "TotalOverpayment", "TotalDeposit", "TotalApplied", "TotalRefunded", "Remaining" };
                dt.InsertSubTotalRows("OfficeLocation", "RowType", totalFields);
                dt.InsertSubTotalRows("Customer", "RowType", totalFields);
                //dt.InsertSubTotalRows("Deal", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------

    }
}
