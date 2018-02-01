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
        public decimal? RentalTax { get; set; }
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
        public decimal? SalesTax { get; set; }
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
        public decimal? PartsTax { get; set; }
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
        public decimal? FacilitiesTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? FacilitiesTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TransporationPrice { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TransporationDiscount { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TransporationCost { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TransporationProfit { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TransporationMarkup { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TransporationMargin { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TransporationSubTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TransporationTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TransporationTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? LaborPrice { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? LaborDiscount { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? LaborCost { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? LaborProfit { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? LaborMarkup { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? LaborMargin { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? LaborSubTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? LaborTax{ get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? LaborTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MiscPrice { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MiscDiscount { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MiscCost { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MiscProfit { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MiscMarkup { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MiscMargin { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MiscSubTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MiscTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MiscTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalSalePrice { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalSaleDiscount { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalSaleCost { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalSaleProfit { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalSaleMarkup { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalSaleMargin { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalSaleSubTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalSaleTax { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? RentalSaleTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TotalPrice { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TotalDiscount { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TotalCost { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TotalProfit { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TotalMarkup { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TotalMargin { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TotalSubTotal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TotalTax{ get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TotalTotal { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TaxCost { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public int? WeightPounds { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? WeightOunces { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? WeightKilograms { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? WeightGrams { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? WeightInCasePounds { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? WeightInCaseOunces { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? WeightInCaseKilograms { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? WeightInCaseGrams { get; set; }
        //------------------------------------------------------------------------------------
    }
}
