using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using System.Threading.Tasks;
using WebLibrary;

namespace WebApi.Modules.Home.Order
{
    public class OrderLogic : OrderBaseLogic
    {
        OrderLoader orderLoader = new OrderLoader();
        OrderBrowseLoader orderBrowseLoader = new OrderBrowseLoader();
        //------------------------------------------------------------------------------------
        public OrderLogic()
        {
            dataLoader = orderLoader;
            browseLoader = orderBrowseLoader;
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
        public async Task<OrderBaseLogic> CopyAsync<T>(QuoteOrderCopyRequest copyRequest)
        {
            string newOrderId = await dealOrder.Copy(copyRequest);
            string[] keys = { newOrderId };
            OrderLogic lCopy = new OrderLogic();
            lCopy.AppConfig = AppConfig;
            lCopy.UserSession = UserSession;
            bool x = await lCopy.LoadAsync<OrderLogic>(keys);
            return lCopy;
        }
        //------------------------------------------------------------------------------------



    }
}
