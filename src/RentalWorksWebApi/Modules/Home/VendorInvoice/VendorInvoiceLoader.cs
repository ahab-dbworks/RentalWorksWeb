using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.VendorInvoice
{
    [FwSqlTable("vendorinvoiceview")]
    public class VendorInvoiceLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorno", modeltype: FwDataTypes.Text)]
        public string VendorNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicebatchid", modeltype: FwDataTypes.Text)]
        public string InvoiceBatchId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.Date)]
        public string InputDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputusersid", modeltype: FwDataTypes.Text)]
        public string InputUsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputuser", modeltype: FwDataTypes.Text)]
        public string Inputuser { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstartend", modeltype: FwDataTypes.Text)]
        public string BillingStartEnd { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poorderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podealid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderDealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podealno", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderDealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podeal", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podealnodeal", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderDealNumberDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approveddate", modeltype: FwDataTypes.Date)]
        public string ApprovedDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtoadd1", modeltype: FwDataTypes.Text)]
        public string BillToAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtoadd2", modeltype: FwDataTypes.Text)]
        public string BillToAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtocity", modeltype: FwDataTypes.Text)]
        public string BillToCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtostate", modeltype: FwDataTypes.Text)]
        public string BillToState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtozip", modeltype: FwDataTypes.Text)]
        public string BillToZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billtocountry", modeltype: FwDataTypes.Text)]
        public string BillToCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorphone", modeltype: FwDataTypes.Text)]
        public string VendorPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorfax", modeltype: FwDataTypes.Text)]
        public string VendorFax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceclass", modeltype: FwDataTypes.Boolean)]
        public bool? InvoiceClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "printnotes", modeltype: FwDataTypes.Boolean)]
        public bool? PrintNotes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "payterms", modeltype: FwDataTypes.Boolean)]
        public bool? Payterms { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxid", modeltype: FwDataTypes.Text)]
        public string TaxId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxoptionid", modeltype: FwDataTypes.Text)]
        public string TaxOptionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxitemcode", modeltype: FwDataTypes.Text)]
        public string TaxItemCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentrequestno", modeltype: FwDataTypes.Text)]
        public string PaymentRequestNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentrequeststatus", modeltype: FwDataTypes.Text)]
        public string PaymentRequestStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentrequestrundate", modeltype: FwDataTypes.Date)]
        public string PaymentRequestRunDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sapdocumentno", modeltype: FwDataTypes.Text)]
        public string SapDocumentNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sapdocumentdate", modeltype: FwDataTypes.Date)]
        public string SapDocumentDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sapdocumentamount", modeltype: FwDataTypes.Decimal)]
        public decimal? SapDocumentAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sapvendorinvoicestatus", modeltype: FwDataTypes.Text)]
        public string SapVendorInvoiceStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "checkno", modeltype: FwDataTypes.Text)]
        public string CheckNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billedhiatus", modeltype: FwDataTypes.Boolean)]
        public bool? BilledHiatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetype", modeltype: FwDataTypes.Text)]
        public string InvoiceType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanagerid", modeltype: FwDataTypes.Text)]
        public string ProjectManagerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "i35includepoprefix", modeltype: FwDataTypes.Boolean)]
        public bool? I35IncludePoPrefix { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locdefaultcurrencyid", modeltype: FwDataTypes.Text)]
        public string LocationDefaultCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealbilledextended", modeltype: FwDataTypes.Decimal)]
        public decimal? DealBilledExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDateTime("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
