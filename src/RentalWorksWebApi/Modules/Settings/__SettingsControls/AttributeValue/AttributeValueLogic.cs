using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.AttributeValue
{
    [FwLogic(Id:"2Rm5ntQo9Om")]
    public class AttributeValueLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        AttributeValueRecord attributeValue = new AttributeValueRecord();
        AttributeValueLoader attributeValueLoader = new AttributeValueLoader();
        public AttributeValueLogic()
        {
            dataRecords.Add(attributeValue);
            dataLoader = attributeValueLoader;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"8eA1h7iJKfh", IsPrimaryKey:true)]
        public string AttributeValueId { get { return attributeValue.AttributeValueId; } set { attributeValue.AttributeValueId = value; } }

        [FwLogicProperty(Id:"8eA1h7iJKfh", IsRecordTitle:true)]
        public string AttributeValue { get { return attributeValue.AttributeValue; } set { attributeValue.AttributeValue = value; } }

        [FwLogicProperty(Id:"AW6s9dTyetDd")]
        public string AttributeId { get { return attributeValue.AttributeId; } set { attributeValue.AttributeId = value; } }

        [FwLogicProperty(Id:"8eA1h7iJKfh", IsRecordTitle:true, IsReadOnly:true)]
        public string Attribute { get; set; }

        [FwLogicProperty(Id:"7NJ2MkO6ICGE")]
        public string InventoryTypeId { get; set; }

        [FwLogicProperty(Id:"4H0WYyaQSSa", IsReadOnly:true)]
        public string InventoryType { get; set; }

        [FwLogicProperty(Id:"9tT19lC0sXu", IsReadOnly:true)]
        public bool? NumericOnly { get; set; }

        [FwLogicProperty(Id:"2koRaNC7XH6", IsReadOnly:true)]
        public bool? Inactive { get; set; }

        [FwLogicProperty(Id:"d4FVDVOZMF3D")]
        public string DateStamp { get { return attributeValue.DateStamp; } set { attributeValue.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
