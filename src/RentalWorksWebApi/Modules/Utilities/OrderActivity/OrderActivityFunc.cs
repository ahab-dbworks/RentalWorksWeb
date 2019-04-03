using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace WebApi.Modules.Utilities.OrderActivity
{
    public static class OrderActivityFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public class TOrderActivityCalendarEvent
        {
            public string start { get; set; }
            public string end { get; set; }
            public string text { get; set; }
            public string backColor { get; set; }
            public string textColor { get; set; }
            public string id { get; set; } = "";
        }
        //-------------------------------------------------------------------------------------------------------
        public class TOrderActivityCalendarResponse
        {
            public List<TOrderActivityCalendarEvent> OrderActivityCalendarEvents { get; set; } = new List<TOrderActivityCalendarEvent>();
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TOrderActivityCalendarResponse> GetOrderActivityCalendarData(FwApplicationConfig appConfig, FwUserSession userSession, string WarehouseId, DateTime FromDate, DateTime ToDate)
        {
            TOrderActivityCalendarResponse response = new TOrderActivityCalendarResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getorderactivitydata", appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, WarehouseId);
                    qry.AddParameter("@fromdate", SqlDbType.DateTime, ParameterDirection.Input, FromDate);
                    qry.AddParameter("@todate", SqlDbType.DateTime, ParameterDirection.Input, ToDate);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    //qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    //qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);

                    FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync(true);

                    //response.success = (qry.GetParameter("@status").ToInt32() == 0);
                    //response.msg = qry.GetParameter("@msg").ToString();

                    foreach (List<object> row in dt.Rows)
                    {
                        TOrderActivityCalendarEvent ev = new TOrderActivityCalendarEvent();
                        ev.id = row[dt.GetColumnNo("id")].ToString();
                        ev.start = FwConvert.ToDateTime(row[dt.GetColumnNo("fromdate")].ToString()).ToString("yyyy-MM-ddTHH:mm:ss tt");
                        ev.end = FwConvert.ToDateTime(row[dt.GetColumnNo("todate")].ToString()).ToString("yyyy-MM-ddTHH:mm:ss tt");
                        ev.text = row[dt.GetColumnNo("description")].ToString();
                        ev.backColor = FwConvert.OleColorToHtmlColor(FwConvert.ToInt32(row[dt.GetColumnNo("color")]));
                        ev.textColor = FwConvert.OleColorToHtmlColor(FwConvert.ToInt32(row[dt.GetColumnNo("textcolor")]));
                        response.OrderActivityCalendarEvents.Add(ev);
                    }
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
