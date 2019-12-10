using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Utilities.InventoryPurchaseUtility
{

    public class StartInventoryPurchaseSessionRequest
    {
        public string InventoryId { get; set; }
        public int Quantity { get; set; }
    }

    public class StartInventoryPurchaseSessionResponse
    {
        public string SessionId { get; set; } = "";
    }

    public class UpdateInventoryPurchaseSessionRequest
    {
        public string SessionId { get; set; }
        public string InventoryId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateInventoryPurchaseSessionResponse : TSpStatusResponse { }

    public class InventoryPurchaseAssignBarCodesRequest : TSpStatusResponse
    {
        public string SessionId { get; set; }
    }
    public class InventoryPurchaseAssignBarCodesResponse : TSpStatusResponse { }

    public class InventoryPurchaseCompleteSessionRequest : TSpStatusResponse
    {
        public string SessionId { get; set; }
        public string InventoryId { get; set; }
        public int Quantity { get; set; }
        public string WarehouseId { get; set; }
        public string AisleLocation { get; set; }
        public string ShelfLocation { get; set; }
        public string ManufacturerVendorId { get; set; }
        public string CountryId { get; set; }
        public int? WarrantyDays { get; set; }
        public DateTime? WarrantyExpiration { get; set; }
        public string PurchaseVendorId { get; set; }
        public string OutsidePoNumber { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public string VendorPartNumber { get; set; }
        public decimal? UnitCost { get; set; }
    }
    public class InventoryPurchaseCompleteSessionResponse : TSpStatusResponse
    {
        public List<string> PurchaseId { get; set; } = new List<string>();
        public List<String> ItemId { get; set; } = new List<string>();
        public int QuantityAdded { get; set; } = 0;
    }

    public static class InventoryPurchaseUtilityFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<StartInventoryPurchaseSessionResponse> StartSession(FwApplicationConfig appConfig, FwUserSession userSession, StartInventoryPurchaseSessionRequest request)
        {
            StartInventoryPurchaseSessionResponse response = new StartInventoryPurchaseSessionResponse();
            response.SessionId = await AppFunc.GetNextIdAsync(appConfig);

            string trackedBy = await AppFunc.GetStringDataAsync(appConfig, "master", "masterid", request.InventoryId, "trackedby");
            if (trackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_BAR_CODE) || trackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_SERIAL_NO) || trackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_RFID))
            {
                for (int i = 0; i < request.Quantity; i++)
                {
                    await AppFunc.InsertDataAsync(appConfig, "barcodeholding", new string[] { "sessionid" }, new string[] { response.SessionId });
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<UpdateInventoryPurchaseSessionResponse> UpdateSession(FwApplicationConfig appConfig, FwUserSession userSession, UpdateInventoryPurchaseSessionRequest request)
        {
            UpdateInventoryPurchaseSessionResponse response = new UpdateInventoryPurchaseSessionResponse();

            string trackedBy = await AppFunc.GetStringDataAsync(appConfig, "master", "masterid", request.InventoryId, "trackedby");
            if (trackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_BAR_CODE) || trackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_SERIAL_NO) || trackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_RFID))
            {
                //determine exisitng rowcount
                int existingBarCodes = await AppFunc.GetIntDataAsync(appConfig, "barcodeholding", "sessionid", request.SessionId, "count(*)");

                if (request.Quantity > existingBarCodes)
                {
                    //increase barcodes
                    int increaseQty = (request.Quantity - existingBarCodes);
                    for (int i = 0; i < increaseQty; i++)
                    {
                        await AppFunc.InsertDataAsync(appConfig, "barcodeholding", new string[] { "sessionid" }, new string[] { request.SessionId });
                    }
                }
                else if (request.Quantity < existingBarCodes)
                {
                    //decrease barcodes
                    int decreaseQty = (existingBarCodes - request.Quantity);
                    await AppFunc.DeleteDataAsync(appConfig, "barcodeholding", new string[] { "sessionid" }, new string[] { request.SessionId }, rowCount: decreaseQty);
                }
            }
            else
            {
                await AppFunc.DeleteDataAsync(appConfig, "barcodeholding", new string[] { "sessionid" }, new string[] { request.SessionId });
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<InventoryPurchaseAssignBarCodesResponse> AssignBarCodes(FwApplicationConfig appConfig, FwUserSession userSession, InventoryPurchaseAssignBarCodesRequest request)
        {
            InventoryPurchaseAssignBarCodesResponse response = new InventoryPurchaseAssignBarCodesResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "assignbarcodesfrompurchase", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
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
        public static async Task<InventoryPurchaseCompleteSessionResponse> CompleteSession(FwApplicationConfig appConfig, FwUserSession userSession, InventoryPurchaseCompleteSessionRequest request)
        {
            InventoryPurchaseCompleteSessionResponse response = new InventoryPurchaseCompleteSessionResponse();

            UpdateInventoryPurchaseSessionRequest updateRequest = new UpdateInventoryPurchaseSessionRequest();
            updateRequest.InventoryId = request.InventoryId;
            updateRequest.Quantity = request.Quantity;
            UpdateInventoryPurchaseSessionResponse updateResponse = await UpdateSession(appConfig, userSession, updateRequest);

            if (updateResponse.success)
            {
                string trackedBy = await AppFunc.GetStringDataAsync(appConfig, "master", "masterid", request.InventoryId, "trackedby");
                if (trackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_BAR_CODE) || trackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_SERIAL_NO) || trackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_RFID))
                {
                    // for each request.Quantity {
                    // create Purchase
                    // create Asset
                    // }
                }
                else
                {
                    // create Purchase
                }
                response.success = true;
            }

            return response;
        }
        //------------------------------------------------------------------------------------------------------- 
    }
}
