using FwStandard.Models;
using FwStandard.SqlServer;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Home.ContractItemDetail
{
    public class ContractItem
    {
        public string OrderId { get; set; }
        public string OrderItemId { get; set; }
        public string VendorId { get; set; }
        public string BarCode { get; set; }
        public int Quantity { get; set; }
    }
    public class VoidItemsRequest
    {
        public string ContractId { get; set; }
        public List<ContractItem> Items { get; set; } = new List<ContractItem>();
        public string Reason { get; set; }
        public bool? ReturnToInventory { get; set; }
    }
    public static class ContractItemDetailFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TSpStatusResponse> VoidItems(FwApplicationConfig appConfig, FwUserSession userSession, VoidItemsRequest request)
        {
            TSpStatusResponse response = new TSpStatusResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                foreach (ContractItem Item in request.Items)
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "voidcontractitemweb", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, request.ContractId);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, Item.OrderId);
                    qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, Item.OrderItemId);
                    qry.AddParameter("@vendorid", SqlDbType.NVarChar, ParameterDirection.Input, Item.VendorId);
                    qry.AddParameter("@barcode", SqlDbType.NVarChar, ParameterDirection.Input, Item.BarCode);
                    qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, Item.Quantity);
                    qry.AddParameter("@returntoinventory", SqlDbType.NVarChar, ParameterDirection.Input, request.ReturnToInventory.GetValueOrDefault(false));
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@reason", SqlDbType.NVarChar, ParameterDirection.Input, request.Reason);
                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.status = qry.GetParameter("@status").ToInt32();
                    response.success = (response.status == 0);
                    response.msg = qry.GetParameter("@msg").ToString();
                    if (!response.success)
                    {
                        break;
                    }
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
