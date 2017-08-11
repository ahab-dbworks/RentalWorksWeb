using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.InventoryAttributeValue
{
    public class InventoryAttributeValueLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        InventoryAttributeValueRecord inventoryAttributeValue = new InventoryAttributeValueRecord();
        InventoryAttributeValueLoader inventoryAttributeValueLoader = new InventoryAttributeValueLoader();
        public InventoryAttributeValueLogic()
        {
            dataRecords.Add(inventoryAttributeValue);
            dataLoader = inventoryAttributeValueLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryAttributeValueId { get { return inventoryAttributeValue.InventoryAttributeValueId; } set { inventoryAttributeValue.InventoryAttributeValueId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string InventoryAttributeValue { get { return inventoryAttributeValue.InventoryAttributeValue; } set { inventoryAttributeValue.InventoryAttributeValue = value; } }
        public string InventoryAttributeId { get { return inventoryAttributeValue.InventoryAttributeId; } set { inventoryAttributeValue.InventoryAttributeId = value; } }
        [FwBusinessLogicField(isReadOnly: true, isRecordTitle: true)]
        public string InventoryAttribute { get; set; }
        public string InventoryTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool NumericOnly { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool Inactive { get; set; }
        public string DateStamp { get { return inventoryAttributeValue.DateStamp; } set { inventoryAttributeValue.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
