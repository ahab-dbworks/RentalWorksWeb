using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using WebApi.Logic;

namespace WebApi.Modules.Settings.InventorySettings.InventoryType
{
    [FwLogic(Id:"4Wwwk5wRJyO2")]
    public class InventoryTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryTypeRecord inventoryType = new InventoryTypeRecord();
        InventoryTypeLoader inventoryTypeLoader = new InventoryTypeLoader();
        public InventoryTypeLogic()
        {
            dataRecords.Add(inventoryType);
            dataLoader = inventoryTypeLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"vsKR64DwOycA", IsPrimaryKey:true)]
        public string InventoryTypeId { get { return inventoryType.InventoryTypeId; } set { inventoryType.InventoryTypeId = value; } }

        [FwLogicProperty(Id:"vsKR64DwOycA", IsRecordTitle:true)]
        public string InventoryType { get { return inventoryType.InventoryType; } set { inventoryType.InventoryType = value; } }

        [FwLogicProperty(Id:"RLM6ydWvT3f")]
        public bool? Rental { get { return inventoryType.Rental; } set { inventoryType.Rental = value; } }

        [FwLogicProperty(Id:"DM9gJBpWaHF")]
        public bool? Sales { get { return inventoryType.Sales; } set { inventoryType.Sales = value; } }

        [FwLogicProperty(Id:"bIMffWSYTnS")]
        public bool? Parts { get { return inventoryType.Parts; } set { inventoryType.Parts = value; } }

        [FwLogicProperty(Id:"aq0VtbGTSwL")]
        public bool? Sets { get { return inventoryType.Sets; } set { inventoryType.Sets = value; } }

        [FwLogicProperty(Id:"cDKvEMU5eIW")]
        public bool? Props { get { return inventoryType.Props; } set { inventoryType.Props = value; } }

        [FwLogicProperty(Id:"S2Zen1yFmI5")]
        public bool? Wardrobe { get { return inventoryType.Wardrobe; } set { inventoryType.Wardrobe = value; } }

        [FwLogicProperty(Id:"ayYAG2E9v2W")]
        public bool? Transportation { get { return inventoryType.Transportation; } set { inventoryType.Transportation = value; } }

        [FwLogicProperty(Id:"uPbKsIO7lW7")]
        public int? LowAvailabilityPercent { get { return inventoryType.LowAvailabilityPercent; } set { inventoryType.LowAvailabilityPercent = value; } }

        [FwLogicProperty(Id:"rjbDXxqaT80")]
        public int? BarCodePrintQty { get { return inventoryType.BarCodePrintQty; } set { inventoryType.BarCodePrintQty = value; } }

        [FwLogicProperty(Id:"E8DuOtLTevb")]
        public bool? BarCodePrintUseDesigner { get { return inventoryType.BarCodePrintUseDesigner; } set { inventoryType.BarCodePrintUseDesigner = value; } }

        [FwLogicProperty(Id:"DXzNo8hn1cF")]
        public bool? GroupProfitLoss { get { return inventoryType.GroupProfitLoss; } set { inventoryType.GroupProfitLoss = value; } }

        [FwLogicProperty(Id:"fI6ggQWY1QS6", IsReadOnly:true)]
        public int? CategoryCount { get; set; }

        [FwLogicProperty(Id:"SwsiiWPFjrH")]
        public bool? Inactive { get { return inventoryType.Inactive; } set { inventoryType.Inactive = value; } }

        [FwLogicProperty(Id:"vHKw5zclpVU")]
        public string DateStamp { get { return inventoryType.DateStamp; } set { inventoryType.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            bool rental = (Rental.HasValue ? ((bool)Rental) : false);
            bool sales = (Sales.HasValue ? ((bool)Sales) : false);
            bool parts = (Parts.HasValue ? ((bool)Parts) : false);
            bool sets = (Sets.HasValue ? ((bool)Sets) : false);
            bool props = (Props.HasValue ? ((bool)Props) : false);
            bool wardrobe = (Wardrobe.HasValue ? ((bool)Wardrobe) : false);

            if ((!rental) && (!sales) && (!parts) && (!sets) && (!props) && (!wardrobe))
            {
                Rental = true;
            }
        }
        //------------------------------------------------------------------------------------
    }

}
