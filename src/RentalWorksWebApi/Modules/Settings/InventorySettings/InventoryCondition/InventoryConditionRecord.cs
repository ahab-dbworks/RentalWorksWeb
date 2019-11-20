using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.InventorySettings.InventoryCondition
{
    [FwSqlTable("condition")]
    public class InventoryConditionRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "conditionid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string InventoryConditionId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "condition", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string InventoryCondition { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean)]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean)]
        public bool? Sales { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "sets", modeltype: FwDataTypes.Boolean)]
        public bool? Sets { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "props", modeltype: FwDataTypes.Boolean)]
        public bool? Props { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "wardrobe", modeltype: FwDataTypes.Boolean)]
        public bool? Wardrobe { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
