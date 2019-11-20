using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.InventorySettings.InventoryAdjustmentReason
{
    [FwLogic(Id:"bp52rBlHYWEL")]
    public class InventoryAdjustmentReasonLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryAdjustmentReasonRecord inventoryAdjustmentReason = new InventoryAdjustmentReasonRecord();
        public InventoryAdjustmentReasonLogic()
        {
            dataRecords.Add(inventoryAdjustmentReason);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"HGJBOznLigwx", IsPrimaryKey:true)]
        public string InventoryAdjustmentReasonId { get { return inventoryAdjustmentReason.InventoryAdjustmentReasonId; } set { inventoryAdjustmentReason.InventoryAdjustmentReasonId = value; } }

        [FwLogicProperty(Id:"HGJBOznLigwx", IsRecordTitle:true)]
        public string InventoryAdjustmentReason { get { return inventoryAdjustmentReason.InventoryAdjustmentReason; } set { inventoryAdjustmentReason.InventoryAdjustmentReason = value; } }

        [FwLogicProperty(Id:"jfrJUXVobpC")]
        public bool? Inactive { get { return inventoryAdjustmentReason.Inactive; } set { inventoryAdjustmentReason.Inactive = value; } }

        [FwLogicProperty(Id:"eI5CsFvnxRr")]
        public string DateStamp { get { return inventoryAdjustmentReason.DateStamp; } set { inventoryAdjustmentReason.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
