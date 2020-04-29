using FwStandard.Models;
using FwStandard.SqlServer;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.HomeControls.InventoryAvailability;
using WebApi.Modules.HomeControls.InventoryWarehouse;

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

    public class ChangeICodeRequest
    {
        public string ItemId { get; set; }
        public string InventoryId { get; set; }
        //public string WarehouseId { get; set; }
        //public string Notes { get; set; }
    }

    public class ChangeICodeResponse : TSpStatusResponse
    {
        //public string InventoryId { get; set; }
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

    public class RentalInventoryQcRequiredAllWarehousesRequest
    {
        public string InventoryId { get; set; }
        public bool QcRequired { get; set; }
    }

    public class RentalInventoryQcRequiredAllWarehousesResponse : TSpStatusResponse
    {
    }


    public class ApplyPendingRateUpdateModificationsRequest
    {
        public string RateUpdateBatchName { get; set; }
    }
    public class ApplyPendingRateUpdateModificationsResponse : TSpStatusResponse 
    { 
        public string RateUpdateBatchId { get; set; }
        public string RateUpdateBatchName { get; set; }
    }

    public static class InventoryFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static string GetRateUpdatePendingModificationsWhere() {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            sb.Append("  (newhourlyrate        <> 0) or ");
            sb.Append("  (newhourlycost        <> 0) or ");
            sb.Append("  (newdailyrate         <> 0) or ");
            sb.Append("  (newdailycost         <> 0) or ");
            sb.Append("  (newweeklyrate        <> 0) or ");
            sb.Append("  (newweeklycost        <> 0) or ");
            sb.Append("  (newweek2rate         <> 0) or ");
            sb.Append("  (newweek3rate         <> 0) or ");
            sb.Append("  (newweek4rate         <> 0) or ");
            sb.Append("  (newweek5rate         <> 0) or ");
            sb.Append("  (newmonthlyrate       <> 0) or ");
            sb.Append("  (newmonthlycost       <> 0) or ");
            sb.Append("  (newmanifestvalue     <> 0) or ");
            sb.Append("  (newreplacementcost   <> 0) or ");
            sb.Append("  (newretail            <> 0) or ");
            sb.Append("  (newprice             <> 0) or ");
            sb.Append("  (newdefaultcost       <> 0) or ");
            sb.Append("  (newmaxdiscount       <> 0) or ");
            sb.Append("  (newmindw             <> 0)    ");
            sb.Append(")");
            return sb.ToString();
        }
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
        public static async Task<ChangeICodeResponse> ChangeICode(FwApplicationConfig appConfig, FwUserSession userSession, ChangeICodeRequest request, FwSqlConnection conn = null)
        {
            ChangeICodeResponse response = new ChangeICodeResponse();

            if (string.IsNullOrEmpty(request.ItemId))
            {
                response.msg = "No Bar Code or Serial Number provided.";
            }
            else if (string.IsNullOrEmpty(request.InventoryId))
            {
                response.msg = "No \"Change to I-Code\" provided.";
            }
            else
            {
                if (conn == null)
                {
                    conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
                }
                FwSqlCommand qry = new FwSqlCommand(conn, "changeicodeweb", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@rentalitemid", SqlDbType.NVarChar, ParameterDirection.Input, request.ItemId);
                qry.AddParameter("@newmasterid", SqlDbType.NVarChar, ParameterDirection.Input, request.InventoryId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
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
        public static async Task<RentalInventoryQcRequiredAllWarehousesResponse> SetQcRequiredAllWarehouses(FwApplicationConfig appConfig, FwUserSession userSession, RentalInventoryQcRequiredAllWarehousesRequest request)
        {
            RentalInventoryQcRequiredAllWarehousesResponse response = new RentalInventoryQcRequiredAllWarehousesResponse();

            BrowseRequest warehouseBrowseRequest = new BrowseRequest();
            warehouseBrowseRequest.uniqueids = new Dictionary<string, object>();
            warehouseBrowseRequest.uniqueids.Add("InventoryId", request.InventoryId);

            InventoryWarehouseLogic warehouseSelector = new InventoryWarehouseLogic();
            warehouseSelector.SetDependencies(appConfig, userSession);
            List<InventoryWarehouseLogic> inventoryWarehouses = await warehouseSelector.SelectAsync<InventoryWarehouseLogic>(warehouseBrowseRequest);

            foreach (InventoryWarehouseLogic iw in inventoryWarehouses)
            {
                InventoryWarehouseLogic iw2 = new InventoryWarehouseLogic();
                iw2.SetDependencies(appConfig, userSession);
                iw2.InventoryId = iw.InventoryId;
                iw2.WarehouseId = iw.WarehouseId;
                iw2.QcRequired = request.QcRequired;
                await iw2.SaveAsync(original: iw);
                response.success = true;
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ApplyPendingRateUpdateModificationsResponse> ApplyPendingModificationsAsync(FwApplicationConfig appConfig, FwUserSession userSession, ApplyPendingRateUpdateModificationsRequest request, FwSqlConnection conn = null)
        {
            ApplyPendingRateUpdateModificationsResponse response = new ApplyPendingRateUpdateModificationsResponse();

            if (string.IsNullOrEmpty(request.RateUpdateBatchName))
            {
                response.msg = "Rate Update Batch Name is required.";
            }
            else
            {
                if (conn == null)
                {
                    conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
                }

                //start progress meter

                //create RateUpdateBatchLogic

                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select *                       ");
                qry.Add(" from  rateupdateitemview      ");
                qry.Add(" where " + GetRateUpdatePendingModificationsWhere());
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                foreach (List<object> row in dt.Rows)
                {
                    //row[dt.GetColumnNo("warehouseid")].ToString()
                    
                    // apply change to Master/Warehouse

                    //step progress meter

                }

                //finish progress meter

            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------



    }
}
