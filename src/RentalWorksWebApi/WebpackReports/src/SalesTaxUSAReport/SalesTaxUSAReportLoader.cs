using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using WebLibrary;

namespace WebApi.Modules.Reports.SalesTaxUSAReport
{
    [FwSqlTable("getsalestaxusarpt")]
    public class SalesTaxUSAReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Text)]
        public string SessionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxoptionid", modeltype: FwDataTypes.Text)]
        public string TaxOptionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxoption", modeltype: FwDataTypes.Text)]
        public string TaxOption { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceno", modeltype: FwDataTypes.Text)]
        public string InvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.Date)]
        public string InputDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedate", modeltype: FwDataTypes.Date)]
        public string InvoiceDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstart", modeltype: FwDataTypes.Date)]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? InvoiceTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalrate", modeltype: FwDataTypes.Percentage)]
        public decimal? RentalRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesrate", modeltype: FwDataTypes.Percentage)]
        public decimal? SalesRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborrate", modeltype: FwDataTypes.Percentage)]
        public decimal? LaborRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "donottaxrental", modeltype: FwDataTypes.Boolean)]
        public bool? DoNotTaxRental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "donottaxsales", modeltype: FwDataTypes.Boolean)]
        public bool? DoNotTaxSales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "donottaxlabor", modeltype: FwDataTypes.Boolean)]
        public bool? DoNotTaxLabor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpayadjustment", modeltype: FwDataTypes.Boolean)]
        public bool? QuikPayAdjustment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "writeoffadjustment", modeltype: FwDataTypes.Boolean)]
        public bool? WriteOffAdjustment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxrule", modeltype: FwDataTypes.Boolean)]
        public bool? TaxRule { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxcountry", modeltype: FwDataTypes.Text)]
        public string TaxCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalrental", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalRental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxablerental", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TaxableRental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nontaxablerental", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? NontaxableRental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaltax1", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RentalTax1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaltax2", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RentalTax2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaltax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RentalTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalsales", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalSales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxablesales", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TaxableSales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nontaxablesales", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? NontaxableSales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salestax1", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SalesTax1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salestax2", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SalesTax2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salestax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SalesTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totallabor", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalLabor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxablelabor", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TaxableLabor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nontaxablelabor", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? NontaxableLabor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labortax1", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? LaborTax1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labortax2", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? LaborTax2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labortax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? LaborTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pretaxtotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? PretaxTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nontaxabletotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? NontaxableTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxabletotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TaxableTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totaltax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "gst", modeltype: FwDataTypes.Decimal)]
        public decimal? Gst { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hst", modeltype: FwDataTypes.Decimal)]
        public decimal? Hst { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pst", modeltype: FwDataTypes.Decimal)]
        public decimal? Pst { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "id", modeltype: FwDataTypes.Integer)]
        public int? Id { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string Rowtype { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(SalesTaxUSAReportRequest request)
        {
            string dateField = "invoicedate";
            if (request.DateType.Equals(RwConstants.INVOICE_DATE_TYPE_BILLING_START_DATE))
            {
                dateField = "billingstart";
            }
            else if (request.DateType.Equals(RwConstants.INVOICE_DATE_TYPE_INPUT_DATE))
            {
                dateField = "inputdate";
            }

            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getsalestaxusarpt", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@fromdate", SqlDbType.Date, ParameterDirection.Input, request.FromDate);
                    qry.AddParameter("@todate", SqlDbType.Date, ParameterDirection.Input, request.ToDate);
                    qry.AddParameter("@datetype", SqlDbType.Text, ParameterDirection.Input, dateField);
                    qry.AddParameter("@locationid", SqlDbType.Text, ParameterDirection.Input, request.OfficeLocationId);
                    qry.AddParameter("@departmentid", SqlDbType.Text, ParameterDirection.Input, request.DepartmentId);
                    qry.AddParameter("@statuses", SqlDbType.Text, ParameterDirection.Input, request.Statuses.ToString());

                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
                //--------------------------------------------------------------------------------- 
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "TotalRental", "TaxableRental", "NontaxableRental", "RentalTax", "TotalSales", "TaxableSales", "NontaxableSales", "SalesTax", "TotalLabor", "TaxableLabor", "NontaxableLabor", "LaborTax", "TotalTax" };
                string[] headerFieldsTaxRate = new string[] { "RentalRate", "SalesRate", "LaborRate" };
                dt.InsertSubTotalRows("OfficeLocation", "RowType", totalFields);
                dt.InsertSubTotalRows("TaxOption", "RowType", totalFields, headerFieldsTaxRate, totalFor: "Total for Tax Option");
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
