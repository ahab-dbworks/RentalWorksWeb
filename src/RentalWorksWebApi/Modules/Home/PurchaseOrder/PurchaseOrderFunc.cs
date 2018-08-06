using FwStandard.Models;
using FwStandard.SqlServer;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Modules.Home.PurchaseOrderReceiveItem;
using WebApi.Modules.Home.PurchaseOrderReturnItem;

namespace WebApi.Modules.Home.PurchaseOrder
{

    public static class PurchaseOrderFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ReceiveItemResponse> ReceiveItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId, string purchaseOrderItemId, int quantity)
        {
            ReceiveItemResponse response = new ReceiveItemResponse();
            response.ContractId = contractId;
            response.PurchaseOrderId = purchaseOrderId;
            response.PurchaseOrderItemId = purchaseOrderItemId;
            response.Quantity = quantity;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "receiveitem2", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderId);
                qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderItemId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@barcode", SqlDbType.NVarChar, ParameterDirection.Input, "");
                qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, quantity);
                qry.AddParameter("@qtyordered", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@qtyreceived", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@qtyneedbarcode", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@qtycolor", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.QuantityOrdered = qry.GetParameter("@qtyordered").ToDouble();
                response.QuantityReceived = qry.GetParameter("@qtyreceived").ToDouble();
                response.QuantityNeedBarCode = qry.GetParameter("@qtyneedbarcode").ToDouble();
                if (!qry.GetParameter("@qtycolor").IsDbNull())
                {
                    response.QuantityColor = FwConvert.OleColorToHtmlColor(qry.GetParameter("@qtycolor").ToInt32());
                }
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<SelectAllNoneReceiveItemResponse> SelectAllNoneReceiveItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId, bool selectAll)
        {
            SelectAllNoneReceiveItemResponse response = new SelectAllNoneReceiveItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "selectallreceivefromvendor", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@poid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderId);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@selectallnone", SqlDbType.NVarChar, ParameterDirection.Input, (selectAll ? "A" : "N"));
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNoneReceiveItemResponse> SelectAllReceiveItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId)
        {
            return await SelectAllNoneReceiveItem(appConfig, userSession, contractId, purchaseOrderId, true);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNoneReceiveItemResponse> SelectNoneReceiveItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId)
        {
            return await SelectAllNoneReceiveItem(appConfig, userSession, contractId, purchaseOrderId, false);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ReturnItemResponse> ReturnItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId, string purchaseOrderItemId, int quantity)
        {
            ReturnItemResponse response = new ReturnItemResponse();
            response.ContractId = contractId;
            response.PurchaseOrderId = purchaseOrderId;
            response.PurchaseOrderItemId = purchaseOrderItemId;
            response.Quantity = quantity;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "returnitem", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderId);
                qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderItemId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@barcode", SqlDbType.NVarChar, ParameterDirection.Input, "");
                qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, quantity);
                qry.AddParameter("@qtyordered", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@qtyreceived", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@qtyreturned", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.QuantityOrdered = qry.GetParameter("@qtyordered").ToDouble();
                response.QuantityReceived = qry.GetParameter("@qtyreceived").ToDouble();
                response.QuantityReturned = qry.GetParameter("@qtyreturned").ToDouble();
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<SelectAllNoneReturnItemResponse> SelectAllNoneReturnItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId, bool selectAll)
        {
            SelectAllNoneReturnItemResponse response = new SelectAllNoneReturnItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "selectallreturntovendor", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@poid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderId);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@selectallnone", SqlDbType.NVarChar, ParameterDirection.Input, (selectAll ? "A" : "N"));
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNoneReturnItemResponse> SelectAllReturnItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId)
        {
            return await SelectAllNoneReturnItem(appConfig, userSession, contractId, purchaseOrderId, true);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNoneReturnItemResponse> SelectNoneReturnItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId)
        {
            return await SelectAllNoneReturnItem(appConfig, userSession, contractId, purchaseOrderId, false);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<List<string>> CreateOutContractsFromReceive(FwApplicationConfig appConfig, FwUserSession userSession, string receiveContractId)
        {
            List<string> contractIds = new List<string>();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                string[] outContractIds = new string[0];
                FwSqlCommand qry = new FwSqlCommand(conn, "createoutcontractsfromreceive", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@receivecontractid", SqlDbType.NVarChar, ParameterDirection.Input, receiveContractId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@outcontractids", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                outContractIds = qry.GetParameter("@outcontractids").ToString().Split(',');
                foreach (string outContractId in outContractIds)
                {
                    contractIds.Add(outContractId);
                }
            }
            return contractIds;
        }
        //-------------------------------------------------------------------------------------------------------            
    }
}
