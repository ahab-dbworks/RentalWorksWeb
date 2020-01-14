using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi;

namespace WebApi.Modules.Warehouse.CheckIn
{

    public class CheckInContractRequest
    {
        public string OrderId;
        public string DealId;
        public string DepartmentId;
        public string OfficeLocationId;
        public string WarehouseId;
    }

    public class CheckInContractResponse
    {
        public string ContractId;
    }

    public class CheckInItemRequest
    {
        public string ModuleType;   // O=Order or T=Transfer
        public string ContractId;
        public string Code;
        public string OrderId;
        public string OrderItemId;
        public int? Quantity;
        public bool? AddOrderToContract;
        public bool? SwapItem;
    }

    public class OrderInventoryStatusCheckIn
    {
        public string ICode;
        public string Description;
        public int QuantityOrdered;
        public int QuantitySub;
        public int QuantityStaged;
        public int QuantityOut;
        public int QuantityIn;
        public int QuantityRemaining;
    }

    public class TCheckInItemResponse : TSpStatusResponse
    {
        public string ContractId;
        public string OrderId;
        public string OrderNumber;
        public string OrderDescription;
        public string DealId;
        public string Deal;
        public string DepartmentId;
        public string Department;
        public string InventoryId;
        public string OrderItemId;
        public OrderInventoryStatusCheckIn InventoryStatus = new OrderInventoryStatusCheckIn();
        public bool ShowNewOrder;
        public bool ShowSwap;
    }

    public class CancelCheckInItemRequest
    {
        public string ContractId;
        public string OrderId;
        public string OrderItemId;
        public string InventoryId;
        public string VendorId;
        public string Description;
        public int Quantity;
    }

    public class CancelCheckInItemResponse : TSpStatusResponse { };

