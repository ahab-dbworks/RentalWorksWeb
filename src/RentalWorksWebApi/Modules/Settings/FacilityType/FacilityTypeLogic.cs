using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
using WebApi.Modules.Settings.InventoryType;

namespace WebApi.Modules.Settings.FacilityType
{
    public class FacilityTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryTypeRecord inventoryType = new InventoryTypeRecord();
        FacilityTypeLoader inventoryTypeLoader = new FacilityTypeLoader();
        public FacilityTypeLogic()
        {
            dataRecords.Add(inventoryType);
            dataLoader = inventoryTypeLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string FacilityTypeId { get { return inventoryType.InventoryTypeId; } set { inventoryType.InventoryTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string FacilityType { get { return inventoryType.InventoryType; } set { inventoryType.InventoryType = value; } }
        public bool? Facilities { get { return inventoryType.Facilities; } set { inventoryType.Facilities = value; } }
        public bool? StageScheduling { get { return inventoryType.StageScheduling; } set { inventoryType.StageScheduling = value; } }
        public decimal? FacilitiesDefaultDw { get { return inventoryType.FacilitiesDefaultDw; } set { inventoryType.FacilitiesDefaultDw = value; } }
        public bool? FacilitiesDoNotDefaultRateOnBooking { get { return inventoryType.FacilitiesDoNotDefaultRateOnBooking; } set { inventoryType.FacilitiesDoNotDefaultRateOnBooking = value; } }
        public bool? GroupProfitLoss { get { return inventoryType.GroupProfitLoss; } set { inventoryType.GroupProfitLoss = value; } }
        public bool? Inactive { get { return inventoryType.Inactive; } set { inventoryType.Inactive = value; } }
        public string DateStamp { get { return inventoryType.DateStamp; } set { inventoryType.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public override void BeforeSave()
        {
            Facilities = true;
        }
        //------------------------------------------------------------------------------------
    }

}
