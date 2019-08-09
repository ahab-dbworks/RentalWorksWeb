using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;
namespace WebApi.Modules.Home.GLDistribution
{
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
        public override async Task<FwJsonDataTable> BrowseAsync(BrowseRequest request, FwCustomFields customFields = null)
        {
            string invoiceId = GetUniqueIdAsString("InvoiceId", request) ?? "";
            string receiptId = GetUniqueIdAsString("ReceiptId", request) ?? "";
            string vendorInvoiceId = GetUniqueIdAsString("VendorInvoiceId", request) ?? "";

            FwJsonDataTable dt = null;

            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getglforwebgrid ", this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@invoiceid", SqlDbType.NVarChar, ParameterDirection.Input, invoiceId);
                    qry.AddParameter("@arid", SqlDbType.NVarChar, ParameterDirection.Input, receiptId);
                    qry.AddParameter("@vendorinvoiceid", SqlDbType.NVarChar, ParameterDirection.Input, vendorInvoiceId);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }
            return dt;


        }
        //------------------------------------------------------------------------------------

    }
}
