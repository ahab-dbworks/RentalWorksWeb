using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Home.DealOrder;
using RentalWorksWebApi.Modules.Home.DealOrderDetail;
using RentalWorksWebApi.Modules.Home.Order;

namespace RentalWorksWebApi.Modules.Home.Quote
{
    public class QuoteLogic : OrderBaseLogic
    {
        QuoteLoader quoteLoader = new QuoteLoader();
        //------------------------------------------------------------------------------------
        public QuoteLogic()
        {
            dataLoader = quoteLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string QuoteId { get { return dealOrder.OrderId; } set { dealOrder.OrderId = value; dealOrderDetail.OrderId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isRecordTitle: true)]
        public string QuoteNumber { get { return dealOrder.OrderNumber; } set { dealOrder.OrderNumber = value; } }
        //------------------------------------------------------------------------------------
        public string QuoteDate { get { return dealOrder.OrderDate; } set { dealOrder.OrderDate = value; } }
        //------------------------------------------------------------------------------------
    }
}
