using FwStandard.Models;
using FwStandard.SqlServer;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.HomeControls.InventoryAvailability;
using WebApi.Modules.HomeControls.OrderItem;

namespace WebApi.Modules.Transfers.TransferOrder
{
    public static class TransferOrderFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TSpStatusResponse> AfterSaveTransfer(FwApplicationConfig appConfig, FwUserSession userSession, string id, FwSqlConnection conn = null)
        {
            TSpStatusResponse response = new TSpStatusResponse();

            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }

            using (FwSqlCommand qry = new FwSqlCommand(conn, "aftersavetransferweb", appConfig.DatabaseSettings.QueryTimeout))
            {
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, id);
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
        public static async Task<bool> ConfirmTransfer(FwApplicationConfig appConfig, FwUserSession userSession, string transferOrderId)
        {
            bool success = false;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "toggleconfirmtransfer", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, transferOrderId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                await qry.ExecuteNonQueryAsync();
                success = true;
            }

            if (success)
            {
                //using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                //{
                //    FwSqlCommand qryItems = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                //    qryItems.Add("select distinct mi.masterid, mi.warehouseid, m.class            ");
                //    qryItems.Add(" from  masteritem mi                                            ");
                //    qryItems.Add("            join master m on (mi.masterid = m.masterid)         ");
                //    qryItems.Add(" where mi.orderid = @orderid                                    ");
                //    qryItems.AddParameter("@orderid", id);
                //    FwJsonDataTable dt = await qryItems.QueryToFwJsonTableAsync();
                //
                //    foreach (List<object> row in dt.Rows)
                //    {
                //        string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                //        string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
                //        string classification = row[dt.GetColumnNo("class")].ToString();
                //        HomeControls.InventoryAvailability.InventoryAvailabilityFunc.RequestRecalc(inventoryId, warehouseId, classification);
                //    }
                //}


                // request availability recalculation on all rental and sale items
                BrowseRequest itemBrowseRequest = new BrowseRequest();
                itemBrowseRequest.uniqueids = new Dictionary<string, object>();
                itemBrowseRequest.uniqueids.Add("OrderId", transferOrderId);
                itemBrowseRequest.uniqueids.Add("NoAvailabilityCheck", true);
                itemBrowseRequest.uniqueids.Add("RecType", RwConstants.RECTYPE_RENTAL + "," + RwConstants.RECTYPE_SALE);

                OrderItemLogic itemSelector = new OrderItemLogic();
                itemSelector.SetDependencies(appConfig, userSession);
                List<OrderItemLogic> items = await itemSelector.SelectAsync<OrderItemLogic>(itemBrowseRequest);

                foreach (OrderItemLogic i in items)
                {
                    if ((!string.IsNullOrEmpty(i.InventoryId)) &&  (i.QuantityOrdered != 0))
                    {
                        if (!string.IsNullOrEmpty(i.WarehouseId))
                        {
                            InventoryAvailabilityFunc.RequestRecalc(i.InventoryId, i.WarehouseId, i.InventoryClass);
                        }
                        if (!string.IsNullOrEmpty(i.ReturnToWarehouseId))
                        {
                            InventoryAvailabilityFunc.RequestRecalc(i.InventoryId, i.ReturnToWarehouseId, i.InventoryClass);
                        }
                    }
                }
                success = true;

            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> UpdateTransferLineItemsWarehouses(FwApplicationConfig appConfig, FwUserSession userSession, string transferOrderId, string newFromWarehouseId, string newToWarehouseId, FwSqlConnection conn =  null)
        {
            bool success = false;
            BrowseRequest itemBrowseRequest = new BrowseRequest();
            itemBrowseRequest.uniqueids = new Dictionary<string, object>();
            itemBrowseRequest.uniqueids.Add("OrderId", transferOrderId);
            itemBrowseRequest.uniqueids.Add("NoAvailabilityCheck", true);

            OrderItemLogic itemSelector = new OrderItemLogic();
            itemSelector.SetDependencies(appConfig, userSession);
            List<OrderItemLogic> items = await itemSelector.SelectAsync<OrderItemLogic>(itemBrowseRequest, conn);

            foreach (OrderItemLogic iOrig in items)
            {
                OrderItemLogic iNew = iOrig.MakeCopy<OrderItemLogic>();
                iNew.SetDependencies(appConfig, userSession);
                iNew.WarehouseId = newFromWarehouseId;
                iNew.ReturnToWarehouseId = newToWarehouseId;
                int rowsAffected = await iNew.SaveAsync(original: iOrig, conn: conn);
                success = (rowsAffected > 0);
                if (!success)
                {
                    break;
                }
            }

            return success;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
