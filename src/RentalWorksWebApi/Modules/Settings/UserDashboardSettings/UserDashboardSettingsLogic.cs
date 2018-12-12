using FwStandard.AppManager;
using FwStandard.BusinessLogic;
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
    [FwLogic(Id: "4wHzCi9ODcIR9")]
    public class UserDashboardSettingsLogic : AppBusinessLogic
    {
        public class UserDashboardSetting
        {
            [FwLogicProperty(Id: "uoiTLL0jYwA8")]
            public string userWidgetId { get; set; }

            [FwLogicProperty(Id: "Kp2dJDkbPTpu")]
            public string value { get; set; }

            [FwLogicProperty(Id: "NXRy8pQDQqwN")]
            public string text { get; set; }

            [FwLogicProperty(Id: "4MxlimAYESIg")]
            public bool? selected { get; set; }

            [FwLogicProperty(Id: "PetDFdwX7FQS")]
            public string apiname { get; set; }

            [FwLogicProperty(Id: "DJ6nwgtnXfty")]
            public string clickpath { get; set; }

            [FwLogicProperty(Id: "5AfOVvSNfeDR")]
            public string defaulttype { get; set; }

            [FwLogicProperty(Id: "1N8BnB9e2h1r")]
            public string widgettype { get; set; }

            [FwLogicProperty(Id: "8w5PYsX24XtS")]
            public int? defaultDataPoints { get; set; }

            [FwLogicProperty(Id: "tEWTlewmof8c")]
            public int? dataPoints { get; set; }

            [FwLogicProperty(Id: "fhepRMTIoPrB")]
            public string defaultAxisNumberFormatId { get; set; }

            [FwLogicProperty(Id: "F8fBb6m5STcB")]
            public string defaultAxisNumberFormat { get; set; }

            [FwLogicProperty(Id: "cs50aoSnCLpm")]
            public string defaultAxisNumberFormatMask { get; set; }

            [FwLogicProperty(Id: "bzY2uJN3Z7Mv")]
            public string axisNumberFormatId { get; set; }

            [FwLogicProperty(Id: "OFnRIsGuJ7IG")]
            public string axisNumberFormat { get; set; }

            [FwLogicProperty(Id: "d7HaW4Rygrgw")]
            public string axisNumberFormatMask { get; set; }

            [FwLogicProperty(Id: "7nr6tRwCANkM")]
            public string defaultDataNumberFormatId { get; set; }

            [FwLogicProperty(Id: "pjLvL61Nk8xv")]
            public string defaultDataNumberFormat { get; set; }

            [FwLogicProperty(Id: "9ZeYdh0eAfbB")]
            public string defaultDataNumberFormatMask { get; set; }

            [FwLogicProperty(Id: "36nz45tfs4pp")]
            public string dataNumberFormatId { get; set; }

            [FwLogicProperty(Id: "XuRq6NemRtVJ")]
            public string dataNumberFormat { get; set; }

            [FwLogicProperty(Id: "kRpwFBx8w0iV")]
            public string dataNumberFormatMask { get; set; }

            [FwLogicProperty(Id: "DcfKXn0BmpO0l")]
            public string defaultDateBehavior { get; set; }
            [FwLogicProperty(Id: "SVfGQzuLNjj8j")]
            public string dateBehavior { get; set; }
            [FwLogicProperty(Id: "NX6Ds8RkWf3pw")]
            public string dateFieldDisplayNames { get; set; }
            [FwLogicProperty(Id: "F2dTkVfmGlyla")]
            public string dateFields { get; set; }
            [FwLogicProperty(Id: "mN6MzNVwHusuS")]
            public string defaultDateField { get; set; }
            [FwLogicProperty(Id: "rZTRLc5UXHPvp")]
            public string dateField { get; set; }
            [FwLogicProperty(Id: "ddDKpCrfFdRcs")]
            public DateTime? defaultFromDate { get; set; }
            [FwLogicProperty(Id: "1FwqriE388QFk")]
            public DateTime? fromDate { get; set; }
            [FwLogicProperty(Id: "HhvTT4MNPVsu7")]
            public DateTime? defaultToDate { get; set; }
            [FwLogicProperty(Id: "hRvFdpdIUPtbM")]
            public DateTime? toDate { get; set; }
        }

        protected SqlServerConfig _dbConfig { get; set; }
        //------------------------------------------------------------------------------------
        public UserDashboardSettingsLogic()
        {
            DashboardSettingsTitle = "Dashboard Settings";
            LoadOriginalBeforeSaving = false;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "766wAbVOC8nt5", IsPrimaryKey: true)]
        public string UserId { get; set; }

        [FwLogicProperty(Id: "sinlv4VG74o9")]
        public int? WidgetsPerRow { get; set; }

        [JsonIgnore]
        [FwLogicProperty(Id: "0D4rUx4DvdRG", IsRecordTitle: true)]
        public string DashboardSettingsTitle { get; set; }

        [FwLogicProperty(Id: "oZMIf4l1CVS5")]
        public List<UserDashboardSetting> Widgets { get; set; }
        [FwLogicProperty(Id: "lqNWOoaHp83RB")]
        public List<UserDashboardSetting> DefaultWidgets { get; set; }

        //------------------------------------------------------------------------------------
        public void SetDbConfig(SqlServerConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        //------------------------------------------------------------------------------------
        //public async Task<bool> LoadAsync(string webUsersId)
        public override async Task<bool> LoadAsync<T>(object[] primaryKeyValues)
        {
            string webUsersId = primaryKeyValues[0].ToString();

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
            DefaultWidgets = new List<UserDashboardSetting>();

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

                qry.AddColumn("defaultdatebehavior");           //21
                qry.AddColumn("datebehavior");                  //22
                qry.AddColumn("datefielddisplaynames");         //23
                qry.AddColumn("datefields");                    //24
                qry.AddColumn("defaultdatefield");              //25
                qry.AddColumn("datefield");                     //26
                qry.AddColumn("defaultfromdate");               //27
                qry.AddColumn("fromdate");                      //28
                qry.AddColumn("defaulttodate");                 //29
                qry.AddColumn("todate");                        //30

                qry.AddColumn("orderby");                       //31
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

                    string defaultdatebehavior = table.Rows[r][21].ToString();
                    string datebehavior = table.Rows[r][22].ToString();
                    string datefielddisplaynames = table.Rows[r][23].ToString();
                    string datefields = table.Rows[r][24].ToString();
                    string defaultdatefield = table.Rows[r][25].ToString();
                    string datefield = table.Rows[r][26].ToString();
                    DateTime? defaultfromdate = null;
                    DateTime? fromdate = null;
                    DateTime? defaulttodate = null;
                    DateTime? todate = null;
                    if (!table.Rows[r][27].ToString().Equals(""))
                    {
                        defaultfromdate = FwConvert.ToDateTime(table.Rows[r][27].ToString());
                    }
                    if (!table.Rows[r][28].ToString().Equals(""))
                    {
                        fromdate = FwConvert.ToDateTime(table.Rows[r][28].ToString());
                    }
                    if (!table.Rows[r][29].ToString().Equals(""))
                    {
                        defaulttodate = FwConvert.ToDateTime(table.Rows[r][29].ToString());
                    }
                    if (!table.Rows[r][30].ToString().Equals(""))
                    {
                        todate = FwConvert.ToDateTime(table.Rows[r][30].ToString());
                    }

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
                    w.defaultDataNumberFormatMask = defaultdatanumberformatmask;
                    w.dataNumberFormatId = datanumberformatid;
                    w.dataNumberFormat = datanumberformat;
                    w.dataNumberFormatMask = datanumberformatmask;

                    w.defaultDateBehavior = defaultdatebehavior;
                    w.dateBehavior = datebehavior;
                    w.dateFieldDisplayNames = datefielddisplaynames;
                    w.dateFields = datefields;
                    w.defaultDateField = defaultdatefield;
                    w.dateField = datefield;
                    w.defaultFromDate = defaultfromdate;
                    w.fromDate = fromdate;
                    w.defaultToDate = defaulttodate;
                    w.toDate = todate;

                    Widgets.Add(w);
                    DefaultWidgets.Add(w);
                    loaded = true;
                }
            }
            return loaded;
        }
        //------------------------------------------------------------------------------------
        public override async Task<int> SaveAsync(FwBusinessLogic original)
        {
            int savedCount = 0;

            // can remove all this "prev" stuff if Dashboard Settings form can send back the actual "userWidgetId"
            UserDashboardSettingsLogic lPrev = new UserDashboardSettingsLogic();
            lPrev.SetDbConfig(_dbConfig);
            lPrev.SetDependencies(AppConfig, UserSession);
            await lPrev.LoadAsync<UserDashboardSettingsLogic>(new object[] { UserId });

            UserDashboardSetting wPrev = null;
            UserWidgetLogic uw = null;

            if (WidgetsPerRow != null)
            {
                WebUserRecord webUser = new WebUserRecord();
                webUser.SetDependencies(AppConfig, UserSession);
                webUser.WebUserId = UserId;
                webUser.DashboardWidgetsPerRow = WidgetsPerRow;
                await webUser.SaveAsync(null);

            }

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
                    uw.SetDependencies(AppConfig, UserSession);
                    if (!wPrev.userWidgetId.Equals(string.Empty))
                    {
                        object[] pk = { wPrev.userWidgetId };
                        await uw.LoadAsync<UserWidgetLogic>(pk);
                        uw.AppConfig = AppConfig;
                    }
                    uw.UserId = UserId;
                    uw.WidgetId = w.value;
                    uw.OrderBy = widgetPosition;

                    if (wPrev.selected.GetValueOrDefault(false) && (!w.selected.GetValueOrDefault(false)))
                    {
                        await uw.DeleteAsync();
                    }
                    else if (w.selected.GetValueOrDefault(false))
                    {
                        await uw.SaveAsync(null);
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
