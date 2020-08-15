using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using System;
using System.Reflection;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.HomeControls.Tax;
using WebApi;

namespace WebApi.Modules.Billing.Invoice
{
    [FwLogic(Id: "MIXP71vrgfdz")]
    public class InvoiceLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InvoiceRecord invoice = new InvoiceRecord();
        TaxRecord tax = new TaxRecord();

        InvoiceLoader invoiceLoader = new InvoiceLoader();
        InvoiceBrowseLoader invoiceBrowseLoader = new InvoiceBrowseLoader();
        public InvoiceLogic()
        {
            dataRecords.Add(invoice);
            dataRecords.Add(tax);

            dataLoader = invoiceLoader;
            browseLoader = invoiceBrowseLoader;

            BeforeSave += OnBeforeSave;
            AfterSave += OnAfterSave;

            invoice.BeforeSave += OnBeforeSaveInvoice;
            invoice.AfterSave += OnAfterSaveInvoice;

            ForceSave = true;

        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "oPBmsLfmVQDA", IsPrimaryKey: true)]
        public string InvoiceId { get { return invoice.InvoiceId; } set { invoice.InvoiceId = value; } }

        [FwLogicProperty(Id: "Eg8VdZEbbo25", IsRecordTitle: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string InvoiceNumber { get { return invoice.InvoiceNumber; } set { invoice.InvoiceNumber = value; } }

        [FwLogicProperty(Id: "muDfynMff7Hy")]
        public string InvoiceDate { get { return invoice.InvoiceDate; } set { invoice.InvoiceDate = value; } }

        [FwLogicProperty(Id: "pyloqM7YJynM", IsReadOnly: true)]
        public string InvoiceDueDate { get; set; }

        [FwLogicProperty(Id: "xSvVrp2IR3db", DisableDirectModify: true)]
        public string InvoiceType { get { return invoice.InvoiceType; } set { invoice.InvoiceType = value; } }

        [FwLogicProperty(Id: "jqgSo9AZFl3s")]
        public string BillingStartDate { get { return invoice.BillingStartDate; } set { invoice.BillingStartDate = value; } }

        [FwLogicProperty(Id: "QDeUxAJnHIsp")]
        public string BillingEndDate { get { return invoice.BillingEndDate; } set { invoice.BillingEndDate = value; } }

        [FwLogicProperty(Id: "r2d50s00tU8m4", IsReadOnly: true)]
        public string UsageStartDate { get; set; }

        [FwLogicProperty(Id: "kSfl2WWGJZsWD", IsReadOnly: true)]
        public string UsageEndDate { get; set; }

        [FwLogicProperty(Id: "qs381b0OIlKQ", IsReadOnly: true)]
        public string OrderId { get; set; }

        [FwLogicProperty(Id: "AP9ydaPNTwYK")]
        public string OrderNumber { get { return invoice.OrderNumber; } set { invoice.OrderNumber = value; } }

        [FwLogicProperty(Id: "dNnsl6xvv9PA", IsReadOnly: true)]
        public string OrderDescription { get; set; }

        [FwLogicProperty(Id: "CRyT47QVHJNV", IsReadOnly: true)]
        public string OrderDate { get; set; }

        [FwLogicProperty(Id: "mJcmx3jBfu70", IsReadOnly: true)]
        public string OrderLocation { get; set; }

        [FwLogicProperty(Id: "cabRRQZhTyk7")]
        public string InvoiceDescription { get { return invoice.InvoiceDescription; } set { invoice.InvoiceDescription = value; } }

        [FwLogicProperty(Id: "7fYCeGvDGDRCS", IsReadOnly: true)]
        public string CreditingInvoiceId { get; set; }
        [FwLogicProperty(Id: "VH9tmyDATY2HP", IsReadOnly: true)]
        public string CreditingInvoiceNumber { get; set; }

        [FwLogicProperty(Id: "HuB25hwx1f59", IsReadOnly: true)]
        public string CustomerId { get; set; }

        [FwLogicProperty(Id: "HuB25hwx1f59", IsReadOnly: true)]
        public string Customer { get; set; }

        [FwLogicProperty(Id: "HuB25hwx1f59", IsReadOnly: true)]
        public string CustomerTypeId { get; set; }

        [FwLogicProperty(Id: "HuB25hwx1f59", IsReadOnly: true)]
        public string CustomerType { get; set; }

        [FwLogicProperty(Id: "GU5MIIzd35ms")]
        public string DealId { get { return invoice.DealId; } set { invoice.DealId = value; } }

        [FwLogicProperty(Id: "GtCFbX8VwcwR", IsReadOnly: true)]
        public string Deal { get; set; }

        [FwLogicProperty(Id: "GtCFbX8VwcwR", IsReadOnly: true)]
        public string DealNumber { get; set; }

        [FwLogicProperty(Id: "GtCFbX8VwcwR", IsReadOnly: true)]
        public string DealTypeId { get; set; }

        [FwLogicProperty(Id: "eL7L3SkraiWs")]
        public string DepartmentId { get { return invoice.DepartmentId; } set { invoice.DepartmentId = value; } }

        [FwLogicProperty(Id: "BJSzUIhOGU8g", IsReadOnly: true)]
        public string Department { get; set; }

        [FwLogicProperty(Id: "cBPz3uC1iBKO", IsReadOnly: true)]
        public string PurchaseOrderNumber { get; set; }

        [FwLogicProperty(Id: "gbjEsq2kr1wh")]
        public string WorkAuthorizationNumber { get { return invoice.WorkAuthorizationNumber; } set { invoice.WorkAuthorizationNumber = value; } }

        [FwLogicProperty(Id: "ru1fRazQa3dV", DisableDirectModify: true)]
        public string Status { get { return invoice.Status; } set { invoice.Status = value; } }

        [FwLogicProperty(Id: "9TmbcT4DPrUQ")]
        public string StatusDate { get { return invoice.StatusDate; } set { invoice.StatusDate = value; } }

        [FwLogicProperty(Id: "i38jKJ7b09W4")]
        public bool? IsNoCharge { get { return invoice.IsNoCharge; } set { invoice.IsNoCharge = value; } }

        [FwLogicProperty(Id: "bGLsWvCXZubg")]
        public bool? IsAdjusted { get { return invoice.IsAdjusted; } set { invoice.IsAdjusted = value; } }

        [FwLogicProperty(Id: "UKCgcI6SiWls", IsReadOnly: true)]
        public bool? IsBilledHiatus { get; set; }

        [FwLogicProperty(Id: "t1C6S1q6N1Ab", IsReadOnly: true)]
        public int? EpisodeNumber { get; set; }

        [FwLogicProperty(Id: "x3yurVlbYVjj", IsReadOnly: true)]
        public bool? HasLockedTotal { get; set; }

        [FwLogicProperty(Id: "F39EfTR3DvBi")]
        public bool? IsAlteredDates { get { return invoice.IsAlteredDates; } set { invoice.IsAlteredDates = value; } }

        [FwLogicProperty(Id: "5uyCk2Eh4fGp", DisableDirectModify: true)]
        public string OfficeLocationId { get { return invoice.OfficeLocationId; } set { invoice.OfficeLocationId = value; } }

        [FwLogicProperty(Id: "TPUmaObMXlUMH", IsReadOnly: true)]
        public string OfficeLocation { get; set; }

        [FwLogicProperty(Id: "aPWbnng5uQaH", DisableDirectAssign: true, DisableDirectModify: true)]
        public string InvoiceCreationBatchId { get { return invoice.InvoiceCreationBatchId; } set { invoice.InvoiceCreationBatchId = value; } }

        [FwLogicProperty(Id: "eRMKEC7W0NQK", IsReadOnly: true)]
        public int? InvoiceCreationBatchNumber { get; set; }

        [FwLogicProperty(Id: "jvaNK3ANoOZL")]
        public string InvoiceGroupNumber { get { return invoice.InvoiceGroupNumber; } set { invoice.InvoiceGroupNumber = value; } }

        //[FwLogicProperty(Id:"6XEQxe6oZmFD", IsReadOnly:true)]
        //public string CrfId { get; set; }

        //[FwLogicProperty(Id:"xMuGtBD6Yqjn", IsReadOnly:true)]
        //public int? Crfno { get; set; }

        [FwLogicProperty(Id: "uT0rueH1NWux", IsReadOnly: true)]
        public bool? RentalSale { get; set; }

        [FwLogicProperty(Id: "gHP8XSc6A4UJ", IsReadOnly: true)]
        public bool? LossAndDamage { get; set; }

        [FwLogicProperty(Id: "Tdr6b6T9HgRF", IsReadOnly: true)]
        public bool? Repair { get; set; }

        [FwLogicProperty(Id: "VdCAabLP2ymk", IsReadOnly: true)]
        public string InputByUserId { get; set; }

        [FwLogicProperty(Id: "CABj1uFI2Cig", IsReadOnly: true)]
        public string FlatPoId { get; set; }

        [FwLogicProperty(Id: "6ax2siVrPUzS", IsReadOnly: true)]
        public string OrderType { get; set; }

        [FwLogicProperty(Id: "PhVvkTmLMq7w")]
        public string RebateCustomerId { get { return invoice.RebateCustomerId; } set { invoice.RebateCustomerId = value; } }

        [FwLogicProperty(Id: "HGROzIjDqyxL", IsReadOnly: true)]
        public string Orbitsapchgmajor { get { return invoice.Orbitsapchgmajor; } set { invoice.Orbitsapchgmajor = value; } }

        [FwLogicProperty(Id: "qhmIdfWPdLn1")]
        public string Orbitsapchgsub { get { return invoice.Orbitsapchgsub; } set { invoice.Orbitsapchgsub = value; } }

        [FwLogicProperty(Id: "LCKvW0kbc9iN")]
        public string Orbitsapchgdetail { get { return invoice.Orbitsapchgdetail; } set { invoice.Orbitsapchgdetail = value; } }

        [FwLogicProperty(Id: "gfb8fC8M08Wg")]
        public string Orbitsapchgdeal { get { return invoice.Orbitsapchgdeal; } set { invoice.Orbitsapchgdeal = value; } }

        [FwLogicProperty(Id: "0tDc8RxUdFS2")]
        public string Orbitsapchgset { get { return invoice.Orbitsapchgset; } set { invoice.Orbitsapchgset = value; } }

        [FwLogicProperty(Id: "klbXixbJTsi8", IsReadOnly: true)]
        public bool? ExcludeFromFlatPo { get; set; }

        [FwLogicProperty(Id: "gFOBHPhPDEAL")]
        public bool? IsSplitRental { get { return invoice.IsSplitRental; } set { invoice.IsSplitRental = value; } }

        [FwLogicProperty(Id: "fK0WeMOwsyzt")]
        public bool? IsRebateRental { get { return invoice.IsRebateRental; } set { invoice.IsRebateRental = value; } }

        [FwLogicProperty(Id: "PxC2tdasLl1N", IsReadOnly: true)]
        public decimal? InvoiceListTotal { get; set; }

        [FwLogicProperty(Id: "Kv6NSLZJgsyo")]
        public decimal? InvoiceGrossTotal { get { return invoice.InvoiceGrossTotal; } set { invoice.InvoiceGrossTotal = value; } }

        [FwLogicProperty(Id: "LKJYNS6kRKsh", IsReadOnly: true)]
        public decimal? InvoiceDiscountTotal { get; set; }

        [FwLogicProperty(Id: "eZ0Q5npyGVQj")]
        public decimal? InvoiceDaysPerWeekDiscountTotal { get; set; }

        [FwLogicProperty(Id: "Ze6wuKQob0z7")]
        public string ReferenceNumber { get { return invoice.ReferenceNumber; } set { invoice.ReferenceNumber = value; } }

        [FwLogicProperty(Id: "aT8g3bEcJU5E")]
        public string AgentId { get { return invoice.AgentId; } set { invoice.AgentId = value; } }

        [FwLogicProperty(Id: "kBsuVk91SWtt", IsReadOnly: true)]
        public string Agent { get; set; }

        [FwLogicProperty(Id: "8IxDwNywbP8m")]
        public string ProjectManagerId { get { return invoice.ProjectManagerId; } set { invoice.ProjectManagerId = value; } }

        [FwLogicProperty(Id: "c4xIVabX4i3E", IsReadOnly: true)]
        public string ProjectManager { get; set; }

        [FwLogicProperty(Id: "tZyOANDHo8Fw", IsReadOnly: true)]
        public string BillToAddress1 { get; set; }

        [FwLogicProperty(Id: "2uZH5Xexi9KV", IsReadOnly: true)]
        public string BillToAddress2 { get; set; }

        [FwLogicProperty(Id: "OEpfI9ylzesb", IsReadOnly: true)]
        public string BillToCity { get; set; }

        [FwLogicProperty(Id: "AtPsKGTs1ivW", IsReadOnly: true)]
        public string BillToState { get; set; }

        [FwLogicProperty(Id: "vQBGjZOT0vJN", IsReadOnly: true)]
        public string BillToZipCode { get; set; }

        [FwLogicProperty(Id: "H38yt0cXj2r9n", IsReadOnly: true)]
        public string BillToCountryId { get; set; }

        [FwLogicProperty(Id: "x0nfCbNXPBcM", IsReadOnly: true)]
        public string BillToCountry { get; set; }

        [FwLogicProperty(Id: "cN4PoCg3Tjbq")]
        public string InvoiceClass { get { return invoice.InvoiceClass; } set { invoice.InvoiceClass = value; } }

        [FwLogicProperty(Id: "0nKdiFtH7Atq", IsReadOnly: true)]
        public string PrintNotes { get; set; }


        [FwLogicProperty(Id: "fMBviWLkZudRV")]
        public string PaymentTermsId { get { return invoice.PaymentTermsId; } set { invoice.PaymentTermsId = value; } }

        [FwLogicProperty(Id: "Hr94ZKAyt5CP", IsReadOnly: true)]
        public string PaymentTerms { get; set; }


        [FwLogicProperty(Id: "RwKCZSEa720Vv")]
        public string PaymentTypeId { get { return invoice.PaymentTypeId; } set { invoice.PaymentTypeId = value; } }

        [FwLogicProperty(Id: "NRs7x9yVZ3qdM", IsReadOnly: true)]
        public string PaymentType { get; set; }


        [FwLogicProperty(Id: "578Sa5Ns8Ute", DisableDirectAssign: true, DisableDirectModify: true)]
        public string TaxId { get { return invoice.TaxId; } set { invoice.TaxId = value; tax.TaxId = value; } }

        [FwLogicProperty(Id: "H0RhmRE6Y7u7", IsReadOnly: true)]
        public string TaxOptionId { get { return tax.TaxOptionId; } set { tax.TaxOptionId = value; } }

        [FwLogicProperty(Id: "H0RhmRE6Y7u7", IsReadOnly: true)]
        public string TaxOption { get; set; }

        [FwLogicProperty(Id: "8eTjH77B3Uko", IsReadOnly: true)]
        public string Tax1Name { get; set; }

        [FwLogicProperty(Id: "MbAzfH623Y9Y", IsReadOnly: true)]
        public string Tax2Name { get; set; }

        [FwLogicProperty(Id: "DHGwxB6DpL9g")]
        public decimal? RentalTaxRate1 { get { return tax.RentalTaxRate1; } set { tax.RentalTaxRate1 = value; } }

        [FwLogicProperty(Id: "9oGIwHGeUgqi")]
        public decimal? SalesTaxRate1 { get { return tax.SalesTaxRate1; } set { tax.SalesTaxRate1 = value; } }

        [FwLogicProperty(Id: "wvSyYyw8kF6G")]
        public decimal? LaborTaxRate1 { get { return tax.LaborTaxRate1; } set { tax.LaborTaxRate1 = value; } }

        [FwLogicProperty(Id: "m6zjObFbQKme")]
        public decimal? RentalTaxRate2 { get { return tax.RentalTaxRate2; } set { tax.RentalTaxRate2 = value; } }

        [FwLogicProperty(Id: "l9GX2xdTsLci")]
        public decimal? SalesTaxRate2 { get { return tax.SalesTaxRate2; } set { tax.SalesTaxRate2 = value; } }

        [FwLogicProperty(Id: "vhWuApLqB1CI")]
        public decimal? LaborTaxRate2 { get { return tax.LaborTaxRate2; } set { tax.LaborTaxRate2 = value; } }

        [FwLogicProperty(Id: "ctLo1B1EWVjy", IsReadOnly: true)]
        public string TaxItemCode { get; set; }

        [FwLogicProperty(Id: "QbXgDeRDw2GH", IsReadOnly: true)]
        public string TaxVendor { get; set; }


        [FwLogicProperty(Id: "gvf0dR5AH7Pq", IsReadOnly: true)]
        public string TaxCountry { get; set; }

        [FwLogicProperty(Id: "bTah1r8n4OuH", IsReadOnly: true)]
        public string ChargeBatchId { get; set; }

        [FwLogicProperty(Id: "t4QXvKBgq89m", IsReadOnly: true)]
        public string ChargeBatchNumber { get; set; }

        [FwLogicProperty(Id: "55310YPPvz33")]
        public bool? QuikPayDiscount { get { return invoice.QuikPayDiscount; } set { invoice.QuikPayDiscount = value; } }

        [FwLogicProperty(Id: "4ga8qCHEMpxr")]
        public decimal? QuikPayRentalTotal { get { return invoice.QuikPayRentalTotal; } set { invoice.QuikPayRentalTotal = value; } }

        [FwLogicProperty(Id: "dRP18MAYRgvC", IsReadOnly: true)]
        public decimal? QuikPayTotal { get; set; }

        [FwLogicProperty(Id: "BR8x92QjOdSu", IsReadOnly: true)]
        public decimal? ReceivedTotal { get; set; }

        [FwLogicProperty(Id: "ZL3CQzMcFzAy")]
        public string RateType { get { return invoice.RateType; } set { invoice.RateType = value; } }

        [FwLogicProperty(Id: "e53oRLjbH0kI", IsReadOnly: true)]
        public decimal? ConsignmentRevenue { get; set; }

        [FwLogicProperty(Id: "4RgJud7SP30a")]
        public bool? IsNonBillable { get { return invoice.IsNonBillable; } set { invoice.IsNonBillable = value; } }

        [FwLogicProperty(Id: "9OnrVe14Xe2p")]
        public string CurrencyId { get { return invoice.CurrencyId; } set { invoice.CurrencyId = value; } }

        [FwLogicProperty(Id: "sCSkPb8mzrS6", IsReadOnly: true)]
        public string CurrencyCode { get; set; }

        [FwLogicProperty(Id: "FMm5KqUxeXVSq", IsReadOnly: true)]
        public string CurrencySymbol { get; set; }

        [FwLogicProperty(Id: "kAQAfXxTyZSQ", IsReadOnly: true)]
        public string OfficeLocationDefaultCurrencyId { get; set; }

        [FwLogicProperty(Id: "3shH1jnmPobf", IsReadOnly: true)]
        public string DivisionId { get; set; }

        [FwLogicProperty(Id: "pQSPf1IVWSUE")]
        public string OutsideSalesRepresentativeId { get { return invoice.OutsideSalesRepresentativeId; } set { invoice.OutsideSalesRepresentativeId = value; } }

        [FwLogicProperty(Id: "KsHaI2nIYPiW", IsReadOnly: true)]
        public string OutsideSalesRepresentative { get; set; }

        [FwLogicProperty(Id: "4up8AiiVxTmC", IsReadOnly: true)]
        public bool? ExportTaxAsLineItem { get; set; }


        [FwLogicProperty(Id: "lcK9rFRAr06av", IsReadOnly: true)]
        public string InvoiceNumberColor { get; set; }
        [FwLogicProperty(Id: "6pRwHtKT4YjHn", IsReadOnly: true)]
        public string StatusColor { get; set; }
        [FwLogicProperty(Id: "fqgQVrX67DPq0", IsReadOnly: true)]
        public string OrderNumberColor { get; set; }
        [FwLogicProperty(Id: "xAcPTrf7JAT9f", IsReadOnly: true)]
        public string PurchaseOrderNumberColor { get; set; }
        [FwLogicProperty(Id: "3DqtKdc6BpLnG", IsReadOnly: true)]
        public string DealColor { get; set; }
        [FwLogicProperty(Id: "PoaTqOQketXuF", IsReadOnly: true)]
        public string BillingStartDateColor { get; set; }
        [FwLogicProperty(Id: "JnKIjs2ZgscqD", IsReadOnly: true)]
        public string InvoiceTotalColor { get; set; }
        [FwLogicProperty(Id: "ozQ6H0PzmQsgx", IsReadOnly: true)]
        public string DescriptionColor { get; set; }


        [FwLogicProperty(Id: "I9x7KeH1tdg0")]
        public bool? HasRentalItem { get; set; }

        [FwLogicProperty(Id: "2s2ztIgPdMGc")]
        public bool? HasMeterItem { get; set; }

        [FwLogicProperty(Id: "xA4EgNsUGEd6")]
        public bool? HasSalesItem { get; set; }

        [FwLogicProperty(Id: "C3A9FKk5dbhi")]
        public bool? HasLaborItem { get; set; }

        [FwLogicProperty(Id: "F4nrbSw599zf")]
        public bool? HasMiscellaneousItem { get; set; }

        [FwLogicProperty(Id: "pBK9fqYEPiDN")]
        public bool? HasFacilityItem { get; set; }

        [FwLogicProperty(Id: "nfop0YO1T9vk")]
        public bool? HasTransportationItem { get; set; }

        [FwLogicProperty(Id: "uT0rueH1NWux", IsReadOnly: true)]
        public bool? HasRentalSaleItem { get; set; }


        [FwLogicProperty(Id: "Q0zV0j5lCSBHd", IsReadOnly: true)]
        public bool? HasLossAndDamageItem { get; set; }

        

        [FwLogicProperty(Id: "4NGkESxWvfM5c")]
        public decimal? RentalTotal { get { return invoice.RentalTotal; } set { invoice.RentalTotal = value; } }
        [FwLogicProperty(Id: "yoxCD2WtTm269")]
        public decimal? SalesTotal { get { return invoice.SalesTotal; } set { invoice.SalesTotal = value; } }
        [FwLogicProperty(Id: "pHzuPgIbLokDW")]
        public decimal? FacilitiesTotal { get { return invoice.FacilitiesTotal; } set { invoice.FacilitiesTotal = value; } }
        [FwLogicProperty(Id: "oEzRpXFLt7GJr")]
        public decimal? MiscellaneousTotal { get { return invoice.MiscellaneousTotal; } set { invoice.MiscellaneousTotal = value; } }
        [FwLogicProperty(Id: "p7qgrNPbaiVbb")]
        public decimal? LaborTotal { get { return invoice.LaborTotal; } set { invoice.LaborTotal = value; } }
        [FwLogicProperty(Id: "drfRfBf491XQL")]
        public decimal? PartsTotal { get { return invoice.PartsTotal; } set { invoice.PartsTotal = value; } }
        [FwLogicProperty(Id: "TZ6bSob77cN2X")]
        public decimal? AssetSaleTotal { get { return invoice.AssetSaleTotal; } set { invoice.AssetSaleTotal = value; } }


        [FwLogicProperty(Id: "TQdn8N1xdrKn")]
        public decimal? InvoiceSubTotal { get { return invoice.InvoiceSubTotal; } set { invoice.InvoiceSubTotal = value; } }


        [FwLogicProperty(Id: "rvnnlVfaPZ6qa")]
        public decimal? InvoiceTax1 { get { return invoice.InvoiceTax1; } set { invoice.InvoiceTax1 = value; } }

        [FwLogicProperty(Id: "1dWAWCcP18XgS")]
        public decimal? InvoiceTax2 { get { return invoice.InvoiceTax2; } set { invoice.InvoiceTax2 = value; } }

        [FwLogicProperty(Id: "wga1bXifBm3L")]
        public decimal? InvoiceTax { get { return invoice.InvoiceTax; } set { invoice.InvoiceTax = value; } }

        [FwLogicProperty(Id: "M5umZVL08RbV")]
        public decimal? InvoiceTotal { get { return invoice.InvoiceTotal; } set { invoice.InvoiceTotal = value; } }


        [FwLogicProperty(Id: "C5CEuZvg2J4u0", IsReadOnly: true)]
        public bool? IsStandAloneInvoice { get; set; }



        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            if (saveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
            }
            else
            {
                InvoiceLogic lOrig = null;
                if (original != null)
                {
                    lOrig = ((InvoiceLogic)original);
                }

                // no changes allowed at all if processed, closed, or void
                if (isValid)
                {
                    string origStatus = lOrig.Status ?? Status ?? "";
                    if (lOrig.Status.Equals(RwConstants.INVOICE_STATUS_PROCESSED) || lOrig.Status.Equals(RwConstants.INVOICE_STATUS_CLOSED) || lOrig.Status.Equals(RwConstants.INVOICE_STATUS_VOID))
                    {
                        isValid = false;
                        validateMsg = "Cannot modify a " + lOrig.Status + " " + BusinessLogicModuleName + ".";
                    }
                }
            }

            if (isValid)
            {
                PropertyInfo property = typeof(InvoiceLogic).GetProperty(nameof(InvoiceLogic.InvoiceType));
                string[] acceptableValues = { RwConstants.INVOICE_TYPE_BILLING, RwConstants.INVOICE_TYPE_CREDIT, RwConstants.INVOICE_TYPE_ESTIMATE, RwConstants.INVOICE_TYPE_WORKSHEET, RwConstants.INVOICE_TYPE_PREVIEW };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------

        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode == TDataRecordSaveMode.smInsert)
            {
                Status = RwConstants.INVOICE_STATUS_NEW;
                InvoiceType = RwConstants.INVOICE_TYPE_BILLING;
                StatusDate = FwConvert.ToString(DateTime.Today);
                InputByUserId = UserSession.UsersId;
                IsStandAloneInvoice = true;  // invoice created from "New" option
            }
            else //if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                if (e.Original != null)
                {
                    InvoiceLogic orig = ((InvoiceLogic)e.Original);
                    TaxId = orig.TaxId;
                }
            }

        }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSaveInvoice(object sender, BeforeSaveDataRecordEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                bool x = invoice.SetNumber(e.SqlConnection).Result;
                if ((TaxOptionId == null) || (TaxOptionId.Equals(string.Empty)))
                {
                    TaxOptionId = AppFunc.GetLocationAsync(AppConfig, UserSession, OfficeLocationId, "taxoptionid", e.SqlConnection).Result;
                }
            }
        }
        //------------------------------------------------------------------------------------
        public virtual void OnAfterSaveInvoice(object sender, AfterSaveDataRecordEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate)
            {
                if ((TaxOptionId != null) && (!TaxOptionId.Equals(string.Empty)))
                {
                    if (e.Original != null)
                    {
                        TaxId = ((InvoiceRecord)e.Original).TaxId;
                    }

                    if ((TaxId != null) && (!TaxId.Equals(string.Empty)))
                    {
                        bool b = AppFunc.UpdateTaxFromTaxOptionASync(this.AppConfig, this.UserSession, TaxOptionId, TaxId, e.SqlConnection).Result;
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            if (e.SaveMode == TDataRecordSaveMode.smInsert)
            {
                // this is a new Invoice.  TaxId was not known at time of insert.  Need to re-update the data with the known ID
                invoice.TaxId = tax.TaxId;
                int i = invoice.SaveAsync(null).Result;
            }


            //after save - do work in the database
            {
                TSpStatusResponse r = InvoiceFunc.AfterSaveInvoice(AppConfig, UserSession, InvoiceId, e.SqlConnection).Result;
            }

        }
        //------------------------------------------------------------------------------------ 

        public async Task<TSpStatusResponse> Void()
        {
            return await invoice.Void();
        }
        //------------------------------------------------------------------------------------ 

        public async Task<CreditInvoiceReponse> CreditInvoice(CreditInvoiceRequest request)
        {
            return await invoice.CreditInvoice(request);
        }
        //------------------------------------------------------------------------------------ 
        public async Task<ToggleInvoiceApprovedResponse> Approve()
        {
            return await invoice.Approve();
        }
        //------------------------------------------------------------------------------------    
        public async Task<ToggleInvoiceApprovedResponse> Unapprove()
        {
            return await invoice.Unapprove();
        }
        //------------------------------------------------------------------------------------    

    }
}
