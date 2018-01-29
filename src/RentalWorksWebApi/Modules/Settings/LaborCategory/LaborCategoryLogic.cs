using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Modules.Settings.Category;

namespace WebApi.Modules.Settings.LaborCategory
{
    public class LaborCategoryLogic : CategoryLogic
    {
        //------------------------------------------------------------------------------------
        LaborCategoryLoader inventoryCategoryLoader = new LaborCategoryLoader();
        public LaborCategoryLogic()
        {
            dataLoader = inventoryCategoryLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------
        public string LaborTypeId { get { return inventoryCategory.TypeId; } set { inventoryCategory.TypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LaborType { get; set; }


        public bool? DiscountCategoryItems100PercentByDefault { get { return inventoryCategory.DiscountCategoryItems100PercentByDefault; } set { inventoryCategory.DiscountCategoryItems100PercentByDefault = value; } }
        public bool? ExcludeCategoryItemsFromInvoicing { get { return inventoryCategory.ExcludeCategoryItemsFromInvoicing; } set { inventoryCategory.ExcludeCategoryItemsFromInvoicing = value; } }
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            RecType = "L";
        }
        //------------------------------------------------------------------------------------    
    }

}
