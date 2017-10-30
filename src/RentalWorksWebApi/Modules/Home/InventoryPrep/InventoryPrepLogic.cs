using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Home.InventoryPrep
{
    public class InventoryPrepLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryPrepRecord inventoryPrep = new InventoryPrepRecord();
        InventoryPrepLoader inventoryPrepLoader = new InventoryPrepLoader();
        public InventoryPrepLogic()
        {
            dataRecords.Add(inventoryPrep);
            dataLoader = inventoryPrepLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryPrepId { get { return inventoryPrep.InventoryPrepId; } set { inventoryPrep.InventoryPrepId = value; } }
        public string InventoryId { get { return inventoryPrep.InventoryId; } set { inventoryPrep.InventoryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        public string PrepRateId { get { return inventoryPrep.PrepRateId; } set { inventoryPrep.PrepRateId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PrepICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PrepDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PrepUnit { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PrepUnitType { get; set; }
        public bool IsDefault { get { return inventoryPrep.IsDefault; } set { inventoryPrep.IsDefault = value; } }
        public decimal? PrepRate { get { return inventoryPrep.PrepRate; } set { inventoryPrep.PrepRate = value; } }
        public string PrepTime { get { return inventoryPrep.PrepTime; } set { inventoryPrep.PrepTime = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? PrepExtended { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QtyOrdered { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Price { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool OrderId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool MasteritemId { get; set; }
        public string DateStamp { get { return inventoryPrep.DateStamp; } set { inventoryPrep.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}