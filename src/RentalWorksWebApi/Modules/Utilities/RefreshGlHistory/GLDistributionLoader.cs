using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using WebApi.Logic;

namespace WebApi.Modules.Utilities.GLDistribution
{
    [FwSqlTable("dbo.funcvendorinvoiceglweb(@vendorinvoiceid)")]
    public class GLDistributionLoader : AppDataLoadRecord
    {
        private bool _previewing = false;
        //------------------------------------------------------------------------------------ 
        public GLDistributionLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "gldate", modeltype: FwDataTypes.Date)]
        public string Date { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "glno", modeltype: FwDataTypes.Text)]
        public string GlAccountNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "glacctdesc", modeltype: FwDataTypes.Text)]
        public string GlAccountDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "debit", modeltype: FwDataTypes.Decimal)]
        public decimal? Debit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "credit", modeltype: FwDataTypes.Decimal)]
        public decimal? Credit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "glaccountid", modeltype: FwDataTypes.Text)]
        public string GlAccountId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groupheading", modeltype: FwDataTypes.Text)]
        public string GroupHeading { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Integer)]
        public int? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groupheadingorder", modeltype: FwDataTypes.Integer)]
        public int? GroupHeadingOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currency", modeltype: FwDataTypes.Text)]
        public string Currency { get; set; }
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
            string invoiceId = GetUniqueIdAsString("InvoiceId", request) ?? "";
            string receiptId = GetUniqueIdAsString("ReceiptId", request) ?? ""; 
            string vendorInvoiceId = GetUniqueIdAsString("VendorInvoiceId", request) ?? "";
            string paymentId = GetUniqueIdAsString("PaymentId", request) ?? "";
            string purchaseId = GetUniqueIdAsString("PurchaseId", request) ?? "";
            string retiredId = GetUniqueIdAsString("RetiredId", request) ?? "";
            _previewing = GetMiscFieldAsBoolean("Preview", request) ?? false;

            if (_previewing)
            {
                if (!string.IsNullOrEmpty(invoiceId))
                {
                    string invoiceStatus = AppFunc.GetStringDataAsync(AppConfig, "invoice", "invoiceid", invoiceId, "status").Result;
                    if ((invoiceStatus != RwConstants.INVOICE_STATUS_NEW) && (invoiceStatus != RwConstants.INVOICE_STATUS_APPROVED))
                    {
                        _previewing = false;
                    }
                }
                else if (!string.IsNullOrEmpty(vendorInvoiceId))
                {
                    string vendorInvoiceStatus = AppFunc.GetStringDataAsync(AppConfig, "vendorinvoice", "vendorinvoiceid", vendorInvoiceId, "status").Result;
                    if ((vendorInvoiceStatus != RwConstants.VENDOR_INVOICE_STATUS_NEW) && (vendorInvoiceStatus != RwConstants.VENDOR_INVOICE_STATUS_APPROVED))
                    {
                        _previewing = false;
                    }
                }


                if (_previewing)
                {
                    if (!string.IsNullOrEmpty(invoiceId))
                    {
                        bool b = GLDistributionFunc.PostGlForInvoice(AppConfig, invoiceId, true).Result;
                    }
                    else if (!string.IsNullOrEmpty(vendorInvoiceId))
                    {
                        bool b = GLDistributionFunc.PostGlForVendorInvoice(AppConfig, vendorInvoiceId, true).Result;
                    }
                }
            }

            if (!invoiceId.Equals(string.Empty))
            {
                OverrideTableName = "dbo.funcinvoiceglweb(@invoiceid)";
            }
            else if (!receiptId.Equals(string.Empty))
            {
                OverrideTableName = "dbo.funcarglweb(@arid)";
            }
            else if (!vendorInvoiceId.Equals(string.Empty))
            {
                OverrideTableName = "dbo.funcvendorinvoiceglweb(@vendorinvoiceid)";
            }
            else if (!paymentId.Equals(string.Empty))
            {
                OverrideTableName = "dbo.funcpaymentglweb(@paymentid)";
            }
            else if (!purchaseId.Equals(string.Empty))
            {
                OverrideTableName = "dbo.funcpurchaseglweb(@purchaseid)";
            }
            else if (!retiredId.Equals(string.Empty))
            {
                OverrideTableName = "dbo.funcretiredglweb(@retiredid)";
            }

            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();

            if (!invoiceId.Equals(string.Empty))
            {
                select.AddParameter("@invoiceid", invoiceId);
            }
            if (!receiptId.Equals(string.Empty))
            {
                select.AddParameter("@arid", receiptId);
            }
            if (!vendorInvoiceId.Equals(string.Empty))
            {
                select.AddParameter("@vendorinvoiceid", vendorInvoiceId);
            }
            if (!paymentId.Equals(string.Empty))
            {
                select.AddParameter("@paymentid", paymentId);
            }
            if (!purchaseId.Equals(string.Empty))
            {
                select.AddParameter("@purchaseid", purchaseId);
            }
            if (!retiredId.Equals(string.Empty))
            {
                select.AddParameter("@retiredid", retiredId);
            }
        }
        //------------------------------------------------------------------------------------ 
        public void OnAfterBrowse(object sender, AfterBrowseEventArgs e)
        {
            if (_previewing)
            {
                string invoiceId = GetUniqueIdAsString("InvoiceId", e.Request) ?? "";
                string vendorInvoiceId = GetUniqueIdAsString("VendorInvoiceId", e.Request) ?? "";
                if (!string.IsNullOrEmpty(invoiceId))
                {
                    bool b = GLDistributionFunc.DeleteGlForInvoice(AppConfig, invoiceId).Result;
                }
                else if (!string.IsNullOrEmpty(vendorInvoiceId))
                {
                    bool b = GLDistributionFunc.DeleteGlForVendorInvoice(AppConfig, vendorInvoiceId).Result;
                }

            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
