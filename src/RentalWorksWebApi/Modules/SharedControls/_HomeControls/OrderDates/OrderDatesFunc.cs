using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Home.OrderDates
{

    //public class OrderDatesAndTime
    //{
    //    public string OrderTypeDateTypeId { get; set; }
    //    //public string ActivityType { get; set; }
    //    public DateTime? Date { get; set; }
    //    public string Time { get; set; }
    //    public bool? IsProductionActivity { get; set; }
    //    public bool? IsMilestone { get; set; }
    //}

    //public class ApplyOrderDatesAndTimesRequest
    //{
    //    public string OrderId;
    //    public List<OrderDatesAndTime> DatesAndTimes = new List<OrderDatesAndTime>();
    //}

    //public class ApplyOrderDatesAndTimesResponse : TSpStatusResponse
    //{
    //}

    public static class OrderDatesFunc
    {
        //-------------------------------------------------------------------------------------------------------
        //public static async Task<ApplyOrderDatesAndTimesResponse> ApplyOrderDatesAndTimes(FwApplicationConfig appConfig, FwUserSession userSession, ApplyOrderDatesAndTimesRequest request)
        //{
        //    ApplyOrderDatesAndTimesResponse response = new ApplyOrderDatesAndTimesResponse();
        //    response.success = true;  // initialize to true
        //    string session1Id = AppFunc.GetNextIdAsync(appConfig).Result;
        //    string session2Id = AppFunc.GetNextIdAsync(appConfig).Result;

        //    using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
        //    {
        //        if (response.success) 
        //        {
        //            FwSqlCommand qry = new FwSqlCommand(conn, "snapshotorderdatesandtimesweb", appConfig.DatabaseSettings.QueryTimeout);
        //            qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
        //            qry.AddParameter("@session1id", SqlDbType.NVarChar, ParameterDirection.Input, session1Id);
        //            qry.AddParameter("@session2id", SqlDbType.NVarChar, ParameterDirection.Input, session2Id);
        //            qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
        //            qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
        //            await qry.ExecuteNonQueryAsync();
        //            response.status = qry.GetParameter("@status").ToInt32();
        //            response.success = (response.status == 0);
        //            response.msg = qry.GetParameter("@msg").ToString();
        //        }

        //        if (response.success) // continue if no errors occurred above
        //        {
        //            foreach (OrderDatesAndTime dt in request.DatesAndTimes)
        //            {
        //                if (response.success) // continue while no errors have occurred
        //                {
        //                    FwSqlCommand qryDt = new FwSqlCommand(conn, "saveorderdateandtimeweb", appConfig.DatabaseSettings.QueryTimeout);
        //                    qryDt.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
        //                    qryDt.AddParameter("@ordertypedatetypeid", SqlDbType.NVarChar, ParameterDirection.Input, dt.OrderTypeDateTypeId);
        //                    qryDt.AddParameter("@session2id", SqlDbType.NVarChar, ParameterDirection.Input, session2Id);
        //                    qryDt.AddParameter("@date", SqlDbType.Date, ParameterDirection.Input, dt.Date);
        //                    qryDt.AddParameter("@time", SqlDbType.NVarChar, ParameterDirection.Input, dt.Time);
        //                    qryDt.AddParameter("@productionactivity", SqlDbType.NVarChar, ParameterDirection.Input, dt.IsProductionActivity);
        //                    qryDt.AddParameter("@milestone", SqlDbType.NVarChar, ParameterDirection.Input, dt.IsMilestone);
        //                    qryDt.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
        //                    qryDt.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
        //                    await qryDt.ExecuteNonQueryAsync();
        //                    response.status = qryDt.GetParameter("@status").ToInt32();
        //                    response.success = (response.status == 0);
        //                    response.msg = qryDt.GetParameter("@msg").ToString();
        //                }
        //            }
        //        }

        //        if (response.success) // continue if no errors occurred above
        //        {
        //            FwSqlCommand qry = new FwSqlCommand(conn, "applyorderdatesandtimesweb", appConfig.DatabaseSettings.QueryTimeout);
        //            qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
        //            qry.AddParameter("@session1id", SqlDbType.NVarChar, ParameterDirection.Input, session1Id);
        //            qry.AddParameter("@session2id", SqlDbType.NVarChar, ParameterDirection.Input, session2Id);
        //            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
        //            qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
        //            qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
        //            await qry.ExecuteNonQueryAsync();
        //            response.status = qry.GetParameter("@status").ToInt32();
        //            response.success = (response.status == 0);
        //            response.msg = qry.GetParameter("@msg").ToString();
        //        }
        //    }
        //    return response;
        //}
        ////-------------------------------------------------------------------------------------------------------
    }
}
