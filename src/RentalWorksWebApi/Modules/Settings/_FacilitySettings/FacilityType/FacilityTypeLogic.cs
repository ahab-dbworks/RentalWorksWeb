using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Settings.InventoryType;

namespace WebApi.Modules.Settings.FacilityType
{
    [FwLogic(Id:"nAwu7gO72LKR")]
    public class FacilityTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryTypeRecord inventoryType = new InventoryTypeRecord();
        FacilityTypeLoader inventoryTypeLoader = new FacilityTypeLoader();
        public FacilityTypeLogic()
        {
            dataRecords.Add(inventoryType);
            dataLoader = inventoryTypeLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"A63PDISJ01vn", IsPrimaryKey:true)]
        public string FacilityTypeId { get { return inventoryType.InventoryTypeId; } set { inventoryType.InventoryTypeId = value; } }

        [FwLogicProperty(Id:"A63PDISJ01vn", IsRecordTitle:true)]
        public string FacilityType { get { return inventoryType.InventoryType; } set { inventoryType.InventoryType = value; } }

        [FwLogicProperty(Id:"AZvBPa2IY")]
        public bool? Facilities { get { return inventoryType.Facilities; } set { inventoryType.Facilities = value; } }

        [FwLogicProperty(Id:"rtDzYTD6PH")]
        public bool? StageScheduling { get { return inventoryType.StageScheduling; } set { inventoryType.StageScheduling = value; } }

        [FwLogicProperty(Id:"OTnv8ZytYH")]
        public decimal? FacilitiesDefaultDw { get { return inventoryType.FacilitiesDefaultDw; } set { inventoryType.FacilitiesDefaultDw = value; } }

        [FwLogicProperty(Id:"jdOIgqvpv5")]
        public bool? FacilitiesDoNotDefaultRateOnBooking { get { return inventoryType.FacilitiesDoNotDefaultRateOnBooking; } set { inventoryType.FacilitiesDoNotDefaultRateOnBooking = value; } }

        [FwLogicProperty(Id:"Tf34uwcUAb")]
        public bool? GroupProfitLoss { get { return inventoryType.GroupProfitLoss; } set { inventoryType.GroupProfitLoss = value; } }

        [FwLogicProperty(Id:"oh9uNEnvOI")]
        public bool? Inactive { get { return inventoryType.Inactive; } set { inventoryType.Inactive = value; } }

        [FwLogicProperty(Id:"er9NYVxrEA")]
        public string DateStamp { get { return inventoryType.DateStamp; } set { inventoryType.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            Facilities = true;
        }
        //------------------------------------------------------------------------------------
    }

}
