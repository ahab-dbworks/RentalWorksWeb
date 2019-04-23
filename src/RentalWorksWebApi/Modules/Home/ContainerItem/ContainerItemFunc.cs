using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Home.ContainerItem
{
    public class EmptyContainerRequest
    {
        public string ContainerItemId { get; set; }
        public string DeleteAll { get; set; }
    }

    public class EmptyContainerItemResponse : TSpStatusReponse
    {
        public string InContractId = "";
    }

    public class RemoveFromContainerRequest
    {
        public string ContainerItemId { get; set; }
        public string ItemId { get; set; }
        public string InventoryId { get; set; }
        public string ConsignorId { get; set; }
        public string ConsignorAgreementId { get; set; }
        public decimal? Quantity { get; set; }
    }

    public class RemoveFromContainerResponse : TSpStatusReponse
    {
        public string OutputContractId = "";
        public decimal? QuantityRemoved;
    }


    public static class ContainerItemFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<EmptyContainerItemResponse> EmptyContainer(FwApplicationConfig appConfig, FwUserSession userSession, EmptyContainerRequest request)
        {
            EmptyContainerItemResponse response = new EmptyContainerItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "emptycontainer", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@containerrentalitemid", SqlDbType.NVarChar, ParameterDirection.Input, request.ContainerItemId);
                qry.AddParameter("@deleteall", SqlDbType.NVarChar, ParameterDirection.Input, request.DeleteAll);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@incontractid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.InContractId = qry.GetParameter("@incontractid").ToString();
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<RemoveFromContainerResponse> RemoveFromContainer(FwApplicationConfig appConfig, FwUserSession userSession, RemoveFromContainerRequest request)
        {
            RemoveFromContainerResponse response = new RemoveFromContainerResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "removeitemfromcontainer", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@rentalitemid", SqlDbType.NVarChar, ParameterDirection.Input, request.ItemId);
                qry.AddParameter("@containeritemid", SqlDbType.NVarChar, ParameterDirection.Input, request.ContainerItemId);
                qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, request.InventoryId);
                qry.AddParameter("@consignorid", SqlDbType.NVarChar, ParameterDirection.Input, request.ConsignorId);
                qry.AddParameter("@consignoragreementid", SqlDbType.NVarChar, ParameterDirection.Input, request.ConsignorAgreementId);
                qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, request.Quantity);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@finalizecontract", SqlDbType.NVarChar, ParameterDirection.Input, "T");
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, "");
                qry.AddParameter("@outputcontractid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@qtyremoved", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.OutputContractId = qry.GetParameter("@outputcontractid").ToString();
                response.QuantityRemoved = qry.GetParameter("@qtyremoved").ToInt32();
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
