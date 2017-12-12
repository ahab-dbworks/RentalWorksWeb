using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.PoImportance
{
    [FwSqlTable("poimportance")]
    public class PoImportanceRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "poimportanceid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string PoImportanceId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "poimportance", modeltype: FwDataTypes.Text, maxlength: 40, required: true)]
        public string PoImportance { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactiveflg", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
