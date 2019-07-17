using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Home.PurchaseOrder;
using WebLibrary;

namespace WebApi.Modules.Home.Contract
{
    public static class ContractFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TSpStatusResponse> AssignContract(FwApplicationConfig appConfig, FwUserSession userSession, string contractId)
        {
            TSpStatusResponse response = new TSpStatusResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "assigncontract", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                //qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                //qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                //qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                //response.success = (qry.GetParameter("@status").ToInt32() == 0);
                //response.msg = qry.GetParameter("@msg").ToString();
                response.success = true;
                response.msg = "";
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<PurchaseOrderReceiveBarCodeAddItemsResponse> AddInventoryFromReceive(FwApplicationConfig appConfig, FwUserSession userSession, string poId, string contractId)
        {
            PurchaseOrderReceiveBarCodeAddItemsResponse response = new PurchaseOrderReceiveBarCodeAddItemsResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "addinventoryfromreceive", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@poid", SqlDbType.NVarChar, ParameterDirection.Input, poId);
                qry.AddParameter("@receivecontractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@itemsadded", SqlDbType.Int, ParameterDirection.Output);
                //qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                //qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                //response.success = (qry.GetParameter("@status").ToInt32() == 0);
                //response.msg = qry.GetParameter("@msg").ToString();
                response.ItemsAdded = qry.GetParameter("@itemsadded").ToInt32();
                response.success = true;
                response.msg = "";
            }
            return response;
        }
        //------------------------------------------------------------------------------------------------------- 
        public static async Task<PurchaseOrderReceiveAssignBarCodesResponse> AssignBarCodesFromReceive(FwApplicationConfig appConfig, FwUserSession userSession, string poId, string contractId)
        {
            PurchaseOrderReceiveAssignBarCodesResponse response = new PurchaseOrderReceiveAssignBarCodesResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "assignbarcodesfromreceive", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@poid", SqlDbType.NVarChar, ParameterDirection.Input, poId);
                qry.AddParameter("@receivecontractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //------------------------------------------------------------------------------------------------------- 
        public static async Task<bool> SuspendedSessionsExist(FwApplicationConfig appConfig, FwUserSession userSession, string sessionType, string orderType = "")
        {
            bool suspendedSessionsExist = false;
            if (orderType.Equals(""))
            {
                orderType = RwConstants.ORDER_TYPE_ORDER;
            }
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "suspendedsessionsexist", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@sessiontype", SqlDbType.NVarChar, ParameterDirection.Input, sessionType);
                qry.AddParameter("@ordertype", SqlDbType.NVarChar, ParameterDirection.Input, orderType);
                qry.AddParameter("@sessionsexist", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                suspendedSessionsExist = qry.GetParameter("@sessionsexist").ToString().Equals("T");
            }
            return suspendedSessionsExist;
        }
        //------------------------------------------------------------------------------------------------------- 
        public static async Task<TSpStatusResponse> CancelContract(FwApplicationConfig appConfig, FwUserSession userSession, CancelContractRequest request)
        {
            TSpStatusResponse response = new TSpStatusResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "cancelcontract", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, request.ContractId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                await qry.ExecuteNonQueryAsync();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
