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
            public string CheckNumber { get; set; }
            public string PaymentType { get; set; }
            public decimal? Amount { get; set; }

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
                    r.CheckNumber = row[dt.GetColumnNo("arid")].ToString();
                    r.PaymentType = row[dt.GetColumnNo("paytype")].ToString();
                    r.Amount = FwConvert.ToDecimal(row[dt.GetColumnNo("amount")].ToString());
                    Receipts.Add(r);
                }
            }

            loaded = true;
            return loaded;
        }
    }
    //------------------------------------------------------------------------------------ 
}

