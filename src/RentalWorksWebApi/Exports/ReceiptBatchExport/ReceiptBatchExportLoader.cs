using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
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

            public class Invoice
            {
                public string InvoiceId { get; set; }
                public string InvoiceNumber { get; set; }
                public DateTime? InvoiceDate { get; set; }
                public string InvoiceDescription { get; set; }
                public string Customer { get; set; }
                public string CustomerNumber { get; set; }
                public string DealNumber { get; set; }
                public decimal? InvoiceSubTotal { get; set; }
                public decimal? InvoiceTax { get; set; }
                public decimal? InvoiceTotal { get; set; }
            }

            public List<Invoice> Invoices = new List<Invoice>(new Invoice[] { new Invoice() });
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

            loaded = true;
            return loaded;
        }
    }
    //------------------------------------------------------------------------------------ 
}

