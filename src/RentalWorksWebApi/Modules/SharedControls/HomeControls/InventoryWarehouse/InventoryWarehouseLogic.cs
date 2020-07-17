using FwStandard.AppManager;
using WebApi.Modules.HomeControls.MasterWarehouse;

namespace WebApi.Modules.HomeControls.InventoryWarehouse
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

        [FwLogicProperty(Id: "AWeQcDYDVIXW", IsPrimaryKey: true)]
        public string InventoryId { get { return masterWarehouse.MasterId; } set { masterWarehouse.MasterId = value; } }

        [FwLogicProperty(Id: "ygJ61wLnITwh")]
        public decimal? HourlyRate { get { return masterWarehouse.HourlyRate; } set { masterWarehouse.HourlyRate = value; } }

        [FwLogicProperty(Id: "RBkZNr31QfUj")]
        public decimal? HourlyCost { get { return masterWarehouse.HourlyCost; } set { masterWarehouse.HourlyCost = value; } }

        [FwLogicProperty(Id: "KE9YqJB564ga")]
        public decimal? DailyRate { get { return masterWarehouse.DailyRate; } set { masterWarehouse.DailyRate = value; } }

        [FwLogicProperty(Id: "zy83RvQp0Ed9")]
        public decimal? DailyCost { get { return masterWarehouse.DailyCost; } set { masterWarehouse.DailyCost = value; } }

        [FwLogicProperty(Id: "TtYvqXhTNRmr")]
        public decimal? WeeklyRate { get { return masterWarehouse.WeeklyRate; } set { masterWarehouse.WeeklyRate = value; } }

        [FwLogicProperty(Id: "MvENYHVYmOtS")]
        public decimal? WeeklyCost { get { return masterWarehouse.WeeklyCost; } set { masterWarehouse.WeeklyCost = value; } }

        [FwLogicProperty(Id: "oM35O69R0YC0")]
        public decimal? Week2Rate { get { return masterWarehouse.Week2Rate; } set { masterWarehouse.Week2Rate = value; } }

        [FwLogicProperty(Id: "5JsGUV6Ck76e")]
        public decimal? Week3Rate { get { return masterWarehouse.Week3Rate; } set { masterWarehouse.Week3Rate = value; } }

        [FwLogicProperty(Id: "Guk00HL0Rh6X")]
        public decimal? Week4Rate { get { return masterWarehouse.Week4Rate; } set { masterWarehouse.Week4Rate = value; } }

        //[FwLogicProperty(Id:"X3aPlKeZzwTM")]
        //public decimal? Week5Rate { get { return masterWarehouse.Week5Rate; } set { masterWarehouse.Week5Rate = value; } }

        [FwLogicProperty(Id: "KNVTpOJGY9Ul")]
        public decimal? MonthlyRate { get { return masterWarehouse.MonthlyRate; } set { masterWarehouse.MonthlyRate = value; } }

        [FwLogicProperty(Id: "76Fszf70uW7M")]
        public decimal? MonthlyCost { get { return masterWarehouse.MonthlyCost; } set { masterWarehouse.MonthlyCost = value; } }

        [FwLogicProperty(Id: "aPegxxwq7Uz6")]
        public decimal? Retail { get { return masterWarehouse.Retail; } set { masterWarehouse.Retail = value; } }

        [FwLogicProperty(Id: "J3YLTLn4nup6")]
        public decimal? Price { get { return masterWarehouse.Price; } set { masterWarehouse.Price = value; } }

        [FwLogicProperty(Id: "t8YxBYmSPLQ0")]
        public decimal? DefaultCost { get { return masterWarehouse.DefaultCost; } set { masterWarehouse.DefaultCost = value; } }

        [FwLogicProperty(Id: "PQ1cgBLC7WYa")]
        public decimal? AverageCost { get { return masterWarehouse.Cost; } set { masterWarehouse.Cost = value; } }

        [FwLogicProperty(Id: "vuleIbvNPo8i", IsReadOnly: true)]
        public decimal? MarkupPercent { get; set; }

        [FwLogicProperty(Id: "OCxKRNj05RAP")]
        public int? ReorderPoint { get { return masterWarehouse.ReorderPoint; } set { masterWarehouse.ReorderPoint = value; } }

        [FwLogicProperty(Id: "BR2a8sJVxeai")]
        public int? ReorderQuantity { get { return masterWarehouse.ReorderQuantity; } set { masterWarehouse.ReorderQuantity = value; } }

        [FwLogicProperty(Id: "5B0V0SxTMAHe")]
        public decimal? MaximumDiscount { get { return masterWarehouse.MaximumDiscount; } set { masterWarehouse.MaximumDiscount = value; } }

        [FwLogicProperty(Id: "0qlPoJQ2Lzjp")]
        public bool? HasTieredCost { get { return masterWarehouse.HasTieredCost; } set { masterWarehouse.HasTieredCost = value; } }

        [FwLogicProperty(Id: "vN379ABBZUIe")]
        public decimal? RestockingFee { get { return masterWarehouse.RestockingFee; } set { masterWarehouse.RestockingFee = value; } }

        [FwLogicProperty(Id: "wD6PYirZf2Md")]
        public decimal? RestockingPercent { get { return masterWarehouse.RestockingPercent; } set { masterWarehouse.RestockingPercent = value; } }

        [FwLogicProperty(Id: "l09CMmgI4AvL", IsReadOnly: true)]
        public string DateOfLastPhysicalInventory { get; set; }

        [FwLogicProperty(Id: "8t94kl96CWah", IsReadOnly: true)]
        public decimal? Qty { get; set; }

        [FwLogicProperty(Id: "8t94kl96CWah", IsReadOnly: true)]
        public decimal? QtyConsigned { get; set; }

        [FwLogicProperty(Id: "8t94kl96CWah", IsReadOnly: true)]
        public decimal? QtyIn { get; set; }

        [FwLogicProperty(Id: "8t94kl96CWah", IsReadOnly: true)]
        public decimal? QtyInContainer { get; set; }

        [FwLogicProperty(Id: "b4p1WX1a5jtM", IsReadOnly: true)]
        public int? QtyQcRequired { get; set; }

        [FwLogicProperty(Id: "8t94kl96CWah", IsReadOnly: true)]
        public decimal? QtyStaged { get; set; }

        [FwLogicProperty(Id: "8t94kl96CWah", IsReadOnly: true)]
        public decimal? QtyOut { get; set; }

        [FwLogicProperty(Id: "8t94kl96CWah", IsReadOnly: true)]
        public decimal? QtyInRepair { get; set; }

        [FwLogicProperty(Id: "8t94kl96CWah", IsReadOnly: true)]
        public decimal? QtyOnPo { get; set; }

        [FwLogicProperty(Id: "8t94kl96CWah", IsReadOnly: true)]
        public decimal? QtyAllocated { get; set; }

        [FwLogicProperty(Id: "LbPjdpcgrksZ")]
        public string AisleLocation { get { return masterWarehouse.AisleLocation; } set { masterWarehouse.AisleLocation = value; } }

        [FwLogicProperty(Id: "fSct5X0OLRkb")]
        public string ShelfLocation { get { return masterWarehouse.ShelfLocation; } set { masterWarehouse.ShelfLocation = value; } }

        [FwLogicProperty(Id: "0oTWLVTaJfa7")]
        public bool? AvailabilityByHour { get { return masterWarehouse.AvailabilityByHour; } set { masterWarehouse.AvailabilityByHour = value; } }

        [FwLogicProperty(Id: "QF13QowS0hHM")]
        public bool? AvailabilityByDeal { get { return masterWarehouse.AvailabilityByDeal; } set { masterWarehouse.AvailabilityByDeal = value; } }

        [FwLogicProperty(Id: "8h0UprimGOLu")]
        public bool? AvailabilityByAsset { get { return masterWarehouse.AvailabilityByAsset; } set { masterWarehouse.AvailabilityByAsset = value; } }

        [FwLogicProperty(Id: "HeqOyCt2zhA7")]
        public bool? QcRequired { get { return masterWarehouse.QcRequired; } set { masterWarehouse.QcRequired = value; } }

        [FwLogicProperty(Id: "gCd611Iw16O2")]
        public int? AvailabilityQcDelay { get { return masterWarehouse.AvailabilityQcDelay; } set { masterWarehouse.AvailabilityQcDelay = value; } }

        [FwLogicProperty(Id: "IPBggukwKwKk")]
        public bool? AllowAllUsersAddToOrder { get { return masterWarehouse.AllowAllUsersAddToOrder; } set { masterWarehouse.AllowAllUsersAddToOrder = value; } }

        [FwLogicProperty(Id: "IGu61XAFP8WiH")]
        public decimal? UnitValue { get { return masterWarehouse.UnitValue; } set { masterWarehouse.UnitValue = value; } }

        [FwLogicProperty(Id: "dqjGqmmSwBFqn")]
        public decimal? ReplacementCost { get { return masterWarehouse.ReplacementCost; } set { masterWarehouse.ReplacementCost = value; } }

        [FwLogicProperty(Id: "C2sCuHzswgRvN", IsReadOnly: true)]
        public bool? MarkupReplacementCost { get; set; }

        [FwLogicProperty(Id: "x1svhuYzTnIih", IsReadOnly: true)]
        public decimal? ReplacementCostMarkupPercent { get; set; }



        //------------------------------------------------------------------------------------
    }

}
