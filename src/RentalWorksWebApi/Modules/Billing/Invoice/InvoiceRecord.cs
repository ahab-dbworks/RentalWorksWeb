using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Logic;
using WebApi;

namespace WebApi.Modules.Billing.Invoice
{
    [FwSqlTable("invoice")]
    public class InvoiceRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string InvoiceId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime", required: true)]
        public string InvoiceDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstart", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicedesc", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50, required: true)]
        public string InvoiceDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingend", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string BillingEndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime", required: true)]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytermsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PaymentTermsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PaymentTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicebatchid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InvoiceCreationBatchId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approverid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ApproverId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approveddate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string Approveddate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10, required: true)]
        public string InvoiceType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nocharge", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsNoCharge { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputby", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string Inputby { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditinginvoiceid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CreditinginvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjusted", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsAdjusted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string Inputdate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "altereddates", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsAlteredDates { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modby", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string Modby { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "moddate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string Moddate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RateType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "printlaborcomponent", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Printlaborcomponent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        public decimal? InvoiceTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborcomponentpct", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? Laborcomponentpct { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "worksheetid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string WorksheetId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ismisc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsStandAloneInvoice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string InvoiceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceclass", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string InvoiceClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicesubtotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        public decimal? InvoiceSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetax1", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? InvoiceTax1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetax2", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? InvoiceTax2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicetax", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        public decimal? InvoiceTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "summaryinvoicegroup", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? Summaryinvoicegroup { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 15, required: true)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjustcommission", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Adjustcommission { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "commissionbasedon", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string Commissionbasedon { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "commissionincludesvendoritems", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Commissionincludesvendoritems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "commissionrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        public decimal? Commissionrate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpaydiscount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? QuikPayDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpaydiscountdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string Quikpaydiscountdate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpayrentaldiscountamt", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        public decimal? Quikpayrentaldiscountamt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpayrentaldiscountpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 16, scale: 10)]
        public decimal? Quikpayrentaldiscountpct { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpayrentaldiscounttype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string Quikpayrentaldiscounttype { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpayrentaltotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        public decimal? QuikPayRentalTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesrepresentativeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OutsideSalesRepresentativeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "excludeflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Excludeflg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exportdetail", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Exportdetail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invgroupno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string InvoiceGroupNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newgroupflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Newgroupflg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapaccountno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string Orbitsapaccountno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgdeal", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6)]
        public string Orbitsapchgdeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgdetail", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
        public string Orbitsapchgdetail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgmajor", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
        public string Orbitsapchgmajor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgset", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
        public string Orbitsapchgset { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapchgsub", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 2)]
        public string Orbitsapchgsub { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapcostobject", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 24)]
        public string Orbitsapcostobject { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapcreditchgdeal", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6)]
        public string Orbitsapcreditchgdeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapcreditchgdetail", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
        public string Orbitsapcreditchgdetail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapcreditchgmajor", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
        public string Orbitsapcreditchgmajor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapcreditchgset", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
        public string Orbitsapcreditchgset { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsapcreditchgsub", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 2)]
        public string Orbitsapcreditchgsub { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orbitsaptype", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Orbitsaptype { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedrebaterate", modeltype: FwDataTypes.Integer, sqltype: "smallint")]
        public int? Ownedrebaterate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedsplitrate", modeltype: FwDataTypes.Integer, sqltype: "smallint")]
        public int? Ownedsplitrate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rebatecustomerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RebateCustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rebaterentalflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsRebateRental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "resendflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Resendflg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "splitrentalflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsSplitRental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorrebaterate", modeltype: FwDataTypes.Integer, sqltype: "smallint")]
        public int? Vendorrebaterate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorsplitrate", modeltype: FwDataTypes.Integer, sqltype: "smallint")]
        public int? Vendorsplitrate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "wano", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string WorkAuthorizationNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicegrosstotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 19, scale: 2)]
        public decimal? InvoiceGrossTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaltotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 11, scale: 2)]
        public decimal? RentalTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "splitrentaltaxflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Splitrentaltaxflg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "refno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string ReferenceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "voidreason", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Voidreason { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "voidusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string VoidusersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "voiddate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string Voiddate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditmethod", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string Creditmethod { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lockeddiscountpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? Lockeddiscountpct { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string TaxId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjustconsignmentfee", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Adjustconsignmentfee { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "termsconditionsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string TermsconditionsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "applanguageid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ApplanguageId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjustcost", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Adjustcost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "archived", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Archived { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "metertotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? MeterTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salestotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? SalesTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacetotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? FacilitiesTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misctotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? MiscellaneousTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labortotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? LaborTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partstotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? PartsTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assettotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? AssetSaleTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalbeforeoverride", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? Totalbeforeoverride { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nonbillable", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsNonBillable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "commissioncurrencyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CommissioncurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanagerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ProjectManagerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<bool> SetNumber(FwSqlConnection conn)
        {
            InvoiceNumber = await AppFunc.GetNextModuleCounterAsync(AppConfig, UserSession, RwConstants.MODULE_INVOICE, OfficeLocationId, conn);

            return true;
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task<TSpStatusResponse> Void()
        {
            return await InvoiceFunc.VoidInvoice(AppConfig, UserSession, InvoiceId);
        }
        //-------------------------------------------------------------------------------------------------------
        public async Task<CreditInvoiceReponse> CreditInvoice(CreditInvoiceRequest request)
        {
            return await InvoiceFunc.CreateInvoiceCredit(AppConfig, UserSession, request);
        }
        //------------------------------------------------------------------------------------ 
        public async Task<ToggleInvoiceApprovedResponse> Approve()
        {
            ToggleInvoiceApprovedResponse response = new ToggleInvoiceApprovedResponse();
            if (Status.Equals(RwConstants.INVOICE_STATUS_NEW))
            {
                response = await InvoiceFunc.ToggleInvoiceApproved(AppConfig, UserSession, InvoiceId);
            }
            else
            {
                response.msg = "Cannot approve because status is " + Status + ".";
                response.success = false;
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
        public async Task<ToggleInvoiceApprovedResponse> Unapprove()
        {
            ToggleInvoiceApprovedResponse response = new ToggleInvoiceApprovedResponse();
            if (Status.Equals(RwConstants.INVOICE_STATUS_APPROVED))
            {
                response = await InvoiceFunc.ToggleInvoiceApproved(AppConfig, UserSession, InvoiceId);
            }
            else
            {
                response.msg = "Cannot unapprove because status is " + Status + ".";
                response.success = false;
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------    

    }
}
