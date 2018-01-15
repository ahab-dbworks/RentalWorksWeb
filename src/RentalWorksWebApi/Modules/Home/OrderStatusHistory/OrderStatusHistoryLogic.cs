using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Home.OrderStatusHistory
{
    public class OrderStatusHistoryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderStatusHistoryRecord orderStatusHistory = new OrderStatusHistoryRecord();
        OrderStatusHistoryLoader orderStatusHistoryLoader = new OrderStatusHistoryLoader();
        public OrderStatusHistoryLogic()
        {
            dataRecords.Add(orderStatusHistory);
            dataLoader = orderStatusHistoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string Id { get { return orderStatusHistory.Id; } set { orderStatusHistory.Id = value; } }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string Internalchar { get { return orderStatusHistory.Internalchar; } set { orderStatusHistory.Internalchar = value; } }
        public string OrderId { get { return orderStatusHistory.OrderId; } set { orderStatusHistory.OrderId = value; } }
        public string StatusDateTime { get { return orderStatusHistory.StatusDateTime; } set { orderStatusHistory.StatusDateTime = value; } }
        public string Status { get { return orderStatusHistory.Status; } set { orderStatusHistory.Status = value; } }
        public string UserId { get { return orderStatusHistory.UserId; } set { orderStatusHistory.UserId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string UserFullName { get; set; }
        public string FunctionName { get { return orderStatusHistory.FunctionName; } set { orderStatusHistory.FunctionName = value; } }
        public string DateStamp { get { return orderStatusHistory.DateStamp; } set { orderStatusHistory.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}