using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Exports.InvoiceExport
{
    public class ReceiptBatchExportRequest
    {
        public string BatchId { get; set; }
    }

    public class ReceiptBatchExportLoader : AppDataLoadRecord  // maybe add a new superclass that all Exports inherit from?
    {
        public string BatchId { get; set; }
        public string BatchNumber { get; set; }
        public DateTime? BatchDateTime { get; set; }

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

            public List<Invoice> Invoices = new List<Invoice>();
        }

        public List<BatchReceipt> Receipts = new List<BatchReceipt>();

        //protected string recType = "";
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        //public string InvoiceId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
        //public string RowType { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        //public string RecType { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rectypedisplay", modeltype: FwDataTypes.Text)]
        //public string RecTypeDisplay { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        //public string ICode { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        //public string Description { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qty", modeltype: FwDataTypes.Decimal)]
        //public decimal? Quantity { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "extended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        //public decimal? Extended { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Text)]
        //public string OrderBy { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        //public decimal? Rate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "discountpct", modeltype: FwDataTypes.DecimalString2Digits)]
        //public decimal? DiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<ReceiptBatchExportLoader> DoLoad<ReceiptBatchExportLoader>(ReceiptBatchExportRequest request)
        {
            ReceiptBatchExportLoader batchLoader;

            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();
                using (FwSqlCommand qry = new FwSqlCommand(conn, "webgetreceiptexportbatch", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@invoiceid", SqlDbType.Text, ParameterDirection.Input, request.BatchId);
                    AddPropertiesAsQueryColumns(qry);
                    Task<ReceiptBatchExportLoader> taskBatch = qry.QueryToTypedObjectAsync<ReceiptBatchExportLoader>();

                    await Task.WhenAll(new Task[] { taskBatch });

                    batchLoader = taskBatch.Result;

                }
            }

            return batchLoader;
        }
        //------------------------------------------------------------------------------------ 
    }
}
