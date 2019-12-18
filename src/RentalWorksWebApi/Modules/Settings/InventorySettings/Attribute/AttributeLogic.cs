using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.InventorySettings.Attribute
{
    [FwLogic(Id:"jZX9RL41g3A")]
    public class WarehouseCatalog : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        AttributeRecord attribute = new AttributeRecord();
        AttributeLoader attributeLoader = new AttributeLoader();
        public WarehouseCatalog()
        {
            dataRecords.Add(attribute);
            dataLoader = attributeLoader;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"l2rVUwScu1v", IsPrimaryKey:true)]
        public string AttributeId { get { return attribute.AttributeId; } set { attribute.AttributeId = value; } }

        [FwLogicProperty(Id:"l2rVUwScu1v", IsRecordTitle:true)]
        public string Attribute { get { return attribute.Attribute; } set { attribute.Attribute = value; } }

        [FwLogicProperty(Id:"uoURQ6afgUFP")]
        public string InventoryTypeId { get { return attribute.InventoryTypeId; } set { attribute.InventoryTypeId = value; } }

        [FwLogicProperty(Id:"LVUDSsIVbwj", IsReadOnly:true)]
        public string InventoryType { get; set; }

        [FwLogicProperty(Id:"M7AtUoU0ARAD")]
        public bool? NumericOnly { get { return attribute.NumericOnly; } set { attribute.NumericOnly = value; } }

        [FwLogicProperty(Id: "TQx32QxokWaXd", IsReadOnly: true)]
        public int? ValueCount { get; set;}

        [FwLogicProperty(Id:"EYun1oi3qPF7")]
        public bool? Inactive { get { return attribute.Inactive; } set { attribute.Inactive = value; } }

        [FwLogicProperty(Id:"MHN1WQdP4F0V")]
        public string DateStamp { get { return attribute.DateStamp; } set { attribute.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
