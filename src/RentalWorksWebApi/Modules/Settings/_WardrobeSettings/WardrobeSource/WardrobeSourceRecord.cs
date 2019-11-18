using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.WardrobeSource
{
    [FwSqlTable("wardrobesource")]
    public class WardrobeSourceRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "wardrobesourceid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string WardrobeSourceId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "wardrobesource", modeltype: FwDataTypes.Text, maxlength: 255, required: true)]
        public string WardrobeSource { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
