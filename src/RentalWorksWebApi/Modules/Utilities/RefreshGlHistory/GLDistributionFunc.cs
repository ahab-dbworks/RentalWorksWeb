using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Utilities.GLDistribution
{
    public class RefreshGLHistoryRequest
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class RefreshGLHistoryResponse : TSpStatusResponse
    {
    }


    public static class GLDistributionFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<RefreshGLHistoryResponse> RefreshGLHistory(FwApplicationConfig appConfig, FwUserSession userSession, RefreshGLHistoryRequest request)
        {
            RefreshGLHistoryResponse response = new RefreshGLHistoryResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "refreshgl", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@fromdate", SqlDbType.NVarChar, ParameterDirection.Input, request.FromDate);
                qry.AddParameter("@todate", SqlDbType.NVarChar, ParameterDirection.Input, request.ToDate);
                await qry.ExecuteNonQueryAsync();
                //response.status = qry.GetParameter("@status").ToInt32();
                //response.success = (response.status == 0);
                //response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
