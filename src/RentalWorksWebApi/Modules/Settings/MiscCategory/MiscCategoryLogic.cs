using RentalWorksWebApi.Modules.Settings.InventoryCategory;

namespace RentalWorksWebApi.Modules.Settings.MiscCategory
{
    public class MiscCategoryLogic : InventoryCategoryLogic
    {
        //------------------------------------------------------------------------------------
        MiscCategoryLoader inventoryCategoryLoader = new MiscCategoryLoader();
        public MiscCategoryLogic()
        {
            dataLoader = inventoryCategoryLoader;
        }
        //------------------------------------------------------------------------------------


        public bool DiscountCategoryItems100PercentByDefault { get { return inventoryCategory.DiscountCategoryItems100PercentByDefault; } set { inventoryCategory.DiscountCategoryItems100PercentByDefault = value; } }
        public bool ExcludeCategoryItemsFromInvoicing { get { return inventoryCategory.ExcludeCategoryItemsFromInvoicing; } set { inventoryCategory.ExcludeCategoryItemsFromInvoicing = value; } }
        public override void BeforeSave()
        {
            RecType = "M";
        }
        //------------------------------------------------------------------------------------    
    }

}
