using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Utilities.OrderActivityType
{
    [FwLogic(Id: "msAlquOdYjM43")]
    public class OrderActivityTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderActivityTypeLoader OrderActivityTypeLoader = new OrderActivityTypeLoader();
        public OrderActivityTypeLogic()
        {
            dataLoader = OrderActivityTypeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "y91I3WM7tbZj", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "kmwoLfHqAc5V", IsReadOnly: true)]
        public string ActivityType { get; set; }
        [FwLogicProperty(Id: "Gcuk5eG3ZA6k", IsReadOnly: true)]
        public bool? IsSystemType { get; set; }
        [FwLogicProperty(Id: "XuDYlERF8ZcF", IsReadOnly: true)]
        public string Color { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
