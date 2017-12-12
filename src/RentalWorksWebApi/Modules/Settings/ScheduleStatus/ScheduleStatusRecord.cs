using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using Newtonsoft.Json;
using WebApi.Data;

namespace WebApi.Modules.Settings.ScheduleStatus
{
    [FwSqlTable("schedulestatus")]
    public class ScheduleStatusRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "schedulestatusid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string ScheduleStatusId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "schedulestatus", modeltype: FwDataTypes.Text, maxlength: 12, required: true)]
        public string ScheduleStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "statusaction", modeltype: FwDataTypes.Text, maxlength: 12, required: true)]
        public string StatusAction { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text, maxlength: 1, required: true)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.OleToHtmlColor)]
        public string Color { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "whitetext", modeltype: FwDataTypes.Boolean)]
        public bool? WhiteText { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
