using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Home.ItemAttributeValue
{
    [FwLogic(Id:"JxbFFl07vDPS")]
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
        [FwLogicProperty(Id:"oWTdXmNjMNXx", IsPrimaryKey:true)]
        public string ItemAttributeValueId { get { return itemAttribute.ItemAttributeValueId; } set { itemAttribute.ItemAttributeValueId = value; } }

        [FwLogicProperty(Id:"dmkzb33RTd58")]
        public string AttributeId { get { return itemAttribute.AttributeId; } set { itemAttribute.AttributeId = value; } }

        [FwLogicProperty(Id:"7A5KgwRYErEj")]
        public string ItemId { get { return itemAttribute.UniqueId; } set { itemAttribute.UniqueId = value; } }

        [FwLogicProperty(Id:"oWTdXmNjMNXx", IsReadOnly:true)]
        public string Attribute { get; set; }

        [FwLogicProperty(Id:"uEFXoCsyOgny")]
        public string AttributeValueId { get { return itemAttribute.AttributeValueId; } set { itemAttribute.AttributeValueId = value; } }

        [FwLogicProperty(Id:"oWTdXmNjMNXx", IsReadOnly:true)]
        public string AttributeValue { get; set; }

        [FwLogicProperty(Id:"q3RDqN9QAv72")]
        public decimal? NumericValue { get { return itemAttribute.NumericValue; } set { itemAttribute.NumericValue = value; } }

        [FwLogicProperty(Id:"QoKIlCE2GtZZ", IsReadOnly:true)]
        public bool? NumericOnly { get; set; }

        [FwLogicProperty(Id:"EI8nc8OBs0DD")]
        public string DateStamp { get { return itemAttribute.DateStamp; } set { itemAttribute.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
