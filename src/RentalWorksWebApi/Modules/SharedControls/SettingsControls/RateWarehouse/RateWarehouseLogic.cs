using FwStandard.AppManager;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.HomeControls.Master;
using WebApi.Modules.Settings.Category;
using System;
using static FwStandard.Data.FwDataReadWriteRecord;
using WebApi.Modules.HomeControls.MasterWarehouse;

namespace WebApi.Modules.Settings.RateWarehouse
{
    public class RateWarehouseLogic : MasterWarehouseLogic
    {
        //------------------------------------------------------------------------------------
        RateWarehouseLoader rateWarehouseLoader = new RateWarehouseLoader();
        public RateWarehouseLogic() : base()
        {
            dataLoader = rateWarehouseLoader;
        }
        //------------------------------------------------------------------------------------ 

        [FwLogicProperty(Id:"Hj2OjRx170BZG", IsPrimaryKey:true)]
        public string RateId { get { return masterWarehouse.MasterId; } set { masterWarehouse.MasterId = value; } }

        [FwLogicProperty(Id:"RRuC4GbdAI3X")]
        public decimal? HourlyRate { get { return masterWarehouse.HourlyRate; } set { masterWarehouse.HourlyRate = value; } }

        [FwLogicProperty(Id:"vTTcy01H8Iy2")]
        public decimal? HourlyCost { get { return masterWarehouse.HourlyCost; } set { masterWarehouse.HourlyCost = value; } }

        [FwLogicProperty(Id:"oxBQO9U3Vv2js", IsReadOnly:true)]
        public decimal? HourlyMarkupPercent { get; set;}

        [FwLogicProperty(Id:"SjPuEj3Jnv2W")]
        public decimal? DailyRate { get { return masterWarehouse.DailyRate; } set { masterWarehouse.DailyRate = value; } }

        [FwLogicProperty(Id:"qTsa7nRH1XL4")]
        public decimal? DailyCost { get { return masterWarehouse.DailyCost; } set { masterWarehouse.DailyCost = value; } }

        [FwLogicProperty(Id:"HIbBdAdgXRH3d", IsReadOnly:true)]
        public decimal? DailyMarkupPercent { get; set; }

        [FwLogicProperty(Id:"6yedelWSkV1G")]
        public decimal? WeeklyRate { get { return masterWarehouse.WeeklyRate; } set { masterWarehouse.WeeklyRate = value; } }

        [FwLogicProperty(Id:"WEC6luPUtZ8e")]
        public decimal? WeeklyCost { get { return masterWarehouse.WeeklyCost; } set { masterWarehouse.WeeklyCost = value; } }

        [FwLogicProperty(Id:"yHYenYeRLTjCu", IsReadOnly:true)]
        public decimal? WeeklyMarkupPercent { get; set; }

        [FwLogicProperty(Id:"w86YRkEpx0r5")]
        public decimal? MonthlyRate { get { return masterWarehouse.MonthlyRate; } set { masterWarehouse.MonthlyRate = value; } }

        [FwLogicProperty(Id:"jhN6nmF8xtM4")]
        public decimal? MonthlyCost { get { return masterWarehouse.MonthlyCost; } set { masterWarehouse.MonthlyCost = value; } }

        [FwLogicProperty(Id:"yHYenYeRLTjCu", IsReadOnly:true)]
        public decimal? MonthlyMarkupPercent { get; set; }

        [FwLogicProperty(Id:"GKu0LMcyJUdd")]
        public decimal? Price { get { return masterWarehouse.Price; } set { masterWarehouse.Price = value; } }

        [FwLogicProperty(Id:"NbA2yKWDxHmM")]
        public decimal? Cost { get { return masterWarehouse.Cost; } set { masterWarehouse.Cost = value; } }

        [FwLogicProperty(Id:"yHYenYeRLTjCu", IsReadOnly:true)]
        public decimal? MarkupPercent { get; set; }

        [FwLogicProperty(Id:"8b2433yDtZgh")]
        public string DefaultStartTime { get { return masterWarehouse.DefaultStartTime; } set { masterWarehouse.DefaultStartTime = value; } }

        [FwLogicProperty(Id:"M9lPmnNEFN7G")]
        public string DefaultStopTime { get { return masterWarehouse.DefaultStopTime; } set { masterWarehouse.DefaultStopTime = value; } }

        //------------------------------------------------------------------------------------
    }

}
