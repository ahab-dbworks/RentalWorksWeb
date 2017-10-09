using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Home.Master;
using RentalWorksWebApi.Modules.Settings.InventoryCategory;
using System;
using static FwStandard.DataLayer.FwDataReadWriteRecord;
using RentalWorksWebApi.Modules.Settings.MasterWarehouse;

namespace RentalWorksWebApi.Modules.Settings.ItemWarehouse
{
    public class ItemWarehouseLogic : MasterWarehouseLogic
    {
        //------------------------------------------------------------------------------------
        ItemWarehouseLoader rateWarehouseLoader = new ItemWarehouseLoader();
        public ItemWarehouseLogic() : base()
        {
            dataLoader = rateWarehouseLoader;
        }
        //------------------------------------------------------------------------------------ 

        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ItemId { get { return masterWarehouse.MasterId; } set { masterWarehouse.MasterId = value; } }
        public decimal? HourlyRate { get { return masterWarehouse.HourlyRate; } set { masterWarehouse.HourlyRate = value; } }
        public decimal? HourlyCost { get { return masterWarehouse.HourlyCost; } set { masterWarehouse.HourlyCost = value; } }
        public decimal? DailyRate { get { return masterWarehouse.DailyRate; } set { masterWarehouse.DailyRate = value; } }
        public decimal? DailyCost { get { return masterWarehouse.DailyCost; } set { masterWarehouse.DailyCost = value; } }
        public decimal? WeeklyRate { get { return masterWarehouse.WeeklyRate; } set { masterWarehouse.WeeklyRate = value; } }
        public decimal? WeeklyCost { get { return masterWarehouse.WeeklyCost; } set { masterWarehouse.WeeklyCost = value; } }
        public decimal? MonthlyRate { get { return masterWarehouse.MonthlyRate; } set { masterWarehouse.MonthlyRate = value; } }
        public decimal? MonthlyCost { get { return masterWarehouse.MonthlyCost; } set { masterWarehouse.MonthlyCost = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Qty { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QtyConsigned { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QtyIn { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QtyInContainer { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? QtyQcRequired { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QtyStaged { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QtyOut { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QtyInRepair { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QtyOnPo { get; set; }
        public string AisleLoc { get { return masterWarehouse.AisleLoc; } set { masterWarehouse.AisleLoc = value; } }
        public string ShelfLoc { get { return masterWarehouse.ShelfLoc; } set { masterWarehouse.ShelfLoc = value; } }
        //------------------------------------------------------------------------------------
    }

}
