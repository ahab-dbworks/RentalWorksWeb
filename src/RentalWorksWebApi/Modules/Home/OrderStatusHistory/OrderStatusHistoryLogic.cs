using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Home.OrderStatusHistory
{
    [FwLogic(Id:"stzYbdzF1W4x")]
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
        [FwLogicProperty(Id:"BPwdwqVTk5bg", IsPrimaryKey:true)]
        public int? Id { get { return orderStatusHistory.Id; } set { orderStatusHistory.Id = value; } }

        [FwLogicProperty(Id:"x9M7R51KT873", IsPrimaryKey:true)]
        public string Internalchar { get { return orderStatusHistory.Internalchar; } set { orderStatusHistory.Internalchar = value; } }

        [FwLogicProperty(Id:"e6METREX0Z90")]
        public string OrderId { get { return orderStatusHistory.OrderId; } set { orderStatusHistory.OrderId = value; } }

        [FwLogicProperty(Id:"9GzVwAcFNy8T")]
        public string StatusDateTime { get { return orderStatusHistory.StatusDateTime; } set { orderStatusHistory.StatusDateTime = value; } }

        [FwLogicProperty(Id:"iyKBmeD273bV")]
        public string Status { get { return orderStatusHistory.Status; } set { orderStatusHistory.Status = value; } }

        [FwLogicProperty(Id:"ivie2jRRPusc")]
        public string UserId { get { return orderStatusHistory.UserId; } set { orderStatusHistory.UserId = value; } }

        [FwLogicProperty(Id:"DLlkLoqgbZtb", IsReadOnly:true)]
        public string UserFullName { get; set; }

        [FwLogicProperty(Id:"qaQISJiRbXCA")]
        public string FunctionName { get { return orderStatusHistory.FunctionName; } set { orderStatusHistory.FunctionName = value; } }

        [FwLogicProperty(Id:"z0EVTt7KxoUR")]
        public string DateStamp { get { return orderStatusHistory.DateStamp; } set { orderStatusHistory.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
