using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Home.Master;
using WebApi.Modules.Settings.InventoryCategory;
using System;
using static FwStandard.DataLayer.FwDataReadWriteRecord;
using WebApi.Modules.Home.MasterWarehouse;

namespace WebApi.Modules.Home.InventoryWarehouse
{
    public class InventoryWarehouseLogic : MasterWarehouseLogic
    {
        //------------------------------------------------------------------------------------
        InventoryWarehouseLoader inventoryWarehouseLoader = new InventoryWarehouseLoader();
        public InventoryWarehouseLogic() : base()
        {
            dataLoader = inventoryWarehouseLoader;
        }
        //------------------------------------------------------------------------------------ 

        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryId { get { return masterWarehouse.MasterId; } set { masterWarehouse.MasterId = value; } }
        public decimal? HourlyRate { get { return masterWarehouse.HourlyRate; } set { masterWarehouse.HourlyRate = value; } }
        public decimal? HourlyCost { get { return masterWarehouse.HourlyCost; } set { masterWarehouse.HourlyCost = value; } }
        public decimal? DailyRate { get { return masterWarehouse.DailyRate; } set { masterWarehouse.DailyRate = value; } }
        public decimal? DailyCost { get { return masterWarehouse.DailyCost; } set { masterWarehouse.DailyCost = value; } }
        public decimal? WeeklyRate { get { return masterWarehouse.WeeklyRate; } set { masterWarehouse.WeeklyRate = value; } }
        public decimal? WeeklyCost { get { return masterWarehouse.WeeklyCost; } set { masterWarehouse.WeeklyCost = value; } }
        public decimal? MonthlyRate { get { return masterWarehouse.MonthlyRate; } set { masterWarehouse.MonthlyRate = value; } }
        public decimal? MonthlyCost { get { return masterWarehouse.MonthlyCost; } set { masterWarehouse.MonthlyCost = value; } }
        public decimal? Retail { get { return masterWarehouse.Retail; } set { masterWarehouse.Retail = value; } }
        public decimal? Price { get { return masterWarehouse.Price; } set { masterWarehouse.Price = value; } }
        public decimal? DefaultCost { get { return masterWarehouse.DefaultCost; } set { masterWarehouse.DefaultCost = value; } }
        public decimal? AverageCost { get { return masterWarehouse.AverageCost; } set { masterWarehouse.AverageCost = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? MarkupPercent { get; set; }
        public int? ReorderPoint { get { return masterWarehouse.ReorderPoint; } set { masterWarehouse.ReorderPoint = value; } }
        public int? ReorderQuantity { get { return masterWarehouse.ReorderQuantity; } set { masterWarehouse.ReorderQuantity = value; } }
        public decimal? MaximumDiscount { get { return masterWarehouse.MaximumDiscount; } set { masterWarehouse.MaximumDiscount = value; } }
        public bool? HasTieredCost { get { return masterWarehouse.HasTieredCost; } set { masterWarehouse.HasTieredCost = value; } }
        public decimal? RestockingFee { get { return masterWarehouse.RestockingFee; } set { masterWarehouse.RestockingFee = value; } }
        public decimal? RestockingPercent { get { return masterWarehouse.RestockingPercent; } set { masterWarehouse.RestockingPercent = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DateOfLastPhysicalInventory { get; set; }
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
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QtyAllocated { get; set; }
        public string AisleLocation { get { return masterWarehouse.AisleLocation; } set { masterWarehouse.AisleLocation = value; } }
        public string ShelfLocation { get { return masterWarehouse.ShelfLocation; } set { masterWarehouse.ShelfLocation = value; } }
        public bool? AvailabilityByHour { get { return masterWarehouse.AvailabilityByHour; } set { masterWarehouse.AvailabilityByHour = value; } }
        public bool? AvailabilityByDeal { get { return masterWarehouse.AvailabilityByDeal; } set { masterWarehouse.AvailabilityByDeal = value; } }
        public bool? AvailabilityByAsset { get { return masterWarehouse.AvailabilityByAsset; } set { masterWarehouse.AvailabilityByAsset = value; } }
        public bool? QcRequired { get { return masterWarehouse.QcRequired; } set { masterWarehouse.QcRequired = value; } }
        public int? AvailabilityQcDelay { get { return masterWarehouse.AvailabilityQcDelay; } set { masterWarehouse.AvailabilityQcDelay = value; } }
        public bool? AllowAllUsersAddToOrder { get { return masterWarehouse.AllowAllUsersAddToOrder; } set { masterWarehouse.AllowAllUsersAddToOrder = value; } }


        //------------------------------------------------------------------------------------
    }

}
