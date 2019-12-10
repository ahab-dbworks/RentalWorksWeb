using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.HomeControls.Inventory;
using WebApi.Modules.Inventory.Asset;
using WebApi.Modules.Inventory.Purchase;

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
            updateRequest.SessionId = request.SessionId;
            UpdateInventoryPurchaseSessionResponse updateResponse = await UpdateSession(appConfig, userSession, updateRequest);

            response.success = false;
            bool isValidRequest = true;

            if (isValidRequest)
            {
                if (string.IsNullOrEmpty(request.InventoryId))
                {
                    isValidRequest = false;
                    response.msg = "InventoryId cannot be blank.";
                }
            }
            if (isValidRequest)
            {
                if (string.IsNullOrEmpty(request.WarehouseId))
                {
                    isValidRequest = false;
                    response.msg = "WarehouseId cannot be blank.";
                }
            }
            if (isValidRequest)
            {
                if (request.Quantity <= 0)
                {
                    isValidRequest = false;
                    response.msg = "Quantity must be greater than zero.";
                }
            }

            string[] inventoryData = await AppFunc.GetStringDataAsync(appConfig, "master", new string[] { "masterid" }, new string[] { request.InventoryId }, new string[] { "availfor", "class", "trackedby" });
            string availableFor = inventoryData[0];
            string classification = inventoryData[1];
            string trackedBy = inventoryData[2];
            List<string> barCodes = new List<string>();

            if (isValidRequest)
            {
                if (!(availableFor.Equals(RwConstants.INVENTORY_AVAILABLE_FOR_RENT)))
                {
                    isValidRequest = false;
                    response.msg = "Only Rental Inventory can be purchased here.";
                }
            }

            if (isValidRequest)
            {
                if (!(classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_ITEM) || classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_ACCESSORY)))
                {
                    isValidRequest = false;
                    response.msg = "Only Items and Accessories can be purchased here.";
                }
            }

            if (isValidRequest)
            {
                if (trackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_BAR_CODE) || trackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_SERIAL_NO) || trackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_RFID))
                {
                    using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                    {
                        barCodes.Clear();
                        FwSqlCommand qryBarCode = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                        qryBarCode.Add("select b.barcode                                                         ");
                        qryBarCode.Add(" from  barcodeholding b                                                  ");
                        qryBarCode.Add(" where b.sessionid = @sessionid                                          ");
                        qryBarCode.Add("order by b.barcode                                                       ");
                        qryBarCode.AddParameter("@sessionid", request.SessionId);
                        FwJsonDataTable dtBarCode = await qryBarCode.QueryToFwJsonTableAsync();

                        foreach (List<object> rowBarCode in dtBarCode.Rows)
                        {
                            string barCode = rowBarCode[dtBarCode.GetColumnNo("barcode")].ToString();
                            if (string.IsNullOrEmpty(barCode))
                            {
                                isValidRequest = false;
                                response.msg = "All items need to have a valid Bar Code supplied.";
                            }
                            else
                            {
                                barCodes.Add(barCode);
                            }
                        }
                    }

                    if (isValidRequest)
                    {
                        if (!barCodes.Count.Equals(request.Quantity))
                        {
                            isValidRequest = false;
                            response.msg = $"Invalid Quantity {request.Quantity.ToString()}, {barCodes.Count.ToString()} Bar Code supplied.";
                        }
                    }
                }
                else if (trackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_QUANTITY))
                {
                    isValidRequest = true;
                }
            }

            if (isValidRequest)
            {
                if (updateResponse.success)
                {
                    FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
                    try
                    {
                        await conn.GetConnection().OpenAsync();
                        conn.BeginTransaction();

                        if (trackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_BAR_CODE) || trackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_SERIAL_NO) || trackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_RFID))
                        {
                            foreach (string barCode in barCodes)
                            {
                                // create a Purchase for each bar code
                                PurchaseLogic purchase = new PurchaseLogic();
                                purchase.SetDependencies(appConfig, userSession);
                                purchase.SessionId = request.SessionId;
                                purchase.InventoryId = request.InventoryId;
                                purchase.WarehouseId = request.WarehouseId;
                                purchase.Quantity = 1;
                                purchase.Ownership = RwConstants.INVENTORY_OWNERSHIP_OWNED;
                                purchase.VendorId = request.PurchaseVendorId;
                                purchase.OutsidePurchaseOrderNumber = request.OutsidePoNumber;
                                purchase.PurchaseDate = request.PurchaseDate;
                                purchase.ReceiveDate = request.ReceiveDate;
                                purchase.VendorPartNumber = request.VendorPartNumber;
                                purchase.UnitCost = request.UnitCost;
                                purchase.UnitCostWithTax = request.UnitCost;
                                //purchase.CurrencyId = ??
                                purchase.InputDate = DateTime.Today;
                                purchase.InputByUserId = userSession.UsersId;
                                purchase.PurchaseNotes = "Inventory Purchase Utility";
                                await purchase.SaveAsync(null, conn);

                                ItemLogic item = new ItemLogic();
                                item.SetDependencies(appConfig, userSession);
                                item.PurchaseId = purchase.PurchaseId;
                                item.InventoryId = purchase.InventoryId;
                                item.InventoryStatusId = RwGlobals.INVENTORY_STATUS_IN_ID;
                                item.BarCode = barCode;
                                item.AisleLocation = request.AisleLocation;
                                item.ShelfLocation = request.ShelfLocation;
                                item.ManufacturerId = request.ManufacturerVendorId;
                                item.CountryOfOriginId = request.CountryId;
                                item.WarrantyPeriod = request.WarrantyDays;
                                item.WarrantyExpiration = request.WarrantyExpiration;
                                await item.SaveAsync(null, conn);

                                // update stored quantity for item
                                UpdateInventoryQuantityRequest qtyRequest = new UpdateInventoryQuantityRequest();
                                qtyRequest.InventoryId = purchase.InventoryId;
                                qtyRequest.WarehouseId = purchase.WarehouseId;
                                qtyRequest.TransactionType = RwConstants.INVENTORY_QUANTITY_TRANSACTION_TYPE_PURCHASE;
                                qtyRequest.QuantityChange = 1;
                                qtyRequest.UpdateCost = true;
                                qtyRequest.CostPerItem = purchase.UnitCost;
                                qtyRequest.UniqueId1 = purchase.PurchaseId;
                                UpdateInventoryQuantityResponse qtyResponse = await AppFunc.UpdateInventoryQuantity(appConfig, userSession, qtyRequest);
                            }

                        }
                        else
                        {
                            // create Purchase
                            PurchaseLogic purchase = new PurchaseLogic();
                            purchase.SetDependencies(appConfig, userSession);
                            purchase.SessionId = request.SessionId;
                            purchase.InventoryId = request.InventoryId;
                            purchase.WarehouseId = request.WarehouseId;
                            purchase.Quantity = request.Quantity;
                            purchase.Ownership = RwConstants.INVENTORY_OWNERSHIP_OWNED;
                            //purchase.AisleLocation = request.AisleLocation;
                            //purchase.ShelfLocation = request.ShelfLocation;
                            //purchase.ManufacturerVendorId = request.ManufacturerVendorId;
                            //purchase.CountryId = request.CountryId;
                            //purchase.WarrantyDays = request.WarrantyDays;
                            //purchase.WarrantyExpiration = request.WarrantyExpiration;
                            purchase.VendorId = request.PurchaseVendorId;
                            purchase.OutsidePurchaseOrderNumber = request.OutsidePoNumber;
                            purchase.PurchaseDate = request.PurchaseDate;
                            purchase.ReceiveDate = request.ReceiveDate;
                            purchase.VendorPartNumber = request.VendorPartNumber;
                            purchase.UnitCost = request.UnitCost;
                            purchase.UnitCostWithTax = request.UnitCost;
                            //purchase.CurrencyId = ??
                            purchase.InputDate = DateTime.Today;
                            purchase.InputByUserId = userSession.UsersId;
                            purchase.PurchaseNotes = "Inventory Purchase Utility";
                            await purchase.SaveAsync(null, conn);

                            // update stored quantity for item
                            UpdateInventoryQuantityRequest qtyRequest = new UpdateInventoryQuantityRequest();
                            qtyRequest.InventoryId = purchase.InventoryId;
                            qtyRequest.WarehouseId = purchase.WarehouseId;
                            qtyRequest.TransactionType = RwConstants.INVENTORY_QUANTITY_TRANSACTION_TYPE_PURCHASE;
                            qtyRequest.QuantityChange = FwConvert.ToDecimal(purchase.Quantity);
                            qtyRequest.UpdateCost = true;
                            qtyRequest.CostPerItem = purchase.UnitCost;
                            qtyRequest.UniqueId1 = purchase.PurchaseId;
                            UpdateInventoryQuantityResponse qtyResponse = await AppFunc.UpdateInventoryQuantity(appConfig, userSession, qtyRequest);

                        }

                        conn.CommitTransaction();
                        response.success = true;
                    }

                    catch (Exception ex)
                    {
                        conn.RollbackTransaction();
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            return response;
        }
        //------------------------------------------------------------------------------------------------------- 
    }
}
