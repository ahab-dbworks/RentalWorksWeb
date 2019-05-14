using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;

namespace WebApi.Modules.Reports.ProfitLossReport
{
    [FwSqlTable("dbo.funcprofitandlossrpt('@fromdate','@todate')")]
    public class ProfitLossReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "eventid", modeltype: FwDataTypes.Text)]
        public string EventId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "eventno", modeltype: FwDataTypes.Text)]
        public string EventNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "event", modeltype: FwDataTypes.Text)]
        public string Event { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "eventmanager", modeltype: FwDataTypes.Text)]
        public string EventManager { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderstatus", modeltype: FwDataTypes.Text)]
        public string OrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderlocation", modeltype: FwDataTypes.Text)]
        public string OrderLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "profitlossdate", modeltype: FwDataTypes.Date)]
        public string ProfitLossDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "revenue", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Revenue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "expenses", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Expenses { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "profit", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Profit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "marginpct", modeltype: FwDataTypes.Decimal)]
        public decimal? MarginPercentage { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(ProfitLossReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                //--------------------------------------------------------------------------------- 
                // below uses a "select" query to populate the FwJsonDataTable 
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();
                    select.AddWhereIn("locationid", request.OfficeLocationId);
                    select.AddWhereIn("departmentid", request.DepartmentId);
                    select.AddWhereIn("agentid", request.AgentId);
                    select.AddWhereIn("customerid", request.CustomerId);
                    select.AddWhereIn("dealid", request.DealId);
                    select.AddWhereIn("orderid", request.OrderId);
                    if ((string.IsNullOrEmpty(request.DateField)) || (!request.DateField.Equals("profitlossdate")))
                    {
                        request.DateField = "profitlossdate";
                    }
                    select.AddParameter("@fromdate", request.FromDate);
                    select.AddParameter("@todate", request.ToDate);
                    select.AddParameter("@datefield", request.DateField);
                    select.AddWhereIn("and", "orderstatus", request.Statuses.ToString(), false);
                    select.AddOrderBy("location, department, customer, deal, agent");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }

                //--------------------------------------------------------------------------------- 
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "Revenue", "Expenses", "Profit" };
                dt.InsertSubTotalRows("OfficeLocation", "RowType", totalFields);
                dt.InsertSubTotalRows("Department", "RowType", totalFields);
                dt.InsertSubTotalRows("Customer", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
