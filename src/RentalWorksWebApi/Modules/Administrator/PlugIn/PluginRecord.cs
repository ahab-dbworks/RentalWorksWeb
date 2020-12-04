using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Administrator.Plugin
{
    [FwSqlTable("plugin")]
    public class PluginRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pluginid", modeltype: FwDataTypes.Integer, sqltype: "integer", identity: true, isPrimaryKey: true)]
        public int PluginId { get; set; } = 0;
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "settings", modeltype: FwDataTypes.Text, sqltype: "varchar")]
        public string Settings { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
