using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;
namespace WebApi.Modules.Home.GLDistribution
{
    [FwSqlTable("dbo.funcvendorinvoiceglweb(@vendorinvoiceid)")]
    public class GLDistributionLoader : AppDataLoadRecord
    {
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
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            string invoiceId = GetUniqueIdAsString("InvoiceId", request) ?? "";
            string receiptId = GetUniqueIdAsString("ReceiptId", request) ?? "";
            string vendorInvoiceId = GetUniqueIdAsString("VendorInvoiceId", request) ?? "";

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
        }
        //------------------------------------------------------------------------------------ 
    }
}
