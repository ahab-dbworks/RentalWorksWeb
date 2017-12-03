using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Home.ItemAttributeValue;

namespace RentalWorksWebApi.Modules.Home.InventoryAttributeValue
{
    public class InventoryAttributeValueLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ItemAttributeValueRecord itemAttribute = new ItemAttributeValueRecord();
        InventoryAttributeValueLoader itemAttributeLoader = new InventoryAttributeValueLoader();
        public InventoryAttributeValueLogic()
        {
            dataRecords.Add(itemAttribute);
            dataLoader = itemAttributeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryAttributeValueId { get { return itemAttribute.ItemAttributeValueId; } set { itemAttribute.ItemAttributeValueId = value; } }
        public string AttributeId { get { return itemAttribute.AttributeId; } set { itemAttribute.AttributeId = value; } }
        public string InventoryId { get { return itemAttribute.UniqueId; } set { itemAttribute.UniqueId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Attribute { get; set; }
        public string AttributeValueId { get { return itemAttribute.AttributeValueId; } set { itemAttribute.AttributeValueId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AttributeValue { get; set; }
        public decimal? NumericValue { get { return itemAttribute.NumericValue; } set { itemAttribute.NumericValue = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? NumericOnly { get; set; }
        public string DateStamp { get { return itemAttribute.DateStamp; } set { itemAttribute.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}