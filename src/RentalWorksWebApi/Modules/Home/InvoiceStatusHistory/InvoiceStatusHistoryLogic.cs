using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.InvoiceStatusHistory
{
    [FwLogic(Id: "34PFkXHRufp2k")]
    public class InvoiceStatusHistoryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InvoiceStatusHistoryLoader invoiceStatusHistoryLoader = new InvoiceStatusHistoryLoader();
        public InvoiceStatusHistoryLogic()
        {
            dataLoader = invoiceStatusHistoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "qG0lLJRCJOpv", IsReadOnly: true)]
        public string StatusDate { get; set; }
        [FwLogicProperty(Id: "xjdUQ60z7AWZ", IsReadOnly: true)]
        public string Status { get; set; }
        [FwLogicProperty(Id: "If3dTqqzTXAH", IsReadOnly: true)]
        public string NameFirstMiddleLast { get; set; }
        [FwLogicProperty(Id: "C9OsMfIlKM5h", IsReadOnly: true)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
