using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Home.Master;
using RentalWorksWebApi.Modules.Settings.InventoryCategory;
using System;
using static FwStandard.DataLayer.FwDataReadWriteRecord;
using RentalWorksWebApi.Modules.Home.MasterWarehouse;

namespace RentalWorksWebApi.Modules.Settings.GeneratorTypeWarehouse
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string GeneratorTypeId { get { return masterWarehouse.MasterId; } set { masterWarehouse.MasterId = value; } }
        public decimal? HourlyRate { get { return masterWarehouse.HourlyRate; } set { masterWarehouse.HourlyRate = value; } }
        public decimal? DailyRate { get { return masterWarehouse.DailyRate; } set { masterWarehouse.DailyRate = value; } }
        public decimal? WeeklyRate { get { return masterWarehouse.WeeklyRate; } set { masterWarehouse.WeeklyRate = value; } }
        public decimal? MonthlyRate { get { return masterWarehouse.MonthlyRate; } set { masterWarehouse.MonthlyRate = value; } }
        //------------------------------------------------------------------------------------
    }

}
