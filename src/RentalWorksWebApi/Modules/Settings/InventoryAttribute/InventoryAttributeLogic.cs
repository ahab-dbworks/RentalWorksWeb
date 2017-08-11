using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.InventoryAttribute
{
    public class InventoryAttributeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryAttributeRecord inventoryAttribute = new InventoryAttributeRecord();
        InventoryAttributeLoader inventoryAttributeLoader = new InventoryAttributeLoader();
        public InventoryAttributeLogic()
        {
            dataRecords.Add(inventoryAttribute);
            dataLoader = inventoryAttributeLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryAttributeId { get { return inventoryAttribute.InventoryAttributeId; } set { inventoryAttribute.InventoryAttributeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string InventoryAttribute { get { return inventoryAttribute.InventoryAttribute; } set { inventoryAttribute.InventoryAttribute = value; } }
        public string InventoryTypeId { get { return inventoryAttribute.InventoryTypeId; } set { inventoryAttribute.InventoryTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryType { get; set; }
        public bool NumericOnly { get { return inventoryAttribute.NumericOnly; } set { inventoryAttribute.NumericOnly = value; } }
        public bool Inactive { get { return inventoryAttribute.Inactive; } set { inventoryAttribute.Inactive = value; } }
        public string DateStamp { get { return inventoryAttribute.DateStamp; } set { inventoryAttribute.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
