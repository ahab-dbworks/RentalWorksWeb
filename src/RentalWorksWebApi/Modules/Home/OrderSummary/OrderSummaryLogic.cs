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
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SalesPrice { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SalesDiscount { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SalesCost { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SalesProfit { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SalesMarkup { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SalesMargin { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SalesSubTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? SalesTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PartsPrice { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PartsDiscount { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PartsCost { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PartsProfit { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PartsMarkup { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PartsMargin { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PartsSubTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PartsTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? FacilitiesPrice { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? FacilitiesDiscount { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? FacilitiesCost { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? FacilitiesProfit { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? FacilitiesMarkup { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? FacilitiesMargin { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? FacilitiesSubTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? FacilitiesTotal { get; set; }
        //------------------------------------------------------------------------------------
        public decimal? TransporationPrice { get; set; }
        public decimal? TransporationDiscount { get; set; }
        public decimal? TransporationCost { get; set; }
        public decimal? TransporationProfit { get; set; }
        public decimal? TransporationMarkup { get; set; }
        public decimal? TransporationMargin { get; set; }
        public decimal? TransporationSubTotal { get; set; }
        public decimal? TransporationTotal { get; set; }
        //------------------------------------------------------------------------------------
        public decimal? LaborPrice { get; set; }
        public decimal? LaborDiscount { get; set; }
        public decimal? LaborCost { get; set; }
        public decimal? LaborProfit { get; set; }
        public decimal? LaborMarkup { get; set; }
        public decimal? LaborMargin { get; set; }
        public decimal? LaborSubTotal { get; set; }
        public decimal? LaborTotal { get; set; }
        //------------------------------------------------------------------------------------
        public decimal? MiscPrice { get; set; }
        public decimal? MiscDiscount { get; set; }
        public decimal? MiscCost { get; set; }
        public decimal? MiscProfit { get; set; }
        public decimal? MiscMarkup { get; set; }
        public decimal? MiscMargin { get; set; }
        public decimal? MiscSubTotal { get; set; }
        public decimal? MiscTotal { get; set; }
        //------------------------------------------------------------------------------------
        public decimal? RentalSalePrice { get; set; }
        public decimal? RentalSaleDiscount { get; set; }
        public decimal? RentalSaleCost { get; set; }
        public decimal? RentalSaleProfit { get; set; }
        public decimal? RentalSaleMarkup { get; set; }
        public decimal? RentalSaleMargin { get; set; }
        //------------------------------------------------------------------------------------
        public int? WeightPounds { get; set; }
        public int? WeightOunces { get; set; }
        public int? WeightKilograms { get; set; }
        public int? WeightGrams { get; set; }
        public int? WeightInCasePounds { get; set; }
        public int? WeightInCaseOunces { get; set; }
        public int? WeightInCaseKilograms { get; set; }
        public int? WeightInCaseGrams { get; set; }
        //------------------------------------------------------------------------------------
    }
}
