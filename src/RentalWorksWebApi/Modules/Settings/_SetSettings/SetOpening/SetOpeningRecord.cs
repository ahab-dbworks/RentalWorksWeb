using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.SetOpening
{
    [FwSqlTable("opening")]
    public class SetOpeningRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "openingid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string SetOpeningId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "opening", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string SetOpening { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
