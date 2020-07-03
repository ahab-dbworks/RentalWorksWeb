using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.UserProfile
{
    [FwSqlTable("webusersview")]
    public class UserProfileLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webusersid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string UserId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "username", modeltype: FwDataTypes.Text)]
        public string UserName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "browsedefaultrows", modeltype: FwDataTypes.Integer)]
        public int? BrowseDefaultRows { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "applicationtheme", modeltype: FwDataTypes.Text)]
        public string ApplicationTheme { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "homemenuguid", modeltype: FwDataTypes.Text)]
        public string HomeMenuGuid { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "homemenupath", modeltype: FwDataTypes.Text)]
        public string HomeMenuPath { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "successsoundid", modeltype: FwDataTypes.Text)]
        public string SuccessSoundId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "successsound", modeltype: FwDataTypes.Text)]
        public string SuccessSound { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "successbase64sound", modeltype: FwDataTypes.Text)]
        public string SuccessBase64Sound { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "errorsoundid", modeltype: FwDataTypes.Text)]
        public string ErrorSoundId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "errorsound", modeltype: FwDataTypes.Text)]
        public string ErrorSound { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "errorbase64sound", modeltype: FwDataTypes.Text)]
        public string ErrorBase64Sound { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notificationsoundid", modeltype: FwDataTypes.Text)]
        public string NotificationSoundId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notificationsound", modeltype: FwDataTypes.Text)]
        public string NotificationSound { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notificationbase64sound", modeltype: FwDataTypes.Text)]
        public string NotificationBase64Sound { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "toolbarjson", modeltype: FwDataTypes.Text)]
        public string FavoritesJson { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "firstdayofweek", modeltype: FwDataTypes.Integer)]
        public int? FirstDayOfWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
        }
        //------------------------------------------------------------------------------------ 
    }
}