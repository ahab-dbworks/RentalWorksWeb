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
        [FwLogicProperty(Id: "0ER4D9RgSNSM", IsReadOnly: true, IsPrimaryKey: true)]
        public string OrderInvoiceId { get; set; }
        [FwLogicProperty(Id: "1rqCJrcRz2pH", IsReadOnly: true)]
        public string OrderId { get; set; }
        [FwLogicProperty(Id: "2U2pUVNybwUW", IsReadOnly: true)]
        public string InvoiceId { get; set; }
        [FwLogicProperty(Id: "2VS36wFZsArQ", IsReadOnly: true)]
        public string InvoiceDate { get; set; }
        [FwLogicProperty(Id: "2zjhQ7l3Oyh4", IsReadOnly: true)]
        public string InvoiceStatus { get; set; }
        [FwLogicProperty(Id: "3cPMi7DtcqS2", IsReadOnly: true)]
        public string InvoiceNumber { get; set; }
        [FwLogicProperty(Id: "GByFyuwLmewq", IsReadOnly: true)]
        public string InvoiceType { get; set; }
        [FwLogicProperty(Id: "7KDfnp5kIil3", IsReadOnly: true)]
        public string BillingStart { get; set; }
        [FwLogicProperty(Id: "7LaaQVMJIEhA", IsReadOnly: true)]
        public string BillingEnd { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
