using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.OrderItemRecType
{
    public class OrderItemRecTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderItemRecTypeLoader orderItemRecTypeLoader = new OrderItemRecTypeLoader();
        public OrderItemRecTypeLogic()
        {
            dataLoader = orderItemRecTypeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true, isPrimaryKey: true)]
        public string RecType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RecTypeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}