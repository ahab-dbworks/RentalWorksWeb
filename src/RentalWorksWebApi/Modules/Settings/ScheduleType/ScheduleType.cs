using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.ScheduleType
{
    [FwSqlTable("scheduletype")]
    public class ScheduleTypeRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "scheduletypeid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string ScheduleTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "scheduletype", dataType: FwDataTypes.Text, length: 12)]
        public string ScheduleType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
