using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using WebLibrary;

namespace WebApi.Modules.Reports.VendorInvoiceSummaryReport
{
    [FwSqlTable("vendorinvoiceviewwithaccruals")]
    public class VendorInvoiceSummaryReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorinvoiceid", modeltype: FwDataTypes.Text)]
        public string VendorInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requisitionno", modeltype: FwDataTypes.Text)]
        public string RequisitionNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorno", modeltype: FwDataTypes.Text)]
        public string VendorNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicebatchid", modeltype: FwDataTypes.Text)]
        public string InvoiceBatchId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.Date)]
        public string InputDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputusersid", modeltype: FwDataTypes.Text)]
        public string InputByUsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputuser", modeltype: FwDataTypes.Text)]
        public string InputByUser { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invno", modeltype: FwDataTypes.Text)]
        public string InvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invdate", modeltype: FwDataTypes.Date)]
        public string InvoiceDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceduedate", modeltype: FwDataTypes.Date)]
        public string InvoiceDueDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstart", modeltype: FwDataTypes.Date)]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingend", modeltype: FwDataTypes.Date)]
        public string BillingEndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstartend", modeltype: FwDataTypes.Text)]
        public string BillingDateRange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poorderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podeal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podealnodeal", modeltype: FwDataTypes.Text)]
        public string DealNumberAndDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approveddate", modeltype: FwDataTypes.Date)]
        public string ApprovedDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetotal", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? InvoiceTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "payterms", modeltype: FwDataTypes.Boolean)]
        public bool? PaymentTerms { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxid", modeltype: FwDataTypes.Text)]
        public string TaxId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxoptionid", modeltype: FwDataTypes.Text)]
        public string TaxOptionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxitemcode", modeltype: FwDataTypes.Text)]
        public string TaxItemCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billedhiatus", modeltype: FwDataTypes.Boolean)]
        public bool? BilledHiatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetype", modeltype: FwDataTypes.Text)]
        public string InvoiceType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanagerid", modeltype: FwDataTypes.Text)]
        public string ProjectManagerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locdefaultcurrencyid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationDefaultCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(VendorInvoiceSummaryReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();

                    select.AddWhereIn("locationid", request.OfficeLocationId);
                    select.AddWhereIn("departmentid", request.DepartmentId);
                    select.AddWhereIn("vendorid", request.VendorId);
                    select.AddWhereIn("dealid", request.DealId);
                    select.AddWhereIn("orderid", request.PurchaseOrderId);

                    string dateField = "invdate";
                    if (request.DateType.Equals(RwConstants.INVOICE_DATE_TYPE_BILLING_START_DATE))
                    {
                        dateField = "billingstart";
                    }
                    addDateFilterToSelect(dateField, request.FromDate, select, ">=", "fromdate");
                    addDateFilterToSelect(dateField, request.ToDate, select, "<=", "todate");
                    select.AddWhereIn("and", "status", request.Statuses.ToString(), false);

                    //accruals
                    if (request.IncludeAccruals.GetValueOrDefault(false))
                    {
                        if (request.AccrualFromDate != null)
                        {
                            select.AddWhere("invno = '" + RwConstants.VENDOR_INVOICE_NUMBER_ACCRUAL + "'");
                            dateField = "billingend";
                            addDateFilterToSelect(dateField, request.AccrualFromDate, select, ">=", "accrualfromdate");
                        }
                        if (request.AccrualToDate != null)
                        {
                            select.AddWhere("invno = '" + RwConstants.VENDOR_INVOICE_NUMBER_ACCRUAL + "'");
                            dateField = "billingend";
                            addDateFilterToSelect(dateField, request.AccrualToDate, select, "<=", "accrualtodate");
                        }
                    }
                    else
                    {
                        select.AddWhere("invno <> '" + RwConstants.VENDOR_INVOICE_NUMBER_ACCRUAL + "'");
                    }


                    select.AddOrderBy("location, department, vendor, pono");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            string[] totalFields = new string[] { "InvoiceTotal" };
            dt.InsertSubTotalRows("OfficeLocation", "RowType", totalFields);
            dt.InsertSubTotalRows("Department", "RowType", totalFields);
            dt.InsertSubTotalRows("Vendor", "RowType", totalFields);
            dt.InsertSubTotalRows("PurchaseOrderNumber", "RowType", totalFields);
            dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
