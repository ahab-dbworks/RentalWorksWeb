using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.WardrobeCare
{
    [FwSqlTable("wardrobecare")]
    public class WardrobeCareRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "wardrobecareid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string WardrobeCareId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "wardrobecare", modeltype: FwDataTypes.Text, maxlength: 255, required: true)]
        public string WardrobeCare { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
