using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using WebApi.Modules.Settings.Category;

namespace WebApi.Modules.Settings.InventorySettings.RentalCategory
{
    public class RentalCategoryLogic : CategoryLogic
    {
        //------------------------------------------------------------------------------------
        RentalCategoryLoader inventoryCategoryLoader = new RentalCategoryLoader();
        public RentalCategoryLogic()
        {
            dataLoader = inventoryCategoryLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------

        [FwLogicProperty(Id:"Ue8PWIHEYUeY")]
        public string InventoryTypeId { get { return inventoryCategory.TypeId; } set { inventoryCategory.TypeId = value; } }

        [FwLogicProperty(Id:"HOMRz6tkFodml", IsReadOnly:true)]
        public string InventoryType { get; set; }



        [FwLogicProperty(Id:"XCDI7K51Pf3g")]
        public bool? SubsRequireQc { get { return inventoryCategory.SubsRequireQc; } set { inventoryCategory.SubsRequireQc = value; } }

        [FwLogicProperty(Id:"eGWmol2ZSvCB")]
        public bool? AllCategoryItemsAreSubstitutes { get { return inventoryCategory.AllCategoryItemsAreSubstitutes; } set { inventoryCategory.AllCategoryItemsAreSubstitutes = value; } }

        [FwLogicProperty(Id:"e8E3dWpSl4cI")]
        public bool? BarCodePrintUseDesigner { get { return inventoryCategory.BarCodePrintUseDesigner; } set { inventoryCategory.BarCodePrintUseDesigner = value; } }

        [FwLogicProperty(Id:"OAKtgMPl2DjL")]
        public string InventoryBarCodeDesignerId { get { return inventoryCategory.InventoryBarCodeDesignerId; } set { inventoryCategory.InventoryBarCodeDesignerId = value; } }

        [FwLogicProperty(Id:"8zMxbVDDd0j4b", IsReadOnly:true)]
        public string InventoryBarCodeDesigner { get; set; }

        [FwLogicProperty(Id:"lq2nybLf6qzM")]
        public string BarCodeDesignerId { get { return inventoryCategory.BarCodeDesignerId; } set { inventoryCategory.BarCodeDesignerId = value; } }

        [FwLogicProperty(Id:"8zMxbVDDd0j4b", IsReadOnly:true)]
        public string BarCodeDesigner { get; set; }


        [FwLogicProperty(Id:"TBR5ZAhscogK")]
        public string BarCodeType { get { return inventoryCategory.BarCodeType; } set { inventoryCategory.BarCodeType = value; } }

        [FwLogicProperty(Id:"HTH8N80r53pH")]
        public bool? ScheduleItems { get { return inventoryCategory.ScheduleItems; } set { inventoryCategory.ScheduleItems = value; } }

        [FwLogicProperty(Id:"Rr4ivgpfe9Ue")]
        public bool? HasMaintenance { get { return inventoryCategory.HasMaintenance; } set { inventoryCategory.HasMaintenance = value; } }

        [FwLogicProperty(Id:"v4RK0ntl4GQK")]
        public string PreventiveMaintenanceCycle { get { return inventoryCategory.PreventiveMaintenanceCycle; } set { inventoryCategory.PreventiveMaintenanceCycle = value; } }

        [FwLogicProperty(Id:"keQ2OzKwH9QZ")]
        public int? PreventiveMaintenanceCyclePeriod { get { return inventoryCategory.PreventiveMaintenanceCyclePeriod; } set { inventoryCategory.PreventiveMaintenanceCyclePeriod = value; } }


        //------------------------------------------------------------------------------------    
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            RecType = RwConstants.RECTYPE_RENTAL;
        }
        //------------------------------------------------------------------------------------    
    }

}
