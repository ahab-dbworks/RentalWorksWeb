using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.OrderSetNo
{
    [FwLogic(Id:"HOYjOo2vzeUO")]
    public class OrderSetNoLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderSetNoRecord orderSetNo = new OrderSetNoRecord();
        public OrderSetNoLogic()
        {
            dataRecords.Add(orderSetNo);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"pu8JnuPvLvyV", IsPrimaryKey:true)]
        public string OrderSetNoId { get { return orderSetNo.OrderSetNoId; } set { orderSetNo.OrderSetNoId = value; } }

        [FwLogicProperty(Id:"pu8JnuPvLvyV", IsRecordTitle:true)]
        public string OrderSetNo { get { return orderSetNo.OrderSetNo; } set { orderSetNo.OrderSetNo = value; } }

        [FwLogicProperty(Id:"gEbpc7NBmJG")]
        public string Description { get { return orderSetNo.Description; } set { orderSetNo.Description = value; } }

        [FwLogicProperty(Id:"1LgxVjz72QB")]
        public bool? Inactive { get { return orderSetNo.Inactive; } set { orderSetNo.Inactive = value; } }

        [FwLogicProperty(Id:"SiiQsFjY8Ay")]
        public string DateStamp { get { return orderSetNo.DateStamp; } set { orderSetNo.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
