using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.AttributeValue
{
    public class AttributeValueLogic : RwBusinessLogic
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string AttributeValueId { get { return attributeValue.AttributeValueId; } set { attributeValue.AttributeValueId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string AttributeValue { get { return attributeValue.AttributeValue; } set { attributeValue.AttributeValue = value; } }
        public string AttributeId { get { return attributeValue.AttributeId; } set { attributeValue.AttributeId = value; } }
        [FwBusinessLogicField(isReadOnly: true, isRecordTitle: true)]
        public string Attribute { get; set; }
        public string InventoryTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? NumericOnly { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Inactive { get; set; }
        public string DateStamp { get { return attributeValue.DateStamp; } set { attributeValue.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
