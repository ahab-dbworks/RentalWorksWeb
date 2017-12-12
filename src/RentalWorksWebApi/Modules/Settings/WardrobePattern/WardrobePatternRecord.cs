using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.WardrobePattern
{
    [FwSqlTable("pattern")]
    public class WardrobePatternRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "patternid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string WardrobePatternId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string WardrobePattern { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
