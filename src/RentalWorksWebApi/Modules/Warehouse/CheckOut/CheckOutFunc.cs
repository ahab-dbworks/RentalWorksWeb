using FwStandard.Models;
using FwStandard.SqlServer;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using System;
using WebApi.Modules.HomeControls.StagingSubstituteSession;
using WebApi.Modules.HomeControls.OrderItem;
using System.Collections.Generic;
using WebApi.Modules.HomeControls.CheckOutSubstituteSessionItem;
using WebApi.Modules.Agent.Order;

namespace WebApi.Modules.Warehouse.CheckOut
{
    //-------------------------------------------------------------------------------------------------------
    public class StageItemRequest
    {
        public string OrderId { get; set; }
        public string OrderItemId { get; set; }
        public string VendorId { get; set; }
        public string Code { get; set; }
        public string WarehouseId { get; set; }  // this field is optional.  If ommitted, RWW will use the user's default Warehouse
        public int? Quantity { get; set; }
        public bool? UnstageItem { get; set; }
        public bool? AddItemToOrder { get; set; }
        public bool? AddCompleteToOrder { get; set; }
        public bool? StageIncompleteContainer { get; set; }
    }
    public class StageItemResponse : TSpStatusResponse
    {
        public string InventoryId { get; set; }
        public string OrderItemId { get; set; }
        public int QuantityStaged { get; set; }

        public OrderInventoryStatusCheckOut InventoryStatus = new OrderInventoryStatusCheckOut();

