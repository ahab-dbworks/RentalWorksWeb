using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Modules.Exports.InvoiceBatchExport
{
    public class InvoiceBatchExportRequest: AppExportRequest
    {
        public string BatchId { get; set; }
    }

    public class InvoiceBatchExportResponse: AppExportResponse
    {
    }

    public class InvoiceBatchExportLoader : AppExportLoader 
    {
        public class InvoiceItem
        {
            public string ICode { get; set; }
            public string Description { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? Rate { get; set; }
            public decimal? Extended { get; set; }
        }

        public class BatchInvoice
        {
            public string InvoiceId { get; set; }
            public string InvoiceNumber { get; set; }
            public DateTime? InvoiceDate { get; set; }
            public string InvoiceDescription { get; set; }
            public string Customer { get; set; }
            //public string CustomerNumber { get; set; }
            public string Deal { get; set; }
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
                FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select icb.invoiceid, i.invoiceno, i.invoicedate, i.invoicedesc,        ");
                qry.Add("       i.customer, i.deal, i.dealno, i.invoicesubtotal, i.invoicetax,   ");
                qry.Add("       i.invoicetotal                                                   ");
                qry.Add(" from  invoicechgbatch icb                                              ");
                qry.Add("              join invoiceview i on (icb.invoiceid = i.invoiceid)       ");
                qry.Add(" where icb.chgbatchid = @chgbatchid                                     ");
                qry.Add(" and   i.nocharge     <> 'T'                                            ");
                qry.Add("order by i.invoiceid                                                    ");
                qry.AddParameter("@chgbatchid", request.BatchId);
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                foreach (List<object> row in dt.Rows)
                {
                    BatchInvoice i = new BatchInvoice();
                    i.InvoiceId = row[dt.GetColumnNo("invoiceid")].ToString();
                    i.InvoiceNumber = row[dt.GetColumnNo("invoiceno")].ToString();
                    i.InvoiceDate = FwConvert.ToDateTime(row[dt.GetColumnNo("invoiceno")].ToString());
                    i.InvoiceDescription = row[dt.GetColumnNo("invoicedesc")].ToString();
                    i.Customer = row[dt.GetColumnNo("customer")].ToString();
                    i.Deal = row[dt.GetColumnNo("deal")].ToString();
                    i.DealNumber = row[dt.GetColumnNo("dealno")].ToString();
                    i.InvoiceSubTotal = FwConvert.ToDecimal(row[dt.GetColumnNo("invoicesubtotal")].ToString());
                    i.InvoiceTax = FwConvert.ToDecimal(row[dt.GetColumnNo("invoicetax")].ToString());
                    i.InvoiceTotal = FwConvert.ToDecimal(row[dt.GetColumnNo("invoicetotal")].ToString());
                    Invoices.Add(i);
                }
            }

            foreach (BatchInvoice i in Invoices) 
            {
                if (!string.IsNullOrEmpty(i.InvoiceId))
                {
                    using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
                    {
                        FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout);
                        qry.Add("select ii.masterno, ii.description, ii.qty, ii.rate, ii.linetotal       ");
                        qry.Add(" from  invoiceitemviewweb ii                                            ");
                        qry.Add(" where ii.invoiceid = @invoiceid                                        ");
                        qry.Add("order by ii.itemorder                                                   ");
                        qry.AddParameter("@invoiceid", i.InvoiceId);
                        FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                        foreach (List<object> row in dt.Rows)
                        {
                            InvoiceItem ii = new InvoiceItem();
                            ii.ICode = row[dt.GetColumnNo("masterno")].ToString();
                            ii.Description = row[dt.GetColumnNo("description")].ToString();
                            ii.Quantity = FwConvert.ToDecimal(row[dt.GetColumnNo("qty")].ToString());
                            ii.Rate = FwConvert.ToDecimal(row[dt.GetColumnNo("rate")].ToString());
                            ii.Extended = FwConvert.ToDecimal(row[dt.GetColumnNo("linetotal")].ToString());
                            i.Items.Add(ii);
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
