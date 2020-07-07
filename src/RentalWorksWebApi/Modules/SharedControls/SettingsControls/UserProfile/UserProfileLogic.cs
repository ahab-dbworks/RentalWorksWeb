using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Administrator.User;
using WebApi;

namespace WebApi.Modules.Settings.UserProfile
{
    [FwLogic(Id: "40tMcvuPBfJpi")]
    public class UserProfileLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WebUserRecord webUser = new WebUserRecord();
        UserProfileLoader userSettingsLoader = new UserProfileLoader();

        public UserProfileLogic()
        {
            dataRecords.Add(webUser);
            dataLoader = userSettingsLoader;

            AfterSave += OnAfterSave;

        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "ZX86VoX5ZVvar", IsPrimaryKey: true)]
        public string UserId { get { return webUser.WebUserId; } set { webUser.WebUserId = value; } }

        [FwLogicProperty(Id: "XgeLiOeKHugRB", IsRecordTitle: true, IsReadOnly: true)]
        public string UserName { get; set; }

        [FwLogicProperty(Id: "dmn86kqK6Esn")]
        public int? BrowseDefaultRows { get { return webUser.BrowseDefaultRows; } set { webUser.BrowseDefaultRows = value; } }

        [FwLogicProperty(Id: "L9TyjWQH2YnA")]
        public string ApplicationTheme { get { return webUser.ApplicationTheme; } set { webUser.ApplicationTheme = value; } }

        [FwLogicProperty(Id: "i9AAqjnsCnxK")]
        public string HomeMenuGuid { get { return webUser.HomeMenuGuid; } set { webUser.HomeMenuGuid = value; } }

        [FwLogicProperty(Id: "G7SQHj3idpJf")]
        public string HomeMenuPath { get { return webUser.HomeMenuPath; } set { webUser.HomeMenuPath = value; } }

        [FwLogicProperty(Id: "RZ2BNFDcTLC2")]
        public string SuccessSoundId { get { return webUser.SuccessSoundId; } set { webUser.SuccessSoundId = value; } }

        [FwLogicProperty(Id: "y9L5kSGYGJAx")]
        public string SuccessSound { get; set; }

        private string _successBase64Sound = "";
        [FwLogicProperty(Id: "un2900EpcFpd")]
        public string SuccessBase64Sound
        {
            get
            {
                string sound = _successBase64Sound;
                if (string.IsNullOrEmpty(sound))
                {
                    sound = RwConstants.DEFAULT_SOUND_BASE64;
                }
                return sound;
            }
            set
            {
                _successBase64Sound = value;
            }
        }

        [FwLogicProperty(Id: "fTfhujW6vP6C")]
        public string ErrorSoundId { get { return webUser.ErrorSoundId; } set { webUser.ErrorSoundId = value; } }

        [FwLogicProperty(Id: "jZaf3H62W9Pp")]
        public string ErrorSound { get; set; }

        private string _errorBase64Sound = "";
        [FwLogicProperty(Id: "at37zWVEk7OM")]
        public string ErrorBase64Sound
        {
            get
            {
                string sound = _errorBase64Sound;
                if (string.IsNullOrEmpty(sound))
                {
                    sound = RwConstants.DEFAULT_SOUND_BASE64;
                }
                return sound;
            }
            set
            {
                _errorBase64Sound = value;
            }
        }

        [FwLogicProperty(Id: "7akrq6SztEca")]
        public string NotificationSoundId { get { return webUser.NotificationSoundId; } set { webUser.NotificationSoundId = value; } }

        [FwLogicProperty(Id: "FcspfaoMg0MD")]
        public string NotificationSound { get; set; }

        private string _notificationBase64Sound = "";
        [FwLogicProperty(Id: "RiJwTjmPSP4u")]
        public string NotificationBase64Sound
        {
            get
            {
                string sound = _notificationBase64Sound;
                if (string.IsNullOrEmpty(sound))
                {
                    sound = RwConstants.DEFAULT_SOUND_BASE64;
                }
                return sound;
            }
            set
            {
                _notificationBase64Sound = value;
            }
        }

        [FwLogicProperty(Id: "ypkfs1JBnySIQ")]
        public string FavoritesJson { get; set; }

        [FwLogicProperty(Id: "qmOkT9iNfuVgk")]
        public int? FirstDayOfWeek { get { return webUser.FirstDayOfWeek; } set { webUser.FirstDayOfWeek = value; } }

        [FwLogicProperty(Id: "7doEwy76OLkVF")]
        public bool? SettingsNavigationMenuVisible { get { return webUser.SettingsNavigationMenuVisible; } set { webUser.SettingsNavigationMenuVisible = value; } }

        [FwLogicProperty(Id: "654Ji1gmjrEPD")]
        public bool? ReportsNavigationMenuVisible { get { return webUser.ReportsNavigationMenuVisible; } set { webUser.ReportsNavigationMenuVisible = value; } }

        [FwLogicProperty(Id: "JGq0mOToNeqi")]
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
