using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using WebApi.Modules.Settings.Category;

namespace WebApi.Modules.Settings.LaborSettings.LaborCategory
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
        [FwLogicProperty(Id:"bDiK45ZC4iF")]
        public string LaborTypeId { get { return inventoryCategory.TypeId; } set { inventoryCategory.TypeId = value; } }

        [FwLogicProperty(Id:"9KQQxvobLS6a", IsReadOnly:true)]
        public string LaborType { get; set; }



        [FwLogicProperty(Id:"S4EadeCjaMQ")]
        public bool? DiscountCategoryItems100PercentByDefault { get { return inventoryCategory.DiscountCategoryItems100PercentByDefault; } set { inventoryCategory.DiscountCategoryItems100PercentByDefault = value; } }

        [FwLogicProperty(Id:"dgoXZTlxZY4")]
        public bool? ExcludeCategoryItemsFromInvoicing { get { return inventoryCategory.ExcludeCategoryItemsFromInvoicing; } set { inventoryCategory.ExcludeCategoryItemsFromInvoicing = value; } }

        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            RecType = "L";
        }
        //------------------------------------------------------------------------------------    
    }

}
