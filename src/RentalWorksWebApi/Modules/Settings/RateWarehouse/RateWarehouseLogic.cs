using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Home.Master;
using WebApi.Modules.Settings.Category;
using System;
using static FwStandard.DataLayer.FwDataReadWriteRecord;
using WebApi.Modules.Home.MasterWarehouse;

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

        [FwBusinessLogicField(isPrimaryKey: true)]
        public string RateId { get { return masterWarehouse.MasterId; } set { masterWarehouse.MasterId = value; } }
        public decimal? HourlyRate { get { return masterWarehouse.HourlyRate; } set { masterWarehouse.HourlyRate = value; } }
        public decimal? HourlyCost { get { return masterWarehouse.HourlyCost; } set { masterWarehouse.HourlyCost = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? HourlyMarkupPercent { get; set;}
        public decimal? DailyRate { get { return masterWarehouse.DailyRate; } set { masterWarehouse.DailyRate = value; } }
        public decimal? DailyCost { get { return masterWarehouse.DailyCost; } set { masterWarehouse.DailyCost = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? DailyMarkupPercent { get; set; }
        public decimal? WeeklyRate { get { return masterWarehouse.WeeklyRate; } set { masterWarehouse.WeeklyRate = value; } }
        public decimal? WeeklyCost { get { return masterWarehouse.WeeklyCost; } set { masterWarehouse.WeeklyCost = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? WeeklyMarkupPercent { get; set; }
        public decimal? MonthlyRate { get { return masterWarehouse.MonthlyRate; } set { masterWarehouse.MonthlyRate = value; } }
        public decimal? MonthlyCost { get { return masterWarehouse.MonthlyCost; } set { masterWarehouse.MonthlyCost = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MonthlyMarkupPercent { get; set; }
        public string DefaultStartTime { get { return masterWarehouse.DefaultStartTime; } set { masterWarehouse.DefaultStartTime = value; } }
        public string DefaultStopTime { get { return masterWarehouse.DefaultStopTime; } set { masterWarehouse.DefaultStopTime = value; } }
        //------------------------------------------------------------------------------------
    }

}
