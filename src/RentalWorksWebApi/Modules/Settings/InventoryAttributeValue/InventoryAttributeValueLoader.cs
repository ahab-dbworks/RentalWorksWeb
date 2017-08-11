using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.InventoryAttributeValue
{
    [FwSqlTable("attributevalueview")]
    public class InventoryAttributeValueLoader: RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attributevalueid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InventoryAttributeValueId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attributevalue", modeltype: FwDataTypes.Text)]
        public string InventoryAttributeValue { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attributeid", modeltype: FwDataTypes.Text)]
        public string InventoryAttributeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attribute", modeltype: FwDataTypes.Text)]
        public string InventoryAttribute { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "numericonly", modeltype: FwDataTypes.Boolean)]
        public bool NumericOnly { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
