using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.AttributeValue
{
    [FwSqlTable("attributevalue")]
    public class AttributeValueRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attributevalueid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string AttributeValueId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attributevalue", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string AttributeValue { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attributeid", modeltype: FwDataTypes.Text, maxlength: 8, required: true)]
        public string AttributeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
