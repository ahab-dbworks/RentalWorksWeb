using FwStandard.AppManager;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Home.Master;
using WebApi.Modules.Settings.Category;
using System;
using static FwStandard.Data.FwDataReadWriteRecord;
using WebApi.Modules.Home.MasterWarehouse;

namespace WebApi.Modules.Settings.VehicleTypeWarehouse
{
    public class VehicleTypeWarehouseLogic : MasterWarehouseLogic
    {
        //------------------------------------------------------------------------------------
        VehicleTypeWarehouseLoader vehicleTypeWarehouseLoader = new VehicleTypeWarehouseLoader();
        public VehicleTypeWarehouseLogic() : base()
        {
            dataLoader = vehicleTypeWarehouseLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"oypNuYmNkcsVV", IsPrimaryKey:true)]
        public string VehicleTypeId { get { return masterWarehouse.MasterId; } set { masterWarehouse.MasterId = value; } }

        [FwLogicProperty(Id:"gFa2b48xlI4f")]
        public decimal? HourlyRate { get { return masterWarehouse.HourlyRate; } set { masterWarehouse.HourlyRate = value; } }

        [FwLogicProperty(Id:"OF2Wq1Fugp9y")]
        public decimal? DailyRate { get { return masterWarehouse.DailyRate; } set { masterWarehouse.DailyRate = value; } }

        [FwLogicProperty(Id:"cDzU7H4QgkWP")]
        public decimal? WeeklyRate { get { return masterWarehouse.WeeklyRate; } set { masterWarehouse.WeeklyRate = value; } }

        [FwLogicProperty(Id:"SCSyXHkBm5x9")]
        public decimal? MonthlyRate { get { return masterWarehouse.MonthlyRate; } set { masterWarehouse.MonthlyRate = value; } }

        //------------------------------------------------------------------------------------
    }

}
