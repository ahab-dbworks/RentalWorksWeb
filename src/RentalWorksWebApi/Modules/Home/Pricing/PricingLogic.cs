using FwStandard.BusinessLogic.Attributes;
using System.Reflection;
using WebApi.Logic;
namespace WebApi.Modules.Home.Pricing
{
    public class PricingLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PricingLoader pricingLoader = new PricingLoader();
        public PricingLogic()
        {
            dataLoader = pricingLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string MasterId { get; set; }
        public string WarehouseId { get; set; }
        public string CurrencyId { get; set; }
        public string WarehouseCode { get; set; }
        public string Warehouse { get; set; }
        public decimal? Cost { get; set; }
        public decimal? DefaultCost { get; set; }
        public decimal? Price { get; set; }
        public decimal? MarkupPercent { get; set; }
        public decimal? Retail { get; set; }
        public decimal? HourlyRate { get; set; }
        public decimal? HourlyCost { get; set; }
        public decimal? HourlyMarkupPercent { get; set; }
        public decimal? DailyRate { get; set; }
        public decimal? DailyCost { get; set; }
        public decimal? DailyMarkupPercent { get; set; }
        public decimal? WeeklyRate { get; set; }
        public decimal? Week2Rate { get; set; }
        public decimal? Week3Rate { get; set; }
        public decimal? Week4Rate { get; set; }
        public decimal? Week5Rate { get; set; }
        public decimal? WeeklyCost { get; set; }
        public decimal? WeeklyMarkupPercent { get; set; }
        public decimal? MonthlyRate { get; set; }
        public decimal? MonthlyCost { get; set; }
        public decimal? MonthlyMarkupPercent { get; set; }
        public decimal? MaximumDiscount { get; set; }
        public bool? HasTieredCost { get; set; }
        public decimal? RestockingFee { get; set; }
        public decimal? RestockingPercent { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}