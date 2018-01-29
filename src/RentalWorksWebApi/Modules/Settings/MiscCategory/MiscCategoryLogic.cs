using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Modules.Settings.Category;

namespace WebApi.Modules.Settings.MiscCategory
{
    public class MiscCategoryLogic : CategoryLogic
    {
        //------------------------------------------------------------------------------------
        MiscCategoryLoader inventoryCategoryLoader = new MiscCategoryLoader();
        public MiscCategoryLogic()
        {
            dataLoader = inventoryCategoryLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------
        public string MiscTypeId { get { return inventoryCategory.TypeId; } set { inventoryCategory.TypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MiscType { get; set; }


        public bool? DiscountCategoryItems100PercentByDefault { get { return inventoryCategory.DiscountCategoryItems100PercentByDefault; } set { inventoryCategory.DiscountCategoryItems100PercentByDefault = value; } }
        public bool? ExcludeCategoryItemsFromInvoicing { get { return inventoryCategory.ExcludeCategoryItemsFromInvoicing; } set { inventoryCategory.ExcludeCategoryItemsFromInvoicing = value; } }
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            RecType = "M";
        }
        //------------------------------------------------------------------------------------    
    }

}
