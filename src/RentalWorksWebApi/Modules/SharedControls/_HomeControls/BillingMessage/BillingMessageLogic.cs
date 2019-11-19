using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.BillingMessage
{
    [FwLogic(Id: "m6waP15Dz9lvu")]
    public class BillingMessageLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        BillingMessageLoader billingMessageLoader = new BillingMessageLoader();
        public BillingMessageLogic()
        {
            dataLoader = billingMessageLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "9tPiqdVncWmfA", IsReadOnly: true)]
        public string BillingMessage { get; set; }
        [FwLogicProperty(Id: "0GVvjTdmdnWOe", IsReadOnly: true)]
        public int? BillingMessageId { get; set; }
        [FwLogicProperty(Id: "DU5mvwhGOycfi", IsReadOnly: true)]
        public string SessionId { get; set; }
        [FwLogicProperty(Id: "JxP3x2fzoUNPj", IsReadOnly: true)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
