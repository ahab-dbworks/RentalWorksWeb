using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.InventoryAttributeValue
{
    [FwSqlTable("attributevalue")]
    public class InventoryAttributeValueRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attributevalueid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string InventoryAttributeValueId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attributevalue", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string InventoryAttributeValue { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "attributeid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string InventoryAttributeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
