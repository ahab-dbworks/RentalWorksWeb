using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using WebLibrary;
using System;

namespace WebApi.Modules.Reports.Billing.InvoiceDiscountReport
{
    [FwSqlTable("dbo.funcinvoicediscountrpt(@invoicedatefrom, @invoicedateto, @billingstartdatefrom, @billingstartdateto, @discountpct, @includedeptswithnodata)")]
    public class InvoiceDiscountReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
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
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedate", modeltype: FwDataTypes.Date)]
        public string InvoiceDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedesc", modeltype: FwDataTypes.Text)]
        public string InvoiceDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceno", modeltype: FwDataTypes.Text)]
        public string InvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstart", modeltype: FwDataTypes.Date)]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingend", modeltype: FwDataTypes.Date)]
        public string BillingEndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicegrosstotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? InvoiceGrossTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountamtwdw", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? DiscountAmountWithDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountpct", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? DiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalactual", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalActual { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totallines", modeltype: FwDataTypes.Integer)]
        public int? TotalLines { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountlines", modeltype: FwDataTypes.Integer)]
        public int? DiscountLines { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountlinesoftotal", modeltype: FwDataTypes.Text)]
        public string DiscountLinesOfTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicediscountreasonid", modeltype: FwDataTypes.Text)]
        public string DiscountReasonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicediscountreason", modeltype: FwDataTypes.Text)]
        public string DiscountReason { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(InvoiceDiscountReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;

            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
				select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();

                    if (request.DateType.Equals(RwConstants.INVOICE_DATE_TYPE_BILLING_START_DATE))
                    {
                        select.AddParameter("@invoicedatefrom", DBNull.Value);       // will be ignored
                        select.AddParameter("@invoicedateto", DBNull.Value);         // will be ignored
                        select.AddParameter("@billingstartdatefrom", request.FromDate);
                        select.AddParameter("@billingstartdateto", request.ToDate);
                    }
                    else
                    {
                        select.AddParameter("@invoicedatefrom", request.FromDate);
                        select.AddParameter("@invoicedateto", request.ToDate);
                        select.AddParameter("@billingstartdatefrom", DBNull.Value);  // will be ignored
                        select.AddParameter("@billingstartdateto", DBNull.Value);    // will be ignored
                    }

                    select.AddParameter("@discountpct", request.DiscountPercent);
                    select.AddParameter("@includedeptswithnodata", "F");

                    select.AddWhereIn("locationid", request.OfficeLocationId);
                    select.AddWhereIn("departmentid", request.DepartmentId);
                    select.AddWhereIn("customerid", request.CustomerId);
                    select.AddWhereIn("dealid", request.DealId);
                    select.AddWhereIn("invoicediscountreasonid", request.DiscountReasonId);

                    select.AddOrderBy("location, department,invoicediscountreason, deal, invoicedate, orderno, agent, discountpct");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }

            if (request.IncludeSubHeadingsAndSubTotals)
            {
                dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;
                string[] totalFields = new string[] { "InvoiceGrossTotal", "DiscountAmountWithDaysPerWeek", "TotalActual" };
                dt.InsertSubTotalRows("OfficeLocation", "RowType", totalFields);
                dt.InsertSubTotalRows("Department", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }

            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
