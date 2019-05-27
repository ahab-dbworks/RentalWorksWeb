using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Administrator.User;

namespace WebApi.Modules.Settings.UserSettings
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

        [FwLogicProperty(Id:"un2900EpcFpd")]
        public string SuccessSoundFileName { get; set; }

        [FwLogicProperty(Id:"fTfhujW6vP6C")]
        public string ErrorSoundId { get { return webUser.ErrorSoundId; } set { webUser.ErrorSoundId = value; } }

        [FwLogicProperty(Id:"jZaf3H62W9Pp")]
        public string ErrorSound { get; set; }

        [FwLogicProperty(Id:"at37zWVEk7OM")]
        public string ErrorSoundFileName { get; set; }

        [FwLogicProperty(Id:"7akrq6SztEca")]
        public string NotificationSoundId { get { return webUser.NotificationSoundId; } set { webUser.NotificationSoundId = value; } }

        [FwLogicProperty(Id:"FcspfaoMg0MD")]
        public string NotificationSound { get; set; }

        [FwLogicProperty(Id:"RiJwTjmPSP4u")]
        public string NotificationSoundFileName { get; set; }

        [FwLogicProperty(Id: "ypkfs1JBnySIQ")]
        public string ToolBarJson { get; set; }

        [FwLogicProperty(Id:"JGq0mOToNeqi")]
        public string DateStamp { get { return webUser.DateStamp; } set { webUser.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        public virtual void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            bool saved = webUser.SaveToolBarJsonAsync(ToolBarJson).Result;
            if (saved)
            {
                e.RecordsAffected++;
            }
        }
        //------------------------------------------------------------------------------------
    }
}
