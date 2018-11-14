using FwStandard.AppManager;
using WebApi.Logic;
using WebApi.Modules.Home.ItemAttributeValue;

namespace WebApi.Modules.Home.InventoryAttributeValue
{
    [FwLogic(Id:"IuKD3Wr5bZVl")]
    public class InventoryAttributeValueLogic : AppBusinessLogic
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
        [FwLogicProperty(Id:"wx54Judp7zfW", IsPrimaryKey:true)]
        public string InventoryAttributeValueId { get { return itemAttribute.ItemAttributeValueId; } set { itemAttribute.ItemAttributeValueId = value; } }

        [FwLogicProperty(Id:"7bVkdIqzvoR4")]
        public string AttributeId { get { return itemAttribute.AttributeId; } set { itemAttribute.AttributeId = value; } }

        [FwLogicProperty(Id:"RLlSzXVEMApi")]
        public string InventoryId { get { return itemAttribute.UniqueId; } set { itemAttribute.UniqueId = value; } }

        [FwLogicProperty(Id:"wx54Judp7zfW", IsReadOnly:true)]
        public string Attribute { get; set; }

        [FwLogicProperty(Id:"2kKlV1XruAh5")]
        public string AttributeValueId { get { return itemAttribute.AttributeValueId; } set { itemAttribute.AttributeValueId = value; } }

        [FwLogicProperty(Id:"wx54Judp7zfW", IsReadOnly:true)]
        public string AttributeValue { get; set; }

        [FwLogicProperty(Id:"LGq5cZ1ywfUw")]
        public decimal? NumericValue { get { return itemAttribute.NumericValue; } set { itemAttribute.NumericValue = value; } }

        [FwLogicProperty(Id:"ujJfSCZiOzDQ", IsReadOnly:true)]
        public bool? NumericOnly { get; set; }

        [FwLogicProperty(Id:"TP6oAM97a9NJ")]
        public string DateStamp { get { return itemAttribute.DateStamp; } set { itemAttribute.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
