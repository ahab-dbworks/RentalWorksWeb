using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
using WebApi.Modules.Administrator.User;
using WebLibrary;

namespace WebApi.Modules.Settings.UserSearchSettings
{
    public class UserSearchSettingsLogic : AppBusinessLogic
    {
        UserSearchSettingsRecord userSearchSettings = new UserSearchSettingsRecord();

        //------------------------------------------------------------------------------------
        public UserSearchSettingsLogic()
        {
            dataRecords.Add(userSearchSettings);
            LoadOriginalBeforeSaving = false;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WebUserId { get { return userSearchSettings.WebUserId; } set { userSearchSettings.WebUserId = value; } }
        public string Mode { get { return userSearchSettings.Mode; } set { userSearchSettings.Mode = value; } }
        public string ResultFields { get { return userSearchSettings.ResultFields; } set { userSearchSettings.ResultFields = value; } }
        public bool? DisableAccessoryAutoExpand { get { return userSearchSettings.DisableAccessoryAutoExpand; } set { userSearchSettings.DisableAccessoryAutoExpand = value; } }
        //------------------------------------------------------------------------------------
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            if ((Mode == null) || (!(Mode.Equals(RwConstants.SEARCH_MODE_PREFERENCE_LIST) || Mode.Equals(RwConstants.SEARCH_MODE_PREFERENCE_HYBRID) || Mode.Equals(RwConstants.SEARCH_MODE_PREFERENCE_GRID))))
            {
                Mode = RwConstants.SEARCH_MODE_PREFERENCE_LIST;
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------
    }
}
