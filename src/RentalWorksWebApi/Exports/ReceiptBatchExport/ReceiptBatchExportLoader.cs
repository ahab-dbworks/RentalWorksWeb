using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Modules.Exports.ReceiptBatchExport
{
    public class ReceiptBatchExportRequest : AppExportRequest
    {
        public string BatchId { get; set; }
    }

    public class ReceiptBatchExportResponse : AppExportResponse { }

    public class ReceiptBatchExportLoader : AppExportLoader  // maybe add a new superclass that all Exports inherit from?
    {
        public class ReceiptInvoice
        {
            public string InvoiceId { get; set; }
            public string InvoiceNumber { get; set; }
            public string InvoiceDate { get; set; }
            public string InvoiceDescription { get; set; }
            public string Customer { get; set; }
            public string Deal { get; set; }
            public decimal? Applied { get; set; }
        }

        public class GLTransaction
        {
            public string AccountId { get; set; }
            public string GroupHeading { get; set; }
            public string AccountNumber { get; set; }
            public string AccountDescription { get; set; }
            public decimal? Debit { get; set; }
            public decimal? Credit { get; set; }
            public decimal? Amount { get; set; }
            public string AmountWithCurrencySymbol { get; set; }
            public string OrderNumber { get; set; }
            public string OrderDescription { get; set; }
            public int? OrderBy { get; set; }
            public string Currency { get; set; }
            public string CurrencyCode { get; set; }
            public string CurrencySymbol { get; set; }
            public bool IsAccountsReceivable { get; set; }
        }

        public class BatchReceipt
        {
            public string ReceiptId { get; set; }
            public string ReceiptDate { get; set; }
            public string CheckNumber { get; set; }
            public string PaymentType { get; set; }
            public string PaymentBy { get; set; }
            public string Customer { get; set; }
            public string CustomerNumber { get; set; }
            public string Deal { get; set; }
            public string DealNumber { get; set; }
            public decimal? Amount { get; set; }
            public decimal? AmountNegative { get; set; }
            public string AccountsReceivableAccountNumber { get; set; }
            public string AccountsReceivableAccountDescription { get; set; }
            public string UndepositedFundsAccountNumber { get; set; }
            public string UndepositedFundsAccountDescription { get; set; }

            public List<ReceiptInvoice> Invoices = new List<ReceiptInvoice>(new ReceiptInvoice[] { new ReceiptInvoice() });
            public List<GLTransaction> GLTransactions = new List<GLTransaction>(new GLTransaction[] { new GLTransaction() });
        }

        public List<BatchReceipt> Receipts = new List<BatchReceipt>(new BatchReceipt[] { new BatchReceipt() });

        //------------------------------------------------------------------------------------ 
        public async Task<bool> DoLoad<ReceiptBatchExportLoader>(ReceiptBatchExportRequest request)
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
                Receipts.Clear();
                FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select *                                                                ");
                qry.Add(" from  receiptexportview r                                              ");
                qry.Add(" where r.chgbatchid = @chgbatchid                                       ");
                qry.Add("order by r.checkno                                                      ");
                qry.AddParameter("@chgbatchid", request.BatchId);
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                foreach (List<object> row in dt.Rows)
                {
                    BatchReceipt r = new BatchReceipt();
                    r.ReceiptId = row[dt.GetColumnNo("arid")].ToString();
                    r.ReceiptDate = FwConvert.ToUSShortDate(row[dt.GetColumnNo("ardate")].ToString());
                    r.CheckNumber = row[dt.GetColumnNo("checkno")].ToString();
                    r.PaymentType = row[dt.GetColumnNo("paytype")].ToString();
                    r.PaymentBy = row[dt.GetColumnNo("paymentby")].ToString();
                    r.CustomerNumber = row[dt.GetColumnNo("custno")].ToString();
                    r.Customer = row[dt.GetColumnNo("customer")].ToString();
                    r.DealNumber = row[dt.GetColumnNo("dealno")].ToString();
                    r.Deal = row[dt.GetColumnNo("deal")].ToString();
                    r.Amount = FwConvert.ToDecimal(row[dt.GetColumnNo("amount")].ToString());
                    r.AmountNegative = (-1) * r.Amount;
                    r.AccountsReceivableAccountNumber = row[dt.GetColumnNo("arglno")].ToString();
                    r.AccountsReceivableAccountDescription = row[dt.GetColumnNo("argldesc")].ToString();
                    r.UndepositedFundsAccountNumber = row[dt.GetColumnNo("undepositedfundsglno")].ToString();
                    r.UndepositedFundsAccountDescription = row[dt.GetColumnNo("undepositedfundsgldesc")].ToString();
                    Receipts.Add(r);
                }
            }

            foreach (BatchReceipt r in Receipts)
            {
                if (!string.IsNullOrEmpty(r.ReceiptId))
                {
                    r.Invoices.Clear();
                    //
                    using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
                    {
                        FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout);
                        qry.Add("select *                                         ");
                        qry.Add(" from  arpaymentview v                           ");
                        qry.Add(" where v.arid = @receiptid                       ");
                        //qry.Add("order by gl.orderby                            ");
                        qry.AddParameter("@receiptid", r.ReceiptId);
                        FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                        NumberFormatInfo numberFormat = new CultureInfo("en-US", false).NumberFormat;
                        numberFormat = new CultureInfo("en-US", false).NumberFormat;
                        numberFormat.NumberGroupSeparator = "";
                        numberFormat.NumberDecimalSeparator = ".";
                        numberFormat.NumberDecimalDigits = 2;

                        foreach (List<object> row in dt.Rows)
                        {
                            ReceiptInvoice i = new ReceiptInvoice();
                            i.InvoiceId = row[dt.GetColumnNo("invoiceid")].ToString();
                            i.InvoiceNumber = row[dt.GetColumnNo("invoiceno")].ToString();
                            i.InvoiceDate = FwConvert.ToUSShortDate(row[dt.GetColumnNo("invoicedate")].ToString());
                            i.InvoiceDescription = row[dt.GetColumnNo("invoicedesc")].ToString();
                            i.Customer = row[dt.GetColumnNo("arcustomer")].ToString();
                            i.Deal = row[dt.GetColumnNo("ardeal")].ToString();
                            i.Applied = FwConvert.ToDecimal(row[dt.GetColumnNo("amount")].ToString());
                            r.Invoices.Add(i);
                        }
                    }



                    r.GLTransactions.Clear();
                    using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
                    {
                        FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout);
                        qry.Add("select *                                       ");
                        qry.Add(" from  dbo.funcreceiptglexport(@receiptid) gl  ");
                        qry.Add("order by gl.orderby                            ");
                        qry.AddParameter("@receiptid", r.ReceiptId);
                        FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                        NumberFormatInfo numberFormat = new CultureInfo("en-US", false).NumberFormat;
                        numberFormat = new CultureInfo("en-US", false).NumberFormat;
                        numberFormat.NumberGroupSeparator = "";
                        numberFormat.NumberDecimalSeparator = ".";
                        numberFormat.NumberDecimalDigits = 2;


                        foreach (List<object> row in dt.Rows)
                        {
                            GLTransaction t = new GLTransaction();
                            t.AccountId = row[dt.GetColumnNo("glaccountid")].ToString();
                            t.GroupHeading = row[dt.GetColumnNo("groupheading")].ToString();
                            t.AccountNumber = row[dt.GetColumnNo("glno")].ToString();
                            t.AccountDescription = row[dt.GetColumnNo("glacctdesc")].ToString();
                            t.Debit = FwConvert.ToDecimal(row[dt.GetColumnNo("debit")].ToString());
                            t.Credit = FwConvert.ToDecimal(row[dt.GetColumnNo("credit")].ToString());
                            t.Amount = (t.Debit - t.Credit);
                            t.OrderBy = FwConvert.ToInt32(row[dt.GetColumnNo("orderby")].ToString());
                            t.OrderNumber = row[dt.GetColumnNo("orderno")].ToString();
                            t.OrderDescription = row[dt.GetColumnNo("orderdesc")].ToString();
                            t.Currency = row[dt.GetColumnNo("currency")].ToString();
                            t.CurrencyCode = row[dt.GetColumnNo("currencycode")].ToString();
                            t.CurrencySymbol = row[dt.GetColumnNo("currencysymbol")].ToString();
                            t.AmountWithCurrencySymbol = (t.Amount < 0 ? "-" : "") + t.CurrencySymbol + Math.Abs(t.Amount.GetValueOrDefault(0)).ToString("N", numberFormat);
                            t.IsAccountsReceivable = FwConvert.ToBoolean(row[dt.GetColumnNo("isaccountsreceivable")].ToString());
                            r.GLTransactions.Add(t);
                        }
                    }
                }
            }

            loaded = true;
            return loaded;
        }
    }
    //------------------------------------------------------------------------------------ 
}

