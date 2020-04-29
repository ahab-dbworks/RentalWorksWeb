using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Administrator.User;
using WebApi;

namespace WebApi.Modules.Settings.UserSettings.UserSettings
{
    [FwLogic(Id:"40tMcvuPBfJpi")]
    public class UserSettingsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WebUserRecord webUser = new WebUserRecord();
        UserSettingsLoader userSettingsLoader = new UserSettingsLoader();

        public UserSettingsLogic()
        {
            dataRecords.Add(webUser);
            dataLoader = userSettingsLoader;

            AfterSave += OnAfterSave;

        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"ZX86VoX5ZVvar", IsPrimaryKey:true)]
        public string UserId { get { return webUser.WebUserId; } set { webUser.WebUserId = value; } }

        [FwLogicProperty(Id:"XgeLiOeKHugRB", IsRecordTitle:true, IsReadOnly:true)]
        public string UserName { get; set; }

        [FwLogicProperty(Id:"dmn86kqK6Esn")]
        public int? BrowseDefaultRows { get { return webUser.BrowseDefaultRows; } set { webUser.BrowseDefaultRows = value; } }

        [FwLogicProperty(Id:"L9TyjWQH2YnA")]
        public string ApplicationTheme { get { return webUser.ApplicationTheme; } set { webUser.ApplicationTheme = value; } }

        [FwLogicProperty(Id:"i9AAqjnsCnxK")]
        public string HomeMenuGuid { get { return webUser.HomeMenuGuid; } set { webUser.HomeMenuGuid = value; } }

        [FwLogicProperty(Id:"G7SQHj3idpJf")]
        public string HomeMenuPath { get { return webUser.HomeMenuPath; } set { webUser.HomeMenuPath = value; } }

        [FwLogicProperty(Id:"RZ2BNFDcTLC2")]
        public string SuccessSoundId { get { return webUser.SuccessSoundId; } set { webUser.SuccessSoundId = value; } }

        [FwLogicProperty(Id:"y9L5kSGYGJAx")]
        public string SuccessSound { get; set; }

        private string _successSoundFileName = "";
        [FwLogicProperty(Id:"un2900EpcFpd")]
        //public string SuccessSoundFileName { get; set; }
        public string SuccessSoundFileName
        {
            get
            {
                string fileName = _successSoundFileName;
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = RwConstants.DEFAULT_SOUND_FILE_NAME;
                }
                return fileName;
            }
            set
            {
                _successSoundFileName = value;
            }
        }

        [FwLogicProperty(Id:"fTfhujW6vP6C")]
        public string ErrorSoundId { get { return webUser.ErrorSoundId; } set { webUser.ErrorSoundId = value; } }

        [FwLogicProperty(Id:"jZaf3H62W9Pp")]
        public string ErrorSound { get; set; }

        private string _errorSoundFileName = "";
        [FwLogicProperty(Id:"at37zWVEk7OM")]
        //public string ErrorSoundFileName { get; set; }
        public string ErrorSoundFileName
        {
            get
            {
                string fileName = _errorSoundFileName;
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = RwConstants.DEFAULT_SOUND_FILE_NAME;
                }
                return fileName;
            }
            set
            {
                _errorSoundFileName = value;
            }
        }

        [FwLogicProperty(Id:"7akrq6SztEca")]
        public string NotificationSoundId { get { return webUser.NotificationSoundId; } set { webUser.NotificationSoundId = value; } }

        [FwLogicProperty(Id:"FcspfaoMg0MD")]
        public string NotificationSound { get; set; }

        private string _notificationSoundFileName = "";
        [FwLogicProperty(Id:"RiJwTjmPSP4u")]
        //public string NotificationSoundFileName { get; set; }
        public string NotificationSoundFileName
        {
            get
            {
                string fileName = _notificationSoundFileName;
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = RwConstants.DEFAULT_SOUND_FILE_NAME;
                }
                return fileName;
            }
            set
            {
                _notificationSoundFileName = value;
            }
        }

        [FwLogicProperty(Id: "ypkfs1JBnySIQ")]
        public string FavoritesJson { get; set; }

        [FwLogicProperty(Id: "RNSGN6rtM7kOk", IsReadOnly: true)]
        public bool? WebAdministrator{ get; set; }

        [FwLogicProperty(Id:"JGq0mOToNeqi")]
        public string DateStamp { get { return webUser.DateStamp; } set { webUser.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        public virtual void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            bool saved = webUser.SaveFavoritesJsonAsync(FavoritesJson).Result;
            if (saved)
            {
                e.RecordsAffected++;
            }
        }
        //------------------------------------------------------------------------------------
    }
}
