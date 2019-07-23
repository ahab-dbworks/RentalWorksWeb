using FwStandard.Models;
using FwStandard.SqlServer;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Utilities.MigrateOrders;
using WebLibrary;

namespace WebApi.Modules.Home.MigrateOrders
{


    public class SelectAllNoneMigrateOrdersItemRequest
    {
        [Required]
        public string SessionId { get; set; }
    }


    public class SelectAllNoneMigrateOrdersItemResponse : TSpStatusResponse
    {
    }




    public class RetireMigrateOrdersItemRequest
    {
        [Required]
        public string OrderId { get; set; }
    }


    public class RetireMigrateOrdersItemResponse : TSpStatusResponse
    {
        public string ContractId { get; set; }
    }

    public static class MigrateOrdersFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<StartMigrateOrdersSessionResponse> StartSession(FwApplicationConfig appConfig, FwUserSession userSession, StartMigrateOrdersSessionRequest request)
        {
            StartMigrateOrdersSessionResponse response = new StartMigrateOrdersSessionResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "startldsession", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@dealid", SqlDbType.NVarChar, ParameterDirection.Input, request.DealId);
                qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, request.WarehouseId);
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
        public static async Task<UpdateMigrateOrdersItemResponse> UpdateItem(FwApplicationConfig appConfig, FwUserSession userSession, UpdateMigrateOrdersItemRequest request)
        {
            UpdateMigrateOrdersItemResponse response = new UpdateMigrateOrdersItemResponse();
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
                await qry.ExecuteNonQueryAsync();
                response.NewQuantity = qry.GetParameter("@newqty").ToInt32();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------


        private static async Task<SelectAllNoneMigrateOrdersItemResponse> SelectAllNoneMigrateOrdersItem(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId, bool selectAll)
        {
            SelectAllNoneMigrateOrdersItemResponse response = new SelectAllNoneMigrateOrdersItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "webldselectallnone", appConfig.DatabaseSettings.QueryTimeout);
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
        public static async Task<SelectAllNoneMigrateOrdersItemResponse> SelectAllMigrateOrdersItem(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId)
        {
            return await SelectAllNoneMigrateOrdersItem(appConfig, userSession, sessionId, true);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNoneMigrateOrdersItemResponse> SelectNoneMigrateOrdersItem(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId)
        {
            return await SelectAllNoneMigrateOrdersItem(appConfig, userSession, sessionId, false);
        }
        //-------------------------------------------------------------------------------------------------------



        public static async Task<CompleteMigrateOrdersSessionResponse> CompleteSession(FwApplicationConfig appConfig, FwUserSession userSession, CompleteMigrateOrdersSessionRequest request)
        {
            // consider implementing this to let the API create the new Order for the user.
            //create procedure dbo.createldorder(@sourceorderid char (08),
            //                           @currencyid char (08) = '',
            //                           @usersid char (08),
            //                           @destorderid char (08) output)

            CompleteMigrateOrdersSessionResponse response = new CompleteMigrateOrdersSessionResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "webldcopysession", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
                qry.AddParameter("@destorderid", SqlDbType.NVarChar, ParameterDirection.Input, request.SourceOrderId);
                qry.AddParameter("@usevalue", SqlDbType.NVarChar, ParameterDirection.Input, "");
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



        public static async Task<RetireMigrateOrdersItemResponse> RetireMigrateOrdersItem(FwApplicationConfig appConfig, FwUserSession userSession, string orderId)
        {
            RetireMigrateOrdersItemResponse response = new RetireMigrateOrdersItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "ldcreatecontractandretire", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                //qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                //qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@lostcontractid", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                //response.status = qry.GetParameter("@status").ToInt32();
                //response.success = (response.status == 0);
                //response.msg = qry.GetParameter("@msg").ToString();
                response.ContractId = qry.GetParameter("@lostcontractid").ToString();
                response.success = true;
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
