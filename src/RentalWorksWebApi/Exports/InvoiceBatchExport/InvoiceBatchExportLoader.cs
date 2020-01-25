using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Modules.Exports.InvoiceBatchExport
{
    public class InvoiceBatchExportRequest : AppExportRequest
    {
        public string BatchId { get; set; }
    }

    public class InvoiceBatchExportResponse : AppExportResponse { }

    public class InvoiceBatchExportLoader : AppExportLoader
    {
        public class InvoiceItem
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
            public string IncomeAccountNumber { get; set; }
            public string IncomeAccountDescription { get; set; }
        }

        public class InvoiceTax
        {
            public string TaxOption { get; set; }
            public string Code { get; set; }
            public string Description { get; set; }
            public string Vendor { get; set; }
            public decimal? RentalTaxRate1 { get; set; }
            public decimal? RentalTaxRate2 { get; set; }
            public decimal? SalesTaxRate1 { get; set; }
            public decimal? SalesTaxRate2 { get; set; }
            public decimal? LaborTaxRate1 { get; set; }
            public decimal? LaborTaxRate2 { get; set; }
            public string TaxAccountNumber1 { get; set; }
            public string TaxAccountDescription1 { get; set; }
            public string TaxAccountNumber2 { get; set; }
            public string TaxAccountDescription2 { get; set; }
        }

        public class BatchInvoice
        {
            public string InvoiceId { get; set; }
            public string InvoiceNumber { get; set; }
            public string InvoiceDate { get; set; }
            public string InvoiceDueDate { get; set; }
            public string InvoiceDescription { get; set; }
            public string InvoiceType { get; set; }
            public string InvoiceTypeForQuickBooks { get; set; }
            public string InvoiceClass { get; set; }
            public string Customer { get; set; }
            public string CustomerNumber { get; set; }
            public string Deal { get; set; }
            public string DealNumber { get; set; }
            public decimal? InvoiceSubTotal { get; set; }
            public decimal? InvoiceSubTotalNegative { get; set; }
            public decimal? InvoiceTax { get; set; }
            public decimal? InvoiceTaxNegative { get; set; }
            public decimal? InvoiceTotal { get; set; }
            public decimal? InvoiceTotalNegative { get; set; }
            public string PaymentTerms { get; set; }
            public string PaymentTermsCode { get; set; }
            public string AccountsReceivableAccountNumber { get; set; }
            public string AccountsReceivableAccountDescription { get; set; }
            public string PurchaseOrderNumber { get; set; }
            public string BillToAttention { get; set; }
            public string BillToAddress1 { get; set; }
            public string BillToAddress2 { get; set; }
            public string BillToCity { get; set; }
            public string BillToState { get; set; }
            public string BillToZip { get; set; }
            public string BillToCountry { get; set; }

            public List<InvoiceItem> Items = new List<InvoiceItem>(new InvoiceItem[] { new InvoiceItem() });
            public List<InvoiceTax> Taxes = new List<InvoiceTax>(new InvoiceTax[] { new InvoiceTax() });
        }

        public List<BatchInvoice> Invoices = new List<BatchInvoice>(new BatchInvoice[] { new BatchInvoice() });

        //------------------------------------------------------------------------------------ 
        public async Task<bool> DoLoad<InvoiceBatchExportLoader>(InvoiceBatchExportRequest request)
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
                Invoices.Clear();
                FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select *                                                                ");
                qry.Add(" from  invoiceexportview i                                              ");
                qry.Add(" where i.chgbatchid = @chgbatchid                                       ");
                qry.Add("order by i.invoiceno                                                    ");
                qry.AddParameter("@chgbatchid", request.BatchId);
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                foreach (List<object> row in dt.Rows)
                {
                    BatchInvoice i = new BatchInvoice();
                    i.InvoiceId = row[dt.GetColumnNo("invoiceid")].ToString();
                    i.InvoiceNumber = row[dt.GetColumnNo("invoiceno")].ToString();
                    i.InvoiceDate = FwConvert.ToUSShortDate(row[dt.GetColumnNo("invoicedate")].ToString());   // US specific #jhtodo: add format for user
                    i.InvoiceDueDate = FwConvert.ToUSShortDate(row[dt.GetColumnNo("invoiceduedate")].ToString());   // US specific #jhtodo: add format for user
                    i.InvoiceDescription = row[dt.GetColumnNo("invoicedesc")].ToString();
                    i.InvoiceType = row[dt.GetColumnNo("invoicetype")].ToString();
                    i.InvoiceTypeForQuickBooks = row[dt.GetColumnNo("invoicetypeqbo")].ToString();
                    i.InvoiceClass = row[dt.GetColumnNo("invoiceclass")].ToString();
                    i.Customer = row[dt.GetColumnNo("customer")].ToString();
                    i.CustomerNumber = row[dt.GetColumnNo("customernumber")].ToString();
                    i.Deal = row[dt.GetColumnNo("deal")].ToString();
                    i.DealNumber = row[dt.GetColumnNo("dealnumber")].ToString();
                    i.InvoiceSubTotal = FwConvert.ToDecimal(row[dt.GetColumnNo("invoicesubtotal")].ToString());
                    i.InvoiceSubTotalNegative = (-1) * i.InvoiceSubTotal;
                    i.InvoiceTax = FwConvert.ToDecimal(row[dt.GetColumnNo("invoicetax")].ToString());
                    i.InvoiceTaxNegative = (-1) * i.InvoiceTax;
                    i.InvoiceTotal = FwConvert.ToDecimal(row[dt.GetColumnNo("invoicetotal")].ToString());
                    i.InvoiceTotalNegative = (-1) * i.InvoiceTotal;

                    i.PaymentTerms = row[dt.GetColumnNo("payterms")].ToString();
                    i.PaymentTermsCode = row[dt.GetColumnNo("paytermscode")].ToString();
                    i.AccountsReceivableAccountNumber = row[dt.GetColumnNo("arglno")].ToString();
                    i.AccountsReceivableAccountDescription = row[dt.GetColumnNo("argldesc")].ToString();
                    i.PurchaseOrderNumber = row[dt.GetColumnNo("pono")].ToString();
                    i.BillToAttention = row[dt.GetColumnNo("billattention")].ToString();
                    i.BillToAddress1 = row[dt.GetColumnNo("billadd1")].ToString();
                    i.BillToAddress2 = row[dt.GetColumnNo("billadd2")].ToString();
                    i.BillToCity = row[dt.GetColumnNo("billcity")].ToString();
                    i.BillToState = row[dt.GetColumnNo("billstate")].ToString();
                    i.BillToZip = row[dt.GetColumnNo("billzip")].ToString();
                    i.BillToCountry = row[dt.GetColumnNo("billcountry")].ToString();

                    Invoices.Add(i);
                }
            }

            foreach (BatchInvoice i in Invoices)
            {
                if (!string.IsNullOrEmpty(i.InvoiceId))
                {
                    i.Items.Clear();
                    using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
                    {
                        FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout);
                        qry.Add("select *                                                                            ");
                        qry.Add(" from  invoiceitemexportview ii                                                     ");
                        qry.Add(" where ii.invoiceid = @invoiceid                                                    ");
                        qry.Add("order by ii.itemorder                                                               ");
                        qry.AddParameter("@invoiceid", i.InvoiceId);
                        FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                        foreach (List<object> row in dt.Rows)
                        {
                            InvoiceItem ii = new InvoiceItem();
                            ii.ICode = row[dt.GetColumnNo("masterno")].ToString();
                            ii.Description = row[dt.GetColumnNo("description")].ToString();
                            ii.Quantity = FwConvert.ToDecimal(row[dt.GetColumnNo("qty")].ToString());
                            ii.QuantityNegative = (-1) * ii.Quantity;
                            ii.Rate = FwConvert.ToDecimal(row[dt.GetColumnNo("rate")].ToString());
                            ii.RateNegative = (-1) * ii.Rate;
                            ii.Extended = FwConvert.ToDecimal(row[dt.GetColumnNo("linetotal")].ToString());
                            ii.ExtendedNegative = (-1) * ii.Extended;
                            ii.Taxable = FwConvert.ToBoolean(row[dt.GetColumnNo("taxable")].ToString());
                            ii.TaxableYesNo = ii.Taxable ? "Yes" : "No";
                            ii.IncomeAccountNumber = row[dt.GetColumnNo("incomeglno")].ToString();
                            ii.IncomeAccountDescription = row[dt.GetColumnNo("incomegldesc")].ToString();
                            i.Items.Add(ii);
                        }
                    }


                    i.Taxes.Clear();
                    using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
                    {
                        FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout);
                        qry.Add("select *                                                                            ");
                        qry.Add(" from  invoicetaxexportview t                                                       ");
                        qry.Add(" where t.invoiceid = @invoiceid                                                     ");
                        qry.AddParameter("@invoiceid", i.InvoiceId);
                        FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                        foreach (List<object> row in dt.Rows)
                        {
                            InvoiceTax t = new InvoiceTax();
                            t.TaxOption = row[dt.GetColumnNo("taxoption")].ToString();
                            t.Code = row[dt.GetColumnNo("taxcode")].ToString();
                            t.Description = row[dt.GetColumnNo("taxdescription")].ToString();
                            t.Vendor = row[dt.GetColumnNo("taxvendor")].ToString();
                            t.RentalTaxRate1 = FwConvert.ToDecimal(row[dt.GetColumnNo("rentaltaxrate1")].ToString());
                            t.RentalTaxRate2 = FwConvert.ToDecimal(row[dt.GetColumnNo("rentaltaxrate2")].ToString());
                            t.SalesTaxRate1 = FwConvert.ToDecimal(row[dt.GetColumnNo("salestaxrate1")].ToString());
                            t.SalesTaxRate2 = FwConvert.ToDecimal(row[dt.GetColumnNo("salestaxrate2")].ToString());
                            t.LaborTaxRate1 = FwConvert.ToDecimal(row[dt.GetColumnNo("labortaxrate1")].ToString());
                            t.LaborTaxRate2 = FwConvert.ToDecimal(row[dt.GetColumnNo("labortaxrate2")].ToString());
                            t.TaxAccountNumber1 = row[dt.GetColumnNo("taxglno1")].ToString();
                            t.TaxAccountDescription1 = row[dt.GetColumnNo("taxgldesc1")].ToString();
                            t.TaxAccountNumber2 = row[dt.GetColumnNo("taxglno2")].ToString();
                            t.TaxAccountDescription2 = row[dt.GetColumnNo("taxgldesc2")].ToString();
                            i.Taxes.Add(t);
                        }
                    }

                }
            }

            loaded = true;
            return loaded;
        }
        //------------------------------------------------------------------------------------ 
    }
}
