using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using System;
using FwStandard.Utilities;
using System.Dynamic;
using WebApi.Modules.HomeControls.DealCredit;

namespace WebApi.Modules.Reports.OrderDepletingDepositReceiptReport
{
    [FwSqlTable("arwebrptview")]
    public class ReceiptReportLoader : AppReportLoader
    {
        //public ReceiptReportLoader()
        //{
        //    this.Cte.AppendLine("arwebrptview_cte as (");
        //    this.Cte.AppendLine("  select *");
        //    //this.Cte.AppendLine("    showorders = @showorders,");
        //    //this.Cte.AppendLine("    showdeals = @showdeals,");
        //    //this.Cte.AppendLine("    showinvoices = @showinvoices");
        //    this.Cte.AppendLine("  from arwebrptview with (nolock)");
        //    this.Cte.AppendLine(")");
        //}
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "arid", modeltype: FwDataTypes.Text)]
        public string ReceiptId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ardate", modeltype: FwDataTypes.Date)]
        public string ReceiptDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccode", modeltype: FwDataTypes.Text)]
        public string LocationCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
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
        [FwSqlDataField(column: "paymentby", modeltype: FwDataTypes.Text)]
        public string PaymentBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text)]
        public string PayTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytype", modeltype: FwDataTypes.Text)]
        public string PayType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pmttype", modeltype: FwDataTypes.Text)]
        public string PaymentType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytypeexportpaymentmethod", modeltype: FwDataTypes.Text)]
        public string PayTypeExportPaymentMethod { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "checkno", modeltype: FwDataTypes.Text)]
        public string CheckNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pmtamt", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public string PaymentAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "appliedbyid", modeltype: FwDataTypes.Text)]
        public string AppliedById { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pmtmemo", modeltype: FwDataTypes.Text)]
        public string PaymentMemo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Boolean)]
        public bool? RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectypedisplay", modeltype: FwDataTypes.Text)]
        public string RecTypeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencysymbol", modeltype: FwDataTypes.Boolean)]
        public bool? CurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locdefaultcurrencyid", modeltype: FwDataTypes.Text)]
        public string LocationDefaultCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "overpaymentid", modeltype: FwDataTypes.Text)]
        public string OverPaymentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "authorizationcode", modeltype: FwDataTypes.Text)]
        public string AuthorizationCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add1", modeltype: FwDataTypes.Text)]
        public string Address1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add2", modeltype: FwDataTypes.Text)]
        public string Address2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "city", modeltype: FwDataTypes.Text)]
        public string City { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text)]
        public string Phone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text)]
        public string Zip { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Text)]
        public string ReportType { get; set; }
        //------------------------------------------------------------------------------------ 
        public List<DepletingDepositReceiptReportOrderLoader> Orders { get; set; } = new List<DepletingDepositReceiptReportOrderLoader>(new DepletingDepositReceiptReportOrderLoader[] { new DepletingDepositReceiptReportOrderLoader() });
        //------------------------------------------------------------------------------------ 
        public List<DepletingDepositReceiptReportDealLoader> Deals { get; set; } = new List<DepletingDepositReceiptReportDealLoader>(new DepletingDepositReceiptReportDealLoader[] { new DepletingDepositReceiptReportDealLoader() });
        //------------------------------------------------------------------------------------ 
        public List<DepletingDepositReceiptReportInvoiceLoader> Invoices { get; set; } = new List<DepletingDepositReceiptReportInvoiceLoader>(new DepletingDepositReceiptReportInvoiceLoader[] { new DepletingDepositReceiptReportInvoiceLoader() });
        //------------------------------------------------------------------------------------ 
        public List<DealCreditLoader> Credits { get; set; } = new List<DealCreditLoader>(new DealCreditLoader[] { new DealCreditLoader() });
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            this.addFilterToSelect("OrderId", "orderid", select, request);
            this.addFilterToSelect("DealId", "dealid", select, request);
            this.addFilterToSelect("ReceiptId", "arid", select, request);
        }
        //------------------------------------------------------------------------------------
        public async Task<ReceiptReportLoader> RunReportAsync(ReceiptReportRequest request)
        {
            bool hasReceiptId = !string.IsNullOrEmpty(request.ReceiptId);
            if (!hasReceiptId)
            {
                throw new ArgumentException("ReceiptId ia required");
            }
            
            ReceiptReportLoader report;
            {
                // Load receipts (header)
                var reportHeader = new ReceiptReportLoader();
                reportHeader.SetDependencies(this.AppConfig, this.UserSession);
                var receiptReportLoaderRequest = new BrowseRequest();
                if (!string.IsNullOrEmpty(request.ReceiptId))
                {
                    receiptReportLoaderRequest.uniqueids.ReceiptId = request.ReceiptId;
                }
                var headers = await reportHeader.SelectAsync<ReceiptReportLoader>(receiptReportLoaderRequest);
                if (headers.Count == 0)
                {
                    throw new Exception("Unable to load report: No receipts were found.");
                }
                report = headers[0];
            }

            if (report.RecTypeDisplay == "DEPLETING DEPOSIT")
            {
                if (!string.IsNullOrEmpty(report.OrderId))
                {
                    // Order Depleting Deposit Report
                    report.ReportType = "ORDER_DEPLETING_DEPOSIT";
                    var orderLoader = new DepletingDepositReceiptReportOrderLoader();
                    orderLoader.SetDependencies(this.AppConfig, this.UserSession);
                    BrowseRequest orderLoaderRequest = new BrowseRequest();
                    orderLoaderRequest.activeviewfields["LocationId"] = new List<string>();
                    orderLoaderRequest.activeviewfields["LocationId"].Add(report.LocationId);
                    orderLoaderRequest.uniqueids.ReceiptId = report.ReceiptId;
                    orderLoaderRequest.uniqueids.OrderId = report.OrderId;
                    //orderLoaderRequest.orderby = "ReceiptDate desc";
                    var dtOrders = await orderLoader.BrowseAsync(orderLoaderRequest);
                    dtOrders.InsertTotalRow("RowType", "detail", "grandtotal", new string[] { "PeriodTotal", "ReplacementCost", "DepositAmount" });
                    report.Orders = dtOrders.ToList<DepletingDepositReceiptReportOrderLoader>();
                }
                else
                {
                    // Deal Depleting Deposit Report
                    report.ReportType = "DEAL_DEPLETING_DEPOSIT";
                    var dealLoader = new DepletingDepositReceiptReportDealLoader();
                    dealLoader.SetDependencies(this.AppConfig, this.UserSession);
                    BrowseRequest dealLoaderRequest = new BrowseRequest();
                    dealLoaderRequest.activeviewfields["LocationId"] = new List<string>();
                    dealLoaderRequest.activeviewfields["LocationId"].Add(report.LocationId);
                    dealLoaderRequest.uniqueids.ReceiptId = report.ReceiptId;
                    dealLoaderRequest.uniqueids.DealId = report.DealId;
                    var dtDeals = await dealLoader.BrowseAsync(dealLoaderRequest);
                    dtDeals.InsertTotalRow("RowType", "detail", "grandtotal", new string[] { "DepositAmount" });
                    report.Deals = dtDeals.ToList<DepletingDepositReceiptReportDealLoader>();
                }
            }
            else if (report.RecTypeDisplay != "REFUND")
            {
                // Receipt Report
                report.ReportType = "RECEIPT";
                var invoiceLoader = new DepletingDepositReceiptReportInvoiceLoader();
                invoiceLoader.SetDependencies(this.AppConfig, this.UserSession);
                BrowseRequest invoiceLoaderRequest = new BrowseRequest();
                invoiceLoaderRequest.activeviewfields["LocationId"] = new List<string>();
                invoiceLoaderRequest.activeviewfields["LocationId"].Add(report.LocationId);
                invoiceLoaderRequest.uniqueids.ReceiptId = request.ReceiptId;
                //invoiceLoaderRequest.orderby = "ReceiptDate desc";
                var dtInvoices = await invoiceLoader.BrowseAsync(invoiceLoaderRequest);
                dtInvoices.InsertTotalRow("RowType", "detail", "grandtotal", new string[] { "Applied" });
                report.Invoices = dtInvoices.ToList<DepletingDepositReceiptReportInvoiceLoader>();
            }

            return report;
        }
    }

    [FwSqlTable("orderarwebrptview_cte")]
    public class DepletingDepositReceiptReportOrderLoader : AppReportLoader
    {
        public DepletingDepositReceiptReportOrderLoader()
        {
            this.Cte.AppendLine("orderarwebrptview_cte as (");
            this.Cte.AppendLine("  select v.*,");
            this.Cte.AppendLine("    rowtype='detail',");
            this.Cte.AppendLine("    receiptdate=ar.ardate");
            this.Cte.AppendLine("  from orderarwebrptview v with (nolock) join ar with (nolock) on (v.arid = ar.arid)");
            this.Cte.AppendLine(")");
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "arid", modeltype: FwDataTypes.Text)]
        public string ReceiptId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receiptdate", modeltype: FwDataTypes.Text)]
        public string ReceiptDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "periodtotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public string PeriodTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcost", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public string ReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pmtamt", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public string DepositAmount { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            this.addFilterToSelect("ReceiptId", "arid", select, request);
            this.addFilterToSelect("OrderId", "orderid", select, request);
        }
    }

    [FwSqlTable("dealarwebrptview_cte")]
    public class DepletingDepositReceiptReportDealLoader : AppReportLoader
    {
        public DepletingDepositReceiptReportDealLoader()
        {
            this.Cte.AppendLine("dealarwebrptview_cte as (");
            this.Cte.AppendLine("  select *,");
            this.Cte.AppendLine("    rowtype='detail'");
            this.Cte.AppendLine("  from dealarwebrptview with (nolock)");
            this.Cte.AppendLine(")");
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "arid", modeltype: FwDataTypes.Text)]
        public string ReceiptId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string DealDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pmtamt", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public string DepositAmount { get; set; }
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            this.addFilterToSelect("ReceiptId", "arid", select, request);
            this.addFilterToSelect("DealId", "dealid", select, request);
        }
    }

    [FwSqlTable("invoicearwebrptview_cte")]
    public class DepletingDepositReceiptReportInvoiceLoader : AppReportLoader
    {
        public DepletingDepositReceiptReportInvoiceLoader()
        {
            this.Cte.AppendLine("invoicearwebrptview_cte as (");
            this.Cte.AppendLine("  select iar.*,");
            this.Cte.AppendLine("    rowtype='detail'");
            this.Cte.AppendLine("  from invoicearwebrptview iar with (nolock)");
            this.Cte.AppendLine(")");
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "arid", modeltype: FwDataTypes.Text)]
        public string ArId { get; set; }
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
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public string InvoiceTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "applied", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public string Applied { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            this.addFilterToSelect("ReceiptId", "arid", select, request);
        }
    }
}
