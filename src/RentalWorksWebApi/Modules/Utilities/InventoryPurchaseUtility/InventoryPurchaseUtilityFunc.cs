using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.HomeControls.Inventory;
using WebApi.Modules.Inventory.Asset;
using WebApi.Modules.Inventory.Inventory;
using WebApi.Modules.Inventory.Purchase;
using WebApi.Modules.Inventory.RentalInventory;
using WebApi.Modules.Settings.FiscalYear;

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

    public class ApplyDepreciationRequest
    {
        public string PurchaseId { get; set; }
    }

    public class ApplyDepreciationResponse : TSpStatusResponse { }



    public class InventoryPurchaseAssignBarCodesRequest : TSpStatusResponse
    {
        public string SessionId { get; set; }
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
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
        public string ManufacturerModelNumber { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public string CountryId { get; set; }
        public int? WarrantyDays { get; set; }
        public DateTime? WarrantyExpiration { get; set; }
        public string PurchaseVendorId { get; set; }
        public string OutsidePoNumber { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public string VendorPartNumber { get; set; }
        public string CurrencyId { get; set; }
        public decimal? UnitCost { get; set; }
    }
    public class InventoryPurchaseCompleteSessionResponse : TSpStatusResponse
    {
        public List<string> PurchaseId { get; set; } = new List<string>();
        public List<String> ItemId { get; set; } = new List<string>();
        public int QuantityAdded { get; set; } = 0;
    }

    public class CodeExistsRequest
    {
        public string Code { get; set; }
        public string IgnoreId { get; set; }
    }
    public class CodeExistsResponse : TSpStatusResponse
    {
        public bool Exists { get; set; }
        public string DefinedIn { get; set; }
        public string UniqueId { get; set; }
    }

    public class BarCodeSerial
    {
        public string BarCode { get; set; }
        public string SerialNumber { get; set; }
        public string ManufactureDate { get; set; }
        public BarCodeSerial(string barCode, string serialNumber, string mfgDate)
        {
            this.BarCode = barCode;
            this.SerialNumber = serialNumber;
            this.ManufactureDate = mfgDate;
        }
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
                    await AppFunc.InsertDataAsync(appConfig, "barcodeholding", new string[] { "sessionid", "masterid" }, new string[] { response.SessionId, request.InventoryId });
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
                        response.success = await AppFunc.InsertDataAsync(appConfig, "barcodeholding", new string[] { "sessionid", "masterid" }, new string[] { request.SessionId, request.InventoryId });
                    }
                }
                else if (request.Quantity < existingBarCodes)
                {
                    //decrease barcodes
                    int decreaseQty = (existingBarCodes - request.Quantity);
                    response.success = await AppFunc.DeleteDataAsync(appConfig, "barcodeholding", new string[] { "sessionid" }, new string[] { request.SessionId }, rowCount: decreaseQty);
                }
                else
                {
                    response.success = true;
                }
            }
            else
            {
                response.success = await AppFunc.DeleteDataAsync(appConfig, "barcodeholding", new string[] { "sessionid" }, new string[] { request.SessionId });
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> DeleteSession(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId)
        {
            return await AppFunc.DeleteDataAsync(appConfig, "barcodeholding", new string[] { "sessionid" }, new string[] { sessionId });
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<CodeExistsResponse> CodeExists(FwApplicationConfig appConfig, FwUserSession userSession, CodeExistsRequest request)
        {
            CodeExistsResponse response = new CodeExistsResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "codeexists", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@code", SqlDbType.NVarChar, ParameterDirection.Input, request.Code);
                qry.AddParameter("@ignoreid", SqlDbType.NVarChar, ParameterDirection.Input, request.IgnoreId);
                qry.AddParameter("@doesexist", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@definedin", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@uniqueid", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.Exists = FwConvert.ToBoolean(qry.GetParameter("@doesexist").ToString());
                response.DefinedIn = qry.GetParameter("@definedin").ToString();
                response.UniqueId = qry.GetParameter("@uniqueid").ToString();
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
                qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, request.InventoryId);
                qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, request.WarehouseId);
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
        public static async Task<ApplyDepreciationResponse> ApplyDepreciation(FwApplicationConfig appConfig, FwUserSession userSession, ApplyDepreciationRequest request, FwSqlConnection conn = null)
        {
            ApplyDepreciationResponse response = new ApplyDepreciationResponse();
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "applydepreciation", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@purchaseid", SqlDbType.NVarChar, ParameterDirection.Input, request.PurchaseId);
            await qry.ExecuteNonQueryAsync();
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

            if (isValidRequest)
            {
                if (request.PurchaseDate == null)
                {
                    isValidRequest = false;
                    response.msg = "Purchase Date is required.";
                }
            }

            if (isValidRequest)
            {
                if (request.ReceiveDate == null)
                {
                    isValidRequest = false;
                    response.msg = "Receive Date is required.";
                }
            }

            if (isValidRequest)
            {
                if (request.ReceiveDate < request.PurchaseDate)
                {
                    isValidRequest = false;
                    response.msg = "Receive Date cannot be prior to Purchase Date.";
                }
            }

            if (isValidRequest)
            {
                if (FiscalFunc.DateIsInClosedMonth(appConfig, userSession, request.ReceiveDate.GetValueOrDefault(DateTime.MinValue)).Result)
                {
                    isValidRequest = false;
                    response.msg = "Cannot Purchase or Receive Inventory in a Closed month.";
                }
            }

            RentalInventoryLogic rentalInventory = new RentalInventoryLogic();
            rentalInventory.SetDependencies(appConfig, userSession);
            rentalInventory.InventoryId = request.InventoryId;
            await rentalInventory.LoadAsync<RentalInventoryLogic>();

            List<BarCodeSerial> barCodeSerials = new List<BarCodeSerial>();

            if (isValidRequest)
            {
                if (!(rentalInventory.AvailFor.Equals(RwConstants.INVENTORY_AVAILABLE_FOR_RENT)))
                {
                    isValidRequest = false;
                    response.msg = "Only Rental Inventory can be purchased here.";
                }
            }

            if (isValidRequest)
            {
                if (!(rentalInventory.Classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_ITEM) || rentalInventory.Classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_ACCESSORY)))
                {
                    isValidRequest = false;
                    response.msg = "Only Items and Accessories can be purchased here.";
                }
            }

            if (isValidRequest)
            {
                if (rentalInventory.TrackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_BAR_CODE) || rentalInventory.TrackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_RFID))
                {
                    using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                    {
                        barCodeSerials.Clear();
                        FwSqlCommand qryBarCode = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                        qryBarCode.Add("select b.barcode, b.serialno, b.mfgdate                                  ");
                        qryBarCode.Add(" from  barcodeholding b                                                  ");
                        qryBarCode.Add(" where b.sessionid = @sessionid                                          ");
                        qryBarCode.Add("order by b.barcode                                                       ");
                        qryBarCode.AddParameter("@sessionid", request.SessionId);
                        FwJsonDataTable dtBarCode = await qryBarCode.QueryToFwJsonTableAsync();

                        foreach (List<object> rowBarCode in dtBarCode.Rows)
                        {
                            string barCode = rowBarCode[dtBarCode.GetColumnNo("barcode")].ToString();
                            string serialNumber = rowBarCode[dtBarCode.GetColumnNo("serialno")].ToString();
                            string mfgDate = rowBarCode[dtBarCode.GetColumnNo("mfgdate")].ToString();
                            if (string.IsNullOrEmpty(barCode))
                            {
                                isValidRequest = false;
                                response.msg = "All items need to have a valid Bar Code supplied.";
                            }
                            else
                            {
                                barCodeSerials.Add(new BarCodeSerial(barCode, serialNumber, mfgDate));
                            }
                        }
                    }

                    if (isValidRequest)
                    {
                        if (!barCodeSerials.Count.Equals(request.Quantity))
                        {
                            isValidRequest = false;
                            response.msg = $"Invalid Quantity {request.Quantity.ToString()}, {barCodeSerials.Count.ToString()} Bar Code supplied.";
                        }
                    }
                }
                else if (rentalInventory.TrackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_SERIAL_NO))
                {
                    using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                    {
                        barCodeSerials.Clear();
                        FwSqlCommand qryBarCode = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                        qryBarCode.Add("select b.serialno, b.mfgdate                                             ");
                        qryBarCode.Add(" from  barcodeholding b                                                  ");
                        qryBarCode.Add(" where b.sessionid = @sessionid                                          ");
                        qryBarCode.Add("order by b.serialno                                                      ");
                        qryBarCode.AddParameter("@sessionid", request.SessionId);
                        FwJsonDataTable dtBarCode = await qryBarCode.QueryToFwJsonTableAsync();

                        foreach (List<object> rowBarCode in dtBarCode.Rows)
                        {
                            string serialNumber = rowBarCode[dtBarCode.GetColumnNo("serialno")].ToString();
                            string mfgDate = rowBarCode[dtBarCode.GetColumnNo("mfgdate")].ToString();
                            if (string.IsNullOrEmpty(serialNumber))
                            {
                                isValidRequest = false;
                                response.msg = "All items need to have a valid Serial Number supplied.";
                            }
                            else
                            {
                                barCodeSerials.Add(new BarCodeSerial("", serialNumber, mfgDate));
                            }
                        }
                    }

                    if (isValidRequest)
                    {
                        if (!barCodeSerials.Count.Equals(request.Quantity))
                        {
                            isValidRequest = false;
                            response.msg = $"Invalid Quantity {request.Quantity.ToString()}, {barCodeSerials.Count.ToString()} Serial Numbers supplied.";
                        }
                    }
                }
                else if (rentalInventory.TrackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_QUANTITY))
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

                        if (rentalInventory.TrackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_BAR_CODE) || rentalInventory.TrackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_SERIAL_NO) || rentalInventory.TrackedBy.Equals(RwConstants.INVENTORY_TRACKED_BY_RFID))
                        {
                            foreach (BarCodeSerial barCodeSerial in barCodeSerials)
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
                                purchase.CurrencyId = request.CurrencyId;
                                purchase.InputDate = DateTime.Today;
                                purchase.InputByUserId = userSession.UsersId;
                                purchase.PurchaseNotes = "Inventory Purchase Utility";
                                await purchase.SaveAsync(null, conn);

                                ItemLogic item = new ItemLogic();
                                item.SetDependencies(appConfig, userSession);
                                item.PurchaseId = purchase.PurchaseId;
                                item.InventoryId = purchase.InventoryId;
                                item.WarehouseId = purchase.WarehouseId;
                                item.InventoryStatusId = RwGlobals.INVENTORY_STATUS_IN_ID;
                                item.StatusDate = DateTime.Today.ToString("yyyy-MM-dd");  //?
                                item.BarCode = barCodeSerial.BarCode;
                                item.SerialNumber = barCodeSerial.SerialNumber;
                                item.AisleLocation = request.AisleLocation;
                                item.ShelfLocation = request.ShelfLocation;
                                item.ManufacturerId = request.ManufacturerVendorId;
                                item.ManufacturerModelNumber = request.ManufacturerModelNumber;
                                item.ManufacturerPartNumber = request.ManufacturerPartNumber;
                                item.CountryOfOriginId = request.CountryId;
                                item.WarrantyPeriod = request.WarrantyDays;
                                item.WarrantyExpiration = request.WarrantyExpiration;
                                item.ManufactureDate = barCodeSerial.ManufactureDate;
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
                                UpdateInventoryQuantityResponse qtyResponse = await InventoryFunc.UpdateInventoryQuantity(appConfig, userSession, qtyRequest, conn);

                                response.PurchaseId.Add(purchase.PurchaseId);
                                response.ItemId.Add(item.ItemId);
                                response.QuantityAdded++;

                                if (rentalInventory.IsFixedAsset.GetValueOrDefault(false) && (purchase.ReceiveDate < DateTime.Today))
                                {
                                    ApplyDepreciationRequest depreciationRequest = new ApplyDepreciationRequest();
                                    depreciationRequest.PurchaseId = purchase.PurchaseId;
                                    ApplyDepreciationResponse depreciationReponse = await ApplyDepreciation(appConfig, userSession, depreciationRequest, conn);
                                }
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
                            purchase.CurrencyId = request.CurrencyId;
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
                            UpdateInventoryQuantityResponse qtyResponse = await InventoryFunc.UpdateInventoryQuantity(appConfig, userSession, qtyRequest, conn);

                            response.PurchaseId.Add(purchase.PurchaseId);
                            response.QuantityAdded += request.Quantity;

                            if (rentalInventory.IsFixedAsset.GetValueOrDefault(false) && (purchase.ReceiveDate < DateTime.Today))
                            {
                                ApplyDepreciationRequest depreciationRequest = new ApplyDepreciationRequest();
                                depreciationRequest.PurchaseId = purchase.PurchaseId;
                                ApplyDepreciationResponse depreciationReponse = await ApplyDepreciation(appConfig, userSession, depreciationRequest, conn);
                            }
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
