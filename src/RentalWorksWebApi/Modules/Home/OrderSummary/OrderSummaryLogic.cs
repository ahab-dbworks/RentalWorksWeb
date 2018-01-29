using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Home.OrderSummary
{
    public class OrderSummaryLogic : AppBusinessLogic
    {
        //protected DealOrderRecord dealOrder = new DealOrderRecord();
        protected OrderSummaryLoader orderSummaryLoader = new OrderSummaryLoader();
        //------------------------------------------------------------------------------------
        public OrderSummaryLogic()
        {
            //dataRecords.Add(dealOrder);
            dataLoader = orderSummaryLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalPrice { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalDiscount { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalCost { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalProfit { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalMarkup { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalMargin { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalSubTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalTotal { get; set; }
        //------------------------------------------------------------------------------------


    }
}
