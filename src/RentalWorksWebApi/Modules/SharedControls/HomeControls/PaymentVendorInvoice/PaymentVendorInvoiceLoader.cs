using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using WebApi.Data;
namespace WebApi.Modules.Billing.PaymentVendorInvoice
{
    [FwSqlTable("dbo.funcpaymentvendorinvoiceweb(@locationid, @paymentid, @paymentdate)")]
    public class PaymentVendorInvoiceLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "id", modeltype: FwDataTypes.Text)]
        public string VendorInvoicePaymentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorinvoiceid", modeltype: FwDataTypes.Text)]
        public string VendorInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invno", modeltype: FwDataTypes.Text)]
        public string VendorInvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invdate", modeltype: FwDataTypes.Date)]
        public string VendorInvoiceDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podesc", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requisitionno", modeltype: FwDataTypes.Text)]
        public string RequisitionNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "total", modeltype: FwDataTypes.Decimal)]
        public decimal? Total { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetype", modeltype: FwDataTypes.Text)]
        public string InvoiceType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "applied", modeltype: FwDataTypes.Decimal)]
        public decimal? Applied { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "amount", modeltype: FwDataTypes.Decimal)]
        public decimal? Amount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "due", modeltype: FwDataTypes.Decimal)]
        public decimal? Due { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sapdocumentno", modeltype: FwDataTypes.Text)]
        public string ExternalDocumentNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sapdocumentdate", modeltype: FwDataTypes.Date)]
        public string ExternalDocumentDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencysymbol", modeltype: FwDataTypes.Text)]
        public string CurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            string locationId = GetUniqueIdAsString("OfficeLocationId", request) ?? "";
            string paymentId = GetUniqueIdAsString("PaymentId", request) ?? "";
            DateTime paymentDate = GetUniqueIdAsDate("PaymentDate", request) ?? DateTime.MinValue;

            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 

            select.AddParameter("@locationid", locationId);
            select.AddParameter("@paymentid", paymentId);
            select.AddParameter("@paymentdate", paymentDate);

            addFilterToSelect("VendorId", "vendorid", select, request);
            addFilterToSelect("CurrencyId", "currencyid", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
