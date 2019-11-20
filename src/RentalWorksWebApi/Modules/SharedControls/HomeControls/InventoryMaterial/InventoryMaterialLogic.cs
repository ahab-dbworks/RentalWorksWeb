using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.InventoryMaterial
{
    [FwLogic(Id:"v5boASkjV3te")]
    public class InventoryMaterialLogic : AppBusinessLogic
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
        [FwLogicProperty(Id:"wM6f1THKkCnH", IsPrimaryKey:true)]
        public string InventoryMaterialId { get { return inventoryMaterial.InventoryMaterialId; } set { inventoryMaterial.InventoryMaterialId = value; } }

        [FwLogicProperty(Id:"XlUG56JVItjM")]
        public string InventoryId { get { return inventoryMaterial.InventoryId; } set { inventoryMaterial.InventoryId = value; } }

        [FwLogicProperty(Id:"qIGHN1F6QEQ2")]
        public string MaterialId { get { return inventoryMaterial.MaterialId; } set { inventoryMaterial.MaterialId = value; } }

        [FwLogicProperty(Id:"Zeu8roj5U9Ec", IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"cSyEHJG6AFGI")]
        public string DateStamp { get { return inventoryMaterial.DateStamp; } set { inventoryMaterial.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
