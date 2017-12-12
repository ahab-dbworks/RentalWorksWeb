using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.OrderSetNo
{
    public class OrderSetNoLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderSetNoRecord orderSetNo = new OrderSetNoRecord();
        public OrderSetNoLogic()
        {
            dataRecords.Add(orderSetNo);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string OrderSetNoId { get { return orderSetNo.OrderSetNoId; } set { orderSetNo.OrderSetNoId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string OrderSetNo { get { return orderSetNo.OrderSetNo; } set { orderSetNo.OrderSetNo = value; } }
        public string Description { get { return orderSetNo.Description; } set { orderSetNo.Description = value; } }
        public bool? Inactive { get { return orderSetNo.Inactive; } set { orderSetNo.Inactive = value; } }
        public string DateStamp { get { return orderSetNo.DateStamp; } set { orderSetNo.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}