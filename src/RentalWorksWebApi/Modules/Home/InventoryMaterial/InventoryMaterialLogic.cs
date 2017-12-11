using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Home.InventoryMaterial
{
    public class InventoryMaterialLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryMaterialRecord inventoryMaterial = new InventoryMaterialRecord();
        InventoryMaterialLoader inventoryMaterialLoader = new InventoryMaterialLoader();
        public InventoryMaterialLogic()
        {
            dataRecords.Add(inventoryMaterial);
            dataLoader = inventoryMaterialLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryMaterialId { get { return inventoryMaterial.InventoryMaterialId; } set { inventoryMaterial.InventoryMaterialId = value; } }
        public string InventoryId { get { return inventoryMaterial.InventoryId; } set { inventoryMaterial.InventoryId = value; } }
        public string MaterialId { get { return inventoryMaterial.MaterialId; } set { inventoryMaterial.MaterialId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        public string DateStamp { get { return inventoryMaterial.DateStamp; } set { inventoryMaterial.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}