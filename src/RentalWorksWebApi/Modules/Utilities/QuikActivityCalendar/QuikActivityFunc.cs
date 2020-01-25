using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Utilities.QuikActivity
{
    public static class QuikActivityFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public class TQuikActivityCalendarEvent
        {
            public string start { get; set; }
            public string end { get; set; }
            public string text { get; set; }
            public string backColor { get; set; }
            public string textColor { get; set; }
            public string activityType { get; set; }
            public string id { get; set; } = "";
        }
        //-------------------------------------------------------------------------------------------------------
        public class TQuikActivityCalendarResponse
        {
            public string SessionId { get; set; } = "";
            public List<TQuikActivityCalendarEvent> QuikActivityCalendarEvents { get; set; } = new List<TQuikActivityCalendarEvent>();
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TQuikActivityCalendarResponse> GetQuikActivityCalendarData(FwApplicationConfig appConfig, FwUserSession userSession, string WarehouseId, DateTime FromDate, DateTime ToDate, bool IncludeTimes, string ActivityTypeId)
        {
            TQuikActivityCalendarResponse response = new TQuikActivityCalendarResponse();
            string sessionId = AppFunc.GetNextIdAsync(appConfig).Result;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getquikactivitydatasummary", appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, sessionId);
                    qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, WarehouseId);
                    qry.AddParameter("@fromdate", SqlDbType.DateTime, ParameterDirection.Input, FromDate);
                    qry.AddParameter("@todate", SqlDbType.DateTime, ParameterDirection.Input, ToDate);
                    qry.AddParameter("@includetimes", SqlDbType.NVarChar, ParameterDirection.Input, IncludeTimes);
                    qry.AddParameter("@activitytypeid", SqlDbType.NVarChar, ParameterDirection.Input, ActivityTypeId);
                    FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync(true);

                    foreach (List<object> row in dt.Rows)
                    {
                        TQuikActivityCalendarEvent ev = new TQuikActivityCalendarEvent();
                        ev.id = row[dt.GetColumnNo("id")].ToString();
                        ev.start = FwConvert.ToDateTime(row[dt.GetColumnNo("fromdatetime")].ToString()).ToString("yyyy-MM-ddTHH:mm:ss tt");
                        ev.end = FwConvert.ToDateTime(row[dt.GetColumnNo("todatetime")].ToString()).ToString("yyyy-MM-ddTHH:mm:ss tt");
                        ev.text = row[dt.GetColumnNo("description")].ToString();
                        ev.backColor = FwConvert.OleColorToHtmlColor(FwConvert.ToInt32(row[dt.GetColumnNo("color")]));
                        ev.textColor = FwConvert.OleColorToHtmlColor(FwConvert.ToInt32(row[dt.GetColumnNo("textcolor")]));
                        ev.activityType = row[dt.GetColumnNo("activitytype")].ToString();
                        response.QuikActivityCalendarEvents.Add(ev);
                    }
                    response.SessionId = sessionId;

                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
