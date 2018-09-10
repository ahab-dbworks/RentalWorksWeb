using FwStandard.Models;
using FwStandard.SqlServer;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Home.CheckOut;
using WebLibrary;

namespace WebApi.Home.CheckOut
{

    public class OrderInventoryStatus
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

    public class StageItemReponse : TSpStatusReponse
    {
        public string InventoryId;
        public string OrderItemId;
        public int QuantityStaged;

        public OrderInventoryStatus InventoryStatus = new OrderInventoryStatus();

        public bool ShowAddItemToOrder;
        public bool ShowAddCompleteToOrder;
        public bool ShowUnstage;
    }

    public class CheckOutAllStagedResponse : TSpStatusReponse
    {
        public string ContractId;
    }

    public class SelectAllNoneStageQuantityItemRequest
    {
        [Required]
        public string OrderId { get; set; }
    }


    public class SelectAllNoneStageQuantityItemResponse : TSpStatusReponse
    {
    }



    public static class CheckOutFunc
    {
        /*
                                       @vendorid                     char(08)      = '',
                                       @meter                        numeric(11,2) = 0,
                                       @location                     varchar(255)  = '',
                                       @spaceid                      char(08)      = '',
                                       @spacetypeid                  char(08)      = '',
                                       @facilitiestypeid             char(08)      = '',
                                       @additemtoorder               char(01)      = 'F',
                                       @additemtopackagemasteritemid char(08)      = '',
                                       @addcompletetoorder           char(01)      = 'F',
                                       @addcontainertoorder          char(01)      = 'F',
                                       @overridereservation          char(01)      = 'F',
                                       @stageconsigned               char(01)      = 'F',
                                       @transferrepair               char(01)      = 'F',
                                       @removefromcontainer          char(01)      = 'F',
                                       @releasefromrepair            char(01)      = 'F',       --//jh 06/30/2015 CAS-15904-IVQS
                                       @autostageacc                 char(01)      = 'F',
                                       @contractid                   char(08)      = '',  --// only supply a value if item should go out immediately
                                       @ignoresuspendedin            char(01)      = '',
                                       @incompletecontainerok        char(01)      = 'F',
                                       @ignoreassignedcontract       char(01)      = '',
                                       @rentalitemid                 char(08)      = ''  output,
                                       @consignorid                  char(08)      = ''  output,   --//also used as an input value for quantity items
                                       @consignoragreementid         char(08)      = ''  output,   --//also used as an input value for quantity items
                                       @consignorpoid                char(08)      = ''  output,
                                       @exceptionbatchid             char(30)      = ''  output,

                     */
        //-------------------------------------------------------------------------------------------------------
        public static async Task<StageItemReponse> StageItem(FwApplicationConfig appConfig, FwUserSession userSession, string orderId, string orderItemId, string code, int? quantity, bool addItemToOrder, bool addCompleteToOrder)
        {
            StageItemReponse response = new StageItemReponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "pdastageitem", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.InputOutput, orderItemId);
                qry.AddParameter("@code", SqlDbType.NVarChar, ParameterDirection.Input, code);
                if (quantity != null)
                {
                    qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, quantity);
                }
                qry.AddParameter("@additemtoorder", SqlDbType.NVarChar, ParameterDirection.Input, (addItemToOrder ? "T" : "F"));
                qry.AddParameter("@addcompletetoorder", SqlDbType.NVarChar, ParameterDirection.Input, (addCompleteToOrder ? "T" : "F"));
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
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
                await qry.ExecuteNonQueryAsync(true);
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
                //response.success = ((response.status == 0) || (response.status == 107));
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();

                if ((response.status == 0) && ((quantity == null) || (quantity == 0)) && (qry.GetParameter("@isicode").ToString().Equals("T")))
                {
                    response.status = 107;
                }


            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<SelectAllNoneStageQuantityItemResponse> SelectAllNoneStageQuantityItem(FwApplicationConfig appConfig, FwUserSession userSession, string orderId, bool selectAll)
        {
            SelectAllNoneStageQuantityItemResponse response = new SelectAllNoneStageQuantityItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "selectallstagedquantity", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                qry.AddParameter("@selectallnone", SqlDbType.NVarChar, ParameterDirection.Input, selectAll? RwConstants.SELECT_ALL : RwConstants.SELECT_NONE);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNoneStageQuantityItemResponse> SelectAllStageQuantityItem(FwApplicationConfig appConfig, FwUserSession userSession, string orderId)
        {
            return await SelectAllNoneStageQuantityItem(appConfig, userSession, orderId, true);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNoneStageQuantityItemResponse> SelectNoneStageQuantityItem(FwApplicationConfig appConfig, FwUserSession userSession, string orderId)
        {
            return await SelectAllNoneStageQuantityItem(appConfig, userSession, orderId, false);
        }
        //-------------------------------------------------------------------------------------------------------




        public static async Task<CheckOutAllStagedResponse> CheckOutAllStaged (FwApplicationConfig appConfig, FwUserSession userSession, string orderId)
        {
            CheckOutAllStagedResponse response = new CheckOutAllStagedResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "checkoutallstaged", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.ContractId = qry.GetParameter("@contractid").ToString();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<MoveStagedItemResponse> MoveStagedItemToOut(FwApplicationConfig appConfig, FwUserSession userSession, string orderId, string orderItemId, string vendorId, string contractId, string code, float? quantity)
        {
            MoveStagedItemResponse response = new MoveStagedItemResponse();
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
                await qry.ExecuteNonQueryAsync(true);
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<MoveStagedItemResponse> MoveOutItemToStaged(FwApplicationConfig appConfig, FwUserSession userSession, string orderId, string orderItemId, string vendorId, string contractId, string code, float? quantity)
        {
            MoveStagedItemResponse response = new MoveStagedItemResponse();
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
                await qry.ExecuteNonQueryAsync(true);
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<CreateOutContractResponse> CreateOutContract(FwApplicationConfig appConfig, FwUserSession userSession, string orderId)
        {
            CreateOutContractResponse response = new CreateOutContractResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "createoutcontract", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.ContractId = qry.GetParameter("@contractid").ToString();
                //response.status = qry.GetParameter("@status").ToInt32();
                //response.success = (response.status == 0);
                response.success = true;
                //response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
    //-------------------------------------------------------------------------------------------------------    
}
}
