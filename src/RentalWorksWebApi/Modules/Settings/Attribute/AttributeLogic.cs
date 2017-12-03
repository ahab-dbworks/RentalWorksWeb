using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.Attribute
{
    public class AttributeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        AttributeRecord attribute = new AttributeRecord();
        AttributeLoader attributeLoader = new AttributeLoader();
        public AttributeLogic()
        {
            dataRecords.Add(attribute);
            dataLoader = attributeLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string AttributeId { get { return attribute.AttributeId; } set { attribute.AttributeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Attribute { get { return attribute.Attribute; } set { attribute.Attribute = value; } }
        public string InventoryTypeId { get { return attribute.InventoryTypeId; } set { attribute.InventoryTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryType { get; set; }
        public bool? NumericOnly { get { return attribute.NumericOnly; } set { attribute.NumericOnly = value; } }
        public bool? Inactive { get { return attribute.Inactive; } set { attribute.Inactive = value; } }
        public string DateStamp { get { return attribute.DateStamp; } set { attribute.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
