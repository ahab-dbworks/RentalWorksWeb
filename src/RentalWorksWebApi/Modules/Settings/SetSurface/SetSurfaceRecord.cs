using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.SetSurface
{
    [FwSqlTable("surface")]
    public class SetSurfaceRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "surfaceid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string SetSurfaceId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "surface", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string SetSurface { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
