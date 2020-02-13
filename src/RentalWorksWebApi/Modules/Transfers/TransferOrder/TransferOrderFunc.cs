﻿using FwStandard.Models;
using FwStandard.SqlServer;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

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
        public static async Task<bool> ConfirmTransfer(FwApplicationConfig appConfig, FwUserSession userSession, string id)
        {
            bool success = false;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "toggleconfirmtransfer", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, id);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                await qry.ExecuteNonQueryAsync();
                success = true;
            }

            if (success)
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qryItems = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                    qryItems.Add("select distinct mi.masterid, mi.warehouseid, m.class            ");
                    qryItems.Add(" from  masteritem mi                                            ");
                    qryItems.Add("            join master m on (mi.masterid = m.masterid)         ");
                    qryItems.Add(" where mi.orderid = @orderid                                    ");
                    qryItems.AddParameter("@orderid", id);
                    FwJsonDataTable dt = await qryItems.QueryToFwJsonTableAsync();

                    foreach (List<object> row in dt.Rows)
                    {
                        string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                        string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
                        string classification = row[dt.GetColumnNo("class")].ToString();
                        HomeControls.InventoryAvailability.InventoryAvailabilityFunc.RequestRecalc(inventoryId, warehouseId, classification);
                    }
                }

            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
