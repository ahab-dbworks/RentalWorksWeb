using RentalWorksWebApi.Modules.Settings.InventoryCategory;

namespace RentalWorksWebApi.Modules.Settings.SalesCategory
{
    public class SalesCategoryLogic : InventoryCategoryLogic
    {
        //------------------------------------------------------------------------------------
        SalesCategoryLoader inventoryCategoryLoader = new SalesCategoryLoader();
        public SalesCategoryLogic()
        {
            dataLoader = inventoryCategoryLoader;
        }
        //------------------------------------------------------------------------------------


        public bool SubsRequireQc { get { return inventoryCategory.SubsRequireQc; } set { inventoryCategory.SubsRequireQc = value; } }
        public bool AllCategoryItemsAreSubstitutes { get { return inventoryCategory.AllCategoryItemsAreSubstitutes; } set { inventoryCategory.AllCategoryItemsAreSubstitutes = value; } }
        public bool BarCodePrintUseDesigner { get { return inventoryCategory.BarCodePrintUseDesigner; } set { inventoryCategory.BarCodePrintUseDesigner = value; } }
        public string BarCodeType { get { return inventoryCategory.BarCodeType; } set { inventoryCategory.BarCodeType = value; } }

        public override void BeforeSave()
        {
            RecType = "S";
        }
        //------------------------------------------------------------------------------------    
    }

}
