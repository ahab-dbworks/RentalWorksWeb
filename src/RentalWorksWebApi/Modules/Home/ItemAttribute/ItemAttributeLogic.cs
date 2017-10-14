using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Home.ItemAttribute
{
    public class ItemAttributeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ItemAttributeRecord itemAttribute = new ItemAttributeRecord();
        ItemAttributeLoader itemAttributeLoader = new ItemAttributeLoader();
        public ItemAttributeLogic()
        {
            dataRecords.Add(itemAttribute);
            dataLoader = itemAttributeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ItemAttributeId { get { return itemAttribute.ItemAttributeId; } set { itemAttribute.ItemAttributeId = value; } }
        public string AttributeId { get { return itemAttribute.AttributeId; } set { itemAttribute.AttributeId = value; } }
        public string ItemId { get { return itemAttribute.ItemId; } set { itemAttribute.ItemId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Attribute { get; set; }
        public string AttributeValueId { get { return itemAttribute.AttributeValueId; } set { itemAttribute.AttributeValueId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AttributeValue { get; set; }
        public decimal? NumericValue { get { return itemAttribute.NumericValue; } set { itemAttribute.NumericValue = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool NumericOnly { get; set; }
        public string DateStamp { get { return itemAttribute.DateStamp; } set { itemAttribute.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}