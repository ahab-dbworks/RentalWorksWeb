using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.BillingStatementReport
{
    public class BillingStatementReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "company", modeltype: FwDataTypes.Text)]
        public string OfficeLocationCompany { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locadd1", modeltype: FwDataTypes.Text)]
        public string OfficeLocationAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locadd2", modeltype: FwDataTypes.Text)]
        public string OfficeLocationAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccity", modeltype: FwDataTypes.Text)]
        public string OfficeLocationCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locstate", modeltype: FwDataTypes.Text)]
        public string OfficeLocationState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loczip", modeltype: FwDataTypes.Text)]
        public string OfficeLocationZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccountry", modeltype: FwDataTypes.Text)]
        public string OfficeLocationCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attention", modeltype: FwDataTypes.Text)]
        public string BillToAttention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billadd1", modeltype: FwDataTypes.Text)]
        public string BillToAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billadd2", modeltype: FwDataTypes.Text)]
        public string BilltoAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billcity", modeltype: FwDataTypes.Text)]
        public string BillToCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billstate", modeltype: FwDataTypes.Text)]
        public string BillToState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billzip", modeltype: FwDataTypes.Text)]
        public string BillToZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billcountry", modeltype: FwDataTypes.Text)]
        public string BillToCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "arid", modeltype: FwDataTypes.Text)]
        public string ReceiptId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedate", modeltype: FwDataTypes.Date)]
        public string InvoiceDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceno", modeltype: FwDataTypes.Text)]
        public string InvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nocharge", modeltype: FwDataTypes.Text)]
        public string NoCharge { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "wano", modeltype: FwDataTypes.Text)]
        public string WorkAuthorizationNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedesc", modeltype: FwDataTypes.Text)]
        public string InvoiceDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "due", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Due { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "received", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Received { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remaining", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Remaining { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pmtdate", modeltype: FwDataTypes.Date)]
        public string PaymentDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pmtdesc", modeltype: FwDataTypes.Text)]
        public string PaymentDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pmtamt", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? PaymentAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "arrectype", modeltype: FwDataTypes.Text)]
        public string ARRecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "haspayments", modeltype: FwDataTypes.Boolean)]
        public bool? HasPayments { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pendingfinancecharge", modeltype: FwDataTypes.Decimal)]
        public decimal? PendingFinanceCharge { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealtotal", modeltype: FwDataTypes.Decimal)]
        public decimal? DealTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ytdfinancecharge", modeltype: FwDataTypes.Decimal)]
        public decimal? YearToDateFinanceCharge { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nonbillable", modeltype: FwDataTypes.Boolean)]
        public bool? NonBillable { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(BillingStatementReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {

                using (FwSqlCommand qry = new FwSqlCommand(conn, "getbillingstatementweb", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@fromdate", SqlDbType.Date, ParameterDirection.Input, request.FromDate);
                    qry.AddParameter("@todate", SqlDbType.Date, ParameterDirection.Input, request.ToDate);
                    qry.AddParameter("@locationid", SqlDbType.Text, ParameterDirection.Input, request.OfficeLocationId);
                    qry.AddParameter("@includenocharge", SqlDbType.Text, ParameterDirection.Input, request.IncludeNoCharge);
                    qry.AddParameter("@includepaid", SqlDbType.Text, ParameterDirection.Input, request.IncludePaidInvoices);
                    qry.AddParameter("@includezerobalance", SqlDbType.Text, ParameterDirection.Input, request.IncludeZeroBalance);
                    qry.AddParameter("@paymentsthroughtoday", SqlDbType.Text, ParameterDirection.Input, request.PaymentsThroughToday);
                    qry.AddParameter("@dealstatusid", SqlDbType.Text, ParameterDirection.Input, request.DealStatusId);
                    qry.AddParameter("@dealtypeid", SqlDbType.Text, ParameterDirection.Input, request.DealTypeId);
                    qry.AddParameter("@customerid", SqlDbType.Text, ParameterDirection.Input, request.CustomerId);
                    qry.AddParameter("@dealid", SqlDbType.Text, ParameterDirection.Input, request.DealId);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
                //--------------------------------------------------------------------------------- 
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "Due", "Received", "Remaining" };
                dt.InsertSubTotalRows("Deal", "RowType", totalFields);
                dt.InsertSubTotalRows("InvoiceNumber", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
