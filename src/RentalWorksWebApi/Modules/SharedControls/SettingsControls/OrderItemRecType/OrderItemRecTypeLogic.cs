using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.OrderItemRecType
{
    [FwLogic(Id:"0bsLSqyuMBSy")]
    public class OrderItemRecTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderItemRecTypeLoader orderItemRecTypeLoader = new OrderItemRecTypeLoader();
        public OrderItemRecTypeLogic()
        {
            dataLoader = orderItemRecTypeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"ZIHcidO9f0un", IsPrimaryKey:true, IsReadOnly:true)]
        public string RecType { get; set; }

        [FwLogicProperty(Id:"ZIHcidO9f0un", IsReadOnly:true)]
        public string RecTypeDisplay { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
