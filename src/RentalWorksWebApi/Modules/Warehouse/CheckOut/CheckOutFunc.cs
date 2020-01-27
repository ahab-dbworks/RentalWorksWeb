using FwStandard.Models;
using FwStandard.SqlServer;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi;

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
    public static class CheckOutFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<StageItemResponse> StageItem(FwApplicationConfig appConfig, FwUserSession userSession, StageItemRequest request)
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
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
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

                    response.status = qry.GetParameter("@status").ToInt32();
                    response.success = (response.status == 0);
                    response.msg = qry.GetParameter("@msg").ToString();

                    if ((response.status == 0) && ((request.Quantity == null) || (request.Quantity == 0)) && (qry.GetParameter("@isicode").ToString().Equals("T")))
                    {
                        response.status = 107;
                    }
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
    }
}
