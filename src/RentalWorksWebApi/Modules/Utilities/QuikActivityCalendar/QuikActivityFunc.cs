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
        public class QuikActivityCalendarRequest
        {
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public string OfficeLocationId { get; set; }
            public string WarehouseId { get; set; }
            public string DepartmentId { get; set; }
            public string ActivityTypeId { get; set; }
            public string AssignedToUserId { get; set; }
            public bool? IncludeCompleted { get; set; }
            public bool? IncludeTimes { get; set; }
        }
        public class QuikActivityCalendarEvent
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
            public List<QuikActivityCalendarEvent> QuikActivityCalendarEvents { get; set; } = new List<QuikActivityCalendarEvent>();
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TQuikActivityCalendarResponse> GetQuikActivityCalendarData(FwApplicationConfig appConfig, FwUserSession userSession, QuikActivityCalendarRequest request)
        {
            TQuikActivityCalendarResponse response = new TQuikActivityCalendarResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getquikactivitydatasummary2", appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@fromdate", SqlDbType.DateTime, ParameterDirection.Input, request.FromDate);
                    qry.AddParameter("@todate", SqlDbType.DateTime, ParameterDirection.Input, request.ToDate);
                    qry.AddParameter("@locationid", SqlDbType.NVarChar, ParameterDirection.Input, request.OfficeLocationId);
                    qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, request.WarehouseId);
                    qry.AddParameter("@departmentid", SqlDbType.NVarChar, ParameterDirection.Input, request.DepartmentId);
                    qry.AddParameter("@activitytypeid", SqlDbType.NVarChar, ParameterDirection.Input, request.ActivityTypeId);
                    qry.AddParameter("@assignedtousersid", SqlDbType.NVarChar, ParameterDirection.Input, request.AssignedToUserId);
                    qry.AddParameter("@includecompleted", SqlDbType.NVarChar, ParameterDirection.Input, request.IncludeCompleted);
                    qry.AddParameter("@includetimes", SqlDbType.NVarChar, ParameterDirection.Input, request.IncludeTimes);
                    FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync(true);

                    foreach (List<object> row in dt.Rows)
                    {
                        QuikActivityCalendarEvent ev = new QuikActivityCalendarEvent();
                        ev.id = row[dt.GetColumnNo("id")].ToString();
                        ev.start = FwConvert.ToDateTime(row[dt.GetColumnNo("fromdatetime")].ToString()).ToString("yyyy-MM-ddTHH:mm:ss tt");
                        ev.end = FwConvert.ToDateTime(row[dt.GetColumnNo("todatetime")].ToString()).ToString("yyyy-MM-ddTHH:mm:ss tt");
                        ev.text = row[dt.GetColumnNo("description")].ToString();
                        ev.backColor = FwConvert.OleColorToHtmlColor(FwConvert.ToInt32(row[dt.GetColumnNo("color")]));
                        ev.textColor = FwConvert.OleColorToHtmlColor(FwConvert.ToInt32(row[dt.GetColumnNo("textcolor")]));
                        ev.activityType = row[dt.GetColumnNo("activitytype")].ToString();
                        response.QuikActivityCalendarEvents.Add(ev);
                    }
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
