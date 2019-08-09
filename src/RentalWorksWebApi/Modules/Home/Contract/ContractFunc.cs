using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Home.PurchaseOrder;
using WebLibrary;

namespace WebApi.Modules.Home.Contract
{
    public class CancelContractRequest
    {
        public string ContractId;
    }
    public class VoidContractRequest
    {
        public string ContractId { get; set; }
    }
    public class ChangeContractBillingDateRequest
    {
        public string ContractId { get; set; }
        public string OrderId { get; set; }
        public string PurchaseOrderId { get; set; }
        public DateTime OldBillingDate { get; set; }
        public DateTime NewBillingDate { get; set; }
        public string Reason { get; set; }
    }


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
        public static async Task<bool> SuspendedSessionsExist(FwApplicationConfig appConfig, FwUserSession userSession, string sessionType, string warehouseId)
        {
            bool suspendedSessionsExist = false;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "suspendedsessionsexist", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessiontype", SqlDbType.NVarChar, ParameterDirection.Input, sessionType);
                qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, warehouseId);
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
        public static async Task<TSpStatusResponse> VoidContractAsync(FwApplicationConfig appConfig, FwUserSession userSession, VoidContractRequest request)
        {

            TSpStatusResponse response = new TSpStatusResponse();
            //using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            //{
            //    FwSqlCommand qry = new FwSqlCommand(conn, "voidcontract", appConfig.DatabaseSettings.QueryTimeout);
            //    qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, request.ContractId);
            //    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
            //    await qry.ExecuteNonQueryAsync();
            //}
            response.msg = "Not yet programmed.";
            await Task.CompletedTask;
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TSpStatusResponse> UpdateContractBillingDate(FwApplicationConfig appConfig, FwUserSession userSession, ChangeContractBillingDateRequest request, FwSqlConnection conn = null)
        {

            TSpStatusResponse response = new TSpStatusResponse();
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }


            //#jhtodo should not require an OrderId here. But I don't want to change the SP until we branch the back-end code
            FwSqlCommand orderQry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
            orderQry.Add("select top 1 oc.orderid, oc.poid   ");
            orderQry.Add(" from  ordercontract oc            ");
            orderQry.Add(" where oc.contractid = @contractid ");
            orderQry.AddParameter("@contractid", request.ContractId);
            FwJsonDataTable dt = await orderQry.QueryToFwJsonTableAsync();
            string contractOrderId = dt.Rows[0][dt.GetColumnNo("orderid")].ToString();
            string contractPoId = dt.Rows[0][dt.GetColumnNo("poid")].ToString();
            //#jhtodo end

            FwSqlCommand qry = new FwSqlCommand(conn, "updatecontractrentaldate", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, request.ContractId);
            qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, contractOrderId);
            qry.AddParameter("@poid", SqlDbType.NVarChar, ParameterDirection.Input, contractPoId);
            qry.AddParameter("@oldrentaldate", SqlDbType.Date, ParameterDirection.Input, request.OldBillingDate);
            qry.AddParameter("@newrentaldate", SqlDbType.Date, ParameterDirection.Input, request.NewBillingDate);
            qry.AddParameter("@reason", SqlDbType.NVarChar, ParameterDirection.Input, request.Reason);
            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
            await qry.ExecuteNonQueryAsync();
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
