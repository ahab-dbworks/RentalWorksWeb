using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.VendorInvoiceStatusHistory
{
    [FwLogic(Id: "dJ6cWzBScxHD")]
    public class VendorInvoiceStatusHistoryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        VendorInvoiceStatusHistoryLoader vendorInvoiceStatusHistoryLoader = new VendorInvoiceStatusHistoryLoader();
        public VendorInvoiceStatusHistoryLogic()
        {
            dataLoader = vendorInvoiceStatusHistoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "BeinECz7VejR0", IsReadOnly: true)]
        public string StatusDate { get; set; }
        [FwLogicProperty(Id: "W8KM8t4J66LSk", IsReadOnly: true)]
        public string Status { get; set; }
        [FwLogicProperty(Id: "zyLoDBIPmxtk", IsReadOnly: true)]
        public string NameFirstMiddleLast { get; set; }
        [FwLogicProperty(Id: "qiJYwikQxlaSg", IsReadOnly: true)]
        public string VendorInvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
