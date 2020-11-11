using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using WebApi.Data;
namespace WebApi.Modules.HomeControls.VendorInvoiceItemCorrespondingDealInvoices
{
    [FwSqlTable("orderinvoicewebview")]
    public class VendorInvoiceItemCorrespondingDealInvoicesLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderinvoiceid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string OrderInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedate", modeltype: FwDataTypes.Date)]
        public string InvoiceDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicestatus", modeltype: FwDataTypes.Text)]
        public string InvoiceStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceno", modeltype: FwDataTypes.Text)]
        public string InvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetype", modeltype: FwDataTypes.Text)]
        public string InvoiceType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstart", modeltype: FwDataTypes.Date)]
        public string BillingStart { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingend", modeltype: FwDataTypes.Date)]
        public string BillingEnd { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("OrderId", "orderid", select, request);
            DateTime billingEnd = GetUniqueIdAsDate("BillingEndDate", request) ?? DateTime.MinValue;
            DateTime billingStart = GetUniqueIdAsDate("BillingStartDate", request) ?? DateTime.MinValue;
            //addDateFilterToSelect("billingstart", billingEnd, select, ">=", "billingend");
            //addDateFilterToSelect("billingend", billingStart, select, "<=", "billingstart");
            addDateFilterToSelect("billingstart", billingEnd, select, "<=", "billingend");
            addDateFilterToSelect("billingend", billingStart, select, ">=", "billingstart");
            select.AddWhereIn("and", "invoicetype", RwConstants.INVOICE_TYPE_BILLING + ", " + RwConstants.INVOICE_TYPE_CREDIT);
            select.AddWhere(" (invoicestatus <> '" + RwConstants.INVOICE_STATUS_VOID + "')");
        }
        //------------------------------------------------------------------------------------ 
    }
}
