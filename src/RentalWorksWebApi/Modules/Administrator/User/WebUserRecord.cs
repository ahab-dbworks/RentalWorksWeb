using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Logic;

namespace WebApi.Modules.Administrator.User
{
    [FwSqlTable("webusers")]
    public class WebUserRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "webusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0, isPrimaryKey: true)]
        public string WebUserId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string UserId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "registered", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 1, precision: 0, scale: 0)]
        public string Registered { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime", maxlength: 8, precision: 23, scale: 3)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string ContactId { get; set; }
        //------------------------------------------------------------------------------------
        //[FwSqlDataField(column: "tmppassword", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50, precision: 0, scale: 0)]
        //public string TemporaryPassword { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "changepasswordatlogin", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1, precision: 0, scale: 0)]
        public bool? ChangePasswordAtNextLogin { get; set; }
        //------------------------------------------------------------------------------------
        //[FwSqlDataField(column: "resetpassword", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 1, precision: 0, scale: 0)]
        //public string resetpassword { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "registerdate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime", maxlength: 4, precision: 16, scale: 0)]
        public string RegisterDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "webaccess", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1, precision: 0, scale: 0)]
        public bool? WebAccess { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "webadministrator", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1, precision: 0, scale: 0)]
        public bool? WebAdministrator { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "webpassword", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, precision: 0, scale: 0)]
        public string WebPassword { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "webquoterequest", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1, precision: 0, scale: 0)]
        public bool? HasWebQuoteRequest { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "webreports", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1, precision: 0, scale: 0)]
        public bool? HasWebReports { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "expiredays", modeltype: FwDataTypes.Integer, sqltype: "int", maxlength: 4, precision: 10, scale: 0)]
        public int? ExpireDays { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "expireflg", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1, precision: 0, scale: 0)]
        public bool? ExpirePassword { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pwupdated", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime", maxlength: 8, precision: 23, scale: 3)]
        public string PasswordLastUpdated { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "lockaccount", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1, precision: 0, scale: 0)]
        public bool? LockAccount { get; set; }
        //------------------------------------------------------------------------------------
        //[FwSqlDataField(column: "settings", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: -1, precision: 0, scale: 0)]
        //public string Settings { get; set; }
        ////------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dashboardwidgetsperrow", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? DashboardWidgetsPerRow { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "searchmodepreference", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string SearchModePreference { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "browsedefaultrows", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? BrowseDefaultRows { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "applicationtheme", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string ApplicationTheme { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "homemenuguid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        public string HomeMenuGuid { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "homemenupath", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        public string HomeMenuPath { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "successsoundid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string SuccessSoundId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "errorsoundid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string ErrorSoundId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notificationsoundid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8)]
        public string NotificationSoundId { get; set; }
        //------------------------------------------------------------------------------------

        public async Task<bool> SaveToolBarJsonAsync(string Note)
        {
            return await AppFunc.SaveNoteAsync(AppConfig, UserSession, WebUserId, "TOOLBAR", "", Note);
        }
        //-------------------------------------------------------------------------------------------------------

    }
}