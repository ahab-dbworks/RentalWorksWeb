using FwStandard.BusinessLogic.Attributes;
using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Settings.UserDashboardSettings
{
    public class UserDashboardSettingsLogic : AppBusinessLogic
    {
        public class UserDashboardSetting
        {
            public string userWidgetId { get; set; }
            public string value { get; set; }
            public string text { get; set; }
            public bool selected { get; set; }
        }

        protected SqlServerConfig _dbConfig { get; set; }
        //------------------------------------------------------------------------------------
        public UserDashboardSettingsLogic()
        {
            DashboardSettingsTitle = "Dashboard Settings";
        }
        //------------------------------------------------------------------------------------
        public string UserId { get; set; }
        [FwBusinessLogicField(isRecordTitle: true)]
        [JsonIgnore]
        public string DashboardSettingsTitle { get; set; }
        public List<UserDashboardSetting> Widgets { get; set; }
        //------------------------------------------------------------------------------------
        public void SetDbConfig(SqlServerConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        //------------------------------------------------------------------------------------
        public async Task<bool> LoadAsync(string webUsersId)
        {
            //FwCustomFields customFields = null;
            bool loaded = false;
            UserId = webUsersId;
            Widgets = new List<UserDashboardSetting>();

            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                qry.Add("exec getwebuserdashboardsettings '" + UserId + "'");
                qry.AddColumn("webuserswidgetid");  //0
                qry.AddColumn("widgetid");          //1
                qry.AddColumn("widget");            //2
                qry.AddColumn("defaulttype");       //3
                qry.AddColumn("widgettype");        //4
                qry.AddColumn("orderby");           //5
                FwJsonDataTable table = await qry.QueryToFwJsonTableAsync(true);
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    UserDashboardSetting w = new UserDashboardSetting();
                    string UserWidgetId = table.Rows[r][0].ToString();
                    string widgetId = table.Rows[r][1].ToString();
                    string widgetName = table.Rows[r][2].ToString();

                    w.userWidgetId = UserWidgetId;
                    w.value = widgetId;
                    w.text = widgetName;
                    w.selected = (!UserWidgetId.Equals(string.Empty));
                    Widgets.Add(w);
                    loaded = true;
                }
            }
            return loaded;
        }
        //------------------------------------------------------------------------------------
        //public async Task<int> SaveAsync()
        //{
        //    int savedCount = 0;


        //    UserDashboardSettingsLogic lPrev = new UserDashboardSettingsLogic();
        //    lPrev.SetDbConfig(_dbConfig);
        //    await lPrev.LoadAsync(UserId);

        //    UserDashboardSetting wPrev = null;

        //    foreach (UserDashboardSetting w in Widgets)
        //    {
        //        wPrev = null;

        //        foreach (UserDashboardSetting wPrevTest in lPrev.Widgets)
        //        {
        //            if (wPrevTest.text.Equals(w.text))
        //            {
        //                wPrev = wPrevTest;
        //                break;
        //            }
        //        }

        //        if (wPrev != null)
        //        {
        //            if (wPrev.selected != w.selected)
        //            {
        //            }
                    
        //        }

        //    }

        //    Widgets = new List<UserDashboardSetting>();

        //    using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
        //    {
        //        FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
        //        qry.Add("exec getwebuserdashboardsettings '" + UserId + "'");
        //        qry.AddColumn("webuserswidgetid");  //0
        //        qry.AddColumn("widgetid");          //1
        //        qry.AddColumn("widget");            //2
        //        qry.AddColumn("defaulttype");       //3
        //        qry.AddColumn("widgettype");        //4
        //        qry.AddColumn("orderby");           //5
        //        FwJsonDataTable table = await qry.QueryToFwJsonTableAsync(true);
        //        for (int r = 0; r < table.Rows.Count; r++)
        //        {
        //            UserDashboardSetting w = new UserDashboardSetting();
        //            string UserWidgetId = table.Rows[r][0].ToString();
        //            w.value = table.Rows[r][1].ToString();
        //            w.text = table.Rows[r][2].ToString();
        //            w.selected = (!UserWidgetId.Equals(string.Empty));
        //            Widgets.Add(w);
        //            savedCount++;
        //        }
        //    }
        //    return savedCount;
        //}


        /*
        
                public virtual async Task<int> SaveAsync()
        {
            int rowsAffected = 0;
            TDataRecordSaveMode saveMode = (AllPrimaryKeysHaveValues ? TDataRecordSaveMode.smUpdate : TDataRecordSaveMode.smInsert);

            BeforeSaveEventArgs beforeSaveArgs = new BeforeSaveEventArgs();
            AfterSaveEventArgs afterSaveArgs = new AfterSaveEventArgs();
            beforeSaveArgs.SaveMode = saveMode;
            afterSaveArgs.SaveMode = saveMode;
            if (BeforeSave != null)
            {
                BeforeSave(this, beforeSaveArgs);
            }
            if (beforeSaveArgs.PerformSave)
            {
                foreach (FwDataReadWriteRecord rec in dataRecords)
                {
                    rowsAffected += await rec.SaveAsync();
                }
                await _Custom.LoadCustomFieldsAsync(GetType().Name.Replace("Logic", ""));
                await _Custom.SaveAsync(GetPrimaryKeys());
                afterSaveArgs.SavePerformed = (rowsAffected > 0);
                if (AfterSave != null)
                {
                    AfterSave(this, afterSaveArgs);
                }
            }
            return rowsAffected;
        }
        //------------------------------------------------------------------------------------
        */
    }
}
