using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.InventoryAdjustmentReason
{
    public class InventoryAdjustmentReasonLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryAdjustmentReasonRecord inventoryAdjustmentReason = new InventoryAdjustmentReasonRecord();
        public InventoryAdjustmentReasonLogic()
        {
            dataRecords.Add(inventoryAdjustmentReason);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryAdjustmentReasonId { get { return inventoryAdjustmentReason.InventoryAdjustmentReasonId; } set { inventoryAdjustmentReason.InventoryAdjustmentReasonId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string InventoryAdjustmentReason { get { return inventoryAdjustmentReason.InventoryAdjustmentReason; } set { inventoryAdjustmentReason.InventoryAdjustmentReason = value; } }
        public bool Inactive { get { return inventoryAdjustmentReason.Inactive; } set { inventoryAdjustmentReason.Inactive = value; } }
        public string DateStamp { get { return inventoryAdjustmentReason.DateStamp; } set { inventoryAdjustmentReason.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
