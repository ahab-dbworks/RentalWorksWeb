using FwStandard.Models;
using FwStandard.SqlServer;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Home.Contract;
using WebLibrary;

namespace WebApi.Modules.Utilities.Migrate
{


    public class SelectAllNoneMigrateItemRequest
    {
        [Required]
        public string SessionId { get; set; }
    }


    public class SelectAllNoneMigrateItemResponse : TSpStatusResponse
    {
    }


    public static class MigrateFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<StartMigrateSessionResponse> StartSession(FwApplicationConfig appConfig, FwUserSession userSession, StartMigrateSessionRequest request)
        {
            StartMigrateSessionResponse response = new StartMigrateSessionResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "startmigratesession", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@dealid", SqlDbType.NVarChar, ParameterDirection.Input, request.DealId);
                qry.AddParameter("@departmentid", SqlDbType.NVarChar, ParameterDirection.Input, request.DepartmentId);
                qry.AddParameter("@orderids", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderIds);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.SessionId = qry.GetParameter("@sessionid").ToString();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<UpdateMigrateItemResponse> UpdateItem(FwApplicationConfig appConfig, FwUserSession userSession, UpdateMigrateItemRequest request)
        {
            UpdateMigrateItemResponse response = new UpdateMigrateItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "updatemigrateitem", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderItemId);
                qry.AddParameter("@barcode", SqlDbType.NVarChar, ParameterDirection.Input, request.BarCode);
                qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, request.Quantity);
                qry.AddParameter("@newqty", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.NewQuantity = qry.GetParameter("@newqty").ToInt32();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------


        private static async Task<SelectAllNoneMigrateItemResponse> SelectAllNoneMigrateItem(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId, bool selectAll)
        {
            SelectAllNoneMigrateItemResponse response = new SelectAllNoneMigrateItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "webmigrateselectallnone", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, sessionId);
                qry.AddParameter("@selectallnone", SqlDbType.NVarChar, ParameterDirection.Input, selectAll ? RwConstants.SELECT_ALL : RwConstants.SELECT_NONE);
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
        public static async Task<SelectAllNoneMigrateItemResponse> SelectAllMigrateItem(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId)
        {
            return await SelectAllNoneMigrateItem(appConfig, userSession, sessionId, true);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNoneMigrateItemResponse> SelectNoneMigrateItem(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId)
        {
            return await SelectAllNoneMigrateItem(appConfig, userSession, sessionId, false);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<CompleteMigrateSessionResponse> CompleteSession(FwApplicationConfig appConfig, FwUserSession userSession, CompleteMigrateSessionRequest request)
        {
            //#jhtodo: implement this here in the API layer for better auditing

            CompleteMigrateSessionResponse response = new CompleteMigrateSessionResponse();

            if (string.IsNullOrEmpty(request.SessionId))
            {
                response.success = false;
                response.msg = "SessionId is required.";
            }
            else if (request.MigrateToNewOrder.GetValueOrDefault(false) && (string.IsNullOrEmpty(request.NewOrderDealId)))
            {
                response.success = false;
                response.msg = "New Order Deal is required.";
            }
            else if (request.MigrateToNewOrder.GetValueOrDefault(false) && (string.IsNullOrEmpty(request.NewOrderDescription)))
            {
                response.success = false;
                response.msg = "New Order Description is required.";
            }
            else if (request.MigrateToNewOrder.GetValueOrDefault(false) && (string.IsNullOrEmpty(request.NewOrderOfficeLocationId)))
            {
                response.success = false;
                response.msg = "New Order Office Location is required.";
            }
            else if (request.MigrateToNewOrder.GetValueOrDefault(false) && (string.IsNullOrEmpty(request.NewOrderRateType)))
            {
                response.success = false;
                response.msg = "New Order Office Rate Type is required.";
            }
            else if (request.MigrateToNewOrder.GetValueOrDefault(false) && ((request.NewOrderFromDate == null) || (request.NewOrderToDate == null)))
            {
                response.success = false;
                response.msg = "New Order \"From\" and \"To\" Dates are required.";
            }
            else if (request.MigrateToExistingOrder.GetValueOrDefault(false) && (string.IsNullOrEmpty(request.ExistingOrderId)))
            {
                response.success = false;
                response.msg = "Existing Order not specified.";
            }
            else
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "webmigrate", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
                    qry.AddParameter("@migratetoneworder", SqlDbType.NVarChar, ParameterDirection.Input, request.MigrateToNewOrder.GetValueOrDefault(false));
                    qry.AddParameter("@neworderlocationid", SqlDbType.NVarChar, ParameterDirection.Input, request.NewOrderOfficeLocationId);
                    qry.AddParameter("@neworderwarehouseid", SqlDbType.NVarChar, ParameterDirection.Input, request.NewOrderWarehouseId);
                    qry.AddParameter("@neworderdealid", SqlDbType.NVarChar, ParameterDirection.Input, request.NewOrderDealId);
                    qry.AddParameter("@neworderdesc", SqlDbType.NVarChar, ParameterDirection.Input, request.NewOrderDescription);
                    qry.AddParameter("@neworderratetype", SqlDbType.NVarChar, ParameterDirection.Input, request.NewOrderRateType);
                    qry.AddParameter("@neworderfromdate", SqlDbType.DateTime, ParameterDirection.Input, request.NewOrderFromDate);
                    qry.AddParameter("@neworderfromtime", SqlDbType.NVarChar, ParameterDirection.Input, request.NewOrderFromTime);
                    qry.AddParameter("@newordertodate", SqlDbType.DateTime, ParameterDirection.Input, request.NewOrderToDate);
                    qry.AddParameter("@newordertotime", SqlDbType.NVarChar, ParameterDirection.Input, request.NewOrderToTime);
                    qry.AddParameter("@neworderbillperiodend", SqlDbType.DateTime, ParameterDirection.Input, request.NewOrderBillingStopDate);
                    qry.AddParameter("@neworderpendingpo", SqlDbType.NVarChar, ParameterDirection.Input, request.NewOrderPendingPO.GetValueOrDefault(false));
                    qry.AddParameter("@neworderflatpo", SqlDbType.NVarChar, ParameterDirection.Input, request.NewOrderFlatPO.GetValueOrDefault(false));
                    qry.AddParameter("@neworderpono", SqlDbType.NVarChar, ParameterDirection.Input, request.NewOrderPurchaseOrderNumber);
                    qry.AddParameter("@neworderpoamt", SqlDbType.Decimal, ParameterDirection.Input, request.NewOrderPurchaseOrderAmount);
                    qry.AddParameter("@migratetoexistingorder", SqlDbType.NVarChar, ParameterDirection.Input, request.MigrateToExistingOrder.GetValueOrDefault(false));
                    qry.AddParameter("@existingorderid", SqlDbType.NVarChar, ParameterDirection.Input, request.ExistingOrderId);
                    qry.AddParameter("@inventoryfulfillincrement", SqlDbType.NVarChar, ParameterDirection.Input, request.InventoryFulfillIncrement);
                    qry.AddParameter("@inventorycheckedorstaged", SqlDbType.NVarChar, ParameterDirection.Input, request.InventoryCheckedOrStaged);
                    qry.AddParameter("@copylineitemnotes", SqlDbType.NVarChar, ParameterDirection.Input, request.CopyLineItemNotes.GetValueOrDefault(false));
                    qry.AddParameter("@copyordernotes", SqlDbType.NVarChar, ParameterDirection.Input, request.CopyOrderNotes.GetValueOrDefault(false));
                    qry.AddParameter("@copyrates", SqlDbType.NVarChar, ParameterDirection.Input, request.CopyRentalRates.GetValueOrDefault(false));
                    qry.AddParameter("@updatebillperiodend", SqlDbType.NVarChar, ParameterDirection.Input, request.UpdateBillingStopDate.GetValueOrDefault(false));
                    qry.AddParameter("@billperiodend", SqlDbType.NVarChar, ParameterDirection.Input, request.BillingStopDate);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.status = qry.GetParameter("@status").ToInt32();
                    response.success = (response.status == 0);
                    response.msg = qry.GetParameter("@msg").ToString();

                    if (response.success)
                    {
                        response.Contracts = new List<Home.Contract.ContractLogic>();

                        FwSqlCommand qryContract = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                        qryContract.Add("select c.contractid             ");
                        qryContract.Add(" from  contract c with (nolock) ");
                        qryContract.Add(" where c.sessionid = @sessionid ");
                        qryContract.AddParameter("@sessionid", request.SessionId);
                        FwJsonDataTable dt = await qryContract.QueryToFwJsonTableAsync();

                        foreach (List<object> row in dt.Rows)
                        {
                            string contractId = row[dt.GetColumnNo("contractid")].ToString();
                            ContractLogic l = new ContractLogic();
                            l.SetDependencies(appConfig, userSession);
                            l.ContractId = contractId;
                            await l.LoadAsync<ContractLogic>();
                            response.Contracts.Add(l);
                        }
                    }
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