        public bool ShowAddItemToOrder { get; set; }
        public bool ShowAddCompleteToOrder { get; set; }
        public bool ShowUnstage { get; set; }
        public bool ShowStageIncompleteContainer { get; set; }
    }
    //-------------------------------------------------------------------------------------------------------
    public class UnstageItemRequest
    {
        public string OrderId { get; set; }
        public string OrderItemId { get; set; }
        public string VendorId { get; set; }
        public string ConsignorAgreementId { get; set; }
        public string Code { get; set; }
        public int? Quantity { get; set; }
    }
    public class UnstageItemResponse : TSpStatusResponse
    {
        public string InventoryId { get; set; }
        public string ItemId { get; set; }
        public string ConsignorId { get; set; }
        public string ConsignorAgreementId { get; set; }
        public OrderInventoryStatusCheckOut InventoryStatus = new OrderInventoryStatusCheckOut();
    }
    //-------------------------------------------------------------------------------------------------------
    public class CheckOutAllStagedRequest
    {
        public string OrderId { get; set; }
        public string OfficeLocationId { get; set; }
        public string WarehouseId { get; set; }
    }
    public class CheckOutAllStagedResponse : TSpStatusResponse
    {
        public string ContractId { get; set; }
    }
    //-------------------------------------------------------------------------------------------------------
    public class CreateOutContractRequest
    {
        public string OrderId { get; set; }
        public string OfficeLocationId { get; set; }
        public string WarehouseId { get; set; }
    }
    public class CreateOutContractResponse : TSpStatusResponse
    {
        public string ContractId { get; set; }
    }
    //-------------------------------------------------------------------------------------------------------
    public class MoveStagedItemRequest
    {
        public string OrderId { get; set; }
        public string OrderItemId { get; set; }
        public string VendorId { get; set; }
        public string ContractId { get; set; }
        public string Code { get; set; }
        public float? Quantity { get; set; }
    }
    public class MoveStagedItemResponse : TSpStatusResponse { }
    //-------------------------------------------------------------------------------------------------------
    public class OrderInventoryStatusCheckOut
    {
        public string ICode { get; set; }
        public string Description { get; set; }
        public int QuantityOrdered { get; set; }
        public int QuantitySub { get; set; }
        public int QuantityStaged { get; set; }
        public int QuantityOut { get; set; }
        public int QuantityIn { get; set; }
        public int QuantityRemaining { get; set; }
    }
    //-------------------------------------------------------------------------------------------------------
    public class SelectAllNoneStageQuantityItemRequest
    {
        [Required]
        public string OrderId { get; set; }
        [Required]
        public string WarehouseId { get; set; }
    }
    public class SelectAllNoneStageQuantityItemResponse : TSpStatusResponse
    {
    }
    //-------------------------------------------------------------------------------------------------------
    public class StagingTabsResponse : TSpStatusResponse
    {
        public bool QuantityTab { get; set; } = false;
        public bool HoldingTab { get; set; } = false;
        public bool SerialTab { get; set; } = false;
        public bool UsageTab { get; set; } = false;
        public bool ConsignmentTab { get; set; } = false;
    }
    //-------------------------------------------------------------------------------------------------------
    public class DecreaseOrderQuantityRequest
    {
        public string OrderId { get; set; }
        public string OrderItemId { get; set; }
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public int? Quantity { get; set; }
    }
    public class DecreaseOrderQuantityResponse : TSpStatusResponse { }
    //-------------------------------------------------------------------------------------------------------
    public class StagingStartSubstituteSessionRequest
    {
        public string OrderId { get; set; }
        public string OrderItemId { get; set; }
    }
    public class StagingStartSubstituteSessionResponse : TSpStatusResponse
    {
        public string SessionId { get; set; }
        public string OrderId { get; set; }
        public string OrderItemId { get; set; }
        public string ICode { get; set; }
        public string Description { get; set; }
        public decimal? QuantityOrdered { get; set; }
        public decimal? QuantityRemaining { get; set; }
    }
    //-------------------------------------------------------------------------------------------------------
    public class StagingAddSubstituteItemToSessionRequest
    {
        public string SessionId { get; set; }
        public string Code { get; set; }
        public string WarehouseId { get; set; }  // this field is optional.  If ommitted, RWW will use the user's default Warehouse
        public int? Quantity { get; set; }
    }
    public class StagingAddSubstituteItemToSessionResponse : TSpStatusResponse
    {
        public string ICode { get; set; }
        public string Description { get; set; }
    }
    //-------------------------------------------------------------------------------------------------------
    public class StagingApplySubstituteSessionRequest
    {
        public string SessionId { get; set; }
        public int? Quantity { get; set; }
    }
    public class StagingApplySubstituteSessionResponse : TSpStatusResponse
    {
    }
    //-------------------------------------------------------------------------------------------------------
    public static class CheckOutFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<StageItemResponse> StageItem(FwApplicationConfig appConfig, FwUserSession userSession, StageItemRequest request, FwSqlConnection conn = null)
        {
            StageItemResponse response = new StageItemResponse();
            if (string.IsNullOrEmpty(request.OrderId))
            {
                response.msg = "OrderId is required.";
            }
            else if ((string.IsNullOrEmpty(request.OrderItemId)) && (string.IsNullOrEmpty(request.Code)))
            {
                response.msg = "Must supply a Code or OrderItemId to stage items.";
            }
            else
            {
                if (conn == null)
                {
                    conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
                }

                //if (request.AddItemToOrder.GetValueOrDefault(false) || request.AddCompleteToOrder.GetValueOrDefault(false)) { }  // do auditing here

                FwSqlCommand qry = new FwSqlCommand(conn, "pdastageitem", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.InputOutput, request.OrderItemId);
                qry.AddParameter("@code", SqlDbType.NVarChar, ParameterDirection.Input, request.Code);
                qry.AddParameter("@vendorid", SqlDbType.NVarChar, ParameterDirection.Input, request.VendorId);
                if (request.Quantity != null)
                {
                    qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, request.Quantity);
                }
                qry.AddParameter("@additemtoorder", SqlDbType.NVarChar, ParameterDirection.Input, (request.AddItemToOrder.GetValueOrDefault(false) ? "T" : "F"));
                qry.AddParameter("@addcompletetoorder", SqlDbType.NVarChar, ParameterDirection.Input, (request.AddCompleteToOrder.GetValueOrDefault(false) ? "T" : "F"));
                qry.AddParameter("@incompletecontainerok", SqlDbType.NVarChar, ParameterDirection.Input, (request.StageIncompleteContainer.GetValueOrDefault(false) ? "T" : "F"));
                qry.AddParameter("@unstage", SqlDbType.NVarChar, ParameterDirection.Input, (request.UnstageItem.GetValueOrDefault(false) ? "T" : "F"));
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@userwarehouseid", SqlDbType.NVarChar, ParameterDirection.Input, request.WarehouseId);
                qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Output);
                //qry.AddParameter("@qtystaged", SqlDbType.Int, ParameterDirection.Output);

