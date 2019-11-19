using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.HomeControls.InvoiceReceipt
{
    [FwSqlTable("arpaymentview")]
    public class InvoiceReceiptLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "id", modeltype: FwDataTypes.Integer, identity: true, isPrimaryKey: true)]
        public int? InvoiceReceiptId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "arid", modeltype: FwDataTypes.Text)]
        public string ReceiptId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ardate", modeltype: FwDataTypes.Date)]
        public string ReceiptDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text)]
        public string PaymentTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytype", modeltype: FwDataTypes.Text)]
        public string PaymentType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "checkno", modeltype: FwDataTypes.Text)]
        public string CheckNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Boolean)]
        public bool? RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentby", modeltype: FwDataTypes.Text)]
        public string PaymentBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "arcustomerid", modeltype: FwDataTypes.Text)]
        public string ReceiptCustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "arcustomer", modeltype: FwDataTypes.Text)]
        public string ReceiptCustomer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ardealid", modeltype: FwDataTypes.Text)]
        public string ReceiptDealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ardeal", modeltype: FwDataTypes.Text)]
        public string ReceiptDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceno", modeltype: FwDataTypes.Text)]
        public string InvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedate", modeltype: FwDataTypes.Date)]
        public string InvoiceDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedesc", modeltype: FwDataTypes.Text)]
        public string InvoiceDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedealid", modeltype: FwDataTypes.Text)]
        public string InvoiceDealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedeal", modeltype: FwDataTypes.Text)]
        public string InvoiceDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "amount", modeltype: FwDataTypes.Decimal)]
        public decimal? Amount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "appliedbyid", modeltype: FwDataTypes.Text)]
        public string AppliedById { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "appliedby", modeltype: FwDataTypes.Text)]
        public string AppliedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pmtmemo", modeltype: FwDataTypes.Text)]
        public string PaymentMemo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDateTime("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("InvoiceId", "invoiceid", select, request); 
            addFilterToSelect("ReceiptId", "arid", select, request); 
            addFilterToSelect("DealId", "invoicedealid", select, request); 
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
