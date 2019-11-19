using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.LaborSettings.CrewScheduleStatus
{
    [FwSqlTable("crewschedulestatusview")]
    public class CrewScheduleStatusLoader: AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "schedulestatusid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string ScheduleStatusId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "schedulestatus", modeltype: FwDataTypes.Text)]
        public string ScheduleStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "statusaction", modeltype: FwDataTypes.Text)]
        public string StatusAction { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
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
