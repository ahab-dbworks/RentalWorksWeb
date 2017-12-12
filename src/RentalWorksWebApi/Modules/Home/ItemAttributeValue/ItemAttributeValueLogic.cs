using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;

namespace WebApi.Modules.Home.ItemAttributeValue
{
    public class ItemAttributeValueLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ItemAttributeValueRecord itemAttribute = new ItemAttributeValueRecord();
        ItemAttributeValueLoader itemAttributeLoader = new ItemAttributeValueLoader();
        public ItemAttributeValueLogic()
        {
            dataRecords.Add(itemAttribute);
            dataLoader = itemAttributeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ItemAttributeValueId { get { return itemAttribute.ItemAttributeValueId; } set { itemAttribute.ItemAttributeValueId = value; } }
        public string AttributeId { get { return itemAttribute.AttributeId; } set { itemAttribute.AttributeId = value; } }
        public string ItemId { get { return itemAttribute.UniqueId; } set { itemAttribute.UniqueId = value; } }
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