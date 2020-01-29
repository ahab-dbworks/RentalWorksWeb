using FwStandard.SqlServer;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Modules.Exports.VendorInvoiceBatchExport
{
    public class VendorInvoiceBatchExportRequest : AppExportRequest
    {
        public string BatchId { get; set; }
    }

    public class VendorInvoiceBatchExportResponse : AppExportResponse { }

    public class VendorInvoiceBatchExportLoader : AppExportLoader
    {

        public class VendorInvoiceItem
        {
            public string ICode { get; set; }
            public string Description { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? QuantityNegative { get; set; }
            public decimal? Rate { get; set; }
            public decimal? RateNegative { get; set; }
            public decimal? Extended { get; set; }
            public decimal? ExtendedNegative { get; set; }
            public bool Taxable { get; set; }
            public string TaxableYesNo { get; set; }
            public string ExpenseAccountNumber { get; set; }
            public string ExpenseAccountDescription { get; set; }
        }

        public class BatchInvoice
        {
            public string VendorInvoiceId { get; set; }
            public string InvoiceNumber { get; set; }
            public string Vendor { get; set; }
            public string VendorNumber { get; set; }
            public List<VendorInvoiceItem> Items = new List<VendorInvoiceItem>(new VendorInvoiceItem[] { new VendorInvoiceItem() });
        }


        public List<BatchInvoice> VendorInvoices = new List<BatchInvoice>(new BatchInvoice[] { new BatchInvoice() });

        public async Task<bool> DoLoad<VendorInvoiceBatchExportLoader>(VendorInvoiceBatchExportRequest request)
        {
            bool loaded = false;

            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select b.chgbatchid, b.chgbatchno, b.chgbatchdatetime   ");
                qry.Add(" from  chgbatch b                                       ");
                qry.Add(" where b.chgbatchid = @chgbatchid                       ");
                qry.AddParameter("@chgbatchid", request.BatchId);
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                foreach (List<object> row in dt.Rows)
                {
                    BatchId = row[dt.GetColumnNo("chgbatchid")].ToString();
                    BatchNumber = row[dt.GetColumnNo("chgbatchno")].ToString();
                    BatchDateTime = FwConvert.ToDateTime(row[dt.GetColumnNo("chgbatchdatetime")].ToString());
                }
            }


            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                VendorInvoices.Clear();
                FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select *                                                                ");
                qry.Add(" from  vendorinvoiceexportview i                                        ");
                qry.Add(" where i.chgbatchid = @chgbatchid                                       ");
                qry.Add("order by i.invno                                                        ");
                qry.AddParameter("@chgbatchid", request.BatchId);
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                foreach (List<object> row in dt.Rows)
                {
                    BatchInvoice i = new BatchInvoice();
                    i.VendorInvoiceId = row[dt.GetColumnNo("vendorinvoiceid")].ToString();
                    i.InvoiceNumber = row[dt.GetColumnNo("invno")].ToString();
                    //i.InvoiceDate = FwConvert.ToUSShortDate(row[dt.GetColumnNo("invoicedate")].ToString());   // US specific #jhtodo: add format for user
                    //i.InvoiceDueDate = FwConvert.ToUSShortDate(row[dt.GetColumnNo("invoiceduedate")].ToString());   // US specific #jhtodo: add format for user
                    //i.InvoiceDescription = row[dt.GetColumnNo("invoicedesc")].ToString();
                    //i.InvoiceType = row[dt.GetColumnNo("invoicetype")].ToString();
                    //i.InvoiceTypeForQuickBooks = row[dt.GetColumnNo("invoicetypeqbo")].ToString();
                    //i.InvoiceClass = row[dt.GetColumnNo("invoiceclass")].ToString();
                    //i.Customer = row[dt.GetColumnNo("customer")].ToString();
                    //i.CustomerNumber = row[dt.GetColumnNo("customernumber")].ToString();
                    i.Vendor = row[dt.GetColumnNo("vendor")].ToString();
                    i.VendorNumber = row[dt.GetColumnNo("vendornumber")].ToString();
                    //i.InvoiceSubTotal = FwConvert.ToDecimal(row[dt.GetColumnNo("invoicesubtotal")].ToString());
                    //i.InvoiceSubTotalNegative = (-1) * i.InvoiceSubTotal;
                    //i.InvoiceTax = FwConvert.ToDecimal(row[dt.GetColumnNo("invoicetax")].ToString());
                    //i.InvoiceTaxNegative = (-1) * i.InvoiceTax;
                    //i.InvoiceTotal = FwConvert.ToDecimal(row[dt.GetColumnNo("invoicetotal")].ToString());
                    //i.InvoiceTotalNegative = (-1) * i.InvoiceTotal;
                    //
                    //i.PaymentTerms = row[dt.GetColumnNo("payterms")].ToString();
                    //i.PaymentTermsCode = row[dt.GetColumnNo("paytermscode")].ToString();
                    //i.AccountsReceivableAccountNumber = row[dt.GetColumnNo("arglno")].ToString();
                    //i.AccountsReceivableAccountDescription = row[dt.GetColumnNo("argldesc")].ToString();
                    //i.PurchaseOrderNumber = row[dt.GetColumnNo("pono")].ToString();
                    //i.BillToAttention = row[dt.GetColumnNo("billattention")].ToString();
                    //i.BillToAddress1 = row[dt.GetColumnNo("billadd1")].ToString();
                    //i.BillToAddress2 = row[dt.GetColumnNo("billadd2")].ToString();
                    //i.BillToCity = row[dt.GetColumnNo("billcity")].ToString();
                    //i.BillToState = row[dt.GetColumnNo("billstate")].ToString();
                    //i.BillToZip = row[dt.GetColumnNo("billzip")].ToString();
                    //i.BillToCountry = row[dt.GetColumnNo("billcountry")].ToString();

                    VendorInvoices.Add(i);
                }
            }

            loaded = true;
            return loaded;
        }
        //------------------------------------------------------------------------------------ 
    }
}
