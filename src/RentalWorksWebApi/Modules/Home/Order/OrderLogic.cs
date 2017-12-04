using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Home.DealOrder;
using RentalWorksWebApi.Modules.Home.DealOrderDetail;

namespace RentalWorksWebApi.Modules.Home.Order
{
    public class OrderLogic : OrderBaseLogic
    {
        OrderLoader orderLoader = new OrderLoader();
        //------------------------------------------------------------------------------------
        public OrderLogic()
        {
            dataLoader = orderLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string OrderId { get { return dealOrder.OrderId; } set { dealOrder.OrderId = value; dealOrderDetail.OrderId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isRecordTitle: true)]
        public string OrderNumber { get { return dealOrder.OrderNumber; } set { dealOrder.OrderNumber = value; } }
        //------------------------------------------------------------------------------------
        public string OrderDate { get { return dealOrder.OrderDate; } set { dealOrder.OrderDate = value; } }
        //------------------------------------------------------------------------------------
    }
}
