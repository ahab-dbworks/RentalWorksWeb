using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using System.Threading.Tasks;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Home.Invoice
{
    public class InvoiceLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InvoiceRecord invoice = new InvoiceRecord();
        InvoiceLoader invoiceLoader = new InvoiceLoader();
        InvoiceBrowseLoader invoiceBrowseLoader = new InvoiceBrowseLoader();
        public InvoiceLogic()
        {
            dataRecords.Add(invoice);
            dataLoader = invoiceLoader;
            browseLoader = invoiceBrowseLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InvoiceId { get { return invoice.InvoiceId; } set { invoice.InvoiceId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string InvoiceNumber { get { return invoice.InvoiceNumber; } set { invoice.InvoiceNumber = value; } }
        public string InvoiceDate { get { return invoice.InvoiceDate; } set { invoice.InvoiceDate = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InvoiceDueDate { get; set; }
        public string InvoiceType { get { return invoice.InvoiceType; } set { invoice.InvoiceType = value; } }
        public string BillingStartDate { get { return invoice.BillingStartDate; } set { invoice.BillingStartDate = value; } }
        public string BillingEndDate { get { return invoice.BillingEndDate; } set { invoice.BillingEndDate = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderId { get; set; }
        public string OrderNumber { get { return invoice.OrderNumber; } set { invoice.OrderNumber = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderLocation { get; set; }
        public string InvoiceDescription { get { return invoice.InvoiceDescription; } set { invoice.InvoiceDescription = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CustomerId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Customer { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CustomerTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CustomerType { get; set; }
        public string DealId { get { return invoice.DealId; } set { invoice.DealId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Deal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealTypeId { get; set; }
        public string DepartmentId { get { return invoice.DepartmentId; } set { invoice.DepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PoNumber { get; set; }
        public string WaNo { get { return invoice.WaNo; } set { invoice.WaNo = value; } }
        public string Status { get { return invoice.Status; } set { invoice.Status = value; } }
        public string StatusDate { get { return invoice.StatusDate; } set { invoice.StatusDate = value; } }
        public bool? IsNoCharge { get { return invoice.IsNoCharge; } set { invoice.IsNoCharge = value; } }
        public bool? IsAdjusted { get { return invoice.IsAdjusted; } set { invoice.IsAdjusted = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? IsBilledHiatus { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? EpisodeNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? HasLockedTotal { get; set; }
        public bool? IsAlteredDates { get { return invoice.IsAlteredDates; } set { invoice.IsAlteredDates = value; } }
        public string LocationId { get { return invoice.LocationId; } set { invoice.LocationId = value; } }
        public string InvoiceCreationBatchId { get { return invoice.InvoiceCreationBatchId; } set { invoice.InvoiceCreationBatchId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? InvoiceCreationBatchNumber { get; set; }
        public string InvoiceGroupNumber { get { return invoice.InvoiceGroupNumber; } set { invoice.InvoiceGroupNumber = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string CrfId { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public int? Crfno { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? RentalSale { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? LossAndDamage { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Repair { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InputByUserId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string FlatPoId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderType { get; set; }
        public string RebateCustomerId { get { return invoice.RebateCustomerId; } set { invoice.RebateCustomerId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Orbitsapchgmajor { get { return invoice.Orbitsapchgmajor; } set { invoice.Orbitsapchgmajor = value; } }
        public string Orbitsapchgsub { get { return invoice.Orbitsapchgsub; } set { invoice.Orbitsapchgsub = value; } }
        public string Orbitsapchgdetail { get { return invoice.Orbitsapchgdetail; } set { invoice.Orbitsapchgdetail = value; } }
        public string Orbitsapchgdeal { get { return invoice.Orbitsapchgdeal; } set { invoice.Orbitsapchgdeal = value; } }
        public string Orbitsapchgset { get { return invoice.Orbitsapchgset; } set { invoice.Orbitsapchgset = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? ExcludeFromFlatPo { get; set; }
        public bool? IsSplitRental { get { return invoice.IsSplitRental; } set { invoice.IsSplitRental = value; } }
        public bool? IsRebateRental { get { return invoice.IsRebateRental; } set { invoice.IsRebateRental = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? InvoiceListTotal { get; set; }
        public decimal? InvoiceGrossTotal { get { return invoice.InvoiceGrossTotal; } set { invoice.InvoiceGrossTotal = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? InvoiceDiscountTotal { get; set; }
        public decimal? InvoiceSubTotal { get { return invoice.InvoiceSubTotal; } set { invoice.InvoiceSubTotal = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? InvoiceDaysPerWeekDiscountTotal { get; set; }
        public decimal? InvoiceTax { get { return invoice.InvoiceTax; } set { invoice.InvoiceTax = value; } }
        public decimal? InvoiceTotal { get { return invoice.InvoiceTotal; } set { invoice.InvoiceTotal = value; } }
        public string ReferenceNumber { get { return invoice.ReferenceNumber; } set { invoice.ReferenceNumber = value; } }
        public string AgentId { get { return invoice.AgentId; } set { invoice.AgentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Agent { get; set; }
        public string ProjectManagerId { get { return invoice.ProjectManagerId; } set { invoice.ProjectManagerId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ProjectManager { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillToAddress1 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillToAddress2 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillToCity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillToState { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillToZipCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillToCountry { get; set; }
        public string InvoiceClass { get { return invoice.InvoiceClass; } set { invoice.InvoiceClass = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PrintNotes { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PaymentTerms { get; set; }
        public string TaxId { get { return invoice.TaxId; } set { invoice.TaxId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxOptionId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxItemCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxVendor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxOption { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxCountry { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ChargeBatchId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ChargeBatchNumber { get; set; }
        public bool? QuikPayDiscount { get { return invoice.QuikPayDiscount; } set { invoice.QuikPayDiscount = value; } }
        public decimal? QuikPayRentalTotal { get { return invoice.QuikPayRentalTotal; } set { invoice.QuikPayRentalTotal = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QuikPayTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? ReceivedTotal { get; set; }
        public string RateType { get { return invoice.RateType; } set { invoice.RateType = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? ConsignmentRevenue { get; set; }
        public bool? IsNonBillable { get { return invoice.IsNonBillable; } set { invoice.IsNonBillable = value; } }
        public string CurrencyId { get { return invoice.CurrencyId; } set { invoice.CurrencyId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CurrencyCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocationDefaultCurrencyId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DivisionId { get; set; }
        public string OutsideSalesRepresentativeId { get { return invoice.OutsideSalesRepresentativeId; } set { invoice.OutsideSalesRepresentativeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OutsideSalesRepresentative { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? ExportTaxAsLineItem { get; set; }



        [FwBusinessLogicField(isReadOnly: true)]
        public bool? HasRentalItem { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? HasMeterItem { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? HasSaleItem { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? HasLaborItem { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? HasMiscItem { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? HasFacilityItem { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? HasTransportationItem { get; set; }



        //------------------------------------------------------------------------------------ 
        public async Task<TSpStatusReponse> Void()
        {
            return await invoice.Void();
        }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            if (saveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
            }
            else
            {
                if (original != null)
                {
                    InvoiceLogic lOrig = ((InvoiceLogic)original);

                    if (isValid)
                    {
                        if (lOrig.Status.Equals(RwConstants.INVOICE_STATUS_PROCESSED) || lOrig.Status.Equals(RwConstants.INVOICE_STATUS_CLOSED) || lOrig.Status.Equals(RwConstants.INVOICE_STATUS_VOID))
                        {
                            isValid = false;
                            validateMsg = "Cannot modify a " + lOrig.Status + " " + BusinessLogicModuleName  + ".";
                        }
                    }
                }
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------

    }
}
