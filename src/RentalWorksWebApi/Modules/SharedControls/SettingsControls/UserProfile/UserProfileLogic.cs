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
        private bool passwordChanged = false;
        private string newPassword;

        public UserProfileLogic()
        {
            dataRecords.Add(webUser);
            dataLoader = userSettingsLoader;
            AfterSave += OnAfterSave;
            ForceSave = true;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "ZX86VoX5ZVvar", IsPrimaryKey: true)]
        public string WebUserId { get { return webUser.WebUserId; } set { webUser.WebUserId = value; } }

        [FwLogicProperty(Id: "XgeLiOeKHugRB", IsRecordTitle: true, IsReadOnly: true)]
        public string UserName { get; set; }

        [FwLogicProperty(Id: "hTLdEyUTAlinh", IsReadOnly: true)]
        public string UserId { get; set; }

        [FwLogicProperty(Id: "WWDywk13J8z2C", IsReadOnly: true)]
        public string LoginName { get; set; }

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
        [FwLogicProperty(Id: "un2900EpcFpd", IsNotAudited: true)]
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
        [FwLogicProperty(Id: "at37zWVEk7OM", IsNotAudited: true)]
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
        [FwLogicProperty(Id: "RiJwTjmPSP4u", IsNotAudited: true)]
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

        [FwLogicProperty(Id: "KBpq7SgKbuls")]
        public bool? MainMenuPinned { get { return webUser.MainMenuPinned; } set { webUser.MainMenuPinned = value; } }

        [FwLogicProperty(Id: "PyJF3dC7sONA9", IsReadOnly: true)]
        public bool? WebAdministrator { get; set; }

        [FwLogicProperty(Id: "fR3t20LsLMMU0")]
        public string QuikActivitySetting { get { return webUser.QuikActivitySetting; } set { webUser.QuikActivitySetting = value; } }

        [FwLogicProperty(Id: "robWDF2GvQMc")]
        public string EmailSignature { get; set; }


        [FwLogicProperty(Id: "BVEDQFlLuiwJb")]
        public string Password

        {
            get { return "?????????"; }
            set
            {
                if (value != null)
                {
                    passwordChanged = true;
                }
                newPassword = value;
            }
        }

        public string DateStamp { get { return webUser.DateStamp; } set { webUser.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        public virtual void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            bool doSaveFavoritesJson = false;
            bool doSaveEmailSignature = false;

            UserProfileLogic orig = null;
            if (e.Original != null)
            {
                orig = (UserProfileLogic)e.Original;
            }


            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                doSaveFavoritesJson = true;
                doSaveEmailSignature = true;
            }
            else if (e.Original != null)
            {
                //UserProfileLogic orig = (UserProfileLogic)e.Original;
                doSaveEmailSignature = (!orig.EmailSignature.Equals(EmailSignature));
                doSaveFavoritesJson = (!orig.FavoritesJson.Equals(FavoritesJson));
            }

            if (doSaveFavoritesJson)
            {
                bool saved = webUser.SaveFavoritesJsonAsync(FavoritesJson).Result;
                if (saved)
                {
                    e.RecordsAffected++;
                }
            }

            if (doSaveEmailSignature)
            {
                bool saved = AppFunc.SaveNoteAsync(AppConfig, UserSession, UserSession.UsersId, RwConstants.WEBUSER_NOTE_TYPE_EMAIL_SIGNATURE, "", EmailSignature).Result;
                if (saved)
                {
                    e.RecordsAffected++;
                }
            }

            if (passwordChanged)
            {
                UserLogic origUser = new UserLogic();
                origUser.SetDependencies(AppConfig, UserSession);
                origUser.UserId = orig.UserId;
                bool userLoaded = origUser.LoadAsync<UserLogic>(e.SqlConnection).Result;

                UserLogic newUser = origUser.MakeCopy<UserLogic>();
                newUser.SetDependencies(AppConfig, UserSession);
                newUser.Password = newPassword;
                int i = newUser.SaveAsync(original: origUser, conn: e.SqlConnection).Result;
                if (i > 0)
                {
                    e.RecordsAffected++;
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
