using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.InventoryAttribute
{
    [FwSqlTable("attributeview")]
    public class InventoryAttributeLoader: RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attributeid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InventoryAttributeId { get; set; } = "";
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