    public static class CheckInFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string> CreateCheckInContract(FwApplicationConfig appConfig, FwUserSession userSession, CheckInContractRequest request)
        {
            string contractId = "";
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "createincontract", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId?? "");
                qry.AddParameter("@dealid", SqlDbType.NVarChar, ParameterDirection.Input, request.DealId ?? "");
                qry.AddParameter("@departmentid", SqlDbType.NVarChar, ParameterDirection.Input, request.DepartmentId);
                qry.AddParameter("@locationid", SqlDbType.NVarChar, ParameterDirection.Input, request.OfficeLocationId);
                qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, request.WarehouseId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                contractId = qry.GetParameter("@contractid").ToString();
            }
            return contractId;
        }
        //-------------------------------------------------------------------------------------------------------
        //public static async Task<TCheckInItemResponse> CheckInItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string code, string orderItemId, int? quantity, bool? addOrderToContract, bool? swapItem)
        public static async Task<TCheckInItemResponse> CheckInItem(FwApplicationConfig appConfig, FwUserSession userSession, CheckInItemRequest request)
        {

            /*
             
create procedure dbo.pdacheckinitem(@code                   varchar(255),
                                    @usersid                char(08),
                                    @qty                    numeric ,
                                    @neworderaction         char(01),            --//Y = yes check-In, S = Swap else do nothing
                                    @moduletype             char(01),            --//O = Order       , T = Transfer,     P = Package Truck
                                    @checkinmode            char(01),            --//M = Multi Order , O = Single Order, D = Deal, S = Session
                                    @containeritemid        char(08)   = '',  --//mv 5/2/16 CAS-16066-F6P9
                                    @containeroutcontractid char(08)   = '',  --//mv 5/2/16 CAS-16066-F6P9
                                    @trackedby              varchar(255)    = null,
                                    @aisle                  varchar(255)    = null,
                                    @shelf                  varchar(255)    = null,
                                    @parentid               char(08)        = null,
                                    @disablemultiorder      char(01)        = '',  --//eg 1/15/15 CAS-09425-H05S
                                    @vendorid               char(08)        = '' output, --//input/output
                                    @vendor                 varchar(255)    = '' output,
                                    @incontractid           char(08)        = '' output, --//input/output
                                    @orderid                char(08)        = '' output, --//input/output
                                    @dealid                 char(08)        = '' output, --//input/output
                                    @departmentid           char(08)        = '' output, --//input/output
                                    @itemorderid            char(08)        = '' output,
                                    @masteritemid           char(08)        = '' output,
                                    @masterid               char(08)        = '' output,
                                    @warehouseid            char(08)        = '' output,
                                    @ordertranid            integer         = 0  output,
                                    @internalchar           char(01)        = '' output,
                                    @rentalitemid           char(08)        = '' output,
                                    @isicode                char(01)        = '' output,
                                    @orderno                varchar(255)    = '' output,
                                    @masterno               varchar(255)    = '' output,
                                    @description            varchar(255)    = '' output,
                                    @allowswap              char(01)        = '' output,
                                    @qtyordered             numeric(09, 02) = 0  output,
                                    @subqty                 numeric(09, 02) = 0  output,
                                    @stillout               numeric(09, 02) = 0  output,
                                    @totalin                numeric(09, 02) = 0  output,
                                    @sessionin              numeric(09, 02) = 0  output,
                                    @pendingrepairid        char(08)        = '' output,  --//jh 05/12/2016 CAS-17895-K7P2
                                    @exchangecontractid     char(08)        = '' output,  --//input/output

                                    @genericmsg             varchar(255)    = '' output,
                                    @status                 numeric         = 0  output,
                                    @msg                    varchar(255)    = '' output
             
             
             */
            TCheckInItemResponse response = new TCheckInItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "pdacheckinitem", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@incontractid", SqlDbType.NVarChar, ParameterDirection.InputOutput, request.ContractId);
                qry.AddParameter("@code", SqlDbType.NVarChar, ParameterDirection.Input, request.Code);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.InputOutput, request.OrderItemId);
                if (request.Quantity != null)
                {
                    qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, request.Quantity);
                }
                qry.AddParameter("@neworderaction", SqlDbType.NVarChar, ParameterDirection.Input, (request.AddOrderToContract.GetValueOrDefault(false) ? RwConstants.CHECK_IN_NEW_ORDER_ACTION_ADD_NEW_ORDER : (request.SwapItem.GetValueOrDefault(false) ? RwConstants.CHECK_IN_NEW_ORDER_ACTION_SWAP_ITEM : "")));
                qry.AddParameter("@moduletype", SqlDbType.NVarChar, ParameterDirection.Input, request.ModuleType);
                qry.AddParameter("@checkinmode", SqlDbType.NVarChar, ParameterDirection.Input, request.ModuleType);
                qry.AddParameter("@itemorderid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@orderno", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@orderdesc", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@dealid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@deal", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@departmentid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@department", SqlDbType.NVarChar, ParameterDirection.Output);
                //qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@masterno", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@isicode", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@description", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@allowneworder", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@allowswap", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@qtyordered", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@subqty", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@stillout", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@totalin", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.ContractId = qry.GetParameter("@incontractid").ToString();
                response.OrderId = qry.GetParameter("@itemorderid").ToString();
                response.OrderNumber = qry.GetParameter("@orderno").ToString();
                response.OrderDescription = qry.GetParameter("@orderdesc").ToString();
                response.DealId = qry.GetParameter("@dealid").ToString();
                response.Deal = qry.GetParameter("@deal").ToString();
                response.DepartmentId = qry.GetParameter("@departmentid").ToString();
                response.Department = qry.GetParameter("@department").ToString();
                response.InventoryId = qry.GetParameter("@masterid").ToString();
                response.OrderItemId = qry.GetParameter("@masteritemid").ToString();
                response.ShowNewOrder = qry.GetParameter("@allowneworder").ToString().Equals("T");
                response.ShowSwap = qry.GetParameter("@allowswap").ToString().Equals("T");

                response.InventoryStatus.ICode = qry.GetParameter("@masterno").ToString();
                response.InventoryStatus.Description = qry.GetParameter("@description").ToString();
                response.InventoryStatus.QuantityOrdered = qry.GetParameter("@qtyordered").ToInt32();
                response.InventoryStatus.QuantitySub = qry.GetParameter("@subqty").ToInt32();
                //response.InventoryStatus.QuantityStaged = qry.GetParameter("@qtystaged").ToInt32();
                response.InventoryStatus.QuantityOut = qry.GetParameter("@stillout").ToInt32();
                response.InventoryStatus.QuantityIn = qry.GetParameter("@totalin").ToInt32();
                //response.InventoryStatus.QuantityRemaining = qry.GetParameter("@qtyremaining").ToInt32();
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
        public static async Task<CancelCheckInItemResponse>CancelCheckInItems(FwApplicationConfig appConfig, FwUserSession userSession, CancelCheckInItemRequest request)
        {
            CancelCheckInItemResponse response = new CancelCheckInItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "chkinitemcancel", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, request.ContractId);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderItemId);
                qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, request.InventoryId);
                qry.AddParameter("@vendorid", SqlDbType.NVarChar, ParameterDirection.Input, request.VendorId ?? "");
                qry.AddParameter("@description", SqlDbType.NVarChar, ParameterDirection.Input, request.Description);
                qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, request.Quantity);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@ordertranid", SqlDbType.Int, ParameterDirection.Input, 0);
                qry.AddParameter("@internalchar", SqlDbType.NVarChar, ParameterDirection.Input, "");
                await qry.ExecuteNonQueryAsync();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
