using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.InventoryGroup
{
    public class InventoryGroupLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryGroupRecord inventoryGroup = new InventoryGroupRecord();
        public InventoryGroupLogic()
        {
            dataRecords.Add(inventoryGroup);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryGroupId { get { return inventoryGroup.InventoryGroupId; } set { inventoryGroup.InventoryGroupId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string InventoryGroup { get { return inventoryGroup.InventoryGroup; } set { inventoryGroup.InventoryGroup = value; } }
        //public string WarehouseId { get { return inventoryGroup.WarehouseId; } set { inventoryGroup.WarehouseId = value; } }
        public string RecType { get { return inventoryGroup.RecType; } set { inventoryGroup.RecType = value; } }
        //public bool Includeconsigned { get { return inventoryGroup.Includeconsigned; } set { inventoryGroup.Includeconsigned = value; } }
        //public bool Includeowned { get { return inventoryGroup.Includeowned; } set { inventoryGroup.Includeowned = value; } }
        //public bool Ranka { get { return inventoryGroup.Ranka; } set { inventoryGroup.Ranka = value; } }
        //public bool Rankb { get { return inventoryGroup.Rankb; } set { inventoryGroup.Rankb = value; } }
        //public bool Rankc { get { return inventoryGroup.Rankc; } set { inventoryGroup.Rankc = value; } }
        //public bool Rankd { get { return inventoryGroup.Rankd; } set { inventoryGroup.Rankd = value; } }
        //public string Trackedby { get { return inventoryGroup.Trackedby; } set { inventoryGroup.Trackedby = value; } }
        //public string Aisle { get { return inventoryGroup.Aisle; } set { inventoryGroup.Aisle = value; } }
        //public string Shelf { get { return inventoryGroup.Shelf; } set { inventoryGroup.Shelf = value; } }
        public bool Inactive { get { return inventoryGroup.Inactive; } set { inventoryGroup.Inactive = value; } }
        public string DateStamp { get { return inventoryGroup.DateStamp; } set { inventoryGroup.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}