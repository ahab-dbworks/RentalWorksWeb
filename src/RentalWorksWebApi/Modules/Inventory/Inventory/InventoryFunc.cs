using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Inventory.Inventory
{

    public class ChangeInventoryTrackedByRequest
    {
        public string InventoryId { get; set; }
        public string OldTrackedBy { get; set; }
        public string NewTrackedBy { get; set; }
    }

    public class ChangeInventoryTrackedByResponse : TSpStatusResponse
    {
        public int BarCodesCreated { get; set; }
    }


    public class RetireInventoryRequest
    {
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public string ItemId { get; set; }
        public string RetiredReasonId { get; set; }
        public string Notes { get; set; }
        public decimal? Quantity { get; set; }
        //public string ConsignorId { get; set; }
        //public string ConsignorAgreementId { get; set; }
    }

    public class RetireInventoryResponse : TSpStatusResponse
    {
        public string RetiredId { get; set; }
    }


    public static class InventoryFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ChangeInventoryTrackedByResponse> ChangeInventoryTrackedBy(FwApplicationConfig appConfig, FwUserSession userSession, ChangeInventoryTrackedByRequest request, FwSqlConnection conn = null)
        {
            ChangeInventoryTrackedByResponse response = new ChangeInventoryTrackedByResponse();

            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "changemastertrackedby", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, request.InventoryId);
            qry.AddParameter("@oldtrackedby", SqlDbType.NVarChar, ParameterDirection.Input, request.OldTrackedBy);
            qry.AddParameter("@newtrackedby", SqlDbType.NVarChar, ParameterDirection.Input, request.NewTrackedBy);
            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
            qry.AddParameter("@success", SqlDbType.NVarChar, ParameterDirection.Output);
            qry.AddParameter("@barcodescreated", SqlDbType.Int, ParameterDirection.Output);
            await qry.ExecuteNonQueryAsync();
            response.success = FwConvert.ToBoolean(qry.GetParameter("@success").ToString());
            response.BarCodesCreated = FwConvert.ToInt32(qry.GetParameter("@barcodescreated").ToString());
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<RetireInventoryResponse> RetireInventory(FwApplicationConfig appConfig, FwUserSession userSession, RetireInventoryRequest request, FwSqlConnection conn = null)
        {
            RetireInventoryResponse response = new RetireInventoryResponse();

            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "retireitems", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, request.InventoryId);
            qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, request.WarehouseId);
            qry.AddParameter("@rentalitemid", SqlDbType.NVarChar, ParameterDirection.Input, request.ItemId);
            //qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, request.ContractId);
            qry.AddParameter("@retiredreasonid", SqlDbType.NVarChar, ParameterDirection.Input, request.RetiredReasonId);
            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
            qry.AddParameter("@notes", SqlDbType.NVarChar, ParameterDirection.Input, request.Notes);
            qry.AddParameter("@qty", SqlDbType.Decimal, ParameterDirection.Input, request.Quantity);
            //qry.AddParameter("@outonly", SqlDbType.NVarChar, ParameterDirection.Input, request.OutOnly);
            //qry.AddParameter("@outorderid", SqlDbType.NVarChar, ParameterDirection.Input, request.xxxx);
            //qry.AddParameter("@outmasteritemid", SqlDbType.NVarChar, ParameterDirection.Input, request.xxxxx);
            //qry.AddParameter("@outcontractid", SqlDbType.NVarChar, ParameterDirection.Input, request.xxxx);
            //qry.AddParameter("@consignorid", SqlDbType.NVarChar, ParameterDirection.Input, request.ConsignorId);
            //qry.AddParameter("@consignoragreementid", SqlDbType.NVarChar, ParameterDirection.Input, request.ConsignorAgreementId);
            //qry.AddParameter("@billedtoorderid", SqlDbType.NVarChar, ParameterDirection.Input, request.xxxx);
            //qry.AddParameter("@billedtomasteritemid", SqlDbType.NVarChar, ParameterDirection.Input, request.xxxx);
            //qry.AddParameter("@lostorderid", SqlDbType.NVarChar, ParameterDirection.Input, request.xxxx);
            //qry.AddParameter("@physicalid", SqlDbType.NVarChar, ParameterDirection.Input, request.xxxx);
            //qry.AddParameter("@physicalitemid", SqlDbType.Int, ParameterDirection.Input, request.xxxx);
            qry.AddParameter("@retiredid", SqlDbType.NVarChar, ParameterDirection.Output);
            await qry.ExecuteNonQueryAsync();
            response.RetiredId = qry.GetParameter("@retiredid").ToString();
            response.success = !string.IsNullOrEmpty(response.RetiredId);
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
