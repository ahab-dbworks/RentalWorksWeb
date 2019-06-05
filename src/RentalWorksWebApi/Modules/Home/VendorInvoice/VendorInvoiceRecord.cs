using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Threading.Tasks;
using WebApi.Data;
namespace WebApi.Modules.Home.VendorInvoice
{
    [FwSqlTable("vendorinvoice")]
    public class VendorInvoiceRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorinvoiceid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string VendorInvoiceId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InputUsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ModUsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 15, required: true)]
        public string InvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime", required: true)]
        public string InvoiceDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string InputDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "moddate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string ModDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approvedusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ApprovedUsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "credit", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        public decimal? Credit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approveddate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string ApprovedDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discounttype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 15)]
        public string DiscountType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorinvoicenumber", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? VendorInvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? InvoiceTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingend", modeltype: FwDataTypes.Date, sqltype: "smalldatetime", required: true)]
        public string BillingEndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstart", modeltype: FwDataTypes.Date, sqltype: "smalldatetime", required: true)]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountamt", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        public decimal? DiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 16, scale: 9)]
        public decimal? DiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetax", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? InvoiceTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string TaxId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytermsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PaymentTermsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "duedate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string DueDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "changedfrompo", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ChangedFromPo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditingvendorinvoiceid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CreditingVendorInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditmethod", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string CreditMethod { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string InvoiceType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicebatchid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InvoiceBatchId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "payeeaddressid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PayeeAddressId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapaccountno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string OrbitSapAccountNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgdeal", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 6)]
        public string OrbitSapChgDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgdetail", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 3)]
        public string OrbitSapChgMajor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgmajor", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 3)]
        public string Orbitsapchgmajor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgset", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 3)]
        public string OrbitSapChgSet { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgsub", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 2)]
        public string OrbitSapChgSub { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapcostobject", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 24)]
        public string OrbitSapCostObject { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsaptype", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? OrbitSapType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "wano", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)]
        public string WorkAuthorizationNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentrequestno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string PaymentRequestNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentrequeststatus", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string PaymentRequestStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentrequestrundate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string PaymentRequestRunDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sapdocumentno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string SapDocumentNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sapdocumentdate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string SapDocumentDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sapdocumentamount", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? SapDocumentAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deptexportcode", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string DepartmentExportCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billedhiatus", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? BilledHiatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "i35includepoprefix", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? I35IncludePoPrefix { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "i035summarizesr", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? I035SummarizesR { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poorderno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podealid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PurchaseOrderDealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podealno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string PurchaseOrderDealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podeal", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string PurchaseOrderDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<ToggleVendorInvoiceApprovedReponse> ToggleApproved()
        {
            ToggleVendorInvoiceApprovedReponse response = await VendorInvoiceFunc.ToggleVendorInvoiceApproved(AppConfig, UserSession, VendorInvoiceId);
            return response;
        }
        //-------------------------------------------------------------------------------------------------------    

    }
}
