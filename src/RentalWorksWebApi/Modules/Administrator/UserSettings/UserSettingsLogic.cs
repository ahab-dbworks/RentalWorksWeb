using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Administrator.UserSettings
{
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string UserId { get { return userSettings.UserId; } set { userSettings.UserId = value; } }
        [FwBusinessLogicField(isRecordTitle: true, isReadOnly: true)]
        public string UserName { get; set; }
        public string BrowseDefaultRows { get; set; }
        public string ApplicationTheme { get; set; }
        public string DateStamp { get { return userSettings.DateStamp; } set { userSettings.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSaveUserSettings(object sender, BeforeSaveEventArgs e)
        {
            userSettings.Settings = "<settings><settings></settings><browsedefaultrows>" + BrowseDefaultRows + "</browsedefaultrows><applicationtheme>" + ApplicationTheme + "</applicationtheme></settings>";
        }
        //-------------------------------------------------------------------------------------------------------   
    }
}