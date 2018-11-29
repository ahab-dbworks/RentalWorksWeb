using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using System;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Home.VendorInvoice
{
    [FwLogic(Id: "pfsVSz8PrlUI")]
    public class VendorInvoiceLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        VendorInvoiceRecord vendorInvoice = new VendorInvoiceRecord();
        VendorInvoiceLoader vendorInvoiceLoader = new VendorInvoiceLoader();
        VendorInvoiceBrowseLoader vendorInvoiceBrowseLoader = new VendorInvoiceBrowseLoader();
        public VendorInvoiceLogic()
        {
            dataRecords.Add(vendorInvoice);
            dataLoader = vendorInvoiceLoader;
            browseLoader = vendorInvoiceBrowseLoader;
            BeforeSave += OnBeforeSave;
            AfterSave += OnAfterSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "S2jBvP6ya0tp", IsPrimaryKey: true)]
        public string VendorInvoiceId { get { return vendorInvoice.VendorInvoiceId; } set { vendorInvoice.VendorInvoiceId = value; } }

        [FwLogicProperty(Id: "56AMOQD5lQf8")]
        public string PurchaseOrderId { get { return vendorInvoice.PurchaseOrderId; } set { vendorInvoice.PurchaseOrderId = value; } }

        [FwLogicProperty(Id: "BWgU1hFaxksh", IsReadOnly: true)]
        public string PurchaseOrderNumber { get; set; }

        [FwLogicProperty(Id: "cqBjiMNbok04", IsReadOnly: true)]
        public string RequistionNumber { get; set; }

        [FwLogicProperty(Id: "DSdY8dEIISL4", IsReadOnly: true)]
        public string Vendor { get; set; }

        [FwLogicProperty(Id: "5OjlJUmk2rju", IsReadOnly: true)]
        public string VendorNumber { get; set; }

        [FwLogicProperty(Id: "TrfsW9WTmwzN")]
        public string VendorId { get { return vendorInvoice.VendorId; } set { vendorInvoice.VendorId = value; } }

        [FwLogicProperty(Id: "TNEZCloHOwi4")]
        public string InvoiceBatchId { get { return vendorInvoice.InvoiceBatchId; } set { vendorInvoice.InvoiceBatchId = value; } }

        [FwLogicProperty(Id: "TAsEhqdwaEos", IsRecordTitle: true)]
        public string InvoiceNumber { get { return vendorInvoice.InvoiceNumber; } set { vendorInvoice.InvoiceNumber = value; } }

        [FwLogicProperty(Id: "UtYo18YqYzpw")]
        public string InvoiceDate { get { return vendorInvoice.InvoiceDate; } set { vendorInvoice.InvoiceDate = value; } }

        [FwLogicProperty(Id: "EEu0kqcEV1B6", IsReadOnly: true)]
        public string InvoiceDueDate { get; set; }

        [FwLogicProperty(Id: "PQbf0GLvV6PV")]
        public string BillingStartDate { get { return vendorInvoice.BillingStartDate; } set { vendorInvoice.BillingStartDate = value; } }

        [FwLogicProperty(Id: "2Y7as3SDnyBG")]
        public string BillingEndDate { get { return vendorInvoice.BillingEndDate; } set { vendorInvoice.BillingEndDate = value; } }

        [FwLogicProperty(Id: "I4nqAJPzhgDV", IsReadOnly: true)]
        public string BillingStartAndEndDates { get; set; }

        [FwLogicProperty(Id: "3Z558g3cC1Tl")]
        public string Status { get { return vendorInvoice.Status; } set { vendorInvoice.Status = value; } }

        [FwLogicProperty(Id: "hS6DjWdIrRdg")]
        public string StatusDate { get { return vendorInvoice.StatusDate; } set { vendorInvoice.StatusDate = value; } }

        [FwLogicProperty(Id: "2gIjGGRWiyRU", IsReadOnly: true)]
        public string OrderDescription { get; set; }

        [FwLogicProperty(Id: "WiEZTOSP16lW", IsReadOnly: true)]
        public string DepartmentId { get; set; }

        [FwLogicProperty(Id: "1qg4eux9mj7I", IsReadOnly: true)]
        public string Department { get; set; }

        [FwLogicProperty(Id: "XUT1BHUBqYoK")]
        public string OrderNumber { get { return vendorInvoice.OrderNumber; } set { vendorInvoice.OrderNumber = value; } }

        [FwLogicProperty(Id: "lHvxzPFzwB7r")]
        public string PurchaseOrderDealId { get { return vendorInvoice.PurchaseOrderDealId; } set { vendorInvoice.PurchaseOrderDealId = value; } }

        [FwLogicProperty(Id: "GTVHuLXK8oY9")]
        public string PurchaseOrderDealNumber { get { return vendorInvoice.PurchaseOrderDealNumber; } set { vendorInvoice.PurchaseOrderDealNumber = value; } }

        [FwLogicProperty(Id: "bMw7tvr1WUxa")]
        public string PurchaseOrderDeal { get { return vendorInvoice.PurchaseOrderDeal; } set { vendorInvoice.PurchaseOrderDeal = value; } }

        [FwLogicProperty(Id: "igqkGeIV82n8", IsReadOnly: true)]
        public string PurchaseOrderDealNumberDeal { get; set; }

        [FwLogicProperty(Id: "64ZW2YPy5Gv", IsReadOnly: true)]
        public string PurchaseOrderDate { get; set; }

        [FwLogicProperty(Id: "YmeGNtMeXXF", IsReadOnly: true)]
        public string PurchaseOrderEstimatedStopDate { get; set; }

        [FwLogicProperty(Id: "pSNJePtSl5S", IsReadOnly: true)]
        public string PurchaseOrderEstimatedStartDate { get; set; }

        [FwLogicProperty(Id: "sawG1EfUbCz", IsReadOnly: true)]
        public string PurchaseOrderBillingCycleId { get; set; }

        [FwLogicProperty(Id: "WVL9pUKdqre", IsReadOnly: true)]
        public string PurchaseOrderBillingCycle { get; set; }

        [FwLogicProperty(Id: "FDC3i66l8C4", IsReadOnly: true)]
        public string PurchaseOrderPaymentTermsId { get; set; }

        [FwLogicProperty(Id: "Oa4U4uhNvtb", IsReadOnly: true)]
        public string PurchaseOrderPaymentTerms { get; set; }

        [FwLogicProperty(Id: "r3ZPOUz3dSaZ")]
        public string ApprovedDate { get { return vendorInvoice.ApprovedDate; } set { vendorInvoice.ApprovedDate = value; } }

        [FwLogicProperty(Id: "t9PRHkZMhPw9")]
        public string LocationId { get { return vendorInvoice.LocationId; } set { vendorInvoice.LocationId = value; } }

        [FwLogicProperty(Id: "Eg3SV7Rfta0u", IsReadOnly: true)]
        public string Location { get; set; }

        [FwLogicProperty(Id: "jVswGAeWvveN", IsReadOnly: true)]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id: "TqcDyE0azhQ3", IsReadOnly: true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id: "pdlflzfcPDLf")]
        public decimal? InvoiceTotal { get { return vendorInvoice.InvoiceTotal; } set { vendorInvoice.InvoiceTotal = value; } }

        [FwLogicProperty(Id: "uJFiJVc9owxf", IsReadOnly: true)]
        public string BillToAddress1 { get; set; }

        [FwLogicProperty(Id: "tVjytewEwwjo", IsReadOnly: true)]
        public string BillToAddress2 { get; set; }

        [FwLogicProperty(Id: "9CKL3t1os6mt", IsReadOnly: true)]
        public string BillToCity { get; set; }

        [FwLogicProperty(Id: "grhjU6GqlzzY", IsReadOnly: true)]
        public string BillToState { get; set; }

        [FwLogicProperty(Id: "860rQzIiTBuV", IsReadOnly: true)]
        public string BillToZipCode { get; set; }

        [FwLogicProperty(Id: "2AvoYT9KXyNt", IsReadOnly: true)]
        public string BillToCountry { get; set; }

        [FwLogicProperty(Id: "o0hhgZYFDx3a", IsReadOnly: true)]
        public string VendorPhone { get; set; }

        [FwLogicProperty(Id: "Hh228JHkOgRk", IsReadOnly: true)]
        public string VendorFax { get; set; }

        [FwLogicProperty(Id: "buvpUufNde7W", IsReadOnly: true)]
        public bool? InvoiceClass { get; set; }

        [FwLogicProperty(Id: "MyL1YZCwCafI", IsReadOnly: true)]
        public bool? PrintNotes { get; set; }

        [FwLogicProperty(Id: "RNBDNCR4prQc", IsReadOnly: true)]
        public bool? PaymentTerms { get; set; }

        [FwLogicProperty(Id: "PULgmE963dDY")]
        public string TaxId { get { return vendorInvoice.TaxId; } set { vendorInvoice.TaxId = value; } }

        [FwLogicProperty(Id: "WnaRWLQ8BWsh", IsReadOnly: true)]
        public string TaxOptionId { get; set; }

        [FwLogicProperty(Id: "4dw1Z2OPALPa", IsReadOnly: true)]
        public string TaxItemCode { get; set; }

        [FwLogicProperty(Id: "XVUfyJk6KrnN", IsReadOnly: true)]
        public string Notes { get; set; }

        [FwLogicProperty(Id: "OxpdKhWqAZf0")]
        public bool? BilledHiatus { get { return vendorInvoice.BilledHiatus; } set { vendorInvoice.BilledHiatus = value; } }

        [FwLogicProperty(Id: "DVWaSPTM4J38")]
        public string InvoiceType { get { return vendorInvoice.InvoiceType; } set { vendorInvoice.InvoiceType = value; } }

        [FwLogicProperty(Id: "nzBzbxhhvxWm", IsReadOnly: true)]
        public string AgentId { get; set; }

        [FwLogicProperty(Id: "eR73ZSW5CwY4", IsReadOnly: true)]
        public string ProjectManagerId { get; set; }

        [FwLogicProperty(Id: "jYUSibN11hDX")]
        public string CurrencyId { get { return vendorInvoice.CurrencyId; } set { vendorInvoice.CurrencyId = value; } }

        [FwLogicProperty(Id: "EyxG6pYsKq5S", IsReadOnly: true)]
        public string CurrencyCode { get; set; }

        [FwLogicProperty(Id: "xI4mRlnfFgfn", IsReadOnly: true)]
        public string LocationDefaultCurrencyId { get; set; }

        [FwLogicProperty(Id: "dQsRxjFq1bqk", IsReadOnly: true)]
        public decimal? DealBilledExtended { get; set; }

        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            if (saveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                if (original != null)
                {
                    VendorInvoiceLogic orig = (VendorInvoiceLogic)original;

                    if (((orig.PurchaseOrderId != null) && (PurchaseOrderId != null) && (!orig.PurchaseOrderId.Equals(PurchaseOrderId))) || ((orig.PurchaseOrderId == null) && (PurchaseOrderId != null)))
                    {
                        validateMsg = "Cannot change the Purchase Order for an existing Vendor Invoice.";
                        isValid = false;
                    }
                }
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode == TDataRecordSaveMode.smInsert)
            {
                Status = RwConstants.INVOICE_STATUS_NEW;
                StatusDate = FwConvert.ToString(DateTime.Today);
            }
        }
        //------------------------------------------------------------------------------------ 
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            if (e.SaveMode == TDataRecordSaveMode.smInsert)
            {
                UpdateVendorInvoiceItemsRequest request = new UpdateVendorInvoiceItemsRequest();
                request.VendorInvoiceId = VendorInvoiceId;
                request.PurchaseOrderId = PurchaseOrderId;
                request.BillingStartDate = FwConvert.ToDateTime(BillingStartDate);
                request.BillingEndDate = FwConvert.ToDateTime(BillingEndDate);
                UpdateVendorInvoiceItemsReponse response = VendorInvoiceFunc.UpdateVendorInvoiceItem(AppConfig, UserSession, request).Result;
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
