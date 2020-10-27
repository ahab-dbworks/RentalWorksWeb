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
            public string OrderNumber { get; set; }
            public string OrderDescription { get; set; }
            public string ICode { get; set; }
            public string Description { get; set; }
            public string DescriptionWithoutDoubleQuotes { get; set; }
            public string DescriptionWithoutCommas { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? QuantityNegative { get; set; }
            public decimal? UnitCost { get; set; }
            public decimal? UnitCostNegative { get; set; }
            public decimal? Extended { get; set; }
            public decimal? ExtendedNegative { get; set; }
            public bool Taxable { get; set; }
            public string TaxableYesNo { get; set; }
            public string TaxableYN { get; set; }
            public string AccountNumber { get; set; }
            public string AccountDescription { get; set; }
        }

        public class BatchInvoice
        {
            public string VendorInvoiceId { get; set; }
            public string InvoiceNumber { get; set; }
            public string InvoiceDate { get; set; }
            public string InvoiceDueDate { get; set; }
            public string Vendor { get; set; }
            public string VendorNumber { get; set; }
            public string PurchaseOrderNumber { get; set; }
            public string PurchaseOrderDescription { get; set; }
            public decimal? InvoiceSubTotal { get; set; }
            public decimal? InvoiceSubTotalNegative { get; set; }
            public decimal? InvoiceTax { get; set; }
            public decimal? InvoiceTaxNegative { get; set; }
            public decimal? InvoiceTotal { get; set; }
            public decimal? InvoiceTotalNegative { get; set; }
            public string PaymentTerms { get; set; }
            public string PaymentTermsCode { get; set; }
            public List<VendorInvoiceItem> Items = new List<VendorInvoiceItem>(new VendorInvoiceItem[] { new VendorInvoiceItem() });
            public int ItemCount { get; set; }
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
                    i.InvoiceDate = FwConvert.ToShortDate(row[dt.GetColumnNo("invoicedate")].ToString());
                    i.InvoiceDueDate = FwConvert.ToShortDate(row[dt.GetColumnNo("invoiceduedate")].ToString());
                    i.PurchaseOrderNumber= row[dt.GetColumnNo("pono")].ToString();
                    i.PurchaseOrderDescription= row[dt.GetColumnNo("podesc")].ToString();
                    i.Vendor = row[dt.GetColumnNo("vendor")].ToString();
                    i.VendorNumber = row[dt.GetColumnNo("vendornumber")].ToString();
                    i.InvoiceSubTotal = FwConvert.ToDecimal(row[dt.GetColumnNo("invoicesubtotal")].ToString());
                    i.InvoiceSubTotalNegative = (-1) * i.InvoiceSubTotal;
                    i.InvoiceTax = FwConvert.ToDecimal(row[dt.GetColumnNo("invoicetax")].ToString());
                    i.InvoiceTaxNegative = (-1) * i.InvoiceTax;
                    i.InvoiceTotal = FwConvert.ToDecimal(row[dt.GetColumnNo("invoicetotal")].ToString());
                    i.InvoiceTotalNegative = (-1) * i.InvoiceTotal;
                    i.PaymentTerms = row[dt.GetColumnNo("payterms")].ToString();
                    i.PaymentTermsCode = row[dt.GetColumnNo("paytermscode")].ToString();
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

            foreach (BatchInvoice i in VendorInvoices)
            {
                if (!string.IsNullOrEmpty(i.VendorInvoiceId))
                {
                    i.Items.Clear();
                    using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
                    {
                        FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout);
                        qry.Add("select *                                                                            ");
                        qry.Add(" from  vendorinvoiceitemexportview vii                                              ");
                        qry.Add(" where vii.vendorinvoiceid = @vendorinvoiceid                                        ");
                        qry.Add("order by vii.itemorder                                                              ");
                        qry.AddParameter("@vendorinvoiceid", i.VendorInvoiceId);
                        FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                        foreach (List<object> row in dt.Rows)
                        {
                            VendorInvoiceItem ii = new VendorInvoiceItem();
                            ii.OrderNumber= row[dt.GetColumnNo("poorderno")].ToString();
                            ii.OrderDescription= row[dt.GetColumnNo("poorderdesc")].ToString();
                            ii.ICode = row[dt.GetColumnNo("masterno")].ToString();
                            ii.Description = row[dt.GetColumnNo("description")].ToString();
                            ii.DescriptionWithoutDoubleQuotes = ii.Description.Replace("\"", "");
                            ii.DescriptionWithoutCommas = ii.Description.Replace(",", "");
                            ii.Quantity = FwConvert.ToDecimal(row[dt.GetColumnNo("qty")].ToString());
                            ii.QuantityNegative = (-1) * ii.Quantity;
                            ii.UnitCost = FwConvert.ToDecimal(row[dt.GetColumnNo("cost")].ToString());
                            ii.UnitCostNegative = (-1) * ii.UnitCost;
                            ii.Extended = FwConvert.ToDecimal(row[dt.GetColumnNo("extended")].ToString());
                            ii.ExtendedNegative = (-1) * ii.Extended;
                            ii.Taxable = FwConvert.ToBoolean(row[dt.GetColumnNo("taxable")].ToString());
                            ii.TaxableYesNo = ii.Taxable ? "Yes" : "No";
                            ii.TaxableYN = ii.Taxable ? "Y" : "N";
                            ii.AccountNumber = row[dt.GetColumnNo("glno")].ToString();
                            ii.AccountDescription = row[dt.GetColumnNo("glacctdesc")].ToString();
                            i.Items.Add(ii);
                        }
                    }
                    i.ItemCount = i.Items.Count;
                }
            }

            loaded = true;
            return loaded;
        }
        //------------------------------------------------------------------------------------ 
    }
}
