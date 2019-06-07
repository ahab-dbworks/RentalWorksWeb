using FwStandard.Models;
using FwStandard.SqlServer;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Home.LossAndDamage
{


    public class SelectAllNoneLossAndDamageItemRequest
    {
        [Required]
        public string SessionId { get; set; }
    }


    public class SelectAllNoneLossAndDamageItemResponse : TSpStatusResponse
    {
    }




    public class RetireLossAndDamageItemRequest
    {
        [Required]
        public string OrderId { get; set; }
    }


    public class RetireLossAndDamageItemResponse : TSpStatusResponse
    {
        public string ContractId { get; set; }
    }

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
                await qry.ExecuteNonQueryAsync();
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
                await qry.ExecuteNonQueryAsync();
                response.NewQuantity = qry.GetParameter("@newqty").ToInt32();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------


        private static async Task<SelectAllNoneLossAndDamageItemResponse> SelectAllNoneLossAndDamageItem(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId, bool selectAll)
        {
            SelectAllNoneLossAndDamageItemResponse response = new SelectAllNoneLossAndDamageItemResponse();
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
        public static async Task<SelectAllNoneLossAndDamageItemResponse> SelectAllLossAndDamageItem(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId)
        {
            return await SelectAllNoneLossAndDamageItem(appConfig, userSession, sessionId, true);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNoneLossAndDamageItemResponse> SelectNoneLossAndDamageItem(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId)
        {
            return await SelectAllNoneLossAndDamageItem(appConfig, userSession, sessionId, false);
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
                await qry.ExecuteNonQueryAsync();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------



        public static async Task<RetireLossAndDamageItemResponse> RetireLossAndDamageItem(FwApplicationConfig appConfig, FwUserSession userSession, string orderId)
        {
            RetireLossAndDamageItemResponse response = new RetireLossAndDamageItemResponse();
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
