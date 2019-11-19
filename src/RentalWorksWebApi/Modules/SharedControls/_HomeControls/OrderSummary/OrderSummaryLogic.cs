using FwStandard.AppManager;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.HomeControls.OrderSummary
{
    [FwLogic(Id:"LQK47SJHMJB8")]
    public class OrderSummaryLogic : AppBusinessLogic
    {
        protected OrderSummaryLoader orderSummaryLoader = new OrderSummaryLoader();
        //------------------------------------------------------------------------------------
        public OrderSummaryLogic()
        {
            dataLoader = orderSummaryLoader;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"8QeMacC8sq7O", IsPrimaryKey:true)]
        public string OrderId { get; set; } = "";

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"A5OqoF4jWmfT", IsPrimaryKey:true)]
        public string TotalType { get; set; } = RwConstants.TOTAL_TYPE_PERIOD;

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"Bhb54uUXNEXe", IsReadOnly:true)]
        public decimal? RentalPrice { get; set; }

        [FwLogicProperty(Id:"ogYhHL2WLtUL", IsReadOnly:true)]
        public decimal? RentalDiscount { get; set; }

        [FwLogicProperty(Id:"xT99IGXj2Aq7", IsReadOnly:true)]
        public decimal? RentalCost { get; set; }

        [FwLogicProperty(Id:"pBtQndYQBNuF", IsReadOnly:true)]
        public decimal? RentalProfit { get; set; }

        [FwLogicProperty(Id:"OrrYjdAgQEzH", IsReadOnly:true)]
        public decimal? RentalMarkup { get; set; }

        [FwLogicProperty(Id:"tkk1F35ngcrF", IsReadOnly:true)]
        public decimal? RentalMargin { get; set; }

        [FwLogicProperty(Id:"RVX5G6rKWBrq", IsReadOnly:true)]
        public decimal? RentalSubTotal { get; set; }

        [FwLogicProperty(Id:"xA8zLQMQSUKY", IsReadOnly:true)]
        public decimal? RentalTax { get; set; }

        [FwLogicProperty(Id:"S7Wieic7pqqF", IsReadOnly:true)]
        public decimal? RentalTotal { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"racijrxcKRG0", IsReadOnly:true)]
        public decimal? SalesPrice { get; set; }

        [FwLogicProperty(Id:"NUW1YdQZO13Z", IsReadOnly:true)]
        public decimal? SalesDiscount { get; set; }

        [FwLogicProperty(Id:"vfLRDbi62QQa", IsReadOnly:true)]
        public decimal? SalesCost { get; set; }

        [FwLogicProperty(Id:"k8NygIY4lD9Z", IsReadOnly:true)]
        public decimal? SalesProfit { get; set; }

        [FwLogicProperty(Id:"UVAnUR9pJZPl", IsReadOnly:true)]
        public decimal? SalesMarkup { get; set; }

        [FwLogicProperty(Id:"YSTh7UHSoNvs", IsReadOnly:true)]
        public decimal? SalesMargin { get; set; }

        [FwLogicProperty(Id:"JI7mMMqn44Pl", IsReadOnly:true)]
        public decimal? SalesSubTotal { get; set; }

        [FwLogicProperty(Id:"cG4FEcfVVIgO", IsReadOnly:true)]
        public decimal? SalesTax { get; set; }

        [FwLogicProperty(Id:"JC4r7Dpjulue", IsReadOnly:true)]
        public decimal? SalesTotal { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"vjRlB0HLBJRu", IsReadOnly:true)]
        public decimal? PartsPrice { get; set; }

        [FwLogicProperty(Id:"yXuuXF0Hvng1", IsReadOnly:true)]
        public decimal? PartsDiscount { get; set; }

        [FwLogicProperty(Id:"liAcc0rYYkcd", IsReadOnly:true)]
        public decimal? PartsCost { get; set; }

        [FwLogicProperty(Id:"ml71WQjA7opz", IsReadOnly:true)]
        public decimal? PartsProfit { get; set; }

        [FwLogicProperty(Id:"xtb8TWpRHKMo", IsReadOnly:true)]
        public decimal? PartsMarkup { get; set; }

        [FwLogicProperty(Id:"bh674sDcdfQT", IsReadOnly:true)]
        public decimal? PartsMargin { get; set; }

        [FwLogicProperty(Id:"nm0uGIJ4MDGp", IsReadOnly:true)]
        public decimal? PartsSubTotal { get; set; }

        [FwLogicProperty(Id:"enjLik4tIpaI", IsReadOnly:true)]
        public decimal? PartsTax { get; set; }

        [FwLogicProperty(Id:"gaV3wEhVaC00", IsReadOnly:true)]
        public decimal? PartsTotal { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"IKxHbcBKQQZQ", IsReadOnly:true)]
        public decimal? FacilitiesPrice { get; set; }

        [FwLogicProperty(Id:"rlhLtCpGZo4F", IsReadOnly:true)]
        public decimal? FacilitiesDiscount { get; set; }

        [FwLogicProperty(Id:"lfsqqFwnAFjx", IsReadOnly:true)]
        public decimal? FacilitiesCost { get; set; }

        [FwLogicProperty(Id:"a6Joc4bpi4LP", IsReadOnly:true)]
        public decimal? FacilitiesProfit { get; set; }

        [FwLogicProperty(Id:"aMRIy7SQKtVe", IsReadOnly:true)]
        public decimal? FacilitiesMarkup { get; set; }

        [FwLogicProperty(Id:"GJsr15cMT1iH", IsReadOnly:true)]
        public decimal? FacilitiesMargin { get; set; }

        [FwLogicProperty(Id:"6klobOcKh0Xf", IsReadOnly:true)]
        public decimal? FacilitiesSubTotal { get; set; }

        [FwLogicProperty(Id:"FjYP0DFxnbrj", IsReadOnly:true)]
        public decimal? FacilitiesTax { get; set; }

        [FwLogicProperty(Id:"uLXLVwn3VwWv", IsReadOnly:true)]
        public decimal? FacilitiesTotal { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"iMaXNwks7TBj", IsReadOnly:true)]
        public decimal? TransportationPrice { get; set; }

        [FwLogicProperty(Id:"kSMDieDUJbsG", IsReadOnly:true)]
        public decimal? TransportationDiscount { get; set; }

        [FwLogicProperty(Id:"lS09ALan7E7L", IsReadOnly:true)]
        public decimal? TransportationCost { get; set; }

        [FwLogicProperty(Id:"we21dPYfT77z", IsReadOnly:true)]
        public decimal? TransportationProfit { get; set; }

        [FwLogicProperty(Id:"MIRwGaQAuT3W", IsReadOnly:true)]
        public decimal? TransportationMarkup { get; set; }

        [FwLogicProperty(Id:"44xek7bsGu72", IsReadOnly:true)]
        public decimal? TransportationMargin { get; set; }

        [FwLogicProperty(Id:"Sx0dIojgroUz", IsReadOnly:true)]
        public decimal? TransportationSubTotal { get; set; }

        [FwLogicProperty(Id:"TXZyTQGtPnN5", IsReadOnly:true)]
        public decimal? TransportationTax { get; set; }

        [FwLogicProperty(Id:"Whx6OQF7rZBz", IsReadOnly:true)]
        public decimal? TransportationTotal { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"dlGUo3W3pJcX", IsReadOnly:true)]
        public decimal? LaborPrice { get; set; }

        [FwLogicProperty(Id:"P6xWWEemij02", IsReadOnly:true)]
        public decimal? LaborDiscount { get; set; }

        [FwLogicProperty(Id:"sT43fJDCQjua", IsReadOnly:true)]
        public decimal? LaborCost { get; set; }

        [FwLogicProperty(Id:"jvO1haLLc0ud", IsReadOnly:true)]
        public decimal? LaborProfit { get; set; }

        [FwLogicProperty(Id:"dn5iaR13E3r6", IsReadOnly:true)]
        public decimal? LaborMarkup { get; set; }

        [FwLogicProperty(Id:"xr1BkOvoMS0P", IsReadOnly:true)]
        public decimal? LaborMargin { get; set; }

        [FwLogicProperty(Id:"BbbxGkoHqNkz", IsReadOnly:true)]
        public decimal? LaborSubTotal { get; set; }

        [FwLogicProperty(Id:"MF0w6TApD6PG", IsReadOnly:true)]
        public decimal? LaborTax{ get; set; }

        [FwLogicProperty(Id:"mDPfs9cdkCuk", IsReadOnly:true)]
        public decimal? LaborTotal { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"PRmsR984DfX9", IsReadOnly:true)]
        public decimal? MiscPrice { get; set; }

        [FwLogicProperty(Id:"82lswENXSEcp", IsReadOnly:true)]
        public decimal? MiscDiscount { get; set; }

        [FwLogicProperty(Id:"JszMDrKPZDUN", IsReadOnly:true)]
        public decimal? MiscCost { get; set; }

        [FwLogicProperty(Id:"2KqHDhkLVnxJ", IsReadOnly:true)]
        public decimal? MiscProfit { get; set; }

        [FwLogicProperty(Id:"5vpp4R6NfUS7", IsReadOnly:true)]
        public decimal? MiscMarkup { get; set; }

        [FwLogicProperty(Id:"23j5mAwZfq0K", IsReadOnly:true)]
        public decimal? MiscMargin { get; set; }

        [FwLogicProperty(Id:"Ny336RasJ3hi", IsReadOnly:true)]
        public decimal? MiscSubTotal { get; set; }

        [FwLogicProperty(Id:"bnWLJum9O3rl", IsReadOnly:true)]
        public decimal? MiscTax { get; set; }

        [FwLogicProperty(Id:"eeLGeRvMZq7l", IsReadOnly:true)]
        public decimal? MiscTotal { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"q63xO0waEYBl", IsReadOnly:true)]
        public decimal? RentalSalePrice { get; set; }

        [FwLogicProperty(Id:"hzPs5mlQBSTi", IsReadOnly:true)]
        public decimal? RentalSaleDiscount { get; set; }

        [FwLogicProperty(Id:"cWtFoBGfB2Wv", IsReadOnly:true)]
        public decimal? RentalSaleCost { get; set; }

        [FwLogicProperty(Id:"GG0hIr4lySjb", IsReadOnly:true)]
        public decimal? RentalSaleProfit { get; set; }

        [FwLogicProperty(Id:"5cNpIEQrnR9x", IsReadOnly:true)]
        public decimal? RentalSaleMarkup { get; set; }

        [FwLogicProperty(Id:"7hqQPO48DpIW", IsReadOnly:true)]
        public decimal? RentalSaleMargin { get; set; }

        [FwLogicProperty(Id:"vq06htZN6N14", IsReadOnly:true)]
        public decimal? RentalSaleSubTotal { get; set; }

        [FwLogicProperty(Id:"1IwsUdYoAmmR", IsReadOnly:true)]
        public decimal? RentalSaleTax { get; set; }

        [FwLogicProperty(Id:"8pHGmh8PnMh1", IsReadOnly:true)]
        public decimal? RentalSaleTotal { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"sU1tYRoz2OH0", IsReadOnly:true)]
        public decimal? TotalPrice { get; set; }

        [FwLogicProperty(Id:"5fb6ZFoiad4p", IsReadOnly:true)]
        public decimal? TotalDiscount { get; set; }

        [FwLogicProperty(Id:"kuLexAACY5AA", IsReadOnly:true)]
        public decimal? TotalCost { get; set; }

        [FwLogicProperty(Id:"VspOjV8tInQV", IsReadOnly:true)]
        public decimal? TotalProfit { get; set; }

        [FwLogicProperty(Id:"m81AvsYkXRco", IsReadOnly:true)]
        public decimal? TotalMarkup { get; set; }

        [FwLogicProperty(Id:"cpn4pbpLgS01", IsReadOnly:true)]
        public decimal? TotalMargin { get; set; }

        [FwLogicProperty(Id:"O6PZDJnGTiyN", IsReadOnly:true)]
        public decimal? TotalSubTotal { get; set; }

        [FwLogicProperty(Id:"ldlGFzcfmrlO", IsReadOnly:true)]
        public decimal? TotalTax{ get; set; }

        [FwLogicProperty(Id:"Rk8WH9SbHdQ3", IsReadOnly:true)]
        public decimal? TotalTotal { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"GbQz7DYBRKsI", IsReadOnly:true)]
        public decimal? TaxCost { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"Rrg2lGJkDt6d")]
        public decimal? ReplacementCostTotal { get; set; }

        [FwLogicProperty(Id:"v0g0Pe1QNN3g")]
        public decimal? ValueTotal { get; set; }

        [FwLogicProperty(Id:"yNHX6gbQ8Hdu")]
        public decimal? ReplacementCostOwned { get; set; }

        [FwLogicProperty(Id:"xbcFLmpHFQzh")]
        public decimal? ValueOwned { get; set; }

        [FwLogicProperty(Id:"sK7vCogcAGdo")]
        public decimal? ReplacementCostSubs { get; set; }

        [FwLogicProperty(Id:"r9KR8TBAqS6b")]
        public decimal? ValueSubs { get; set; }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"xKit9ryIpnjG", IsReadOnly:true)]
        public int? WeightPounds { get; set; }

        [FwLogicProperty(Id:"EThMJBm8UKmV", IsReadOnly:true)]
        public int? WeightOunces { get; set; }

        [FwLogicProperty(Id:"SVxdnMqoyuqI", IsReadOnly:true)]
        public int? WeightKilograms { get; set; }

        [FwLogicProperty(Id:"57rJAJKNGjcu", IsReadOnly:true)]
        public int? WeightGrams { get; set; }

        [FwLogicProperty(Id:"ufW2Ikp3Wq3q", IsReadOnly:true)]
        public int? WeightInCasePounds { get; set; }

        [FwLogicProperty(Id:"IZ9n1jbvRSFO", IsReadOnly:true)]
        public int? WeightInCaseOunces { get; set; }

        [FwLogicProperty(Id:"jk3dnV1IxSKU", IsReadOnly:true)]
        public int? WeightInCaseKilograms { get; set; }

        [FwLogicProperty(Id:"8iVXIZxQmT5O", IsReadOnly:true)]
        public int? WeightInCaseGrams { get; set; }

        //------------------------------------------------------------------------------------
    }
}
