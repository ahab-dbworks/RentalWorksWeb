using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.InventoryGroupInventory
{
    public class InventoryGroupInventoryLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryGroupInventoryRecord inventoryGroupInventory = new InventoryGroupInventoryRecord();
        InventoryGroupInventoryLoader inventoryGroupInventoryLoader = new InventoryGroupInventoryLoader();
        public InventoryGroupInventoryLogic()
        {
            dataRecords.Add(inventoryGroupInventory);
            dataLoader = inventoryGroupInventoryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryGroupId { get { return inventoryGroupInventory.InventoryGroupId; } set { inventoryGroupInventory.InventoryGroupId = value; } }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryId { get { return inventoryGroupInventory.InventoryId; } set { inventoryGroupInventory.InventoryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Category { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Rank { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? OrderBy { get; set; }
        public string ConsignorId { get { return inventoryGroupInventory.ConsignorId; } set { inventoryGroupInventory.ConsignorId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Consignor { get; set; }
        public string DateStamp { get { return inventoryGroupInventory.DateStamp; } set { inventoryGroupInventory.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}