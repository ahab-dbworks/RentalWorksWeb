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

namespace WebApi.Modules.Reports.OrderDepletingDepositReceiptReport
{
    [FwSqlTable("orderarwebrptview_cte")]
    public class OrderDepletingDepositReceiptReportLoader : AppReportLoader
    {
        public OrderDepletingDepositReceiptReportLoader()
        {
            this.Cte.AppendLine("orderarwebrptview_cte as (");
            this.Cte.AppendLine("  select *,");
            this.Cte.AppendLine("  customer = 'ANGRY DOG PRODUCTIONS',");
            this.Cte.AppendLine("  department = 'LIGHTING & GRIP'");
            this.Cte.AppendLine("  from orderarwebrptview");
            this.Cte.AppendLine(")");
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "arid", modeltype: FwDataTypes.Text)]
        public string ArId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ardate", modeltype: FwDataTypes.Date)]
        public string ArDate { get; set; }
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
        public string CheckNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pmtamt", modeltype: FwDataTypes.Decimal)]
        public decimal? PaymentAmount { get; set; }
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
        public List<OrderDepletingDepositReceiptReportOrderLoader> Items { get; set; } = new List<OrderDepletingDepositReceiptReportOrderLoader>(new OrderDepletingDepositReceiptReportOrderLoader[] { new OrderDepletingDepositReceiptReportOrderLoader() });
        //------------------------------------------------------------------------------------
        public async Task<OrderDepletingDepositReceiptReportLoader> RunReportAsync(OrderDepletingDepositReceiptReportRequest request)
        {
            OrderDepletingDepositReceiptReportLoader report;

            // Load report header
            var reportHeader = new OrderDepletingDepositReceiptReportLoader();
            reportHeader.SetDependencies(this.AppConfig, this.UserSession);
            var browseRequestHeader = new BrowseRequest();
            var miscfields = new Dictionary<string, string>();
            miscfields["OrderId"] = request.OrderId;
            browseRequestHeader.miscfields = miscfields;
            var taskHeaders = reportHeader.SelectAsync<OrderDepletingDepositReceiptReportLoader>(browseRequestHeader);

            // Load orders
            var reportOrders = new OrderDepletingDepositReceiptReportOrderLoader();
            reportOrders.SetDependencies(this.AppConfig, this.UserSession);
            var taskOrders = reportOrders.LoadItems<OrderDepletingDepositReceiptReportOrderLoader>(request);

            // Wait for the queries to run
            await Task.WhenAll(taskHeaders, taskOrders);

            List<OrderDepletingDepositReceiptReportLoader> headers = taskHeaders.Result;
            if (headers.Count == 0)
            {
                throw new Exception("Unable to load report header.");
            }
            report = headers[0];
            report.Items = taskOrders.Result;

            return report;
        }
    }

    public class OrderDepletingDepositReceiptReportOrderLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "periodtotal", modeltype: FwDataTypes.DecimalString2Digits)]
        public string PeriodTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "replacementcost", modeltype: FwDataTypes.DecimalString2Digits)]
        public string ReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depositamount", modeltype: FwDataTypes.DecimalString2Digits)]
        public string DepositAmount { get; set; }
        //------------------------------------------------------------------------------------
        public async Task<List<T>> LoadItems<T>(OrderDepletingDepositReceiptReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.Add("select rowtype='detail', orderno='L301205', description='Hello Kitty Convention', periodtotal=12500.00, replacementcost=64560.00, depositamount=21517.85");
                    qry.Add("union");
                    qry.Add("select rowtype='detail', orderno='L301206', description='Hello Kitty Uncaged', periodtotal=12500.00, replacementcost=64560.00, depositamount= 102.02");
                    qry.AddParameter("@orderid", SqlDbType.Text, ParameterDirection.Input, request.OrderId);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                    dt.InsertTotalRow("RowType", "detail", "grandtotal", new string[] { "PeriodTotal", "ReplacementCost", "DepositAmount" });
                    var items = dt.ToList<T>();
                    return items;
                }
                //--------------------------------------------------------------------------------- 
            }

        }
    }
}
