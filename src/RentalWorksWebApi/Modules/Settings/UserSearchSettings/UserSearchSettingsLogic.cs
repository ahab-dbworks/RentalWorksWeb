using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
using WebApi.Modules.Administrator.User;
using WebLibrary;

namespace WebApi.Modules.Settings.UserSearchSettings
{
    public class UserSearchSettingsLogic : AppBusinessLogic
    {
        WebUserRecord webUser = new WebUserRecord();

        //------------------------------------------------------------------------------------
        public UserSearchSettingsLogic()
        {
            dataRecords.Add(webUser);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WebUserId { get { return webUser.WebUserId; } set { webUser.WebUserId = value; } }
        public string SearchModePreference { get { return webUser.SearchModePreference; } set { webUser.SearchModePreference = value; } }
        //------------------------------------------------------------------------------------
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            if ((SearchModePreference == null) || (!(SearchModePreference.Equals(RwConstants.SEARCH_MODE_PREFERENCE_LIST) || SearchModePreference.Equals(RwConstants.SEARCH_MODE_PREFERENCE_HYBRID) || SearchModePreference.Equals(RwConstants.SEARCH_MODE_PREFERENCE_GRID))))
            {
                SearchModePreference = RwConstants.SEARCH_MODE_PREFERENCE_LIST;
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------


    }
}
