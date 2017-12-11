using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.OrderTypeContactTitle
{
    public class OrderTypeContactTitleLogic : RwBusinessLogic
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string OrderTypeContactTitleId { get { return orderTypeContactTitle.OrderTypeContactTitleId; } set { orderTypeContactTitle.OrderTypeContactTitleId = value; } }
        public string OrderTypeId { get { return orderTypeContactTitle.OrderTypeId; } set { orderTypeContactTitle.OrderTypeId = value; } }
        public string ContactTitleId { get { return orderTypeContactTitle.ContactTitleId; } set { orderTypeContactTitle.ContactTitleId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ContactTitle { get; set; }
        public string DateStamp { get { return orderTypeContactTitle.DateStamp; } set { orderTypeContactTitle.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}