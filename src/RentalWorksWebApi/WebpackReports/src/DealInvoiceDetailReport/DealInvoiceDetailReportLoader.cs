using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
namespace WebApi.Modules.Reports.DealInvoiceDetailReport
{
    public class DealInvoiceDetailReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "id", modeltype: FwDataTypes.Integer)]
        public int? Id { get; set; }
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
        [FwSqlDataField(column: "divisionid", modeltype: FwDataTypes.Text)]
        public string DivisionId { get; set; }
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
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdate", modeltype: FwDataTypes.Date)]
        public string OrderDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text)]
        public string BillingPeriodId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiod", modeltype: FwDataTypes.Text)]
        public string BillingPeriod { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "periodtype", modeltype: FwDataTypes.Text)]
        public string PeriodType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentfrom", modeltype: FwDataTypes.Date)]
        public string EstimatedStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentto", modeltype: FwDataTypes.Date)]
        public string EstimatedStopDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodstart", modeltype: FwDataTypes.Date)]
        public string BillingPeriodStart { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodend", modeltype: FwDataTypes.Date)]
        public string BillingPeriodEnd { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingnote", modeltype: FwDataTypes.Text)]
        public string BillingNote { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceno", modeltype: FwDataTypes.Text)]
        public string InvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedate", modeltype: FwDataTypes.Date)]
        public string InvoiceDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ismultiorder", modeltype: FwDataTypes.Boolean)]
        public bool? IsMultiOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstart", modeltype: FwDataTypes.Date)]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingend", modeltype: FwDataTypes.Date)]
        public string BillingStopDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nonbillable", modeltype: FwDataTypes.Boolean)]
        public bool? Nonbillable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nocharge", modeltype: FwDataTypes.Boolean)]
        public bool? NoCharge { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nochargedesc", modeltype: FwDataTypes.Text)]
        public string NoChargeDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hiatus", modeltype: FwDataTypes.Boolean)]
        public bool? Hiatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hiatusdesc", modeltype: FwDataTypes.Text)]
        public string HiatusDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "flatpo", modeltype: FwDataTypes.Boolean)]
        public bool? FlatPO { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "flatpodesc", modeltype: FwDataTypes.Text)]
        public string FlatPODescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalrev", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RentalRevenue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RentalCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalnet", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RentalNet { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesrev", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SalesRevenue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salescost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SalesCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesnet", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SalesNet { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacerev", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SpaceRevenue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacecost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SpaceCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacenet", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? SpaceNet { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehiclerev", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? VehicleRevenue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehiclecost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? VehicleCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehiclenet", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? VehicleNet { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborrev", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? LaborRevenue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? LaborCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labornet", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? LaborNet { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscrev", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? MiscRevenue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misccost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? MiscCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscnet", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? MiscNet { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rsrev", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RentalSaleRevenue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rscost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RentalSaleCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rsnet", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? RentalSaleNet { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partsrev", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? PartsRevenue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partscost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? PartsCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partsnet", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? PartsNet { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Tax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalrev", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalRevenue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalnet", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? TotalNet { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(DealInvoiceDetailReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getdealinvoicerpt", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@datetype", SqlDbType.Text, ParameterDirection.Input, request.DateType);
                    qry.AddParameter("@fromdate", SqlDbType.Date, ParameterDirection.Input, request.FromDate);
                    qry.AddParameter("@todate", SqlDbType.Date, ParameterDirection.Input, request.ToDate);
                    qry.AddParameter("@locationid", SqlDbType.Text, ParameterDirection.Input, request.OfficeLocationId);
                    qry.AddParameter("@customerid", SqlDbType.Text, ParameterDirection.Input, request.CustomerId);
                    qry.AddParameter("@dealid", SqlDbType.Text, ParameterDirection.Input, request.DealId);
                    qry.AddParameter("@departmentid", SqlDbType.Text, ParameterDirection.Input, request.DepartmentId);
                    qry.AddParameter("@invoicestatus", SqlDbType.Text, ParameterDirection.Input, request.Statuses.ToString());
                    qry.AddParameter("@deductcosts", SqlDbType.Text, ParameterDirection.Input, request.DeductVendorItemCost);
                    qry.AddParameter("@nocharge", SqlDbType.Text, ParameterDirection.Input, request.NoCharge);
                    qry.AddParameter("@billedhiatus", SqlDbType.Text, ParameterDirection.Input, request.BilledHiatus);
                    qry.AddParameter("@billableflat", SqlDbType.Text, ParameterDirection.Input, request.BillableFlat);

                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
                //--------------------------------------------------------------------------------- 
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "RentalRevenue", "RentalCost", "RentalNet", "SalesRevenue", "SalesCost", "SalesNet", "SpaceRevenue", "SpaceCost", "SpaceNet", "VehicleRevenue", "VehicleCost", "VehicleNet", "LaborRevenue", "LaborCost", "LaborNet", "MiscRevenue", "MiscCost", "MiscNet", "RentalSaleRevenue", "RentalSaleCost", "RentalSaleNet", "PartsCost", "PartsNet", "PartsRevenue", "Tax", "TotalRevenue", "TotalCost", "TotalNet" };
                string[] headerFieldsOrderNumber = new string[] { "OrderDate", "OrderDescription", "OrderNumber", "BillingPeriod", "EstimatedStartDate", "EstimatedStopDate", "BillingPeriodStart", "BillingPeriodEnd" };
                dt.InsertSubTotalRows("OfficeLocation", "RowType", totalFields);
                dt.InsertSubTotalRows("Deal", "RowType", totalFields);
                dt.InsertSubTotalRows("OrderNumber", "RowType", totalFields, headerFieldsOrderNumber, totalFor: "Total for Order");
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