                qry.AddParameter("@isicode", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@masterno", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@description", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@qtyordered", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@qtysub", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@qtystaged", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@qtyout", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@qtyin", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@qtyremaining", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@showadditemtoorder", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@showaddcompletetoorder", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@showunstage", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@showstageincompletecontainer", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.InventoryId = qry.GetParameter("@masterid").ToString();
                response.OrderItemId = qry.GetParameter("@masteritemid").ToString();
                response.QuantityStaged = qry.GetParameter("@qtystaged").ToInt32();

                response.InventoryStatus.ICode = qry.GetParameter("@masterno").ToString();
                response.InventoryStatus.Description = qry.GetParameter("@description").ToString();
                response.InventoryStatus.QuantityOrdered = qry.GetParameter("@qtyordered").ToInt32();
                response.InventoryStatus.QuantitySub = qry.GetParameter("@qtysub").ToInt32();
                response.InventoryStatus.QuantityStaged = qry.GetParameter("@qtystaged").ToInt32();
                response.InventoryStatus.QuantityOut = qry.GetParameter("@qtyout").ToInt32();
                response.InventoryStatus.QuantityIn = qry.GetParameter("@qtyin").ToInt32();
                response.InventoryStatus.QuantityRemaining = qry.GetParameter("@qtyremaining").ToInt32();

                response.ShowAddItemToOrder = qry.GetParameter("@showadditemtoorder").ToString().Equals("T");
                response.ShowAddCompleteToOrder = qry.GetParameter("@showaddcompletetoorder").ToString().Equals("T");
                response.ShowUnstage = qry.GetParameter("@showunstage").ToString().Equals("T");
                response.ShowStageIncompleteContainer = qry.GetParameter("@showstageincompletecontainer").ToString().Equals("T");

                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();

                if ((response.status == 0) && ((request.Quantity == null) || (request.Quantity == 0)) && (qry.GetParameter("@isicode").ToString().Equals("T")))
                {
                    response.status = 107;
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<UnstageItemResponse> UnstageItem(FwApplicationConfig appConfig, FwUserSession userSession, UnstageItemRequest request)
        {
            UnstageItemResponse response = new UnstageItemResponse();
            if (string.IsNullOrEmpty(request.OrderId))
            {
                response.msg = "OrderId is required.";
            }
            else if ((string.IsNullOrEmpty(request.OrderItemId)) && (string.IsNullOrEmpty(request.Code)))
            {
                response.msg = "Must supply a Code or OrderItemId to unstage items.";
            }
            else
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "unstageitem", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                    qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderItemId);
                    qry.AddParameter("@code", SqlDbType.NVarChar, ParameterDirection.Input, request.Code);
                    qry.AddParameter("@vendorid", SqlDbType.NVarChar, ParameterDirection.Input, request.VendorId);
                    qry.AddParameter("@consignorid", SqlDbType.NVarChar, ParameterDirection.InputOutput, "");  //??
                    qry.AddParameter("@consignoragreementid", SqlDbType.NVarChar, ParameterDirection.InputOutput, request.ConsignorAgreementId);
                    if (request.Quantity != null)
                    {
                        qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, request.Quantity);
                    }
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    //qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, request.ContractId);
                    qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@rentalitemid", SqlDbType.NVarChar, ParameterDirection.Output);

                    qry.AddParameter("@masterno", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@description", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@qtyordered", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@qtysub", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@qtystaged", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@qtyout", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@qtyin", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@qtyremaining", SqlDbType.Int, ParameterDirection.Output);

                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();

                    response.InventoryId = qry.GetParameter("@masterid").ToString();
                    response.ItemId = qry.GetParameter("@rentalitemid").ToString();
                    response.ConsignorId = qry.GetParameter("@consignorid").ToString();
                    response.ConsignorAgreementId = qry.GetParameter("@consignoragreementid").ToString();


                    response.InventoryStatus.ICode = qry.GetParameter("@masterno").ToString();
                    response.InventoryStatus.Description = qry.GetParameter("@description").ToString();
                    response.InventoryStatus.QuantityOrdered = qry.GetParameter("@qtyordered").ToInt32();
                    response.InventoryStatus.QuantitySub = qry.GetParameter("@qtysub").ToInt32();
                    response.InventoryStatus.QuantityStaged = qry.GetParameter("@qtystaged").ToInt32();
                    response.InventoryStatus.QuantityOut = qry.GetParameter("@qtyout").ToInt32();
                    response.InventoryStatus.QuantityIn = qry.GetParameter("@qtyin").ToInt32();
                    response.InventoryStatus.QuantityRemaining = qry.GetParameter("@qtyremaining").ToInt32();


                    response.status = qry.GetParameter("@status").ToInt32();
                    response.success = (response.status == 0);
                    response.msg = qry.GetParameter("@msg").ToString();
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<SelectAllNoneStageQuantityItemResponse> SelectAllNoneStageQuantityItem(FwApplicationConfig appConfig, FwUserSession userSession, string orderId, string warehouseId, bool selectAll)
        {
            SelectAllNoneStageQuantityItemResponse response = new SelectAllNoneStageQuantityItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "selectallstagedquantity", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                qry.AddParameter("@selectallnone", SqlDbType.NVarChar, ParameterDirection.Input, selectAll ? RwConstants.SELECT_ALL : RwConstants.SELECT_NONE);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, warehouseId);
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
        public static async Task<SelectAllNoneStageQuantityItemResponse> SelectAllStageQuantityItem(FwApplicationConfig appConfig, FwUserSession userSession, string orderId, string warehouseId)
        {
            return await SelectAllNoneStageQuantityItem(appConfig, userSession, orderId, warehouseId, true);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNoneStageQuantityItemResponse> SelectNoneStageQuantityItem(FwApplicationConfig appConfig, FwUserSession userSession, string orderId, string warehouseId)
        {
            return await SelectAllNoneStageQuantityItem(appConfig, userSession, orderId, warehouseId, false);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<CheckOutAllStagedResponse> CheckOutAllStaged(FwApplicationConfig appConfig, FwUserSession userSession, CheckOutAllStagedRequest request)
        {
            CheckOutAllStagedResponse response = new CheckOutAllStagedResponse();
            if (string.IsNullOrEmpty(request.OrderId))
            {
                response.msg = "OrderId is required.";
            }
            else if (string.IsNullOrEmpty(request.OfficeLocationId))
            {
                response.msg = "OfficeLocationId is required.";
            }
            else if (string.IsNullOrEmpty(request.WarehouseId))
            {
                response.msg = "WarehouseId is required.";
            }
            else
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "checkoutallstaged", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@locationid", SqlDbType.NVarChar, ParameterDirection.Input, request.OfficeLocationId);
                    qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, request.WarehouseId);
                    qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.ContractId = qry.GetParameter("@contractid").ToString();
                    response.status = qry.GetParameter("@status").ToInt32();
                    response.success = (response.status == 0);
                    response.msg = qry.GetParameter("@msg").ToString();
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<MoveStagedItemResponse> MoveStagedItemToOut(FwApplicationConfig appConfig, FwUserSession userSession, string orderId, string orderItemId, string vendorId, string contractId, string code, float? quantity)
        {
            MoveStagedItemResponse response = new MoveStagedItemResponse();
            if (string.IsNullOrEmpty(orderId))
            {
                response.msg = "OrderId is required.";
            }
            else if (string.IsNullOrEmpty(contractId))
            {
                response.msg = "ContractId is required.";
            }
            else if (string.IsNullOrEmpty(code))
            {
                response.msg = "Code is required.";
            }
            else
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "movestageditemtoout", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                    qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                    qry.AddParameter("@code", SqlDbType.NVarChar, ParameterDirection.Input, code);
                    qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, orderItemId);
                    qry.AddParameter("@vendorid", SqlDbType.NVarChar, ParameterDirection.Input, vendorId);
                    if (quantity != null)
                    {
                        qry.AddParameter("@qty", SqlDbType.Float, ParameterDirection.Input, quantity);
                    }
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.status = qry.GetParameter("@status").ToInt32();
                    response.success = (response.status == 0);
                    response.msg = qry.GetParameter("@msg").ToString();
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<MoveStagedItemResponse> MoveOutItemToStaged(FwApplicationConfig appConfig, FwUserSession userSession, string orderId, string orderItemId, string vendorId, string contractId, string code, float? quantity)
        {
            MoveStagedItemResponse response = new MoveStagedItemResponse();
            if (string.IsNullOrEmpty(orderId))
            {
                response.msg = "OrderId is required.";
            }
            else if (string.IsNullOrEmpty(contractId))
            {
                response.msg = "ContractId is required.";
            }
            else if (string.IsNullOrEmpty(code))
            {
                response.msg = "Code is required.";
            }
            else
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "moveoutitemtostaged", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                    qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                    qry.AddParameter("@code", SqlDbType.NVarChar, ParameterDirection.Input, code);
                    qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, orderItemId);
                    qry.AddParameter("@vendorid", SqlDbType.NVarChar, ParameterDirection.Input, vendorId);
                    if (quantity != null)
                    {
                        qry.AddParameter("@qty", SqlDbType.Float, ParameterDirection.Input, quantity);
                    }
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.status = qry.GetParameter("@status").ToInt32();
                    response.success = (response.status == 0);
                    response.msg = qry.GetParameter("@msg").ToString();
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<CreateOutContractResponse> CreateOutContract(FwApplicationConfig appConfig, FwUserSession userSession, CreateOutContractRequest request)
        {
            CreateOutContractResponse response = new CreateOutContractResponse();
            if (string.IsNullOrEmpty(request.OrderId))
            {
                response.msg = "OrderId is required.";
            }
            else if (string.IsNullOrEmpty(request.OfficeLocationId))
            {
                response.msg = "OfficeLocationId is required.";
            }
            else if (string.IsNullOrEmpty(request.WarehouseId))
            {
                response.msg = "WarehouseId is required.";
            }
            else
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "createoutcontract", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@locationid", SqlDbType.NVarChar, ParameterDirection.Input, request.OfficeLocationId);
                    qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, request.WarehouseId);
                    qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.ContractId = qry.GetParameter("@contractid").ToString();
                    //response.status = qry.GetParameter("@status").ToInt32();
                    //response.success = (response.status == 0);
                    response.success = true;
                    //response.msg = qry.GetParameter("@msg").ToString();
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
        public static async Task<DecreaseOrderQuantityResponse> DecreaseOrderQuantity(FwApplicationConfig appConfig, FwUserSession userSession, DecreaseOrderQuantityRequest request)
        {
            DecreaseOrderQuantityResponse response = new DecreaseOrderQuantityResponse();

            int negativeQuantity = System.Math.Abs(Convert.ToInt32(request.Quantity)) * (-1);

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "addtoorder", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                qry.AddParameter("@origmasteritemid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderItemId);
                qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, request.InventoryId);
                qry.AddParameter("@additemtoorder", SqlDbType.NVarChar, ParameterDirection.Input, "T");
                qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, request.WarehouseId);
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
        public static async Task<StagingTabsResponse> GetStagingTabs(FwApplicationConfig appConfig, FwUserSession userSession, string orderId, string warehouseId)
        {
            StagingTabsResponse response = new StagingTabsResponse();

            if (string.IsNullOrEmpty(orderId))
            {
                response.msg = "OrderId is required.";
            }
            else if (string.IsNullOrEmpty(warehouseId))
            {
                response.msg = "WarehouseId is required.";
            }
            else
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "getstagingtabsweb", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                    qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, warehouseId);
                    qry.AddParameter("@hasquantity", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@hasholding", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@hasserial", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@hasusage", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@hasconsignment", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.success = true;
                    response.QuantityTab = FwConvert.ToBoolean(qry.GetParameter("@hasquantity").ToString());
                    response.HoldingTab = FwConvert.ToBoolean(qry.GetParameter("@hasholding").ToString());
                    response.SerialTab = FwConvert.ToBoolean(qry.GetParameter("@hasserial").ToString());
                    response.UsageTab = FwConvert.ToBoolean(qry.GetParameter("@hasusage").ToString());
                    response.ConsignmentTab = FwConvert.ToBoolean(qry.GetParameter("@hasconsignment").ToString());
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
        public static async Task<StagingStartSubstituteSessionResponse> StartSubstituteSession(FwApplicationConfig appConfig, FwUserSession userSession, StagingStartSubstituteSessionRequest request)
        {
            StagingStartSubstituteSessionResponse response = new StagingStartSubstituteSessionResponse();

            if (string.IsNullOrEmpty(request.OrderId))
            {
                response.msg = "OrderId is required.";
            }
            else if (string.IsNullOrEmpty(request.OrderItemId))
            {
                response.msg = "OrderItemId is required.";
            }
            else
            {
                StagingSubstituteSessionLogic s = new StagingSubstituteSessionLogic();
                s.SetDependencies(appConfig, userSession);
                s.OrderId = request.OrderId;
                s.OrderItemId = request.OrderItemId;
                await s.SaveAsync();
                response.SessionId = s.SessionId;
                response.success = true;
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
        public static async Task<StagingAddSubstituteItemToSessionResponse> AddSubstituteItemToSession(FwApplicationConfig appConfig, FwUserSession userSession, StagingAddSubstituteItemToSessionRequest request)
        {
            StagingAddSubstituteItemToSessionResponse response = new StagingAddSubstituteItemToSessionResponse();

            if (string.IsNullOrEmpty(request.SessionId))
            {
                response.msg = "SessionId is required.";
            }
            else if (string.IsNullOrEmpty(request.Code))
            {
                response.msg = "Must supply a Code to add a substitute item.";
            }
            else
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "stageaddsubstituteitemtosession", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
                    qry.AddParameter("@code", SqlDbType.NVarChar, ParameterDirection.Input, request.Code);
                    qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, request.WarehouseId);
                    qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, request.Quantity);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@masterno", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@description", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.status = qry.GetParameter("@status").ToInt32();
                    response.success = (response.status == 0);
                    response.msg = qry.GetParameter("@msg").ToString();
                    response.ICode = qry.GetParameter("@masterno").ToString();
                    response.Description = qry.GetParameter("@description").ToString();
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
        public static async Task<StagingApplySubstituteSessionResponse> ApplySubstituteSession(FwApplicationConfig appConfig, FwUserSession userSession, StagingApplySubstituteSessionRequest request)
        {
            StagingApplySubstituteSessionResponse response = new StagingApplySubstituteSessionResponse();

            if (string.IsNullOrEmpty(request.SessionId))
            {
                response.msg = "SessionId is required.";
            }
            else
            {
                if (request.Quantity == null)
                {
                    request.Quantity = 1;
                }

                response.success = true;
                FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
                try
                {
                    await conn.OpenAsync();
                    conn.BeginTransaction();

                    // load the substitute session from the database
                    StagingSubstituteSessionLogic subSession = new StagingSubstituteSessionLogic();
                    subSession.SetDependencies(appConfig, userSession);
                    subSession.SessionId = request.SessionId;
                    if (response.success)
                    {
                        response.success = await subSession.LoadAsync<StagingSubstituteSessionLogic>(conn: conn);
                        if (!response.success)
                        {
                            response.msg = "Could not load substitute session.";
                        }
                    }

                    // read the OrderItemId from the substitute session
                    // load the OrderItem from the database using the key field above
                    OrderItemLogic oiOrig = new OrderItemLogic();
                    oiOrig.SetDependencies(appConfig, userSession);
                    oiOrig.OrderItemId = subSession.OrderItemId;
                    if (response.success)
                    {
                        response.success = await oiOrig.LoadAsync<OrderItemLogic>(conn: conn);
                        if (!response.success)
                        {
                            response.msg = "Could not load order item.";
                        }
                    }

                    // decrease the Quantity from the OrderItem based on the quantity in this request
                    OrderItemLogic oiMod = oiOrig.MakeCopy<OrderItemLogic>();
                    oiMod.SetDependencies(appConfig, userSession);
                    oiMod.QuantityOrdered--;
                    if (response.success)
                    {
                        int recordsAffected = await oiMod.SaveAsync(original: oiOrig, conn: conn);
                        response.success = (recordsAffected > 0);
                        if (!response.success)
                        {
                            response.msg = "Could not decrease quantity of order item.";
                        }
                    }

                    // insert a new line-item to the Order.
                    //       InventoryId = orig.InventoryId
                    //       OrderId = orig.OrderId
                    //       WarehouseId = orig.WarehouseId
                    //       Description = orig.Description
                    //       (copy all fields from Orig, using orig.MakeCopy(), then blank out the OrderItemId)
                    //       ItemClass = "K"
                    //       Quantity = 1
                    //       ItemOrder = orig.ItemOrder + "A" (to make it sort down immediately beneath the original item)
                    OrderItemLogic oiNew = oiOrig.MakeCopy<OrderItemLogic>();
                    oiNew.SetDependencies(appConfig, userSession);
                    oiNew.ItemClass = RwConstants.ITEMCLASS_KIT;
                    oiNew.QuantityOrdered = 1;
                    oiNew.ItemOrder = oiOrig.ItemOrder + "A";
                    oiNew.OrderItemId = "";
                    oiNew.copying = true;
                    if (response.success)
                    {
                        int recordsAffected = await oiNew.SaveAsync(conn: conn);
                        response.success = (recordsAffected > 0);
                        if (!response.success)
                        {
                            response.msg = "Could not add new order item.";
                        }
                    }

                    if (response.success)
                    {
                        //  reapplymanualsort on this Order
                        TSpStatusResponse reapplySortResponse = await OrderFunc.ReapplyManualSort(appConfig, userSession, oiOrig.OrderId, conn: conn);
                        if (!reapplySortResponse.success)
                        {
                            response.success = false;
                            response.msg = reapplySortResponse.msg;
                        }
                    }


                    string substituteNote = "";
                    if (response.success)
                    {
                        //       For each item in the substitute session:
                        //          insert into the new Kit
                        BrowseRequest substituteItemBrowseRequest = new BrowseRequest();
                        substituteItemBrowseRequest.uniqueids = new Dictionary<string, object>();
                        substituteItemBrowseRequest.uniqueids.Add("SessionId", request.SessionId);

                        CheckOutSubstituteSessionItemLogic substituteItemSelector = new CheckOutSubstituteSessionItemLogic();
                        substituteItemSelector.SetDependencies(appConfig, userSession);
                        List<CheckOutSubstituteSessionItemLogic> substituteItems = await substituteItemSelector.SelectAsync<CheckOutSubstituteSessionItemLogic>(substituteItemBrowseRequest, conn: conn);
                        foreach (CheckOutSubstituteSessionItemLogic substituteItem in substituteItems)
                        {
                            if (response.success)
                            {
                                // insert the item into the substitute kit
                                InsertLineItemRequest insertItemRequest = new InsertLineItemRequest();
                                insertItemRequest.OrderId = oiOrig.OrderId;
                                insertItemRequest.PrimaryItemId = oiNew.OrderItemId;
                                insertItemRequest.BelowOrderItemId = oiNew.OrderItemId;
                                InsertLineItemResponse insertItemResponse = await OrderItemFunc.InsertLineItem(appConfig, userSession, insertItemRequest, conn: conn);
                                if (insertItemResponse.success)
                                {

                                    // set InventoryId and Quantity of newly-inserted item
                                    OrderItemLogic oiSub = new OrderItemLogic();
                                    oiSub.SetDependencies(appConfig, userSession);
                                    oiSub.OrderItemId = insertItemResponse.OrderItemId;
                                    await oiSub.LoadAsync<OrderItemLogic>(conn: conn);

                                    OrderItemLogic oiSubNew = oiSub.MakeCopy<OrderItemLogic>();
                                    oiSubNew.InventoryId = substituteItem.InventoryId;
                                    oiSubNew.Description = substituteItem.Description;
                                    oiSubNew.QuantityOrdered = substituteItem.Quantity;
                                    oiSubNew.Taxable = oiOrig.Taxable;

                                    if (response.success)
                                    {
                                        int recordsAffected = await oiSubNew.SaveAsync(original: oiSub, conn: conn);
                                        response.success = (recordsAffected > 0);
                                        if (!response.success)
                                        {
                                            response.msg = "Could not update new substitute item order item.";
                                        }
                                    }

                                    //stage the item(s) to this line
                                    if (response.success)
                                    {
                                        StageItemRequest stageRequest = new StageItemRequest();
                                        stageRequest.OrderId = oiSubNew.OrderId;
                                        stageRequest.OrderItemId = oiSubNew.OrderItemId;
                                        stageRequest.WarehouseId = oiSubNew.WarehouseId;
                                        if (string.IsNullOrEmpty(substituteItem.BarCode))
                                        {
                                            stageRequest.Code = substituteItem.ICode;
                                        }
                                        else
                                        {
                                            stageRequest.Code = substituteItem.BarCode;
                                        }
                                        stageRequest.Quantity = FwConvert.ToInt32(substituteItem.Quantity);
                                        try
                                        {
                                            StageItemResponse stageResponse = await StageItem(appConfig, userSession, stageRequest, conn: conn);
                                        }
                                        catch (Exception ex)
                                        {
                                            response.success = false;
                                            response.msg = ex.Message;
                                        }

                                    }

                                    if (!substituteNote.Equals(string.Empty))
                                    {
                                        substituteNote += ", ";
                                    }

                                    if (!string.IsNullOrEmpty(substituteItem.BarCode))
                                    {
                                        substituteNote += "(" + FwConvert.ToInt32(substituteItem.Quantity).ToString() + ") ";
                                    }

                                    substituteNote += substituteItem.Description;
                                    if (!string.IsNullOrEmpty(substituteItem.BarCode))
                                    {
                                        substituteNote += " (Bar Code: " + substituteItem.BarCode + ")";
                                    }
                                    else if (substituteItem.Quantity > 1)
                                    {
                                        substituteNote += " (Qty: " + FwConvert.ToInt32(substituteItem.Quantity).ToString() + ")";
                                    }
                                }
                                else
                                {
                                    response.success = false;
                                    response.msg = insertItemResponse.msg;
                                }
                            }
                        }
                    }

                    //  update line-item note on orig
                    //  "Replaced at Staging with Description 1 (Bar Code: ABC45648), Description 2 (Qty. 2), Description 3 (Bar Code: 60680680)"
                    substituteNote = "Replaced (" + request.Quantity.ToString() + ") at Staging with " + substituteNote;
                    oiMod.Notes += substituteNote;
                    oiMod.PrintNoteOnOrder = true;
                    oiMod.PrintNoteOnInvoice = true;
                    if (response.success)
                    {
                        int recordsAffected = await oiMod.SaveAsync(original: oiOrig, conn: conn);
                        response.success = (recordsAffected > 0);
                        if (!response.success)
                        {
                            response.msg = "Could not add substitution note.";
                        }
                    }


                    if (response.success)
                    {
                        //  reapplymanualsort on this Order
                        TSpStatusResponse reapplySortResponse = await OrderFunc.ReapplyManualSort(appConfig, userSession, oiOrig.OrderId, conn: conn);
                        if (!reapplySortResponse.success)
                        {
                            response.success = false;
                            response.msg = reapplySortResponse.msg;
                        }
                    }

                    if (response.success)
                    {
                        //delete the session
                        response.success = await subSession.DeleteAsync(conn: conn);
                        if (!response.success)
                        {
                            response.msg = "Could not delete substitute session.";
                        }
                    }

                    conn.CommitTransaction();
                }
                //finally
                //{
                //    if (response.success)
                //    {
                //        conn.CommitTransaction();
                //    }
                //    else
                //    {
                //        conn.RollbackTransaction();
                //    }
                //    conn.Close();
                //}
                catch (Exception ex)
                {
                    response.success = false;
                    response.msg = ex.Message;
                    conn.RollbackTransaction();
                }
                finally
                {
                    conn.Close();
                }
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
    }
}
