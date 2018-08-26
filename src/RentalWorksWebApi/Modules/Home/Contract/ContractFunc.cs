using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Home.PurchaseOrder;

namespace WebApi.Modules.Home.Contract
{
    public static class ContractFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TSpStatusReponse> AssignContract(FwApplicationConfig appConfig, FwUserSession userSession, string contractId)
        {
            TSpStatusReponse response = new TSpStatusReponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "assigncontract", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                //qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                //qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                //qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
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
                await qry.ExecuteNonQueryAsync(true);
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
                await qry.ExecuteNonQueryAsync(true);
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //------------------------------------------------------------------------------------------------------- 
    }
}
