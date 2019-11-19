using FwStandard.AppManager;
using System.Reflection;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.Pricing
{
    [FwLogic(Id:"AulvGR01yVbOI")]
    public class PricingLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PricingLoader pricingLoader = new PricingLoader();
        public PricingLogic()
        {
            dataLoader = pricingLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"qlSikNAYSpiD4", IsPrimaryKey:true)]
        public string MasterId { get; set; }

        [FwLogicProperty(Id:"CHZRmN6if2IH")]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id:"GsDQF7Bkk6ry")]
        public string CurrencyId { get; set; }

        [FwLogicProperty(Id:"HXNuPQGgidUf")]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id:"WLJleM4IQnao")]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id:"K2keNwGzIMLy")]
        public decimal? Cost { get; set; }

        [FwLogicProperty(Id:"7jfkixsfa9Yv")]
        public decimal? DefaultCost { get; set; }

        [FwLogicProperty(Id:"OzbCd4OTEPM0")]
        public decimal? Price { get; set; }

        [FwLogicProperty(Id:"BY8myaZFA4dC")]
        public decimal? MarkupPercent { get; set; }

        [FwLogicProperty(Id:"sbLKTn94wE3v")]
        public decimal? Retail { get; set; }

        [FwLogicProperty(Id:"hR5SMn90JKs8")]
        public decimal? HourlyRate { get; set; }

        [FwLogicProperty(Id:"a6PaTXp5XO0i")]
        public decimal? HourlyCost { get; set; }

        [FwLogicProperty(Id:"EarmkG3gpsIN")]
        public decimal? HourlyMarkupPercent { get; set; }

        [FwLogicProperty(Id:"Ne3cCGuIh5jG")]
        public decimal? DailyRate { get; set; }

        [FwLogicProperty(Id:"pHNgK6e6EZXH")]
        public decimal? DailyCost { get; set; }

        [FwLogicProperty(Id:"rzZFsZHRvJIf")]
        public decimal? DailyMarkupPercent { get; set; }

        [FwLogicProperty(Id:"Kst86msLbTYi")]
        public decimal? WeeklyRate { get; set; }

        [FwLogicProperty(Id:"1IAjkPEC4JLq")]
        public decimal? Week2Rate { get; set; }

        [FwLogicProperty(Id:"lZv1Dho3XeMw")]
        public decimal? Week3Rate { get; set; }

        [FwLogicProperty(Id:"ZMyXWDQMCFhO")]
        public decimal? Week4Rate { get; set; }

        [FwLogicProperty(Id:"unVlpd6Etrhr")]
        public decimal? Week5Rate { get; set; }

        [FwLogicProperty(Id:"NN0uRruHFURX")]
        public decimal? WeeklyCost { get; set; }

        [FwLogicProperty(Id:"1DEBsWIuOCyC")]
        public decimal? WeeklyMarkupPercent { get; set; }

        [FwLogicProperty(Id:"4ZMf2KiarO8C")]
        public decimal? MonthlyRate { get; set; }

        [FwLogicProperty(Id:"TjysoQzQfwPX")]
        public decimal? MonthlyCost { get; set; }

        [FwLogicProperty(Id:"Qs5v8aqql63I")]
        public decimal? MonthlyMarkupPercent { get; set; }

        [FwLogicProperty(Id:"tkoGnYN3pzjO")]
        public decimal? MaximumDiscount { get; set; }

        [FwLogicProperty(Id:"1Jxo7y5O2zyS")]
        public bool? HasTieredCost { get; set; }

        [FwLogicProperty(Id:"HPhGjniIS0Z4")]
        public decimal? RestockingFee { get; set; }

        [FwLogicProperty(Id:"61aR14VeOjMK")]
        public decimal? RestockingPercent { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
