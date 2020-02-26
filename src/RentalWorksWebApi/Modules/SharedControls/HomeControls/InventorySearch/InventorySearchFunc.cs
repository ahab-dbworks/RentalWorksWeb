using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Modules.HomeControls.InventoryPackageInventory;

namespace WebApi.Modules.HomeControls.InventorySearch
{
    public static class InventorySearchFunc
    {
        //-------------------------------------------------------------------------------------------------------   
        public static async Task<InventorySearchGetTotalResponse> GetTotalAsync(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId)
        {
            InventorySearchGetTotalResponse response = new InventorySearchGetTotalResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "tmpsearchsessiongettotal", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, sessionId);
                qry.AddParameter("@totalqtyinsession", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                int i = await qry.ExecuteNonQueryAsync();
                response.TotalQuantityInSession = qry.GetParameter("@totalqtyinsession").ToDecimal();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------   
        public static async Task<bool> AddToOrderAsync(FwApplicationConfig appConfig, FwUserSession userSession, InventorySearchAddToOrderRequest request)
        {
            bool b = false;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qrySessionItems = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qrySessionItems.Add("select distinct t.masterid, t.warehouseid, m.class              ");
                qrySessionItems.Add(" from  tmpsearchsession t                                       ");
                qrySessionItems.Add("            join master m on (t.masterid = m.masterid)          ");
                qrySessionItems.Add(" where t.sessionid = @sessionid                                 ");
                qrySessionItems.AddParameter("@sessionid", request.SessionId);
                FwJsonDataTable dt = await qrySessionItems.QueryToFwJsonTableAsync();

                // adding to Quote, Order, PO, Transfer, etc.
                FwSqlCommand qry = new FwSqlCommand(conn, "tmpsearchsessionaddtoorder", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                qry.AddParameter("@insertatindex", SqlDbType.NVarChar, ParameterDirection.Input, request.InsertAtIndex);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                int i = await qry.ExecuteNonQueryAsync();

                foreach (List<object> row in dt.Rows)
                {
                    string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                    string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
                    string classification = row[dt.GetColumnNo("class")].ToString();
                    InventoryAvailability.InventoryAvailabilityFunc.RequestRecalc(inventoryId, warehouseId, classification);
                }

            }
            return b;
        }
        //-------------------------------------------------------------------------------------------------------   
        public static async Task<bool> AddToPackageAsync(FwApplicationConfig appConfig, FwUserSession userSession, InventorySearchAddToCompleteKitContainerRequest request)
        {
            bool b = false;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                string invClassification = FwSqlCommand.GetStringDataAsync(conn, appConfig.DatabaseSettings.QueryTimeout, "master", "masterid", request.InventoryId, "class").Result;
                if (invClassification.Equals(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE) || invClassification.Equals(RwConstants.INVENTORY_CLASSIFICATION_KIT))
                {
                    try
                    {
                        await conn.GetConnection().OpenAsync();
                        conn.BeginTransaction();

                        FwSqlCommand qrySessionItems = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                        qrySessionItems.Add("select t.masteritemid, t.masterid, t.warehouseid, t.qty,        ");
                        qrySessionItems.Add("       m.master                                                 ");
                        qrySessionItems.Add(" from  tmpsearchsession t                                       ");
                        qrySessionItems.Add("            join master m on (t.masterid = m.masterid)          ");
                        qrySessionItems.Add(" where t.sessionid = @sessionid                                 ");
                        qrySessionItems.AddParameter("@sessionid", request.SessionId);
                        FwJsonDataTable dt = await qrySessionItems.QueryToFwJsonTableAsync();

                        //#jhtodo: need to handle Containers special here #788
                        foreach (List<object> row in dt.Rows)
                        {
                            InventoryPackageInventoryLogic item = new InventoryPackageInventoryLogic();
                            item.SetDependencies(appConfig, userSession);
                            item.PackageId = request.InventoryId;
                            item.InventoryId = row[dt.GetColumnNo("masterid")].ToString();
                            item.Description = row[dt.GetColumnNo("master")].ToString();
                            item.DefaultQuantity = FwConvert.ToDecimal(row[dt.GetColumnNo("qty")].ToString());
                            //item.WarehouseId = row[dt.GetColumnNo("warehouseid")].ToString();  // (only specify when the Complete/Kit is "warehouse-specific"
                            int saveCount = item.SaveAsync(null, conn: conn).Result;
                        }

                        qrySessionItems = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                        qrySessionItems.Add("delete t                                                        ");
                        qrySessionItems.Add(" from  tmpsearchsession t                                       ");
                        qrySessionItems.Add(" where t.sessionid = @sessionid                                 ");
                        qrySessionItems.AddParameter("@sessionid", request.SessionId);
                        await qrySessionItems.ExecuteAsync();

                        conn.CommitTransaction();
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
                else if (invClassification.Equals(RwConstants.INVENTORY_CLASSIFICATION_CONTAINER))
                {
                    string containerId = FwSqlCommand.GetStringDataAsync(conn, appConfig.DatabaseSettings.QueryTimeout, "master", "masterid", request.InventoryId, "containerid").Result;

                    FwSqlCommand qry = new FwSqlCommand(conn, "tmpsearchsessionaddtoorder", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, containerId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    int i = await qry.ExecuteNonQueryAsync();
                }
                else
                {
                    throw new Exception($"Cannot copy from QuikSearch.  Invalid Inventory Classification: \"{invClassification}\"");
                }
            }
            return b;
        }
        //-------------------------------------------------------------------------------------------------------      
    }
}