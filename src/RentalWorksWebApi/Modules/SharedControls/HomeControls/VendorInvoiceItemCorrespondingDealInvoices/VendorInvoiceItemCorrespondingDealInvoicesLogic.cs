using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.VendorInvoiceItemCorrespondingDealInvoices
{
    [FwLogic(Id: "1rdjeKolcVHP")]
    public class VendorInvoiceItemCorrespondingDealInvoicesLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        VendorInvoiceItemCorrespondingDealInvoicesLoader vendorInvoiceItemCorrespondingDealInvoicesLoader = new VendorInvoiceItemCorrespondingDealInvoicesLoader();
        public VendorInvoiceItemCorrespondingDealInvoicesLogic()
        {
            dataLoader = vendorInvoiceItemCorrespondingDealInvoicesLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "1rqCJrcRz2pH", IsReadOnly: true)]
        public string OrderId { get; set; }
        [FwLogicProperty(Id: "2bpRUQQKlGE5", IsReadOnly: true)]
        public string OrderInvoiceId { get; set; }
        [FwLogicProperty(Id: "2U2pUVNybwUW", IsReadOnly: true)]
        public string InvoiceId { get; set; }
        [FwLogicProperty(Id: "2VS36wFZsArQ", IsReadOnly: true)]
        public string InvoiceDate { get; set; }
        [FwLogicProperty(Id: "2zjhQ7l3Oyh4", IsReadOnly: true)]
        public string InvoiceStatus { get; set; }
        [FwLogicProperty(Id: "3cPMi7DtcqS2", IsReadOnly: true)]
        public string InvoiceNumber { get; set; }
        [FwLogicProperty(Id: "3Fn4sA6WHy2l", IsReadOnly: true)]
        public string InvoiceType { get; set; }
        [FwLogicProperty(Id: "3IzNWmjXrlOy", IsReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwLogicProperty(Id: "3qcPnaq5eRRm", IsReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwLogicProperty(Id: "3Tws8lmrZ3LR", IsReadOnly: true)]
        public string ReferenceNumber { get; set; }
        [FwLogicProperty(Id: "4FiotDPhYsDl", IsReadOnly: true)]
        public string OfficeLocationId { get; set; }
        [FwLogicProperty(Id: "4oCcHG0GKGsU", IsReadOnly: true)]
        public string Location { get; set; }
        [FwLogicProperty(Id: "4tXdauzWqNS0", IsReadOnly: true)]
        public string DepartmentId { get; set; }
        [FwLogicProperty(Id: "4umIhFtxhxri", IsReadOnly: true)]
        public string Department { get; set; }
        [FwLogicProperty(Id: "5Cac6dy3HGvM", IsReadOnly: true)]
        public string AgentId { get; set; }
        [FwLogicProperty(Id: "67V8jIhPAT7N", IsReadOnly: true)]
        public string Agent { get; set; }
        [FwLogicProperty(Id: "6HfV7TnTGZog", IsReadOnly: true)]
        public string ProjectManagerId { get; set; }
        [FwLogicProperty(Id: "7h1akJR3agse", IsReadOnly: true)]
        public string ProjectManager { get; set; }
        [FwLogicProperty(Id: "7KDfnp5kIil3", IsReadOnly: true)]
        public string BillingStart { get; set; }
        [FwLogicProperty(Id: "7LaaQVMJIEhA", IsReadOnly: true)]
        public string BillingEnd { get; set; }
        [FwLogicProperty(Id: "7m4iqaEcq0N4", IsReadOnly: true)]
        public string PurchaseOrderNumber { get; set; }
        [FwLogicProperty(Id: "7n073qja2IGm", IsReadOnly: true)]
        public string FlatPurchaseOrderId { get; set; }
        [FwLogicProperty(Id: "7T7KL9wvIszu", IsReadOnly: true)]
        public bool? ExcludeFromFlat { get; set; }
        [FwLogicProperty(Id: "8eL6KouGUTps", IsReadOnly: true)]
        public bool? BillableFlat { get; set; }
        [FwLogicProperty(Id: "8yBXlA5QETVu", IsReadOnly: true)]
        public decimal? SummaryInvoiceOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
