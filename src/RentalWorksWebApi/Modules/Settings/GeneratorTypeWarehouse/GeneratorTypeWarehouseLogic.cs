using FwStandard.AppManager;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Home.Master;
using WebApi.Modules.Settings.Category;
using System;
using static FwStandard.Data.FwDataReadWriteRecord;
using WebApi.Modules.Home.MasterWarehouse;

namespace WebApi.Modules.Settings.GeneratorTypeWarehouse
{
    public class GeneratorTypeWarehouseLogic : MasterWarehouseLogic
    {
        //------------------------------------------------------------------------------------
        GeneratorTypeWarehouseLoader generatorTypeWarehouseLoader = new GeneratorTypeWarehouseLoader();
        public GeneratorTypeWarehouseLogic() : base()
        {
            dataLoader = generatorTypeWarehouseLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"x43etZ7v2dhK", IsPrimaryKey:true)]
        public string GeneratorTypeId { get { return masterWarehouse.MasterId; } set { masterWarehouse.MasterId = value; } }

        [FwLogicProperty(Id:"2faobIQPTaN")]
        public decimal? HourlyRate { get { return masterWarehouse.HourlyRate; } set { masterWarehouse.HourlyRate = value; } }

        [FwLogicProperty(Id:"QZL4F3DLhXz")]
        public decimal? DailyRate { get { return masterWarehouse.DailyRate; } set { masterWarehouse.DailyRate = value; } }

        [FwLogicProperty(Id:"fJi6U6R3zIs")]
        public decimal? WeeklyRate { get { return masterWarehouse.WeeklyRate; } set { masterWarehouse.WeeklyRate = value; } }

        [FwLogicProperty(Id:"AszH7WGcqCH")]
        public decimal? MonthlyRate { get { return masterWarehouse.MonthlyRate; } set { masterWarehouse.MonthlyRate = value; } }

        //------------------------------------------------------------------------------------
    }

}
