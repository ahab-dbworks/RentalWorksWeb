using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.OrderTypeContactTitle
{
    [FwLogic(Id:"42Nwtr4rZFIu")]
    public class OrderTypeContactTitleLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderTypeContactTitleRecord orderTypeContactTitle = new OrderTypeContactTitleRecord();
        OrderTypeContactTitleLoader orderTypeContactTitleLoader = new OrderTypeContactTitleLoader();
        public OrderTypeContactTitleLogic()
        {
            dataRecords.Add(orderTypeContactTitle);
            dataLoader = orderTypeContactTitleLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"4NZL4Dvt625g", IsPrimaryKey:true)]
        public string OrderTypeContactTitleId { get { return orderTypeContactTitle.OrderTypeContactTitleId; } set { orderTypeContactTitle.OrderTypeContactTitleId = value; } }

        [FwLogicProperty(Id:"KirgoBFsywG")]
        public string OrderTypeId { get { return orderTypeContactTitle.OrderTypeId; } set { orderTypeContactTitle.OrderTypeId = value; } }

        [FwLogicProperty(Id:"gr8Bmx1RlG3")]
        public string ContactTitleId { get { return orderTypeContactTitle.ContactTitleId; } set { orderTypeContactTitle.ContactTitleId = value; } }

        [FwLogicProperty(Id:"4NZL4Dvt625g", IsReadOnly:true)]
        public string ContactTitle { get; set; }

        [FwLogicProperty(Id:"Q1pe6yKvWwg")]
        public string DateStamp { get { return orderTypeContactTitle.DateStamp; } set { orderTypeContactTitle.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
