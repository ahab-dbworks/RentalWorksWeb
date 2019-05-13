using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Administrator.User;
using WebLibrary;

namespace WebApi.Modules.Settings.UserSearchSettings
{
    [FwLogic(Id:"LM4r9tSP6Zw8h")]
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
        [FwLogicProperty(Id:"ULR6GjS4FL8zv", IsPrimaryKey:true)]
        public string WebUserId { get { return userSearchSettings.WebUserId; } set { userSearchSettings.WebUserId = value; } }

        [FwLogicProperty(Id:"xCSEykR3CX4M")]
        public string Mode { get { return userSearchSettings.Mode; } set { userSearchSettings.Mode = value; } }

        [FwLogicProperty(Id:"vbTKbyhDvUsi")]
        public string ResultFields { get { return userSearchSettings.ResultFields; } set { userSearchSettings.ResultFields = value; } }

        [FwLogicProperty(Id:"6p0D6azvkZIA")]
        public bool? DisableAccessoryAutoExpand { get { return userSearchSettings.DisableAccessoryAutoExpand; } set { userSearchSettings.DisableAccessoryAutoExpand = value; } }

        [FwLogicProperty(Id: "g16kb6OfLBMxB")]
        public bool? ShowZeroQuantity { get { return userSearchSettings.ShowZeroQuantity; } set { userSearchSettings.ShowZeroQuantity = value; } }

        [FwLogicProperty(Id: "aK8led2lcYrKC")]
        public bool? HideZeroQuantity { get { return userSearchSettings.HideZeroQuantity; } set { userSearchSettings.HideZeroQuantity = value; } }


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
