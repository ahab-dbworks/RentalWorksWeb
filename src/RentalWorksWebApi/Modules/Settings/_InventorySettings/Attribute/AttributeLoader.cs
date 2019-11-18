using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.Attribute
{
    [FwSqlTable("attributeview")]
    public class AttributeLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attributeid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string AttributeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attribute", modeltype: FwDataTypes.Text)]
        public string Attribute { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "numericonly", modeltype: FwDataTypes.Boolean)]
        public bool? NumericOnly { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "valuecount", modeltype: FwDataTypes.Integer)]
        public int? ValueCount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            if (GetUniqueIdAsBoolean("HasValues", request).GetValueOrDefault(false))
            {
                select.AddWhere("(valuecount > 0)");
            }

        }
        //------------------------------------------------------------------------------------    
    }
}
