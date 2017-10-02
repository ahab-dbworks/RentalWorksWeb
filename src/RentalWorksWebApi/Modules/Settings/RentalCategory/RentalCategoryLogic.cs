using RentalWorksWebApi.Modules.Settings.InventoryCategory;

namespace RentalWorksWebApi.Modules.Settings.RentalCategory
{
    public class RentalCategoryLogic : InventoryCategoryLogic
    {
        //------------------------------------------------------------------------------------
        RentalCategoryLoader inventoryCategoryLoader = new RentalCategoryLoader();
        public RentalCategoryLogic()
        {
            dataLoader = inventoryCategoryLoader;
        }
        //------------------------------------------------------------------------------------


        public bool SubsRequireQc { get { return inventoryCategory.SubsRequireQc; } set { inventoryCategory.SubsRequireQc = value; } }
        public bool AllCategoryItemsAreSubstitutes { get { return inventoryCategory.AllCategoryItemsAreSubstitutes; } set { inventoryCategory.AllCategoryItemsAreSubstitutes = value; } }
        public bool BarCodePrintUseDesigner { get { return inventoryCategory.BarCodePrintUseDesigner; } set { inventoryCategory.BarCodePrintUseDesigner = value; } }
        public string BarCodeType { get { return inventoryCategory.BarCodeType; } set { inventoryCategory.BarCodeType = value; } }
        public bool ScheduleItems { get { return inventoryCategory.ScheduleItems; } set { inventoryCategory.ScheduleItems = value; } }
        public bool HasMaintenance { get { return inventoryCategory.HasMaintenance; } set { inventoryCategory.HasMaintenance = value; } }
        public string PreventiveMaintenanceCycle { get { return inventoryCategory.PreventiveMaintenanceCycle; } set { inventoryCategory.PreventiveMaintenanceCycle = value; } }
        public int? PreventiveMaintenanceCyclePeriod { get { return inventoryCategory.PreventiveMaintenanceCyclePeriod; } set { inventoryCategory.PreventiveMaintenanceCyclePeriod = value; } }
        public int? DepreciationMonths { get { return inventoryCategory.DepreciationMonths; } set { inventoryCategory.DepreciationMonths = value; } }


        public override void BeforeSave()
        {
            RecType = "R";
        }
        //------------------------------------------------------------------------------------    
    }

}
