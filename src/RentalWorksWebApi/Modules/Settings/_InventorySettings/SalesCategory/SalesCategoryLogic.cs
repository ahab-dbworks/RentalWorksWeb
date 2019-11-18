using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using WebApi.Modules.Settings.Category;
using WebLibrary;

namespace WebApi.Modules.Settings.SalesCategory
{
    public class SalesCategoryLogic : CategoryLogic
    {
        //------------------------------------------------------------------------------------
        SalesCategoryLoader inventoryCategoryLoader = new SalesCategoryLoader();
        public SalesCategoryLogic()
        {
            dataLoader = inventoryCategoryLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"F5ysPS8EvuSp")]
        public string InventoryTypeId { get { return inventoryCategory.TypeId; } set { inventoryCategory.TypeId = value; } }

        [FwLogicProperty(Id:"NdCDPJSkb9VlM", IsReadOnly:true)]
        public string InventoryType { get; set; }



        [FwLogicProperty(Id:"11jsxbbilJcu")]
        public bool? SubsRequireQc { get { return inventoryCategory.SubsRequireQc; } set { inventoryCategory.SubsRequireQc = value; } }

        [FwLogicProperty(Id:"xrqiDcfA1zZP")]
        public bool? AllCategoryItemsAreSubstitutes { get { return inventoryCategory.AllCategoryItemsAreSubstitutes; } set { inventoryCategory.AllCategoryItemsAreSubstitutes = value; } }

        [FwLogicProperty(Id:"DfdHrgkUbMbd")]
        public bool? BarCodePrintUseDesigner { get { return inventoryCategory.BarCodePrintUseDesigner; } set { inventoryCategory.BarCodePrintUseDesigner = value; } }

        [FwLogicProperty(Id:"MxrNxg3npI4O")]
        public string InventoryBarCodeDesignerId { get { return inventoryCategory.InventoryBarCodeDesignerId; } set { inventoryCategory.InventoryBarCodeDesignerId = value; } }

        [FwLogicProperty(Id:"urNhOJsGEYtLh", IsReadOnly:true)]
        public string InventoryBarCodeDesigner { get; set; }

        [FwLogicProperty(Id:"JMkLjf9RfJea")]
        public string BarCodeDesignerId { get { return inventoryCategory.BarCodeDesignerId; } set { inventoryCategory.BarCodeDesignerId = value; } }

        [FwLogicProperty(Id:"urNhOJsGEYtLh", IsReadOnly:true)]
        public string BarCodeDesigner { get; set; }


        [FwLogicProperty(Id:"XiwZA8CloAtw")]
        public string BarCodeType { get { return inventoryCategory.BarCodeType; } set { inventoryCategory.BarCodeType = value; } }


        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            RecType = RwConstants.INVENTORY_AVAILABLE_FOR_SALE;
        }
        //------------------------------------------------------------------------------------    
    }

}
