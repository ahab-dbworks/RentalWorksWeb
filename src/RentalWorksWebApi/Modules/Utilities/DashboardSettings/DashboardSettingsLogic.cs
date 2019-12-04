using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Administrator.User;
using WebApi.Modules.Settings.AvailableWidget;
using WebApi.Modules.Settings.WebUserWidget;

namespace WebApi.Modules.Utilities.DashboardSettings
{
    [FwLogic(Id: "4wHzCi9ODcIR9")]
    public class DashboardSettingsLogic : AppBusinessLogic
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

            [FwLogicProperty(Id: "bWL2OfqVzNK")]
            public string modulename { get; set; }

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

            [FwLogicProperty(Id: "taukSU3mlAFvZ")]
            public string defaultDateBehaviorId { get; set; }


            [FwLogicProperty(Id: "DcfKXn0BmpO0l")]
            public string defaultDateBehavior { get; set; }

            [FwLogicProperty(Id: "4IBZ7aWIKV1lX")]
            public string dateBehaviorId { get; set; }

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
            [FwLogicProperty(Id: "IeEt7jOpL7PAi")]
            public string officeLocationId { get; set; }
            [FwLogicProperty(Id: "h1oiCe9vWJnTg")]
            public string officeLocation { get; set; }
            [FwLogicProperty(Id: "s7rkkN7Gajmol")]
            public string officeLocationCode { get; set; }
            [FwLogicProperty(Id: "B9aPJM7TeyjFG")]
            public bool? defaultStacked { get; set; }
            [FwLogicProperty(Id: "Fl99kdJF50cB3")]
            public bool? stacked { get; set; }
        }

        //public class AvailableWidget : WidgetLogic { }
        //------------------------------------------------------------------------------------
        public DashboardSettingsLogic()
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

        [FwLogicProperty(Id: "lqNWOoaHp83RB")]
        public List<AvailableWidgetLogic> AvailableWidgets { get; set; }
        [FwLogicProperty(Id: "oZMIf4l1CVS5")]
        public List<UserDashboardSetting> UserWidgets { get; set; }

        //------------------------------------------------------------------------------------
        public override async Task<bool> LoadAsync<T>(object[] primaryKeyValues, FwSqlConnection conn = null)
        {
            string webUsersId = primaryKeyValues[0].ToString();

            bool loaded = false;
            UserId = webUsersId;
            WebUserRecord webUser = new WebUserRecord();
            webUser.SetDependencies(AppConfig, UserSession);
            webUser.WebUserId = webUsersId;
            webUser = await webUser.GetAsync<WebUserRecord>();
            if (webUser != null)
            {
                WidgetsPerRow = webUser.DashboardWidgetsPerRow;
            }
            if ((WidgetsPerRow == null) || (WidgetsPerRow <= 0))
            {
                WidgetsPerRow = 2;
            }

            UserWidgets = new List<UserDashboardSetting>();

            BrowseRequest request = new BrowseRequest();
            request.pageno = 0;
            request.pagesize = 0;
            request.orderby = "Widget";

            //WidgetLogic l = new WidgetLogic();
            //l.SetDependencies(AppConfig, UserSession);
            //AvailableWidgets = await l.SelectAsync<WidgetLogic>(request);

            // now the available widgets list is filtered by my WebUsersId
            IDictionary<string, object> uniqueIds = new Dictionary<string, object>();
            uniqueIds.Add("WebUserId", UserSession.WebUsersId);
            request.uniqueids = uniqueIds;
            AvailableWidgetLogic l = new AvailableWidgetLogic();
            l.SetDependencies(AppConfig, UserSession);
            AvailableWidgets = await l.SelectAsync<AvailableWidgetLogic>(request);

            if (conn == null)
            {
                conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString);
            }

            FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout);
            qry.Add("exec getwebuserdashboardsettings @webusersid ");
            qry.AddColumn("webuserswidgetid");              //00
            qry.AddColumn("widgetid");                      //01
            qry.AddColumn("widget");                        //02
            qry.AddColumn("apiname");                       //03
            qry.AddColumn("clickpath");                     //04
            qry.AddColumn("defaulttype");                   //05
            qry.AddColumn("widgettype");                    //06
            qry.AddColumn("assignto");                      //07
            qry.AddColumn("defaultdatapoints");             //08
            qry.AddColumn("datapoints");                    //09
            qry.AddColumn("defaultaxisnumberformatid");     //10
            qry.AddColumn("defaultaxisnumberformat");       //11
            qry.AddColumn("defaultaxisnumberformatmask");   //12
            qry.AddColumn("axisnumberformatid");            //13
            qry.AddColumn("axisnumberformat");              //14
            qry.AddColumn("axisnumberformatmask");          //15
            qry.AddColumn("defaultdatanumberformatid");     //16
            qry.AddColumn("defaultdatanumberformat");       //17
            qry.AddColumn("defaultdatanumberformatmask");   //18
            qry.AddColumn("datanumberformatid");            //19
            qry.AddColumn("datanumberformat");              //20
            qry.AddColumn("datanumberformatmask");          //21
            qry.AddColumn("defaultdatebehaviorid");         //22
            qry.AddColumn("defaultdatebehavior");           //23
            qry.AddColumn("datebehaviorid");                //24
            qry.AddColumn("datebehavior");                  //25
            qry.AddColumn("datefielddisplaynames");         //26
            qry.AddColumn("datefields");                    //27
            qry.AddColumn("defaultdatefield");              //28
            qry.AddColumn("datefield");                     //29
            qry.AddColumn("defaultfromdate");               //30
            qry.AddColumn("fromdate");                      //31
            qry.AddColumn("defaulttodate");                 //32
            qry.AddColumn("todate");                        //33
            qry.AddColumn("locationid");                    //34
            qry.AddColumn("location");                      //35
            qry.AddColumn("locationcode");                  //36
            qry.AddColumn("defaultstacked");                //37
            qry.AddColumn("stacked");                       //38
            qry.AddColumn("orderby");                       //39
            qry.AddColumn("modulename");                    //40
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
                string assignto = table.Rows[r][7].ToString();
                int defaultdatapoints = Convert.ToInt32(table.Rows[r][8]);
                int datapoints = Convert.ToInt32(table.Rows[r][9]);
                string defaultaxisnumberformatid = table.Rows[r][10].ToString();
                string defaultaxisnumberformat = table.Rows[r][11].ToString();
                string defaultaxisnumberformatmask = table.Rows[r][12].ToString();
                string axisnumberformatid = table.Rows[r][13].ToString();
                string axisnumberformat = table.Rows[r][14].ToString();
                string axisnumberformatmask = table.Rows[r][15].ToString();
                string defaultdatanumberformatid = table.Rows[r][16].ToString();
                string defaultdatanumberformat = table.Rows[r][17].ToString();
                string defaultdatanumberformatmask = table.Rows[r][18].ToString();
                string datanumberformatid = table.Rows[r][19].ToString();
                string datanumberformat = table.Rows[r][20].ToString();
                string datanumberformatmask = table.Rows[r][21].ToString();
                string defaultdatebehaviorid = table.Rows[r][21].ToString();
                string defaultdatebehavior = table.Rows[r][23].ToString();
                string datebehaviorid = table.Rows[r][24].ToString();
                string datebehavior = table.Rows[r][25].ToString();
                string datefielddisplaynames = table.Rows[r][26].ToString();
                string datefields = table.Rows[r][27].ToString();
                string defaultdatefield = table.Rows[r][28].ToString();
                string datefield = table.Rows[r][29].ToString();
                DateTime? defaultfromdate = null;
                DateTime? fromdate = null;
                DateTime? defaulttodate = null;
                DateTime? todate = null;
                if (!table.Rows[r][30].ToString().Equals(""))
                {
                    defaultfromdate = FwConvert.ToDateTime(table.Rows[r][30].ToString());
                }
                if (!table.Rows[r][31].ToString().Equals(""))
                {
                    fromdate = FwConvert.ToDateTime(table.Rows[r][31].ToString());
                }
                if (!table.Rows[r][32].ToString().Equals(""))
                {
                    defaulttodate = FwConvert.ToDateTime(table.Rows[r][32].ToString());
                }
                if (!table.Rows[r][33].ToString().Equals(""))
                {
                    todate = FwConvert.ToDateTime(table.Rows[r][33].ToString());
                }
                string locationid = table.Rows[r][34].ToString();
                string location = table.Rows[r][35].ToString();
                string locationcode = table.Rows[r][36].ToString();
                bool defaultStacked = FwConvert.ToBoolean(table.Rows[r][37].ToString());
                bool stacked = FwConvert.ToBoolean(table.Rows[r][38].ToString());
                string modulename = table.Rows[r][40].ToString();
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

                w.defaultDateBehaviorId = defaultdatebehaviorid;
                w.defaultDateBehavior = defaultdatebehavior;
                w.dateBehaviorId = datebehaviorid;
                w.dateBehavior = datebehavior;
                w.dateFieldDisplayNames = datefielddisplaynames;
                w.dateFields = datefields;
                w.defaultDateField = defaultdatefield;
                w.dateField = datefield;
                w.defaultFromDate = defaultfromdate;
                w.fromDate = fromdate;
                w.defaultToDate = defaulttodate;
                w.toDate = todate;
                w.officeLocationId = locationid;
                w.officeLocation = location;
                w.officeLocationCode = locationcode;
                w.defaultStacked = defaultStacked;
                w.stacked = stacked;
                w.modulename = modulename;
                UserWidgets.Add(w);
                loaded = true;
            }
            return loaded;
        }
        //------------------------------------------------------------------------------------
        public override async Task<int> SaveAsync(FwBusinessLogic original, FwSqlConnection conn = null, TDataRecordSaveMode saveMode = TDataRecordSaveMode.Auto)
        {
            int savedCount = 0;
            DashboardSettingsLogic orig = new DashboardSettingsLogic();
            orig.SetDependencies(AppConfig, UserSession);
            orig.UserId = UserId;
            await orig.LoadAsync<DashboardSettingsLogic>();

            if (WidgetsPerRow != null)
            {
                WebUserRecord webUser = new WebUserRecord();
                webUser.SetDependencies(AppConfig, UserSession);
                webUser.WebUserId = UserId;
                webUser.DashboardWidgetsPerRow = WidgetsPerRow;
                await webUser.SaveAsync(null);
            }

            int widgetPosition = 0;
            foreach (UserDashboardSetting w in UserWidgets)
            {
                UserWidgetLogic uw = new UserWidgetLogic();
                uw.SetDependencies(AppConfig, UserSession);
                if (!string.IsNullOrEmpty(w.userWidgetId))
                {
                    uw.UserWidgetId = w.userWidgetId;
                    await uw.LoadAsync<UserWidgetLogic>();
                    uw.SetDependencies(AppConfig, UserSession);
                }
                uw.UserId = UserId;
                uw.WidgetId = w.value;
                uw.OrderBy = widgetPosition;
                uw.LoadOriginalBeforeSaving = false;

                if (w.selected.GetValueOrDefault(false))
                {
                    await uw.SaveAsync(null);
                }
                else
                {
                    await uw.DeleteAsync();
                }

                widgetPosition++;
                savedCount++;
            }

            //delete any widgets not included in the save list
            foreach (UserDashboardSetting wOrig in orig.UserWidgets)
            {
                bool exists = false;
                foreach (UserDashboardSetting w in UserWidgets)
                {
                    if (w.userWidgetId.Equals(wOrig.userWidgetId))
                    {
                        exists = true;
                    }
                }
                if (!exists)
                {
                    UserWidgetLogic uw = new UserWidgetLogic();
                    uw.SetDependencies(AppConfig, UserSession);
                    uw.UserWidgetId = wOrig.userWidgetId;
                    await uw.LoadAsync<UserWidgetLogic>();
                    uw.SetDependencies(AppConfig, UserSession);
                    await uw.DeleteAsync();
                }
            }

            return savedCount;
        }
        //------------------------------------------------------------------------------------
    }
}
