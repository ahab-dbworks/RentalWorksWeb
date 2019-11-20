using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.GeneratorSettings.GeneratorWatts
{
    [FwSqlTable("watts")]
    public class GeneratorWattsRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "wattsid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string GeneratorWattsId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "watts", modeltype: FwDataTypes.Text, maxlength: 15, required: true)]
        public string GeneratorWatts { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
