using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using WebApi.Modules.Settings.Category;

namespace WebApi.Modules.Settings.PartsCategory
{
    public class PartsCategoryLogic : CategoryLogic
    {
        //------------------------------------------------------------------------------------
        PartsCategoryLoader inventoryCategoryLoader = new PartsCategoryLoader();
        public PartsCategoryLogic()
        {
            dataLoader = inventoryCategoryLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"RrAB3IGYFFDD")]
        public string InventoryTypeId { get { return inventoryCategory.TypeId; } set { inventoryCategory.TypeId = value; } }

        [FwLogicProperty(Id:"hoWXkmSEq8Tt", IsReadOnly:true)]
        public string InventoryType { get; set; }



        [FwLogicProperty(Id:"cbWXfBnkqu4a")]
        public bool? SubsRequireQc { get { return inventoryCategory.SubsRequireQc; } set { inventoryCategory.SubsRequireQc = value; } }

        [FwLogicProperty(Id:"W6uxBV5ZWnk0")]
        public bool? AllCategoryItemsAreSubstitutes { get { return inventoryCategory.AllCategoryItemsAreSubstitutes; } set { inventoryCategory.AllCategoryItemsAreSubstitutes = value; } }

        [FwLogicProperty(Id:"wF5W68NsJw90")]
        public bool? BarCodePrintUseDesigner { get { return inventoryCategory.BarCodePrintUseDesigner; } set { inventoryCategory.BarCodePrintUseDesigner = value; } }

        [FwLogicProperty(Id:"LfJ07ZhQwShU")]
        public string InventoryBarCodeDesignerId { get { return inventoryCategory.InventoryBarCodeDesignerId; } set { inventoryCategory.InventoryBarCodeDesignerId = value; } }

        [FwLogicProperty(Id:"WMX9SOClKIlG", IsReadOnly:true)]
        public string InventoryBarCodeDesigner { get; set; }

        [FwLogicProperty(Id:"VX05BhI3fmxO")]
        public string BarCodeDesignerId { get { return inventoryCategory.BarCodeDesignerId; } set { inventoryCategory.BarCodeDesignerId = value; } }

        [FwLogicProperty(Id:"WMX9SOClKIlG", IsReadOnly:true)]
        public string BarCodeDesigner { get; set; }


        [FwLogicProperty(Id:"EcCCOPjQnrc1")]
        public string BarCodeType { get { return inventoryCategory.BarCodeType; } set { inventoryCategory.BarCodeType = value; } }


        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            RecType = "P";
        }
        //------------------------------------------------------------------------------------    
    }

}
