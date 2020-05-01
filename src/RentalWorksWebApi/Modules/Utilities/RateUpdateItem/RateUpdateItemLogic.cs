using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Utilities.RateUpdateItem
{
    [FwLogic(Id: "0Ea9i0u3AtEG")]
    public class RateUpdateItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        RateUpdateItemRecord rateUpdateItem = new RateUpdateItemRecord();
        RateUpdateItemLoader rateUpdateItemLoader = new RateUpdateItemLoader();
        public RateUpdateItemLogic()
        {
            dataRecords.Add(rateUpdateItem);
            dataLoader = rateUpdateItemLoader;

            ReloadOnSave = false;
            LoadOriginalBeforeSaving = false;
        }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "J1n135Cf0ZQI", IsPrimaryKey: true)]
        public string InventoryId { get { return rateUpdateItem.InventoryId; } set { rateUpdateItem.InventoryId = value; } }
        [FwLogicProperty(Id: "qyo8o5YEgnRC", IsPrimaryKey: true)]
        public string WarehouseId { get { return rateUpdateItem.WarehouseId; } set { rateUpdateItem.WarehouseId = value; } }
        [FwLogicProperty(Id: "RD0YTpsLQLCn", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "MzJtgJByqaoz", IsReadOnly: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "6Fl8x1jNZdrc", IsReadOnly: true)]
        public string WarehouseCode { get; set; }
        [FwLogicProperty(Id: "OWfOcUWYESaP", IsReadOnly: true)]
        public string Warehouse { get; set; }
        [FwLogicProperty(Id: "xSN9sz5G0lxp", IsReadOnly: true)]
        public string AvailableFor { get; set; }
        [FwLogicProperty(Id: "F6tt94SwSzyC", IsReadOnly: true)]
        public string Rank { get; set; }
        [FwLogicProperty(Id: "ft32BAyeROQS", IsReadOnly: true)]
        public string Classification { get; set; }
        [FwLogicProperty(Id: "zLNzBjF9z3KY", IsReadOnly: true)]
        public string InventoryTypeId { get; set; }
        [FwLogicProperty(Id: "tsONHDyZlXRD", IsReadOnly: true)]
        public string InventoryType { get; set; }
        [FwLogicProperty(Id: "uAOA2Mcry74J", IsReadOnly: true)]
        public string CategoryId { get; set; }
        [FwLogicProperty(Id: "L4IhM0XsL2WT", IsReadOnly: true)]
        public string Category { get; set; }
        [FwLogicProperty(Id: "FxPTd16ERBcx", IsReadOnly: true)]
        public string SubCategoryId { get; set; }
        [FwLogicProperty(Id: "4tl5PMtSp36O", IsReadOnly: true)]
        public string SubCategory { get; set; }
        [FwLogicProperty(Id: "k2KjgP1RHqB2", IsReadOnly: true)]
        public string UnitId { get; set; }
        [FwLogicProperty(Id: "9O51vzxfEMUa", IsReadOnly: true)]
        public string PartNumber { get; set; }
        [FwLogicProperty(Id: "3Wqq2ticMtHI", IsReadOnly: true)]
        public string ManufacturerId { get; set; }
        [FwLogicProperty(Id: "pHdzQF3jcQCw", IsReadOnly: true)]
        public string Manufacturer { get; set; }

        [FwLogicProperty(Id: "31Vzzc6ITOvz")]
        public decimal? Cost { get { return rateUpdateItem.Cost; } set { rateUpdateItem.Cost = value; } }
        [FwLogicProperty(Id: "NirOywh8ChYP")]
        public decimal? NewCost { get { return rateUpdateItem.NewCost; } set { rateUpdateItem.NewCost = value; } }
        [FwLogicProperty(Id: "8rmAX5SLW7Lg")]
        public decimal? DefaultCost { get { return rateUpdateItem.DefaultCost; } set { rateUpdateItem.DefaultCost = value; } }
        [FwLogicProperty(Id: "4zIBRjTWssFp")]
        public decimal? NewDefaultCost { get { return rateUpdateItem.NewDefaultCost; } set { rateUpdateItem.NewDefaultCost = value; } }
        [FwLogicProperty(Id: "w9Kbn3h6vBRR")]
        public decimal? Price { get { return rateUpdateItem.Price; } set { rateUpdateItem.Price = value; } }
        [FwLogicProperty(Id: "CrrmBzvJo51C")]
        public decimal? NewPrice { get { return rateUpdateItem.NewPrice; } set { rateUpdateItem.NewPrice = value; } }

        [FwLogicProperty(Id: "MIYfFqE7WSmY")]
        public decimal? HourlyRate { get { return rateUpdateItem.HourlyRate; } set { rateUpdateItem.HourlyRate = value; } }
        [FwLogicProperty(Id: "ns54VM7vN4Yu")]
        public decimal? DailyRate { get { return rateUpdateItem.DailyRate; } set { rateUpdateItem.DailyRate = value; } }
        [FwLogicProperty(Id: "CTTjRFOrSB1k")]
        public decimal? WeeklyRate { get { return rateUpdateItem.WeeklyRate; } set { rateUpdateItem.WeeklyRate = value; } }
        [FwLogicProperty(Id: "sFOTCggAzoJO")]
        public decimal? MonthlyRate { get { return rateUpdateItem.MonthlyRate; } set { rateUpdateItem.MonthlyRate = value; } }

        [FwLogicProperty(Id: "MMLsJrSIUR3I")]
        public decimal? NewHourlyRate { get { return rateUpdateItem.NewHourlyRate; } set { rateUpdateItem.NewHourlyRate = value; } }
        [FwLogicProperty(Id: "QjwUJRtL4Yyt")]
        public decimal? NewDailyRate { get { return rateUpdateItem.NewDailyRate; } set { rateUpdateItem.NewDailyRate = value; } }
        [FwLogicProperty(Id: "XTgi9DfO0ahe")]
        public decimal? NewWeeklyRate { get { return rateUpdateItem.NewWeeklyRate; } set { rateUpdateItem.NewWeeklyRate = value; } }
        [FwLogicProperty(Id: "NGUeR6QSQ312")]
        public decimal? NewMonthlyRate { get { return rateUpdateItem.NewMonthlyRate; } set { rateUpdateItem.NewMonthlyRate = value; } }

        [FwLogicProperty(Id: "599UkC5WcBPk")]
        public decimal? Week2Rate { get { return rateUpdateItem.Week2Rate; } set { rateUpdateItem.Week2Rate = value; } }
        [FwLogicProperty(Id: "BdVJqPkDsFnU")]
        public decimal? Week3Rate { get { return rateUpdateItem.Week3Rate; } set { rateUpdateItem.Week3Rate = value; } }
        [FwLogicProperty(Id: "EyvJX1xZauh8")]
        public decimal? Week4Rate { get { return rateUpdateItem.Week4Rate; } set { rateUpdateItem.Week4Rate = value; } }
        [FwLogicProperty(Id: "4N1SfE3i9Nt0")]
        public decimal? Week5Rate { get { return rateUpdateItem.Week5Rate; } set { rateUpdateItem.Week5Rate = value; } }

        [FwLogicProperty(Id: "0BitRrJkRiiy")]
        public decimal? NewWeek2Rate { get { return rateUpdateItem.NewWeek2Rate; } set { rateUpdateItem.NewWeek2Rate = value; } }
        [FwLogicProperty(Id: "m2WY6hwU9Iuw")]
        public decimal? NewWeek3Rate { get { return rateUpdateItem.NewWeek3Rate; } set { rateUpdateItem.NewWeek3Rate = value; } }
        [FwLogicProperty(Id: "gZ4NlvnJMzjL")]
        public decimal? NewWeek4Rate { get { return rateUpdateItem.NewWeek4Rate; } set { rateUpdateItem.NewWeek4Rate = value; } }
        [FwLogicProperty(Id: "8b2p0XcjTwF4")]
        public decimal? NewWeek5Rate { get { return rateUpdateItem.NewWeek5Rate; } set { rateUpdateItem.NewWeek5Rate = value; } }

        [FwLogicProperty(Id: "EJEErXxQQt3k")]
        public decimal? MaxDiscount { get { return rateUpdateItem.MaxDiscount; } set { rateUpdateItem.MaxDiscount = value; } }
        [FwLogicProperty(Id: "1QaP8BYmXob1")]
        public decimal? NewMaxDiscount { get { return rateUpdateItem.NewMaxDiscount; } set { rateUpdateItem.NewMaxDiscount = value; } }

        [FwLogicProperty(Id: "txC2HWJv1cNi")]
        public decimal? HourlyCost { get { return rateUpdateItem.HourlyCost; } set { rateUpdateItem.HourlyCost = value; } }
        [FwLogicProperty(Id: "wziTi3DiAXNW")]
        public decimal? NewHourlyCost { get { return rateUpdateItem.NewHourlyCost; } set { rateUpdateItem.NewHourlyCost = value; } }

        [FwLogicProperty(Id: "OsYoioKqVDTu")]
        public decimal? DailyCost { get { return rateUpdateItem.DailyCost; } set { rateUpdateItem.DailyCost = value; } }
        [FwLogicProperty(Id: "ARHS0yEig2Wk")]
        public decimal? NewDailyCost { get { return rateUpdateItem.NewDailyCost; } set { rateUpdateItem.NewDailyCost = value; } }
        [FwLogicProperty(Id: "le8WaehConra")]
        public decimal? WeeklyCost { get { return rateUpdateItem.WeeklyCost; } set { rateUpdateItem.WeeklyCost = value; } }
        [FwLogicProperty(Id: "PSEFxrBtEW0O")]
        public decimal? NewWeeklyCost { get { return rateUpdateItem.NewWeeklyCost; } set { rateUpdateItem.NewWeeklyCost = value; } }
        [FwLogicProperty(Id: "l78Z4ox5OjXg")]
        public decimal? MonthlyCost { get { return rateUpdateItem.MonthlyCost; } set { rateUpdateItem.MonthlyCost = value; } }
        [FwLogicProperty(Id: "koSH1gTXrJD4")]
        public decimal? NewMonthlyCost { get { return rateUpdateItem.NewMonthlyCost; } set { rateUpdateItem.NewMonthlyCost = value; } }
        [FwLogicProperty(Id: "ufSistFUcYdJ")]
        public decimal? UnitValue { get { return rateUpdateItem.UnitValue; } set { rateUpdateItem.UnitValue = value; } }
        [FwLogicProperty(Id: "kRj2DGxZs26O")]
        public decimal? NewUnitValue { get { return rateUpdateItem.NewUnitValue; } set { rateUpdateItem.NewUnitValue = value; } }
        [FwLogicProperty(Id: "lVDiGi37UWlm")]
        public decimal? ReplacementCost { get { return rateUpdateItem.ReplacementCost; } set { rateUpdateItem.ReplacementCost = value; } }
        [FwLogicProperty(Id: "OLzETL4A0rUz")]
        public decimal? NewReplacementCost { get { return rateUpdateItem.NewReplacementCost; } set { rateUpdateItem.NewReplacementCost = value; } }

        [FwLogicProperty(Id: "PzpSGvAUSAcy")]
        public decimal? Retail { get { return rateUpdateItem.Retail; } set { rateUpdateItem.Retail = value; } }
        [FwLogicProperty(Id: "weBi0FRqPYlF")]
        public decimal? NewRetail { get { return rateUpdateItem.NewRetail; } set { rateUpdateItem.NewRetail = value; } }

        [FwLogicProperty(Id: "3IZxh0c4vF0y")]
        public decimal? MinDaysPerWeek { get { return rateUpdateItem.MinDaysPerWeek; } set { rateUpdateItem.MinDaysPerWeek = value; } }
        [FwLogicProperty(Id: "NR05t3p2EaNx")]
        public decimal? NewMinDaysPerWeek { get { return rateUpdateItem.NewMinDaysPerWeek; } set { rateUpdateItem.NewMinDaysPerWeek = value; } }

        [FwLogicProperty(Id: "DlEo0K6orV5N")]
        public string DateStamp { get { return rateUpdateItem.DateStamp; } set { rateUpdateItem.DateStamp = value; } }

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
