using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Home.InventoryCompatible
{
    public class InventoryCompatibleLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryCompatibleRecord inventoryCompatible = new InventoryCompatibleRecord();
        InventoryCompatibleLoader inventoryCompatibleLoader = new InventoryCompatibleLoader();
        public InventoryCompatibleLogic()
        {
            dataRecords.Add(inventoryCompatible);
            dataLoader = inventoryCompatibleLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryCompatibleId { get { return inventoryCompatible.InventoryCompatibleId; } set { inventoryCompatible.InventoryCompatibleId = value; } }
        public string InventoryId { get { return inventoryCompatible.InventoryId; } set { inventoryCompatible.InventoryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        public string CompatibleWithInventoryId { get { return inventoryCompatible.CompatibleWithInventoryId; } set { inventoryCompatible.CompatibleWithInventoryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CompatibleWithICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CompatibleWithDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CompatibleWithClassification { get; set; }
        public string DateStamp { get { return inventoryCompatible.DateStamp; } set { inventoryCompatible.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}