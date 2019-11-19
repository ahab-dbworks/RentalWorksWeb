using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using WebApi.Modules.Settings.Category;

namespace WebApi.Modules.Settings.MiscellaneousSettings.MiscCategory
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
        [FwLogicProperty(Id:"YKKxv6ROQ3i")]
        public string MiscTypeId { get { return inventoryCategory.TypeId; } set { inventoryCategory.TypeId = value; } }

        [FwLogicProperty(Id:"131qN6SIEVId", IsReadOnly:true)]
        public string MiscType { get; set; }



        [FwLogicProperty(Id:"XmmbobroA57")]
        public bool? DiscountCategoryItems100PercentByDefault { get { return inventoryCategory.DiscountCategoryItems100PercentByDefault; } set { inventoryCategory.DiscountCategoryItems100PercentByDefault = value; } }

        [FwLogicProperty(Id:"rH2IdMkLU9Q")]
        public bool? ExcludeCategoryItemsFromInvoicing { get { return inventoryCategory.ExcludeCategoryItemsFromInvoicing; } set { inventoryCategory.ExcludeCategoryItemsFromInvoicing = value; } }

        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            RecType = "M";
        }
        //------------------------------------------------------------------------------------    
    }

}
