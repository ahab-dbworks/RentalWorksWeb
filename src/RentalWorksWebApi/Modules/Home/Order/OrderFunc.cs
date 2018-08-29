using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Modules.Home.OrderItem;
using WebApi.Modules.Home.SubPurchaseOrderItem;

namespace WebApi.Modules.Home.Order
{
    public static class OrderFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string> InsertPackage(FwApplicationConfig appConfig, FwUserSession userSession, OrderItemLogic oi)
        {
            string newOrderItemId = "";
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "insertpackage", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, oi.OrderId);
                qry.AddParameter("@packageid", SqlDbType.NVarChar, ParameterDirection.Input, oi.InventoryId);
                qry.AddParameter("@nestedmasteritemid", SqlDbType.NVarChar, ParameterDirection.Input, "");
                qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, oi.WarehouseId);
                qry.AddParameter("@catalogid", SqlDbType.NVarChar, ParameterDirection.Input, "");
                qry.AddParameter("@rectype", SqlDbType.NVarChar, ParameterDirection.Input, oi.RecType);
                qry.AddParameter("@qty", SqlDbType.NVarChar, ParameterDirection.Input, oi.QuantityOrdered);
                qry.AddParameter("@docheckoutaudit", SqlDbType.NVarChar, ParameterDirection.Input, "F");
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@forcenewrecord", SqlDbType.NVarChar, ParameterDirection.Input, "T");
                qry.AddParameter("@primarymasteritemid", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                newOrderItemId = qry.GetParameter("@primarymasteritemid").ToString();
            }
            return newOrderItemId;
        }
        //-------------------------------------------------------------------------------------------------------

        public static async Task<bool> UpdatePackageQuantities(FwApplicationConfig appConfig, FwUserSession userSession, OrderItemLogic oi)
        {
            bool success = false;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "updatepackageqtys", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, oi.OrderId);
                qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, oi.OrderItemId);
                qry.AddParameter("@newqty", SqlDbType.NVarChar, ParameterDirection.Input, oi.QuantityOrdered);
                qry.AddParameter("@docheckoutaudit", SqlDbType.NVarChar, ParameterDirection.Input, "F");
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@rowsummarized", SqlDbType.NVarChar, ParameterDirection.Input, "F");
                await qry.ExecuteNonQueryAsync(true);
                success = true;
            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------

        public static async Task<CreatePoWorksheetSessionResponse> StartPoWorksheetSession(FwApplicationConfig appConfig, FwUserSession userSession, CreatePoWorksheetSessionRequest request)
        {
            CreatePoWorksheetSessionResponse response = new CreatePoWorksheetSessionResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "startpoworksheetsession", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                qry.AddParameter("@rectype", SqlDbType.NVarChar, ParameterDirection.Input, request.RecType);
                qry.AddParameter("@vendorid", SqlDbType.NVarChar, ParameterDirection.Input, request.VendorId);
                qry.AddParameter("@ratetype", SqlDbType.NVarChar, ParameterDirection.Input, request.RateType);
                qry.AddParameter("@billperiodid", SqlDbType.NVarChar, ParameterDirection.Input, request.BillingCycleId);
                if (request.RequiredDate != null)
                {
                    qry.AddParameter("@requireddate", SqlDbType.Date, ParameterDirection.Input, request.RequiredDate);
                }
                qry.AddParameter("@requiredtime", SqlDbType.NVarChar, ParameterDirection.Input, request.RequiredTime);
                qry.AddParameter("@rentfromdate", SqlDbType.Date, ParameterDirection.Input, request.FromDate);
                if (request.ToDate != null)
                {
                    qry.AddParameter("@renttodate", SqlDbType.Date, ParameterDirection.Input, request.ToDate);
                }
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@deliveryid", SqlDbType.NVarChar, ParameterDirection.Input, request.DeliveryId);
                qry.AddParameter("@adjustcontractdate", SqlDbType.NVarChar, ParameterDirection.Input, (request.AdjustContractDates.GetValueOrDefault(false) ? "T" : "F"));
                qry.AddParameter("@contactid", SqlDbType.NVarChar, ParameterDirection.Input, request.ContactId);
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
        private static async Task<SelectAllNonePoWorksheetItemResponse> SelectAllNonePoWorksheetItem(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId, bool selectAll)
        {
            SelectAllNonePoWorksheetItemResponse response = new SelectAllNonePoWorksheetItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "selectallpoworksheetitems", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, sessionId);
                //qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
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
        public static async Task<SelectAllNonePoWorksheetItemResponse> SelectAllPoWorksheetItem(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId)
        {
            return await SelectAllNonePoWorksheetItem(appConfig, userSession, sessionId, true);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNonePoWorksheetItemResponse> SelectNonePoWorksheetItem(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId)
        {
            return await SelectAllNonePoWorksheetItem(appConfig, userSession, sessionId, false);
        }
        //-------------------------------------------------------------------------------------------------------



        public static async Task<CompletePoWorksheetSessionResponse> CompletePoWorksheetSession(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId)
        {
            CompletePoWorksheetSessionResponse response = new CompletePoWorksheetSessionResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "completepoworksheetsession", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, sessionId);
                qry.AddParameter("@poid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.PurchaseOrderId = qry.GetParameter("@poid").ToString();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
