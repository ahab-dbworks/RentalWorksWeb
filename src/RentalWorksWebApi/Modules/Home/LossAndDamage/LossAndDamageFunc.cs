using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;

namespace WebApi.Modules.Home.LossAndDamage
{
    public static class LossAndDamageFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<StartLossAndDamageSessionResponse> StartSession(FwApplicationConfig appConfig, FwUserSession userSession, StartLossAndDamageSessionRequest request)
        {
            StartLossAndDamageSessionResponse response = new StartLossAndDamageSessionResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "startldsession", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@dealid", SqlDbType.NVarChar, ParameterDirection.Input, request.DealId);
                qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, request.WarehouseId);
                qry.AddParameter("@orderids", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderIds);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.SessionId = qry.GetParameter("@sessionid").ToString();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<UpdateLossAndDamageItemResponse> UpdateItem(FwApplicationConfig appConfig, FwUserSession userSession, UpdateLossAndDamageItemRequest request)
        {
            UpdateLossAndDamageItemResponse response = new UpdateLossAndDamageItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "updatelditem", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderItemId);
                qry.AddParameter("@barcode", SqlDbType.NVarChar, ParameterDirection.Input, request.BarCode);
                qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, request.Quantity);
                qry.AddParameter("@newqty", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.NewQuantity = qry.GetParameter("@newqty").ToInt32();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------

        public static async Task<CompleteLossAndDamageSessionResponse> CompleteSession(FwApplicationConfig appConfig, FwUserSession userSession, CompleteLossAndDamageSessionRequest request)
        {
            // consider implementing this to let the API create the new Order for the user.
            //create procedure dbo.createldorder(@sourceorderid char (08),
            //                           @currencyid char (08) = '',
            //                           @usersid char (08),
            //                           @destorderid char (08) output)

            CompleteLossAndDamageSessionResponse response = new CompleteLossAndDamageSessionResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "webldcopysession", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
                qry.AddParameter("@destorderid", SqlDbType.NVarChar, ParameterDirection.Input, request.SourceOrderId);
                qry.AddParameter("@usevalue", SqlDbType.NVarChar, ParameterDirection.Input, "");
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
