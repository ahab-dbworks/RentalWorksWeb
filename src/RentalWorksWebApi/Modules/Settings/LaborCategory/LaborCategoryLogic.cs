using WebApi.Modules.Settings.InventoryCategory;

namespace WebApi.Modules.Settings.LaborCategory
{
    public class LaborCategoryLogic : InventoryCategoryLogic
    {
        //------------------------------------------------------------------------------------
        LaborCategoryLoader inventoryCategoryLoader = new LaborCategoryLoader();
        public LaborCategoryLogic()
        {
            dataLoader = inventoryCategoryLoader;
        }
        //------------------------------------------------------------------------------------


        public bool? DiscountCategoryItems100PercentByDefault { get { return inventoryCategory.DiscountCategoryItems100PercentByDefault; } set { inventoryCategory.DiscountCategoryItems100PercentByDefault = value; } }
        public bool? ExcludeCategoryItemsFromInvoicing { get { return inventoryCategory.ExcludeCategoryItemsFromInvoicing; } set { inventoryCategory.ExcludeCategoryItemsFromInvoicing = value; } }
        public override void BeforeSave()
        {
            RecType = "L";
        }
        //------------------------------------------------------------------------------------    
    }

}
