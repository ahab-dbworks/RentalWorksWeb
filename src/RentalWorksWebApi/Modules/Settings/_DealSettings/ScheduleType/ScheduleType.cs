using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.DealSettings.ScheduleType
{
    [FwSqlTable("scheduletype")]
    public class ScheduleTypeRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "scheduletypeid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string ScheduleTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "scheduletype", modeltype: FwDataTypes.Text, maxlength: 12)]
        public string ScheduleType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
