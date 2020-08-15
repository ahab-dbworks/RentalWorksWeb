using FwStandard.AppManager;
using System.Reflection;
using WebApi.Logic;
using WebApi.Modules.HomeControls.MasterWarehouse;

namespace WebApi.Modules.HomeControls.Pricing
{
    [FwLogic(Id:"AulvGR01yVbOI")]
    public class PricingLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        MasterWarehouseRecord rec = new MasterWarehouseRecord();
        PricingLoader pricingLoader = new PricingLoader();
        public PricingLogic()
        {
            dataRecords.Add(rec);
            dataLoader = pricingLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Vi4yKgBhWGfgT", IsPrimaryKey: true)]
        public string InventoryId { get { return rec.MasterId; } set { rec.MasterId = value; } }
        [FwLogicProperty(Id: "vI73bnkZ9aNfR", IsPrimaryKey: true)]
        public string WarehouseId { get { return rec.WarehouseId; } set { rec.WarehouseId = value; } }
        [FwLogicProperty(Id: "vIdmL8qHaMh3t", IsReadOnly: true)]
        public string Warehouse { get; set; }
        [FwLogicProperty(Id: "vIJDKVWwvoblu", IsReadOnly: true)]
        public string WarehouseCode { get; set; }
        [FwLogicProperty(Id: "Vj5KWWTLAWQmn", IsReadOnly: true)]
        public string WarehouseDefaultCurrencyId { get; set; }
        [FwLogicProperty(Id: "VJyChLjlQmDyr", IsReadOnly: true)]
        public string CurrencyId { get; set; }
        [FwLogicProperty(Id: "vk80D1SIZGqej", IsReadOnly: true)]
        public string CurrencyCode { get; set; }
        [FwLogicProperty(Id: "vkFNqHPSPUtgv", IsReadOnly: true)]
        public string CurrencySymbol { get; set; }
        [FwLogicProperty(Id: "Vl7ltXjJ4ygDM", IsReadOnly: true)]
        public bool? IsCurrencyDefined { get; set; }
        [FwLogicProperty(Id: "VL9Exl0naohi1")]
        public string DefaultPurchaseCurrencyId { get; set; }//{ get { return rec.DefaultPurchaseCurrencyId; } set { rec.DefaultPurchaseCurrencyId = value; } }
        [FwLogicProperty(Id: "VMBzfEd38Hzst", IsReadOnly: true)]
        public string ConvertFromCurrencyId { get; set; }
        [FwLogicProperty(Id: "VMOgrNO4pll08")]
        public decimal? HourlyRate { get { return rec.HourlyRate; } set { rec.HourlyRate = value; } }
        [FwLogicProperty(Id: "vMt4fjgjbyJDB")]
        public decimal? DailyRate { get { return rec.DailyRate; } set { rec.DailyRate = value; } }
        [FwLogicProperty(Id: "VnIi28xEYtkCj")]
        public decimal? WeeklyRate { get { return rec.WeeklyRate; } set { rec.WeeklyRate = value; } }
        [FwLogicProperty(Id: "VNytHn4GMFnqt")]
        public decimal? Week2Rate { get { return rec.Week2Rate; } set { rec.Week2Rate = value; } }
        [FwLogicProperty(Id: "vnZjqwejloRMh")]
        public decimal? Week3Rate { get { return rec.Week3Rate; } set { rec.Week3Rate = value; } }
        [FwLogicProperty(Id: "VowYlIbKZvFXG")]
        public decimal? Week4Rate { get { return rec.Week4Rate; } set { rec.Week4Rate = value; } }
        [FwLogicProperty(Id: "VOXZfUaAuNwRG")]
        public decimal? Week5Rate { get { return rec.Week5Rate; } set { rec.Week5Rate = value; } }
        [FwLogicProperty(Id: "voYVL7Bs9l6ys")]
        public decimal? MonthlyRate { get { return rec.MonthlyRate; } set { rec.MonthlyRate = value; } }
        [FwLogicProperty(Id: "Vp6vmPBwOBRNW")]
        public decimal? SeriesRate { get; set; }//{ get { return rec.SeriesRate; } set { rec.SeriesRate = value; } }
        [FwLogicProperty(Id: "VPoasuX9s4j6x")]
        public decimal? Year2LeaseRate { get; set; }//{ get { return rec.Year2LeaseRate; } set { rec.Year2LeaseRate = value; } }
        [FwLogicProperty(Id: "vQFM2eXC0g4dI")]
        public decimal? Year3LeaseRate { get; set; }//{ get { return rec.Year3LeaseRate; } set { rec.Year3LeaseRate = value; } }
        [FwLogicProperty(Id: "VQk1EZ2gHdNU0")]
        public decimal? Year4LeaseRate { get; set; }//{ get { return rec.Year4LeaseRate; } set { rec.Year4LeaseRate = value; } }
        [FwLogicProperty(Id: "VqLpIUnuJiBp1", IsReadOnly: true)]
        public decimal? HourlyMarkup { get; set; }
        [FwLogicProperty(Id: "VQmwv9lxIDS29", IsReadOnly: true)]
        public decimal? DailyMarkup { get; set; }
        [FwLogicProperty(Id: "Vquns88oOnsPq", IsReadOnly: true)]
        public decimal? WeeklyMarkup { get; set; }
        [FwLogicProperty(Id: "VREecNXqSbJTF", IsReadOnly: true)]
        public decimal? MonthlyMarkup { get; set; }
        [FwLogicProperty(Id: "Vrk8MCHBoWBtQ")]
        public decimal? Retail { get { return rec.Retail; } set { rec.Retail = value; } }
        [FwLogicProperty(Id: "VrkoshJS6AZrP")]
        public decimal? Price { get { return rec.Price; } set { rec.Price = value; } }
        [FwLogicProperty(Id: "vrn1Lxise6QNx")]
        public decimal? Cost { get { return rec.Cost; } set { rec.Cost = value; } }
        [FwLogicProperty(Id: "vstKeMKNcErFg")]
        public decimal? HourlyCost { get { return rec.HourlyCost; } set { rec.HourlyCost = value; } }
        [FwLogicProperty(Id: "VsXWGckwcEcOo")]
        public decimal? DailyCost { get { return rec.DailyCost; } set { rec.DailyCost = value; } }
        [FwLogicProperty(Id: "VTG3wJdga8iZK")]
        public decimal? WeeklyCost { get { return rec.WeeklyCost; } set { rec.WeeklyCost = value; } }
        [FwLogicProperty(Id: "vTmV516mIwaen")]
        public decimal? MonthlyCost { get { return rec.MonthlyCost; } set { rec.MonthlyCost = value; } }
        [FwLogicProperty(Id: "VunDzwbAAEVjV")]
        public decimal? DefaultCost { get { return rec.DefaultCost; } set { rec.DefaultCost = value; } }
        [FwLogicProperty(Id: "VvdTMcYpdMFsr", IsReadOnly: true)]
        public decimal? DefaultCostConverted { get; set; }
        [FwLogicProperty(Id: "vVfOgYqVXr2Bn", IsReadOnly: true)]
        public decimal? Markup { get; set; }
        [FwLogicProperty(Id: "Vvy9Wtt45V2nT", IsReadOnly: true)]
        public decimal? MarkupCostBasis { get; set; }
        [FwLogicProperty(Id: "VxQ9SuXeqINKt", IsReadOnly: true)]
        public string Classification { get; set; }
        [FwLogicProperty(Id: "Vxyfvf3nHeDvD")]
        public decimal? MaximumDiscount { get { return rec.MaximumDiscount; } set { rec.MaximumDiscount = value; } }
        [FwLogicProperty(Id: "VZbNd2krhtMfP")]
        public decimal? MaxDaysPerWeek { get; set; }//{ get { return rec.MaxDaysPerWeek; } set { rec.MaxDaysPerWeek = value; } }
        [FwLogicProperty(Id: "Vzj3zkwY8NwZf")]
        public decimal? MinDaysPerWeek { get; set; }//{ get { return rec.MinDaysPerWeek; } set { rec.MinDaysPerWeek = value; } }
        [FwLogicProperty(Id: "VzpabwBvdoKFF", IsReadOnly: true)]
        public string AvailableFor { get; set; }
        [FwLogicProperty(Id: "W04Q6UI1t1KXy", IsReadOnly: true)]
        public bool? DefaultRentalRates { get; set; }
        [FwLogicProperty(Id: "w1vV3TWDJ02Op", IsReadOnly: true)]
        public decimal? DefaultDailyRate { get; set; }
        [FwLogicProperty(Id: "w2nMlhoTyR5EK", IsReadOnly: true)]
        public decimal? DefaultWeeklyRate { get; set; }
        [FwLogicProperty(Id: "W2SEzcp0eZinT", IsReadOnly: true)]
        public string Hours { get; set; }
        [FwLogicProperty(Id: "w2x36f0cVpBDU")]
        public bool? HasTieredCost { get { return rec.HasTieredCost; } set { rec.HasTieredCost = value; } }
        [FwLogicProperty(Id: "W3FhLUmn7NPmc")]
        public decimal? ReplacementCost { get { return rec.ReplacementCost; } set { rec.ReplacementCost = value; } }
        [FwLogicProperty(Id: "w3IA3dOYEa85m")]
        public decimal? UnitValue { get { return rec.UnitValue; } set { rec.UnitValue = value; } }
        [FwLogicProperty(Id: "w3v0DURoj5MFg", IsReadOnly: true)]
        public decimal? ReplacementCostConverted { get; set; }
        [FwLogicProperty(Id: "W3Z1oAkYDeqO7", IsReadOnly: true)]
        public decimal? UnitCostConverted { get; set; }
        [FwLogicProperty(Id: "W4CnEHbFsvTqi", IsReadOnly: true)]
        public bool? MarkupReplacementCost { get; set; }
        [FwLogicProperty(Id: "w4Cno9TozZlY6", IsReadOnly: true)]
        public decimal? ReplacementCostMarkupPercent { get; set; }
        [FwLogicProperty(Id: "W4LjRGA0MTwva")]
        public decimal? RestockingFee { get { return rec.RestockingFee; } set { rec.RestockingFee = value; } }
        [FwLogicProperty(Id: "w4MzdtLzLtTyi")]
        public decimal? RestockingPercent { get { return rec.RestockingPercent; } set { rec.RestockingPercent = value; } }
        [FwLogicProperty(Id: "W4sEyJ3YRWsUv", IsReadOnly: true)]
        public bool? CalculateMonthlyRate { get; set; }
        [FwLogicProperty(Id: "w4StgZtfJXCqy", IsReadOnly: true)]
        public bool? CalculateSeriesWeeklyRate { get; set; }
        [FwLogicProperty(Id: "w4Uav6oqhQVzq", IsReadOnly: true)]
        public bool? Calculate2YearLeaseWeeklyRate { get; set; }
        [FwLogicProperty(Id: "W53WwgbLGovB7", IsReadOnly: true)]
        public bool? Calculate3YearLeaseWeeklyRate { get; set; }
        [FwLogicProperty(Id: "W6L6aEIZXkjiD", IsReadOnly: true)]
        public int? WarehouseOrderBy { get; set; }
        [FwLogicProperty(Id: "W6ueocJmhUcbg")]
        public string DateStamp { get { return rec.DateStamp; } set { rec.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
