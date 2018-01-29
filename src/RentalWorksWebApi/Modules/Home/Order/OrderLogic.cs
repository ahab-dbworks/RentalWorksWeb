using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Home.DealOrder;
using WebApi.Modules.Home.DealOrderDetail;
//using WebApi;

namespace WebApi.Modules.Home.Order
{
    public class OrderLogic : OrderBaseLogic
    {
        OrderLoader orderLoader = new OrderLoader();
        //------------------------------------------------------------------------------------
        public OrderLogic()
        {
            dataLoader = orderLoader;
            Type = RwConstants.ORDER_TYPE_ORDER;
            BeforeSave += OnBeforeSave;
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
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode == TDataRecordSaveMode.smInsert)
            {
                Status = "CONFIRMED";
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
