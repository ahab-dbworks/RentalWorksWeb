using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Administrator.User;
using WebApi.Modules.Settings.WebUserWidget;
using WebLibrary;

namespace WebApi.Modules.Settings.UserSearchSettings
{
    public class UserSearchSettingsLogic : AppBusinessLogic
    {
        protected SqlServerConfig _dbConfig { get; set; }
        //------------------------------------------------------------------------------------
        public UserSearchSettingsLogic()
        {
            SearchSettingsTitle = "Search Settings";
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string UserId { get; set; }
        public string SearchModePreference { get; set; }
        [FwBusinessLogicField(isRecordTitle: true)]
        [JsonIgnore]
        public string SearchSettingsTitle { get; set; }
        //------------------------------------------------------------------------------------
        public void SetDbConfig(SqlServerConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        //------------------------------------------------------------------------------------
        public async Task<bool> LoadAsync(string webUsersId)
        {
            bool loaded = true;
            UserId = webUsersId;
            WebUserRecord webUser = new WebUserRecord();
            webUser.SetDependencies(AppConfig, UserSession);
            webUser.WebUserId = webUsersId;
            webUser = await webUser.GetAsync<WebUserRecord>();
            SearchModePreference = webUser.SearchModePreference;
            if ((SearchModePreference == null) || (!(SearchModePreference.Equals(RwConstants.SEARCH_MODE_PREFERENCE_LIST) || SearchModePreference.Equals(RwConstants.SEARCH_MODE_PREFERENCE_HYBRID) || SearchModePreference.Equals(RwConstants.SEARCH_MODE_PREFERENCE_GRID))))
            {
                SearchModePreference = RwConstants.SEARCH_MODE_PREFERENCE_LIST;
            }

            return loaded;
        }
        //------------------------------------------------------------------------------------
        public override async Task<int> SaveAsync(FwBusinessLogic original)
        {
            int savedCount = 0;
            if ((SearchModePreference == null) || (!(SearchModePreference.Equals(RwConstants.SEARCH_MODE_PREFERENCE_LIST) || SearchModePreference.Equals(RwConstants.SEARCH_MODE_PREFERENCE_HYBRID) || SearchModePreference.Equals(RwConstants.SEARCH_MODE_PREFERENCE_GRID))))
            {
                SearchModePreference = RwConstants.SEARCH_MODE_PREFERENCE_LIST;
            }
            WebUserRecord webUser = new WebUserRecord();
            webUser.SetDependencies(AppConfig, UserSession);
            webUser.WebUserId = UserId;
            webUser.SearchModePreference = SearchModePreference;
            await webUser.SaveAsync(null);
            savedCount++;

            return savedCount;
        }
        //------------------------------------------------------------------------------------
    }
}
