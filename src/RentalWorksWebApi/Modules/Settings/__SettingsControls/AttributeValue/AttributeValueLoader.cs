using System.Threading.Tasks;
using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.AttributeValue
{
    [FwSqlTable("attributevalueview")]
    public class AttributeValueLoader: AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attributevalueid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string AttributeValueId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attributevalue", modeltype: FwDataTypes.Text)]
        public string AttributeValue { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attributeid", modeltype: FwDataTypes.Text)]
        public string AttributeId { get; set; }
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
            //select.AddWhere("attributeid = @attributeid");
            //select.AddParameter("@attributeid", request.miscfields.AttributeId.value);
            AddFilterFieldToSelect("AttributeId", "attributeid", select, request);
            addFilterToSelect("AttributeId", "attributeid", select, request);
        }
        //------------------------------------------------------------------------------------

    }
}
