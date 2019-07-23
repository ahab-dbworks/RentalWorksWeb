using FwStandard.Models;
using FwStandard.SqlServer;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Utilities.Migrate
{


    public class SelectAllNoneMigrateItemRequest
    {
        [Required]
        public string SessionId { get; set; }
    }


    public class SelectAllNoneMigrateItemResponse : TSpStatusResponse
    {
    }


    public static class MigrateFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<StartMigrateSessionResponse> StartSession(FwApplicationConfig appConfig, FwUserSession userSession, StartMigrateSessionRequest request)
        {
            StartMigrateSessionResponse response = new StartMigrateSessionResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "startmigratesession", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@dealid", SqlDbType.NVarChar, ParameterDirection.Input, request.DealId);
                qry.AddParameter("@department", SqlDbType.NVarChar, ParameterDirection.Input, request.DepartmentId);
                qry.AddParameter("@orderids", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderIds);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.SessionId = qry.GetParameter("@sessionid").ToString();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<UpdateMigrateItemResponse> UpdateItem(FwApplicationConfig appConfig, FwUserSession userSession, UpdateMigrateItemRequest request)
        {
            UpdateMigrateItemResponse response = new UpdateMigrateItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "updatemigrateitem", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderItemId);
                qry.AddParameter("@barcode", SqlDbType.NVarChar, ParameterDirection.Input, request.BarCode);
                qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, request.Quantity);
                qry.AddParameter("@newqty", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.NewQuantity = qry.GetParameter("@newqty").ToInt32();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------


        private static async Task<SelectAllNoneMigrateItemResponse> SelectAllNoneMigrateItem(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId, bool selectAll)
        {
            SelectAllNoneMigrateItemResponse response = new SelectAllNoneMigrateItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "webmigrateselectallnone", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, sessionId);
                qry.AddParameter("@selectallnone", SqlDbType.NVarChar, ParameterDirection.Input, selectAll ? RwConstants.SELECT_ALL : RwConstants.SELECT_NONE);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNoneMigrateItemResponse> SelectAllMigrateItem(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId)
        {
            return await SelectAllNoneMigrateItem(appConfig, userSession, sessionId, true);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNoneMigrateItemResponse> SelectNoneMigrateItem(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId)
        {
            return await SelectAllNoneMigrateItem(appConfig, userSession, sessionId, false);
        }
        //-------------------------------------------------------------------------------------------------------



        public static async Task<CompleteMigrateSessionResponse> CompleteSession(FwApplicationConfig appConfig, FwUserSession userSession, CompleteMigrateSessionRequest request)
        {
            // consider implementing this to let the API create the new Order for the user.
            //create procedure dbo.createldorder(@sourceorderid char (08),
            //                           @currencyid char (08) = '',
            //                           @usersid char (08),
            //                           @destorderid char (08) output)

            CompleteMigrateSessionResponse response = new CompleteMigrateSessionResponse();
            //using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            //{
            //    FwSqlCommand qry = new FwSqlCommand(conn, "webldcopysession", appConfig.DatabaseSettings.QueryTimeout);
            //    qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
            //    qry.AddParameter("@destorderid", SqlDbType.NVarChar, ParameterDirection.Input, request.SourceOrderId);
            //    qry.AddParameter("@usevalue", SqlDbType.NVarChar, ParameterDirection.Input, "");
            //    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
            //    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
            //    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
            //    await qry.ExecuteNonQueryAsync();
            //    response.status = qry.GetParameter("@status").ToInt32();
            //    response.success = (response.status == 0);
            //    response.msg = qry.GetParameter("@msg").ToString();
            //}
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
