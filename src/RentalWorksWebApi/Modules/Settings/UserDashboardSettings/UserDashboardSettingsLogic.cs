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
            public int defaultDataPoints { get; set; }
            public int dataPoints { get; set; }
            public string defaultAxisNumberFormatId { get; set; }
            public string defaultAxisNumberFormat { get; set; }
            public string defaultAxisNumberFormatMask { get; set; }
            public string axisNumberFormatId { get; set; }
            public string axisNumberFormat { get; set; }
            public string axisNumberFormatMask { get; set; }
            public string defaultDataNumberFormatId { get; set; }
            public string defaultDataNumberFormat { get; set; }
            public string defaultDataNumberFormatMask { get; set; }
            public string dataNumberFormatId { get; set; }
            public string dataNumberFormat { get; set; }
            public string dataNumberFormatMask { get; set; }
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
        public int? WidgetsPerRow { get; set; }
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
            bool loaded = false;
            UserId = webUsersId;
            WebUserRecord webUser = new WebUserRecord();
            webUser.SetDependencies(AppConfig, UserSession);
            webUser.WebUserId = webUsersId;
            webUser = await webUser.GetAsync<WebUserRecord>();
            WidgetsPerRow = webUser.DashboardWidgetsPerRow;
            if ((WidgetsPerRow == null) || (WidgetsPerRow <= 0))
            {
                WidgetsPerRow = 2;
            }

            Widgets = new List<UserDashboardSetting>();

            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                qry.Add("exec getwebuserdashboardsettings @webusersid ");
                qry.AddColumn("webuserswidgetid");              //00
                qry.AddColumn("widgetid");                      //01
                qry.AddColumn("widget");                        //02
                qry.AddColumn("apiname");                       //03
                qry.AddColumn("clickpath");                     //04
                qry.AddColumn("defaulttype");                   //05
                qry.AddColumn("widgettype");                    //06
                qry.AddColumn("defaultdatapoints");             //07
                qry.AddColumn("datapoints");                    //08
                qry.AddColumn("defaultaxisnumberformatid");     //09
                qry.AddColumn("defaultaxisnumberformat");       //10
                qry.AddColumn("defaultaxisnumberformatmask");   //11
                qry.AddColumn("axisnumberformatid");            //12
                qry.AddColumn("axisnumberformat");              //13
                qry.AddColumn("axisnumberformatmask");          //14
                qry.AddColumn("defaultdatanumberformatid");     //15
                qry.AddColumn("defaultdatanumberformat");       //16
                qry.AddColumn("defaultdatanumberformatmask");   //17
                qry.AddColumn("datanumberformatid");            //18
                qry.AddColumn("datanumberformat");              //19
                qry.AddColumn("datanumberformatmask");          //20
                qry.AddColumn("orderby");                       //21
                qry.AddParameter("@webusersid", UserId);
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
                    int defaultdatapoints = Convert.ToInt32(table.Rows[r][7]);
                    int datapoints = Convert.ToInt32(table.Rows[r][8]);
                    string defaultaxisnumberformatid = table.Rows[r][9].ToString();
                    string defaultaxisnumberformat = table.Rows[r][10].ToString();
                    string defaultaxisnumberformatmask = table.Rows[r][11].ToString();
                    string axisnumberformatid = table.Rows[r][12].ToString();
                    string axisnumberformat = table.Rows[r][13].ToString();
                    string axisnumberformatmask = table.Rows[r][14].ToString();
                    string defaultdatanumberformatid = table.Rows[r][15].ToString();
                    string defaultdatanumberformat = table.Rows[r][16].ToString();
                    string defaultdatanumberformatmask = table.Rows[r][17].ToString();
                    string datanumberformatid = table.Rows[r][18].ToString();
                    string datanumberformat = table.Rows[r][19].ToString();
                    string datanumberformatmask = table.Rows[r][20].ToString();

                    w.userWidgetId = UserWidgetId;
                    w.value = widgetId;
                    w.text = widgetName;
                    w.selected = (!UserWidgetId.Equals(string.Empty));
                    w.apiname = apiname;
                    w.clickpath = clickPath;
                    w.defaulttype = defaulttype;
                    w.widgettype = widgettype;
                    w.defaultDataPoints = defaultdatapoints;
                    w.dataPoints = datapoints;
                    w.defaultAxisNumberFormatId = defaultaxisnumberformatid;
                    w.defaultAxisNumberFormat = defaultaxisnumberformat;
                    w.defaultAxisNumberFormatMask = defaultaxisnumberformatmask;
                    w.axisNumberFormatId = axisnumberformatid;
                    w.axisNumberFormat = axisnumberformat;
                    w.axisNumberFormatMask = axisnumberformatmask;
                    w.defaultDataNumberFormatId = defaultdatanumberformatid;
                    w.defaultDataNumberFormat = defaultdatanumberformat;
                    w.defaultDataNumberFormatMask  = defaultdatanumberformatmask;
                    w.dataNumberFormatId = datanumberformatid;
                    w.dataNumberFormat = datanumberformat;
                    w.dataNumberFormatMask = datanumberformatmask;
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
            lPrev.SetDependencies(AppConfig, UserSession);
            await lPrev.LoadAsync(UserId);

            UserDashboardSetting wPrev = null;
            UserWidgetLogic uw = null;

            WebUserRecord webUser = new WebUserRecord();
            webUser.SetDependencies(AppConfig, UserSession);
            webUser.WebUserId = UserId;
            webUser.DashboardWidgetsPerRow = WidgetsPerRow;
            await webUser.SaveAsync();


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
