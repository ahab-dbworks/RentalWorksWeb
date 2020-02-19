using FwStandard.Models;
using FwStandard.SqlServer;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi;
using System;

namespace WebApi.Modules.HomeControls.CheckOutPendingItem
{
    //-------------------------------------------------------------------------------------------------------
    public class CheckOutPendingItemAddToOrderRequest
    {
        public string OrderId { get; set; }
        public string OrderItemId { get; set; }
        public string InventoryId { get; set; }
        public int? Quantity { get; set; }
    }
    //-------------------------------------------------------------------------------------------------------
    public static class CheckOutPendingItemFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TSpStatusResponse> CheckOutPendingItemAddToOrder(FwApplicationConfig appConfig, FwUserSession userSession, CheckOutPendingItemAddToOrderRequest request)
        {
            TSpStatusResponse response = new TSpStatusResponse();

            int negativeQuantity = System.Math.Abs(Convert.ToInt32(request.Quantity)) * (-1);
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "addtoorder", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                qry.AddParameter("@origmasteritemid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderItemId);
                qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, request.InventoryId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, negativeQuantity);
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
    }
}
