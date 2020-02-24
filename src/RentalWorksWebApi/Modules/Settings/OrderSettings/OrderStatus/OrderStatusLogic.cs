using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.OrderSettings.OrderStatus
{
    [FwLogic(Id: "E9GX3tDB9hpHc")]
    public class OrderStatusLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        //OrderStatusRecord orderStatus = new OrderStatusRecord();
        OrderStatusLoader orderStatusLoader = new OrderStatusLoader();
        public OrderStatusLogic()
        {
            //dataRecords.Add(orderStatus);
            dataLoader = orderStatusLoader;
        }
        //------------------------------------------------------------------------------------ 
        //[FwLogicProperty(Id: "R7FCKugmvYISG", IsRecordTitle:true)]
        //public string OrderStatus { get { return orderStatus.OrderStatus; } set { orderStatus.OrderStatus = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
