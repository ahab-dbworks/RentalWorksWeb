using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Modules.Exports.InvoiceBatchExport
{
    public class InvoiceBatchExportRequest  // ideally this class should inherit from a superclass (ie. AppDataExportRequest) where the DataExportFormatId can be declared once there
    {
        //public string DataExportFormatId  // format id needs to be provided in the request from the page
        public string BatchId { get; set; }
    }

    public class InvoiceBatchExportLoader : AppExportLoader  // maybe add a new superclass that all Exports inherit from?
    {
        public string BatchId { get; set; }
        public string BatchNumber { get; set; }
        public DateTime? BatchDateTime { get; set; }
        
        public class BatchInvoice
        {
            public class InvoiceItem
            {
                public string ICode { get; set; }
                public string Description { get; set; }
                public decimal? Quantity { get; set; }
                public decimal? Rate { get; set; }
                public decimal? Extended { get; set; }
            }

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

            public List<InvoiceItem> Items = new List<InvoiceItem>(new InvoiceItem[] { new InvoiceItem() });
        }

        public List<BatchInvoice> Invoices = new List<BatchInvoice>(new BatchInvoice[] { new BatchInvoice() });

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
        public async Task<InvoiceBatchExportLoader> DoLoad<InvoiceBatchExportLoader>(InvoiceBatchExportRequest request)
        {
            InvoiceBatchExportLoader batchLoader;

            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();
                using (FwSqlCommand qry = new FwSqlCommand(conn, "webgetinvoiceexportbatch", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@invoiceid", SqlDbType.Text, ParameterDirection.Input, request.BatchId);
                    AddPropertiesAsQueryColumns(qry);
                    Task<InvoiceBatchExportLoader> taskBatch = qry.QueryToTypedObjectAsync<InvoiceBatchExportLoader>();

                    await Task.WhenAll(new Task[] { taskBatch });

                    batchLoader = taskBatch.Result;

                }
            }

            return batchLoader;
        }
        //------------------------------------------------------------------------------------ 
    }
}
