using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.VendorInvoice
{
    public class VendorInvoiceLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        VendorInvoiceRecord vendorInvoice = new VendorInvoiceRecord();
        VendorInvoiceLoader vendorInvoiceLoader = new VendorInvoiceLoader();
        public VendorInvoiceLogic()
        {
            dataRecords.Add(vendorInvoice);
            dataLoader = vendorInvoiceLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string VendorInvoiceId { get { return vendorInvoice.VendorInvoiceId; } set { vendorInvoice.VendorInvoiceId = value; } }
        public string PurchaseOrderId { get { return vendorInvoice.PurchaseOrderId; } set { vendorInvoice.PurchaseOrderId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PurchaseOrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RequistionNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Vendor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string VendorNumber { get; set; }
        public string VendorId { get { return vendorInvoice.VendorId; } set { vendorInvoice.VendorId = value; } }
        public string InvoiceBatchId { get { return vendorInvoice.InvoiceBatchId; } set { vendorInvoice.InvoiceBatchId = value; } }
        public string InputDate { get { return vendorInvoice.InputDate; } set { vendorInvoice.InputDate = value; } }
        public string InputUsersId { get { return vendorInvoice.InputUsersId; } set { vendorInvoice.InputUsersId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Inputuser { get; set; }
        public string InvoiceNumber { get { return vendorInvoice.InvoiceNumber; } set { vendorInvoice.InvoiceNumber = value; } }
        public string InvoiceDate { get { return vendorInvoice.InvoiceDate; } set { vendorInvoice.InvoiceDate = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InvoiceDueDate { get; set; }
        public string BillingStart { get { return vendorInvoice.BillingStart; } set { vendorInvoice.BillingStart = value; } }
        public string BillingEnd { get { return vendorInvoice.BillingEnd; } set { vendorInvoice.BillingEnd = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillingStartEnd { get; set; }
        public string Status { get { return vendorInvoice.Status; } set { vendorInvoice.Status = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DepartmentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        public string OrderNumber { get { return vendorInvoice.OrderNumber; } set { vendorInvoice.OrderNumber = value; } }
        public string PurchaseOrderDealId { get { return vendorInvoice.PurchaseOrderDealId; } set { vendorInvoice.PurchaseOrderDealId = value; } }
        public string PurchaseOrderDealNumber { get { return vendorInvoice.PurchaseOrderDealNumber; } set { vendorInvoice.PurchaseOrderDealNumber = value; } }
        public string PurchaseOrderDeal { get { return vendorInvoice.PurchaseOrderDeal; } set { vendorInvoice.PurchaseOrderDeal = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PurchaseOrderDealNumberDeal { get; set; }
        public string ApprovedDate { get { return vendorInvoice.ApprovedDate; } set { vendorInvoice.ApprovedDate = value; } }
        public string LocationId { get { return vendorInvoice.LocationId; } set { vendorInvoice.LocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Location { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        public decimal? InvoiceTotal { get { return vendorInvoice.InvoiceTotal; } set { vendorInvoice.InvoiceTotal = value; } }
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
        [FwBusinessLogicField(isReadOnly: true)]
        public string VendorPhone { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string VendorFax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? InvoiceClass { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? PrintNotes { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Payterms { get; set; }
        public string TaxId { get { return vendorInvoice.TaxId; } set { vendorInvoice.TaxId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxOptionId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxItemCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Notes { get; set; }
        public string PaymentRequestNumber { get { return vendorInvoice.PaymentRequestNumber; } set { vendorInvoice.PaymentRequestNumber = value; } }
        public string PaymentRequestStatus { get { return vendorInvoice.PaymentRequestStatus; } set { vendorInvoice.PaymentRequestStatus = value; } }
        public string PaymentRequestRunDate { get { return vendorInvoice.PaymentRequestRunDate; } set { vendorInvoice.PaymentRequestRunDate = value; } }
        public string SapDocumentNumber { get { return vendorInvoice.SapDocumentNumber; } set { vendorInvoice.SapDocumentNumber = value; } }
        public string SapDocumentDate { get { return vendorInvoice.SapDocumentDate; } set { vendorInvoice.SapDocumentDate = value; } }
        public decimal? SapDocumentAmount { get { return vendorInvoice.SapDocumentAmount; } set { vendorInvoice.SapDocumentAmount = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SapVendorInvoiceStatus { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CheckNumber { get; set; }
        public bool? BilledHiatus { get { return vendorInvoice.BilledHiatus; } set { vendorInvoice.BilledHiatus = value; } }
        public string InvoiceType { get { return vendorInvoice.InvoiceType; } set { vendorInvoice.InvoiceType = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AgentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ProjectManagerId { get; set; }
        public bool? I35IncludePoPrefix { get { return vendorInvoice.I35IncludePoPrefix; } set { vendorInvoice.I35IncludePoPrefix = value; } }
        public string CurrencyId { get { return vendorInvoice.CurrencyId; } set { vendorInvoice.CurrencyId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CurrencyCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LocationDefaultCurrencyId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DealBilledExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}
