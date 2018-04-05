using FwStandard.BusinessLogic.Attributes;
using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Settings.WebUserWidget;

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
            public string apiname { get; set; }
            public string clickpath { get; set; }
            public string defaulttype { get; set; }
            public string widgettype { get; set; }
        }

        protected SqlServerConfig _dbConfig { get; set; }
        //------------------------------------------------------------------------------------
        public UserDashboardSettingsLogic()
        {
            DashboardSettingsTitle = "Dashboard Settings";
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
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
                qry.Add("exec getwebuserdashboardsettings '" + UserId + "'"); // todo: fix sql injection
                qry.AddColumn("webuserswidgetid");  //0
                qry.AddColumn("widgetid");          //1
                qry.AddColumn("widget");            //2
                qry.AddColumn("apiname");           //3
                qry.AddColumn("clickpath");         //4
                qry.AddColumn("defaulttype");       //5
                qry.AddColumn("widgettype");        //6
                qry.AddColumn("orderby");           //7
                FwJsonDataTable table = await qry.QueryToFwJsonTableAsync(true);
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    UserDashboardSetting w = new UserDashboardSetting();
                    string UserWidgetId = table.Rows[r][0].ToString();
                    string widgetId = table.Rows[r][1].ToString();
                    string widgetName = table.Rows[r][2].ToString();
                    string apiname = table.Rows[r][3].ToString();
                    string clickPath = table.Rows[r][4].ToString();
                    string defaulttype = table.Rows[r][5].ToString();
                    string widgettype = table.Rows[r][6].ToString();

                    w.userWidgetId = UserWidgetId;
                    w.value = widgetId;
                    w.text = widgetName;
                    w.selected = (!UserWidgetId.Equals(string.Empty));
                    w.apiname = apiname;
                    w.clickpath = clickPath;
                    w.defaulttype = defaulttype;
                    w.widgettype = widgettype;
                    Widgets.Add(w);
                    loaded = true;
                }
            }
            return loaded;
        }
        //------------------------------------------------------------------------------------
        public override async Task<int> SaveAsync()
        {
            int savedCount = 0;

            // can remove all this "prev" stuff if Dashboard Settings form can send back the actual "userWidgetId"
            UserDashboardSettingsLogic lPrev = new UserDashboardSettingsLogic();
            lPrev.SetDbConfig(_dbConfig);
            await lPrev.LoadAsync(UserId);

            UserDashboardSetting wPrev = null;
            UserWidgetLogic uw = null;

            int widgetPosition = 0;
            foreach (UserDashboardSetting w in Widgets)
            {
                wPrev = null;

                foreach (UserDashboardSetting wPrevTest in lPrev.Widgets)
                {
                    if (wPrevTest.text.Equals(w.text))
                    {
                        wPrev = wPrevTest;
                        break;
                    }
                }

                if (wPrev != null)
                {
                    uw = new UserWidgetLogic();
                    uw.AppConfig = AppConfig;
                    if (!wPrev.userWidgetId.Equals(string.Empty))
                    {
                        object[] pk = { wPrev.userWidgetId };
                        await uw.LoadAsync<UserWidgetLogic>(pk);
                        uw.AppConfig = AppConfig;
                    }
                    uw.UserId = UserId;
                    uw.WidgetId = w.value;
                    uw.OrderBy = widgetPosition;

                    if (wPrev.selected && (!w.selected))
                    {
                        await uw.DeleteAsync();
                    }
                    else if (w.selected)
                    {
                        await uw.SaveAsync();
                    }

                }
                widgetPosition++;
                savedCount++;
            }
            return savedCount;
        }
        //------------------------------------------------------------------------------------
    }
}
