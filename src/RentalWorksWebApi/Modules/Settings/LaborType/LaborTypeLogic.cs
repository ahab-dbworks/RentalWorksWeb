using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
using WebApi.Modules.Settings.InventoryType;

namespace WebApi.Modules.Settings.LaborType
{
    public class LaborTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryTypeRecord inventoryType = new InventoryTypeRecord();
        LaborTypeLoader inventoryTypeLoader = new LaborTypeLoader();
        public LaborTypeLogic()
        {
            dataRecords.Add(inventoryType);
            dataLoader = inventoryTypeLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string LaborTypeId { get { return inventoryType.InventoryTypeId; } set { inventoryType.InventoryTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string LaborType { get { return inventoryType.InventoryType; } set { inventoryType.InventoryType = value; } }
        public bool? Labor { get { return inventoryType.Labor; } set { inventoryType.Labor = value; } }
        public bool? GroupProfitLoss { get { return inventoryType.GroupProfitLoss; } set { inventoryType.GroupProfitLoss = value; } }
        public bool? Inactive { get { return inventoryType.Inactive; } set { inventoryType.Inactive = value; } }
        public string DateStamp { get { return inventoryType.DateStamp; } set { inventoryType.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public override void BeforeSave()
        {
            Labor = true;
        }
        //------------------------------------------------------------------------------------
    }

}
