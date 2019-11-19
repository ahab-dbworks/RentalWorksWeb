using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.UserSettings.UserStatus
{
    [FwSqlTable("userresourcestatusview")]
    public class UserStatusLoader: AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "resourcestatusid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string UserStatusId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "resourcestatus", modeltype: FwDataTypes.Text)]
        public string UserStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "assignable", modeltype: FwDataTypes.Boolean)]
        public bool? AvailableToSchedule { get; set; }
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
