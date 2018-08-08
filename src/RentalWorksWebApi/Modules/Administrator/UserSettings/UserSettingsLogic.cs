using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using System.Text;
using WebApi.Logic;
namespace WebApi.Modules.Administrator.UserSettings
{
    public class UserSettingsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        UserSettingsRecord userSettings = new UserSettingsRecord();
        UserSettingsLoader userSettingsLoader = new UserSettingsLoader();

        private UserSettingsLogic lOrig = null;

        public UserSettingsLogic()
        {
            dataRecords.Add(userSettings);
            dataLoader = userSettingsLoader;
            BeforeSave += OnBeforeSaveUserSettings;


        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string UserId { get { return userSettings.UserId; } set { userSettings.UserId = value; } }
        [FwBusinessLogicField(isRecordTitle: true, isReadOnly: true)]
        public string UserName { get; set; }
        public string BrowseDefaultRows { get; set; }
        public string ApplicationTheme { get; set; }
        public string SuccessSoundId { get; set; }
        public string SuccessSound { get; set; }
        public string ErrorSoundId { get; set; }
        public string ErrorSound { get; set; }
        public string NotificationSoundId { get; set; }
        public string NotificationSound { get; set; }
        public string DateStamp { get { return userSettings.DateStamp; } set { userSettings.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, ref string validateMsg)
        {
            bool isValid = true;

            lOrig = new UserSettingsLogic();

            if (saveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate)
            {
                lOrig.SetDependencies(this.AppConfig, this.UserSession);
                object[] pk = GetPrimaryKeys();
                bool b = lOrig.LoadAsync<UserSettingsLogic>(pk).Result;
            }

            return isValid;
        }
        //-------------------------------------------------------------------------------------------------------   
        public void OnBeforeSaveUserSettings(object sender, BeforeSaveEventArgs e)
        {
            //userSettings.Settings = "<settings><settings></settings><browsedefaultrows>" + BrowseDefaultRows + "</browsedefaultrows><applicationtheme>" + ApplicationTheme + "</applicationtheme></settings>";

            if (SuccessSoundId == null)
            {
                SuccessSoundId = lOrig.SuccessSoundId;
                if (SuccessSoundId == null)
                {
                    SuccessSoundId = "";
                }
            }

            if (ErrorSoundId == null)
            {
                ErrorSoundId = lOrig.ErrorSoundId;
                if (ErrorSoundId == null)
                {
                    ErrorSoundId = "";
                }
            }

            if (NotificationSoundId == null)
            {
                NotificationSoundId = lOrig.NotificationSoundId;
                if (NotificationSoundId == null)
                {
                    NotificationSoundId = "";
                }
            }

            StringBuilder settings = new StringBuilder();
            settings.Append("<settings>");
            settings.Append("<settings>");
            settings.Append("</settings>");
            settings.Append("<browsedefaultrows>" + BrowseDefaultRows + "</browsedefaultrows>");
            settings.Append("<applicationtheme>" + ApplicationTheme + "</applicationtheme>");
            settings.Append("<successsoundid>" + SuccessSoundId + "</successsoundid>");
            settings.Append("<errorsoundid>" + ErrorSoundId + "</errorsoundid>");
            settings.Append("<notificationsoundid>" + NotificationSoundId + "</notificationsoundid>");
            settings.Append("</settings>");
            userSettings.Settings = settings.ToString();
        }
        //-------------------------------------------------------------------------------------------------------   
    }
}