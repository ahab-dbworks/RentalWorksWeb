using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using System.Text;
using WebApi.Logic;
namespace WebApi.Modules.Settings.UserSettings
{
    [FwLogic(Id:"40tMcvuPBfJpi")]
    public class UserSettingsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        UserSettingsRecord userSettings = new UserSettingsRecord();
        UserSettingsLoader userSettingsLoader = new UserSettingsLoader();

        public UserSettingsLogic()
        {
            dataRecords.Add(userSettings);
            dataLoader = userSettingsLoader;
            BeforeSave += OnBeforeSaveUserSettings;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"ZX86VoX5ZVvar", IsPrimaryKey:true)]
        public string UserId { get { return userSettings.UserId; } set { userSettings.UserId = value; } }

        [FwLogicProperty(Id:"XgeLiOeKHugRB", IsRecordTitle:true, IsReadOnly:true)]
        public string UserName { get; set; }

        [FwLogicProperty(Id:"dmn86kqK6Esn")]
        public string BrowseDefaultRows { get; set; }

        [FwLogicProperty(Id:"L9TyjWQH2YnA")]
        public string ApplicationTheme { get; set; }

        [FwLogicProperty(Id:"i9AAqjnsCnxK")]
        public string HomeMenuGuid { get; set; }

        [FwLogicProperty(Id:"G7SQHj3idpJf")]
        public string HomeMenuPath { get; set; }

        [FwLogicProperty(Id:"RZ2BNFDcTLC2")]
        public string SuccessSoundId { get; set; }

        [FwLogicProperty(Id:"y9L5kSGYGJAx")]
        public string SuccessSound { get; set; }

        [FwLogicProperty(Id:"un2900EpcFpd")]
        public string SuccessSoundFileName { get; set; }

        [FwLogicProperty(Id:"fTfhujW6vP6C")]
        public string ErrorSoundId { get; set; }

        [FwLogicProperty(Id:"jZaf3H62W9Pp")]
        public string ErrorSound { get; set; }

        [FwLogicProperty(Id:"at37zWVEk7OM")]
        public string ErrorSoundFileName { get; set; }

        [FwLogicProperty(Id:"7akrq6SztEca")]
        public string NotificationSoundId { get; set; }

        [FwLogicProperty(Id:"FcspfaoMg0MD")]
        public string NotificationSound { get; set; }

        [FwLogicProperty(Id:"RiJwTjmPSP4u")]
        public string NotificationSoundFileName { get; set; }

        [FwLogicProperty(Id:"JGq0mOToNeqi")]
        public string DateStamp { get { return userSettings.DateStamp; } set { userSettings.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
        public void OnBeforeSaveUserSettings(object sender, BeforeSaveEventArgs e)
        {
            //userSettings.Settings = "<settings><settings></settings><browsedefaultrows>" + BrowseDefaultRows + "</browsedefaultrows><applicationtheme>" + ApplicationTheme + "</applicationtheme></settings>";

            UserSettingsLogic lOrig = ((UserSettingsLogic)e.Original);
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
            settings.Append("<homemenuguid>" + HomeMenuGuid + "</homemenuguid>");
            settings.Append("<homemenuitem>" + HomeMenuPath + "</homemenuitem>");
            settings.Append("<successsoundid>" + SuccessSoundId + "</successsoundid>");
            settings.Append("<errorsoundid>" + ErrorSoundId + "</errorsoundid>");
            settings.Append("<notificationsoundid>" + NotificationSoundId + "</notificationsoundid>");
            settings.Append("</settings>");
            userSettings.Settings = settings.ToString();
        }
        //-------------------------------------------------------------------------------------------------------   
    }
}
