using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using FwStandard.Models;
using System.Text;

namespace WebApi.Modules.Reports.AccountingReports.DailyReceiptsReport
{
    [FwSqlTable("dailyreceiptview")]
    public class DailyReceiptsReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationcode", modeltype: FwDataTypes.Text)]
        public string Locationcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "name", modeltype: FwDataTypes.Text)]
        public string Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentby", modeltype: FwDataTypes.Text)]
        public string PaymentBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytype", modeltype: FwDataTypes.Text)]
        public string PaymentType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text)]
        public string PaymentTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "checkno", modeltype: FwDataTypes.Text)]
        public string CheckNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceno", modeltype: FwDataTypes.Text)]
        public string InvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ardate", modeltype: FwDataTypes.Date)]
        public string ReceiptDate { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "amount", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        [FwSqlDataField(calculatedColumnSql: "(case when(invoiceid = '') then amount when(invoiceid = min(invoiceid) over(partition by arid order by invoiceid)) then amount else 0.00 end)", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Amount { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "overpayment", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        [FwSqlDataField(calculatedColumnSql: "(case when(invoiceid = '') then overpayment when(invoiceid = min(invoiceid) over(partition by arid order by invoiceid)) then overpayment else 0.00 end)", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Overpayment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "applied", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Applied { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "arid", modeltype: FwDataTypes.Text)]
        public string ReceiptId { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(DailyReceiptsReportRequest request)
        {
            FwJsonDataTable dt = null;

            // build a default "sortBy" object to use for sorting and grouping if none provided in the request
            CheckBoxListItems sortBy = new CheckBoxListItems();
            sortBy.Add(new CheckBoxListItem("OfficeLocation", "Office Location", true));
            sortBy.Add(new CheckBoxListItem("Name", "Customer / Deal", true));
            sortBy.Add(new CheckBoxListItem("PaymentType", "Payment Type", true));

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
                    select.AddWhereIn("paytypeid", request.PaymentTypeId);
                    addDateFilterToSelect("ardate", request.FromDate, select, ">=", "fromdate");
                    addDateFilterToSelect("ardate", request.ToDate, select, "<=", "todate");
                    //select.AddOrderBy("location, ardate, name, arid, invoiceid");

                    // if a valid/non-empty request.SortBy is provided in the request, substitute it in
                    if (request.SortBy != null)
                    {
                        CheckBoxListItems requestedSortBy = request.SortBy.GetSelectedItems();
                        if (requestedSortBy.Count > 0)
                        {
                            sortBy = requestedSortBy;
                        }
                    }

                    StringBuilder orderBy = new StringBuilder();
                    foreach (CheckBoxListItem item in sortBy)
                    {
                        if (orderBy.Length > 0)
                        {
                            orderBy.Append(",");
                        }
                        orderBy.Append(item.value.Equals("OfficeLocation") ? "location" : "");  // can use reflection for this
                        orderBy.Append(item.value.Equals("Name") ? "name" : "");
                        orderBy.Append(item.value.Equals("PaymentType") ? "paytype" : "");
                    }
                    orderBy.Append(", arid, invoiceid");
                    select.AddOrderBy(orderBy.ToString());

                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "Amount", "Overpayment", "Applied" };
                foreach (CheckBoxListItem item in sortBy)
                {
                    dt.InsertSubTotalRows(item.value, "RowType", totalFields);
                }
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
