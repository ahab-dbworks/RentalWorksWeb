using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.GlManual
{
    [FwSqlTable("glmanualview")]
    public class GlManualLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "glid", modeltype: FwDataTypes.Integer, isPrimaryKey: true)]
        public int? Id { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "internalchar", modeltype: FwDataTypes.Text)]
        public string InternalChar { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "arid", modeltype: FwDataTypes.Text)]
        public string ReceiptId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorinvoiceid", modeltype: FwDataTypes.Text)]
        public string VendorInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentid", modeltype: FwDataTypes.Text)]
        public string PaymentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "gldate", modeltype: FwDataTypes.Date)]
        public string GlDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "debitglaccountid", modeltype: FwDataTypes.Text)]
        public string DebitGlAccountId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "debitglno", modeltype: FwDataTypes.Text)]
        public string DebitGlAccountNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "debitglacctdesc", modeltype: FwDataTypes.Text)]
        public string DebitGlAccountDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditglaccountid", modeltype: FwDataTypes.Text)]
        public string CreditGlAccountId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditglno", modeltype: FwDataTypes.Text)]
        public string CreditGlAccountNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditglacctdesc", modeltype: FwDataTypes.Text)]
        public string CreditGlAccountDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "amount", modeltype: FwDataTypes.Decimal)]
        public decimal? Amount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groupheading", modeltype: FwDataTypes.Text)]
        public string GroupHeading { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manual", modeltype: FwDataTypes.Boolean)]
        public bool? IsManual { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDate("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("InvoiceId", "invoiceid", select, request);
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
