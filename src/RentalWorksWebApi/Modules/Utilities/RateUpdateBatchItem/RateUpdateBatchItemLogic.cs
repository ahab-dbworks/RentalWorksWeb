using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Utilities.RateUpdateBatchItem
{
    [FwLogic(Id: "oWtvSCz8kMxBd")]
    public class RateUpdateBatchItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        RateUpdateBatchItemRecord rateUpdateBatchItem = new RateUpdateBatchItemRecord();
        RateUpdateBatchItemLoader rateUpdateBatchItemLoader = new RateUpdateBatchItemLoader();
        public RateUpdateBatchItemLogic()
        {
            dataRecords.Add(rateUpdateBatchItem);
            dataLoader = rateUpdateBatchItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "oWWLi805fFVkl", IsPrimaryKey: true)]
        public int? RateUpdateBatchItemId { get { return rateUpdateBatchItem.RateUpdateBatchItemId; } set { rateUpdateBatchItem.RateUpdateBatchItemId = value; } }
        [FwLogicProperty(Id: "oXmC76ZxWpZo2")]
        public string InventoryId { get { return rateUpdateBatchItem.InventoryId; } set { rateUpdateBatchItem.InventoryId = value; } }
        [FwLogicProperty(Id: "OYbTpgVE82gET", IsReadOnly: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "OYkPW4C6JyXf7", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "OZAvNrxRuVF2V", IsReadOnly: true)]
        public string AvailableFor { get; set; }
        [FwLogicProperty(Id: "OZg83jlRzpJ0f", IsReadOnly: true)]
        public bool? Rank { get; set; }
        [FwLogicProperty(Id: "OZiTmULoiJ3dB", IsReadOnly: true)]
        public string Classification { get; set; }
        [FwLogicProperty(Id: "OZViJThnPF0Kh", IsReadOnly: true)]
        public string InventoryTypeId { get; set; }
        [FwLogicProperty(Id: "P1E3M8FyGRVnL", IsReadOnly: true)]
        public string InventoryType { get; set; }
        [FwLogicProperty(Id: "P4foVeuqIrWiz", IsReadOnly: true)]
        public string CategoryId { get; set; }
        [FwLogicProperty(Id: "p4Rjz0uszyhpS", IsReadOnly: true)]
        public string Category { get; set; }
        [FwLogicProperty(Id: "P4Z31HVC9guaT", IsReadOnly: true)]
        public string SubCategoryId { get; set; }
        [FwLogicProperty(Id: "P5abNQFLQMCzC", IsReadOnly: true)]
        public string SubCategory { get; set; }
        [FwLogicProperty(Id: "P7CzRCsxtUVRv")]
        public string WarehouseId { get { return rateUpdateBatchItem.WarehouseId; } set { rateUpdateBatchItem.WarehouseId = value; } }
        [FwLogicProperty(Id: "p7iEFgNEAJWtv", IsReadOnly: true)]
        public string WarehouseCode { get; set; }
        [FwLogicProperty(Id: "p91X1R2HMFxMo", IsReadOnly: true)]
        public string Warehouse { get; set; }
        [FwLogicProperty(Id: "P96CK7r3iU3CV", IsReadOnly: true)]
        public string ManufacturerId { get; set; }
        [FwLogicProperty(Id: "p992hypY5BbiM", IsReadOnly: true)]
        public string Manufacturer { get; set; }
        [FwLogicProperty(Id: "P9fYTLL9tPBCG", IsReadOnly: true)]
        public string UnitId { get; set; }
        [FwLogicProperty(Id: "P9Gq8IIhMn8LF", IsReadOnly: true)]
        public string PartNumber { get; set; }
        [FwLogicProperty(Id: "P9jKklHKRhIB9")]
        public decimal? OldDefaultCost { get { return rateUpdateBatchItem.OldDefaultCost; } set { rateUpdateBatchItem.OldDefaultCost = value; } }
        [FwLogicProperty(Id: "Pa2VXhhGsIZOU")]
        public decimal? NewDefaultCost { get { return rateUpdateBatchItem.NewDefaultCost; } set { rateUpdateBatchItem.NewDefaultCost = value; } }
        [FwLogicProperty(Id: "pA7oetArOuTTs")]
        public decimal? OldPrice { get { return rateUpdateBatchItem.OldPrice; } set { rateUpdateBatchItem.OldPrice = value; } }
        [FwLogicProperty(Id: "pAEJO8DrzwKAb")]
        public decimal? NewPrice { get { return rateUpdateBatchItem.NewPrice; } set { rateUpdateBatchItem.NewPrice = value; } }
        [FwLogicProperty(Id: "paRQhqY6qrIlk")]
        public decimal? OldRetail { get { return rateUpdateBatchItem.OldRetail; } set { rateUpdateBatchItem.OldRetail = value; } }
        [FwLogicProperty(Id: "PBc8geFT4Hjun")]
        public decimal? NewRetail { get { return rateUpdateBatchItem.NewRetail; } set { rateUpdateBatchItem.NewRetail = value; } }
        [FwLogicProperty(Id: "PBETqj6OxHhWC")]
        public decimal? OldHourlyRate { get { return rateUpdateBatchItem.OldHourlyRate; } set { rateUpdateBatchItem.OldHourlyRate = value; } }
        [FwLogicProperty(Id: "Pbw5hPMkobXPq")]
        public decimal? NewHourlyRate { get { return rateUpdateBatchItem.NewHourlyRate; } set { rateUpdateBatchItem.NewHourlyRate = value; } }
        [FwLogicProperty(Id: "pbyvMCwfTvs7B")]
        public decimal? OldHourlyCost { get { return rateUpdateBatchItem.OldHourlyCost; } set { rateUpdateBatchItem.OldHourlyCost = value; } }
        [FwLogicProperty(Id: "Pc3SISpZSuLxv")]
        public decimal? NewHourlyCost { get { return rateUpdateBatchItem.NewHourlyCost; } set { rateUpdateBatchItem.NewHourlyCost = value; } }
        [FwLogicProperty(Id: "Pcy30eWexmwXE")]
        public decimal? OldDailyRate { get { return rateUpdateBatchItem.OldDailyRate; } set { rateUpdateBatchItem.OldDailyRate = value; } }
        [FwLogicProperty(Id: "PDh9R9UffI4Mw")]
        public decimal? NewDailyRate { get { return rateUpdateBatchItem.NewDailyRate; } set { rateUpdateBatchItem.NewDailyRate = value; } }
        [FwLogicProperty(Id: "peaeW2DIiHcfv")]
        public decimal? OldDailyCost { get { return rateUpdateBatchItem.OldDailyCost; } set { rateUpdateBatchItem.OldDailyCost = value; } }
        [FwLogicProperty(Id: "peebvV2FnBbmM")]
        public decimal? NewDailyCost { get { return rateUpdateBatchItem.NewDailyCost; } set { rateUpdateBatchItem.NewDailyCost = value; } }
        [FwLogicProperty(Id: "pELcscgT6j7HG")]
        public decimal? OldWeeklyRate { get { return rateUpdateBatchItem.OldWeeklyRate; } set { rateUpdateBatchItem.OldWeeklyRate = value; } }
        [FwLogicProperty(Id: "pF8RqEfWzrf45")]
        public decimal? OldWeek2Rate { get { return rateUpdateBatchItem.OldWeek2Rate; } set { rateUpdateBatchItem.OldWeek2Rate = value; } }
        [FwLogicProperty(Id: "PfAPty9P8qZa8")]
        public decimal? OldWeek3Rate { get { return rateUpdateBatchItem.OldWeek3Rate; } set { rateUpdateBatchItem.OldWeek3Rate = value; } }
        [FwLogicProperty(Id: "pfFxa6DXnzClP")]
        public decimal? OldWeek4Rate { get { return rateUpdateBatchItem.OldWeek4Rate; } set { rateUpdateBatchItem.OldWeek4Rate = value; } }
        [FwLogicProperty(Id: "pFMN0Tz0CRiuh")]
        public decimal? OldWeek5Rate { get { return rateUpdateBatchItem.OldWeek5Rate; } set { rateUpdateBatchItem.OldWeek5Rate = value; } }
        [FwLogicProperty(Id: "pfpq5sZetnaFK")]
        public decimal? OldWeeklyCost { get { return rateUpdateBatchItem.OldWeeklyCost; } set { rateUpdateBatchItem.OldWeeklyCost = value; } }
        [FwLogicProperty(Id: "PfWipq9uOKo2C")]
        public decimal? NewWeeklyRate { get { return rateUpdateBatchItem.NewWeeklyRate; } set { rateUpdateBatchItem.NewWeeklyRate = value; } }
        [FwLogicProperty(Id: "PFzhTRWiiApCG")]
        public decimal? NewWeek2Rate { get { return rateUpdateBatchItem.NewWeek2Rate; } set { rateUpdateBatchItem.NewWeek2Rate = value; } }
        [FwLogicProperty(Id: "PgIRfP4RHRrkH")]
        public decimal? NewWeek3Rate { get { return rateUpdateBatchItem.NewWeek3Rate; } set { rateUpdateBatchItem.NewWeek3Rate = value; } }
        [FwLogicProperty(Id: "PGOgeHzbKN1pg")]
        public decimal? NewWeek4Rate { get { return rateUpdateBatchItem.NewWeek4Rate; } set { rateUpdateBatchItem.NewWeek4Rate = value; } }
        [FwLogicProperty(Id: "pht6wCzON0jpW")]
        public decimal? NewWeek5Rate { get { return rateUpdateBatchItem.NewWeek5Rate; } set { rateUpdateBatchItem.NewWeek5Rate = value; } }
        [FwLogicProperty(Id: "PibHZyI2lALEa")]
        public decimal? NewWeeklyCost { get { return rateUpdateBatchItem.NewWeeklyCost; } set { rateUpdateBatchItem.NewWeeklyCost = value; } }
        [FwLogicProperty(Id: "pItDXCmTsbVYV")]
        public decimal? OldMonthlyRate { get { return rateUpdateBatchItem.OldMonthlyRate; } set { rateUpdateBatchItem.OldMonthlyRate = value; } }
        [FwLogicProperty(Id: "pJDJXvVv0juIk")]
        public decimal? OldMonthlyCost { get { return rateUpdateBatchItem.OldMonthlyCost; } set { rateUpdateBatchItem.OldMonthlyCost = value; } }
        [FwLogicProperty(Id: "pJhByDO9GQCNH")]
        public decimal? OldMaxDiscount { get { return rateUpdateBatchItem.OldMaxDiscount; } set { rateUpdateBatchItem.OldMaxDiscount = value; } }
        [FwLogicProperty(Id: "pjHJng7URIYB6")]
        public decimal? NewMonthlyRate { get { return rateUpdateBatchItem.NewMonthlyRate; } set { rateUpdateBatchItem.NewMonthlyRate = value; } }
        [FwLogicProperty(Id: "pJMGcXJaCoL5D")]
        public decimal? NewMonthlyCost { get { return rateUpdateBatchItem.NewMonthlyCost; } set { rateUpdateBatchItem.NewMonthlyCost = value; } }
        [FwLogicProperty(Id: "pJoEhzsueJGYK")]
        public decimal? NewMaxDiscount { get { return rateUpdateBatchItem.NewMaxDiscount; } set { rateUpdateBatchItem.NewMaxDiscount = value; } }
        [FwLogicProperty(Id: "pjwGeJ8og84fb")]
        public decimal? OldUnitValue { get { return rateUpdateBatchItem.OldUnitValue; } set { rateUpdateBatchItem.OldUnitValue = value; } }
        [FwLogicProperty(Id: "pkHlE1wF8k7i0")]
        public decimal? NewUnitValue { get { return rateUpdateBatchItem.NewUnitValue; } set { rateUpdateBatchItem.NewUnitValue = value; } }
        [FwLogicProperty(Id: "pkkPCyQs3In4e")]
        public decimal? OldReplacementCost { get { return rateUpdateBatchItem.OldReplacementCost; } set { rateUpdateBatchItem.OldReplacementCost = value; } }
        [FwLogicProperty(Id: "PKzvLo5zkd1xT")]
        public decimal? NewReplacementCost { get { return rateUpdateBatchItem.NewReplacementCost; } set { rateUpdateBatchItem.NewReplacementCost = value; } }
        [FwLogicProperty(Id: "PmqhemUwYleKT")]
        public decimal? OldMinDw { get { return rateUpdateBatchItem.OldMinDw; } set { rateUpdateBatchItem.OldMinDw = value; } }
        [FwLogicProperty(Id: "PmVIaHjFE1tq9")]
        public decimal? NewMinEw { get { return rateUpdateBatchItem.NewMinEw; } set { rateUpdateBatchItem.NewMinEw = value; } }
        [FwLogicProperty(Id: "pne8JOOoHYiLa")]
        public int? RateUpdateBatchId { get { return rateUpdateBatchItem.RateUpdateBatchId; } set { rateUpdateBatchItem.RateUpdateBatchId = value; } }
        [FwLogicProperty(Id: "pNG6G4lLjMvbU")]
        public string DateStamp { get { return rateUpdateBatchItem.DateStamp; } set { rateUpdateBatchItem.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}
