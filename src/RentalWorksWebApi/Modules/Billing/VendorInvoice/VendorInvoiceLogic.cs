using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using System;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.HomeControls.Tax;
using WebApi;
using WebApi.Modules.Agent.PurchaseOrder;

namespace WebApi.Modules.Billing.VendorInvoice
{
    [FwLogic(Id: "pfsVSz8PrlUI")]
    public class VendorInvoiceLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        VendorInvoiceRecord vendorInvoice = new VendorInvoiceRecord();
        TaxRecord tax = new TaxRecord();

        VendorInvoiceLoader vendorInvoiceLoader = new VendorInvoiceLoader();
        VendorInvoiceBrowseLoader vendorInvoiceBrowseLoader = new VendorInvoiceBrowseLoader();

        private PurchaseOrderLogic insertingPurchaseOrder = null;  // this object is loaded once during "Validate" (for speed) and used downstream 


        //------------------------------------------------------------------------------------ 
        public VendorInvoiceLogic()
        {
            dataRecords.Add(vendorInvoice);
            dataRecords.Add(tax);

            dataLoader = vendorInvoiceLoader;
            browseLoader = vendorInvoiceBrowseLoader;
            BeforeSave += OnBeforeSave;
            AfterSave += OnAfterSave;

            vendorInvoice.AfterSave += OnAfterSaveVendorInvoice;

        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "S2jBvP6ya0tp", IsPrimaryKey: true)]
        public string VendorInvoiceId { get { return vendorInvoice.VendorInvoiceId; } set { vendorInvoice.VendorInvoiceId = value; } }

        [FwLogicProperty(Id: "56AMOQD5lQf8")]
        public string PurchaseOrderId { get { return vendorInvoice.PurchaseOrderId; } set { vendorInvoice.PurchaseOrderId = value; } }

        [FwLogicProperty(Id: "BWgU1hFaxksh", IsReadOnly: true)]
        public string PurchaseOrderNumber { get; set; }

        [FwLogicProperty(Id: "MLXSDL6C0kJE", IsReadOnly: true)]
        public string PurchaseOrderDescription { get; set; }

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


        [FwLogicProperty(Id: "WiEZTOSP16lW", IsReadOnly: true)]
        public string DepartmentId { get; set; }

        [FwLogicProperty(Id: "1qg4eux9mj7I", IsReadOnly: true)]
        public string Department { get; set; }

        //[FwLogicProperty(Id: "XUT1BHUBqYoK")]
        //public string OrderNumber { get { return vendorInvoice.OrderNumber; } set { vendorInvoice.OrderNumber = value; } }

        [FwLogicProperty(Id: "ZC6vSxMXCA07")]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id: "2gIjGGRWiyRU", IsReadOnly: true)]
        public string OrderDescription { get; set; }

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

        [FwLogicProperty(Id: "NX8mTmIkb8bd", IsReadOnly: true)]
        public string PaymentTermsId { get { return vendorInvoice.PaymentTermsId; } set { vendorInvoice.PaymentTermsId = value; } }

        [FwLogicProperty(Id: "RNBDNCR4prQc", IsReadOnly: true)]
        public string PaymentTerms { get; set; }

        [FwLogicProperty(Id: "GcmiwlaxxCU0")]
        public string TaxId { get { return vendorInvoice.TaxId; } set { vendorInvoice.TaxId = value; tax.TaxId = value; } }

        [FwLogicProperty(Id: "wCqz9EjI4xHA")]
        public decimal? RentalTaxRate1 { get { return tax.RentalTaxRate1; } set { tax.RentalTaxRate1 = value; } }

        [FwLogicProperty(Id: "aganIMN8KTlT")]
        public decimal? SalesTaxRate1 { get { return tax.SalesTaxRate1; } set { tax.SalesTaxRate1 = value; } }

        [FwLogicProperty(Id: "1uYFt5yYzEi2")]
        public decimal? LaborTaxRate1 { get { return tax.LaborTaxRate1; } set { tax.LaborTaxRate1 = value; } }

        [FwLogicProperty(Id: "ensRmepM2ow7")]
        public decimal? RentalTaxRate2 { get { return tax.RentalTaxRate2; } set { tax.RentalTaxRate2 = value; } }

        [FwLogicProperty(Id: "ebOOxxd5ZdHB")]
        public decimal? SalesTaxRate2 { get { return tax.SalesTaxRate2; } set { tax.SalesTaxRate2 = value; } }

        [FwLogicProperty(Id: "7TtMY5l14Vpc")]
        public decimal? LaborTaxRate2 { get { return tax.LaborTaxRate2; } set { tax.LaborTaxRate2 = value; } }

        [FwLogicProperty(Id: "WnaRWLQ8BWsh", IsReadOnly: true)]
        public string TaxOptionId { get { return tax.TaxOptionId; } set { tax.TaxOptionId = value; } }

        [FwLogicProperty(Id: "qdXCYBN6y8ct", IsReadOnly: true)]
        public string TaxOption { get; set; }

        [FwLogicProperty(Id: "UVm8YGPRqfx6", IsReadOnly: true)]
        public string Tax1Name { get; set; }

        [FwLogicProperty(Id: "e2akGBUVdA90", IsReadOnly: true)]
        public string Tax2Name { get; set; }

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

        [FwLogicProperty(Id: "Wu9Sm3hxHqiUh", IsReadOnly: true)]
        public string CurrencySymbol { get; set; }

        [FwLogicProperty(Id: "xI4mRlnfFgfn", IsReadOnly: true)]
        public string OfficeLocationDefaultCurrencyId { get; set; }

        [FwLogicProperty(Id: "dQsRxjFq1bqk", IsReadOnly: true)]
        public decimal? DealBilledExtended { get; set; }

        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            if (saveMode.Equals(TDataRecordSaveMode.smInsert))
            {

                if (!string.IsNullOrEmpty(PurchaseOrderId))
                {
                    // load the PurchaseOrder object once here for use downstream
                    insertingPurchaseOrder = new PurchaseOrderLogic();
                    insertingPurchaseOrder.SetDependencies(AppConfig, UserSession);
                    insertingPurchaseOrder.PurchaseOrderId = PurchaseOrderId;
                    bool b = insertingPurchaseOrder.LoadAsync<PurchaseOrderLogic>().Result;
                }

                // Currency must match the PO
                if (isValid)
                {
                    if (string.IsNullOrEmpty(CurrencyId))
                    {
                        CurrencyId = insertingPurchaseOrder.CurrencyId;
                    }
                    if (!CurrencyId.Equals(insertingPurchaseOrder.CurrencyId))
                    {
                        isValid = false;
                        validateMsg = "Cannot modify the Currency of this " + BusinessLogicModuleName + ".  Instead, correct the Purchase Order and create a new " + BusinessLogicModuleName + ".";
                    }
                }

                // default the Tax Option from the PO if none provided
                if (isValid)
                {
                    if (string.IsNullOrEmpty(TaxOptionId))
                    {
                        TaxOptionId = insertingPurchaseOrder.TaxOptionId;
                    }
                }

            }
            else //smUpdate
            {
                VendorInvoiceLogic lOrig = null;
                if (original != null)
                {
                    lOrig = (VendorInvoiceLogic)original;
                }

                if (isValid)
                {
                    if (lOrig != null)
                    {
                        if (((PurchaseOrderId != null) && (lOrig.PurchaseOrderId != null) && (!lOrig.PurchaseOrderId.Equals(PurchaseOrderId))) || ((lOrig.PurchaseOrderId == null) && (PurchaseOrderId != null)))
                        {
                            validateMsg = "Cannot change the Purchase Order for an existing Vendor Invoice.";
                            isValid = false;
                        }
                    }
                }

                // cannot change Currency if this Vendor Invoice is associated to a Purchase Order
                if (isValid)
                {
                    if (lOrig != null)
                    {
                        if ((CurrencyId != null) && (!CurrencyId.Equals(lOrig.CurrencyId)) && (!string.IsNullOrEmpty(lOrig.PurchaseOrderId)))
                        {
                            isValid = false;
                            validateMsg = "Cannot modify the Currency of this " + BusinessLogicModuleName + ".  Instead, delete this " + BusinessLogicModuleName + ", correct the Purchase Order, and create a new " + BusinessLogicModuleName + ".";
                        }
                    }
                }

                // no changes allowed at all if processed or closed
                if (isValid)
                {
                    if (lOrig.Status.Equals(RwConstants.VENDOR_INVOICE_STATUS_PROCESSED) || lOrig.Status.Equals(RwConstants.VENDOR_INVOICE_STATUS_CLOSED))
                    {
                        isValid = false;
                        validateMsg = "Cannot modify a " + lOrig.Status + " " + BusinessLogicModuleName + ".";
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
                Status = RwConstants.VENDOR_INVOICE_STATUS_NEW;
                StatusDate = FwConvert.ToShortDate(DateTime.Today);
            }
            else //if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                if (e.Original != null)
                {
                    VendorInvoiceLogic orig = ((VendorInvoiceLogic)e.Original);
                    TaxId = orig.TaxId;
                }
            }

        }
        //------------------------------------------------------------------------------------ 
        public virtual void OnAfterSaveVendorInvoice(object sender, AfterSaveDataRecordEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate)
            {
                if ((TaxOptionId != null) && (!TaxOptionId.Equals(string.Empty)))
                {
                    if (e.Original != null)
                    {
                        TaxId = ((VendorInvoiceRecord)e.Original).TaxId;
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
                // this is a new Vendor Invoice.  TaxId was not known at time of insert.  Need to re-update the data with the known ID
                vendorInvoice.TaxId = tax.TaxId;
                int i = vendorInvoice.SaveAsync(null).Result;



                UpdateVendorInvoiceItemsRequest request = new UpdateVendorInvoiceItemsRequest();
                request.VendorInvoiceId = VendorInvoiceId;
                request.PurchaseOrderId = PurchaseOrderId;
                request.BillingStartDate = FwConvert.ToDateTime(BillingStartDate);
                request.BillingEndDate = FwConvert.ToDateTime(BillingEndDate);
                UpdateVendorInvoiceItemsResponse response = VendorInvoiceFunc.UpdateVendorInvoiceItem(AppConfig, UserSession, request).Result;
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
