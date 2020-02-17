using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.HomeControls.InventoryAvailability;

namespace WebApi.Modules.Inventory.Inventory
{

    public class UpdateInventoryQuantityRequest
    {
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public string ConsignorId { get; set; }
        public string ConsignorAgreementId { get; set; }
        public string TransactionType { get; set; }
        public string OrderType { get; set; }
        public decimal QuantityChange { get; set; }
        public bool UpdateCost { get; set; }
        public decimal? CostPerItem { get; set; }
        public decimal? ForceCost { get; set; }
        public string UniqueId1 { get; set; }
        public string UniqueId2 { get; set; }
        public string UniqueId3 { get; set; }
        public int? UniqueId4 { get; set; }
        public bool LogOnly { get; set; }
    }
    public class UpdateInventoryQuantityResponse : TSpStatusResponse { }

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
    }

    public class RetireInventoryResponse : TSpStatusResponse
    {
        public string RetiredId { get; set; }
    }

    public class UnretireInventoryRequest
    {
        public string RetiredId { get; set; }
        public string ItemId { get; set; }
        public string UnretiredReasonId { get; set; }
        public string Notes { get; set; }
        public decimal? Quantity { get; set; }
    }

    public class UnretireInventoryResponse : TSpStatusResponse
    {
        public string UnretiredId { get; set; }
    }

    public static class InventoryFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<UpdateInventoryQuantityResponse> UpdateInventoryQuantity(FwApplicationConfig appConfig, FwUserSession userSession, UpdateInventoryQuantityRequest request, FwSqlConnection conn = null)
        {
            UpdateInventoryQuantityResponse response = new UpdateInventoryQuantityResponse();

            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }

            FwSqlCommand qry = new FwSqlCommand(conn, "updatemasterwhqty", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, request.InventoryId);
            qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, request.WarehouseId);
            qry.AddParameter("@consignorid", SqlDbType.NVarChar, ParameterDirection.Input, request.ConsignorId);
            qry.AddParameter("@consignoragreementid", SqlDbType.NVarChar, ParameterDirection.Input, request.ConsignorAgreementId);
            qry.AddParameter("@trantype", SqlDbType.NVarChar, ParameterDirection.Input, request.TransactionType);
            qry.AddParameter("@ordertype", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderType);
            qry.AddParameter("@qtychange", SqlDbType.Decimal, ParameterDirection.Input, request.QuantityChange);
            qry.AddParameter("@updatecost", SqlDbType.NVarChar, ParameterDirection.Input, request.UpdateCost);
            qry.AddParameter("@costperitem", SqlDbType.Decimal, ParameterDirection.Input, request.CostPerItem);
            qry.AddParameter("@forcecost", SqlDbType.NVarChar, ParameterDirection.Input, request.ForceCost);
            qry.AddParameter("@uniqueid1", SqlDbType.NVarChar, ParameterDirection.Input, request.UniqueId1);
            qry.AddParameter("@uniqueid2", SqlDbType.NVarChar, ParameterDirection.Input, request.UniqueId2);
            qry.AddParameter("@uniqueid3", SqlDbType.NVarChar, ParameterDirection.Input, request.UniqueId3);
            qry.AddParameter("@uniqueid4", SqlDbType.Int, ParameterDirection.Input, request.UniqueId4);
            qry.AddParameter("@logonly", SqlDbType.NVarChar, ParameterDirection.Input, request.LogOnly);
            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
            //qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
            //qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
            await qry.ExecuteNonQueryAsync();
            //response.success = (qry.GetParameter("@status").ToInt32() == 0);
            //response.msg = qry.GetParameter("@msg").ToString();
            response.success = true;

            string classification = FwSqlCommand.GetStringDataAsync(conn, appConfig.DatabaseSettings.QueryTimeout, "master", "masterid", request.InventoryId, "class").Result;
            InventoryAvailabilityFunc.RequestRecalc(request.InventoryId, request.WarehouseId, classification);

            return response;
        }
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
        public static async Task<UnretireInventoryResponse> UnretireInventory(FwApplicationConfig appConfig, FwUserSession userSession, UnretireInventoryRequest request, FwSqlConnection conn = null)
        {
            UnretireInventoryResponse response = new UnretireInventoryResponse();

            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "unretireitems", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@retiredid", SqlDbType.NVarChar, ParameterDirection.Input, request.RetiredId);
            qry.AddParameter("@rentalitemid", SqlDbType.NVarChar, ParameterDirection.Input, request.ItemId);
            qry.AddParameter("@unretiredreasonid", SqlDbType.NVarChar, ParameterDirection.Input, request.UnretiredReasonId);
            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
            qry.AddParameter("@notes", SqlDbType.NVarChar, ParameterDirection.Input, request.Notes);
            qry.AddParameter("@qty", SqlDbType.Decimal, ParameterDirection.Input, request.Quantity);
            //qry.AddParameter("@outonly", SqlDbType.NVarChar, ParameterDirection.Input, request.OutOnly);
            //qry.AddParameter("@outorderid", SqlDbType.NVarChar, ParameterDirection.Input, request.xxxx);
            //qry.AddParameter("@outcontractid", SqlDbType.NVarChar, ParameterDirection.Input, request.xxxx);
            qry.AddParameter("@unretiredid", SqlDbType.NVarChar, ParameterDirection.Output);
            await qry.ExecuteNonQueryAsync();
            response.UnretiredId = qry.GetParameter("@unretiredid").ToString();
            response.success = !string.IsNullOrEmpty(response.UnretiredId);
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
