using FwStandard.BusinessLogic.Attributes;
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
        }
        //------------------------------------------------------------------------------------
        public string InventoryTypeId { get { return inventoryCategory.TypeId; } set { inventoryCategory.TypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryType { get; set; }


        public bool? SubsRequireQc { get { return inventoryCategory.SubsRequireQc; } set { inventoryCategory.SubsRequireQc = value; } }
        public bool? AllCategoryItemsAreSubstitutes { get { return inventoryCategory.AllCategoryItemsAreSubstitutes; } set { inventoryCategory.AllCategoryItemsAreSubstitutes = value; } }
        public bool? BarCodePrintUseDesigner { get { return inventoryCategory.BarCodePrintUseDesigner; } set { inventoryCategory.BarCodePrintUseDesigner = value; } }
        public string InventoryBarCodeDesignerId { get { return inventoryCategory.InventoryBarCodeDesignerId; } set { inventoryCategory.InventoryBarCodeDesignerId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryBarCodeDesigner { get; set; }
        public string BarCodeDesignerId { get { return inventoryCategory.BarCodeDesignerId; } set { inventoryCategory.BarCodeDesignerId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BarCodeDesigner { get; set; }

        public string BarCodeType { get { return inventoryCategory.BarCodeType; } set { inventoryCategory.BarCodeType = value; } }

        public override void BeforeSave()
        {
            RecType = "P";
        }
        //------------------------------------------------------------------------------------    
    }

}
