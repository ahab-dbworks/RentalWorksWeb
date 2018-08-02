
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Modules.Home.OrderItem;
using WebApi.Modules.Home.PurchaseOrderReceiveItem;
using WebApi.Modules.Home.PurchaseOrderReturnItem;
using WebApi.Modules.Home.RepairPart;

namespace WebApi.Logic
{

    public class TSpStatusReponse
    {
        public int status;
        public bool success;
        public string msg;
    }

    public static class AppFunc
    {

        //-------------------------------------------------------------------------------------------------------
        public static async Task<string> GetNextIdAsync(FwApplicationConfig appConfig)
        {
            string id = "";
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                id = await FwSqlData.GetNextIdAsync(conn, appConfig.DatabaseSettings);
            }
            return id;
        }
        //-------------------------------------------------------------------------------------------------------
        static public async Task<string> EncryptAsync(FwApplicationConfig appConfig, string data)
        {
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select value = dbo.encrypt(@data)");
                    qry.AddParameter("@data", data);
                    await qry.ExecuteAsync();
                    string value = qry.GetField("value").ToString().Trim();
                    return value;
                }
            }
        }
        //-----------------------------------------------------------------------------
        public static async Task<FwDatabaseField[]> GetDataAsync(FwApplicationConfig appConfig, string tablename, string[] wherecolumns, string[] wherecolumnvalues, string[] selectcolumns)
        {
            FwDatabaseField[] results;

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select top 1 ");
                for (int c = 0; c < selectcolumns.Length; c++)
                {
                    qry.Add(selectcolumns[c]);
                    if (c < (selectcolumns.Length - 1))
                    {
                        qry.Add(",");
                    }
                }
                qry.Add("from " + tablename + " with (nolock)");

                qry.Add("where ");
                for (int c = 0; c < wherecolumns.Length; c++)
                {
                    qry.Add(wherecolumns[c] + " = @wherecolumnvalue" + c.ToString());
                    if (c < (wherecolumns.Length - 1))
                    {
                        qry.Add(" and ");
                    }
                }

                for (int c = 0; c < wherecolumnvalues.Length; c++)
                {
                    qry.AddParameter("@wherecolumnvalue" + c.ToString(), wherecolumnvalues[c]);
                }

                await qry.ExecuteAsync();
                results = new FwDatabaseField[selectcolumns.Length];  // array of nulls

                if (qry.RowCount == 1)
                {
                    for (int c = 0; c < selectcolumns.Length; c++)
                    {
                        results[c] = qry.GetField(selectcolumns[c]);
                    }
                }
            }

            return results;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string[]> GetStringDataAsync(FwApplicationConfig appConfig, string tablename, string[] wherecolumns, string[] wherecolumnvalues, string[] selectcolumns)
        {
            FwDatabaseField[] results = await GetDataAsync(appConfig, tablename, wherecolumns, wherecolumnvalues, selectcolumns);
            string[] resultsStr = new string[results.Length];

            for (int c = 0; c < selectcolumns.Length; c++)
            {
                resultsStr[c] = (results[c] != null) ? results[c].ToString().TrimEnd() : string.Empty;
            }
            return resultsStr;
        }
        //-----------------------------------------------------------------------------
        public static async Task<FwDatabaseField> GetDataAsync(FwApplicationConfig appConfig, string tablename, string wherecolumn, string wherecolumnvalue, string selectcolumn)
        {
            FwDatabaseField result;

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select top 1 " + selectcolumn);
                qry.Add("from " + tablename + " with (nolock)");
                qry.Add("where " + wherecolumn + " = @wherecolumnvalue");
                qry.AddParameter("@wherecolumnvalue", wherecolumnvalue);
                await qry.ExecuteAsync();
                result = (qry.RowCount == 1) ? qry.GetField(selectcolumn) : null;
            }

            return result;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string> GetStringDataAsync(FwApplicationConfig appConfig, string tablename, string wherecolumn, string wherecolumnvalue, string selectcolumn)
        {
            FwDatabaseField field;
            string result = string.Empty;

            field = await GetDataAsync(appConfig, tablename, wherecolumn, wherecolumnvalue, selectcolumn);
            result = (field != null) ? field.ToString().TrimEnd() : string.Empty;

            return result;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<int> GetIntDataAsync(FwApplicationConfig appConfig, string tablename, string wherecolumn, string wherecolumnvalue, string selectcolumn)
        {
            FwDatabaseField field;
            int result = 0;

            field = await GetDataAsync(appConfig, tablename, wherecolumn, wherecolumnvalue, selectcolumn);
            result = ((field != null) ? field.ToInt32() : 0);

            return result;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string> GetNextSystemCounterAsync(FwApplicationConfig appConfig, FwUserSession userSession, string counterColumnName)
        {
            string counter = "";
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "fw_getnextcounter", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@tablename", SqlDbType.NVarChar, ParameterDirection.Input, "syscontrol");
                qry.AddParameter("@columnname", SqlDbType.NVarChar, ParameterDirection.Input, counterColumnName);
                qry.AddParameter("@uniqueid1name", SqlDbType.NVarChar, ParameterDirection.Input, "controlid");
                qry.AddParameter("@uniqueid1valuestr", SqlDbType.NVarChar, ParameterDirection.Input, "1");
                qry.AddParameter("@counter", SqlDbType.Int, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                counter = qry.GetParameter("@counter").ToString().TrimEnd();
            }
            return counter;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string> GetNextModuleCounterAsync(FwApplicationConfig appConfig, FwUserSession userSession, string moduleName)
        {
            string counter = "";
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "getnextcounter", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@module", SqlDbType.NVarChar, ParameterDirection.Input, moduleName);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@newcounter", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                counter = qry.GetParameter("@newcounter").ToString().TrimEnd();
            }
            return counter;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> SaveNoteASync(FwApplicationConfig appConfig, FwUserSession userSession, string uniqueId1, string uniqueId2, string uniqueId3, string note)
        {
            bool saved = false;
            if (note != null)
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "updateappnote", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@uniqueid1", SqlDbType.NVarChar, ParameterDirection.Input, uniqueId1);
                    qry.AddParameter("@uniqueid2", SqlDbType.NVarChar, ParameterDirection.Input, uniqueId2);
                    qry.AddParameter("@uniqueid3", SqlDbType.NVarChar, ParameterDirection.Input, uniqueId3);
                    qry.AddParameter("@note", SqlDbType.NVarChar, ParameterDirection.Input, note);
                    await qry.ExecuteNonQueryAsync(true);
                    saved = true;
                }
            }
            return saved;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> UpdateTaxFromTaxOptionASync(FwApplicationConfig appConfig, FwUserSession userSession, string taxOptionId, string taxId)
        {
            bool saved = false;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "updatetaxfromtaxoption", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@taxoptionid", SqlDbType.NVarChar, ParameterDirection.Input, taxOptionId);
                qry.AddParameter("@taxid", SqlDbType.NVarChar, ParameterDirection.Input, taxId);
                await qry.ExecuteNonQueryAsync(true);
                saved = true;
            }
            return saved;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string> GetDepartmentLocation(FwApplicationConfig appConfig, FwUserSession userSession, string departmentId, string locationId, string fieldName)
        {
            string str = "";
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select " + fieldName);
                qry.Add(" from  deptloc dl ");
                qry.Add(" where dl.departmentid = @departmentid");
                qry.Add(" and   dl.locationid = @locationid");
                qry.AddParameter("@departmentid", departmentId);
                qry.AddParameter("@locationid", locationId);
                qry.AddColumn(fieldName);
                FwJsonDataTable table = await qry.QueryToFwJsonTableAsync(true);
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    str = table.Rows[0][0].ToString();
                }
            }
            return str;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string> GetLocation(FwApplicationConfig appConfig, FwUserSession userSession, string locationId, string fieldName)
        {
            string str = "";
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select " + fieldName);
                qry.Add(" from  location l ");
                qry.Add(" where l.locationid = @locationid");
                qry.AddParameter("@locationid", locationId);
                qry.AddColumn(fieldName);
                FwJsonDataTable table = await qry.QueryToFwJsonTableAsync(true);
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    str = table.Rows[0][0].ToString();
                }
            }
            return str;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TSpStatusReponse> ToggleRepairEstimate(FwApplicationConfig appConfig, FwUserSession userSession, string repairId)
        {
            TSpStatusReponse response = new TSpStatusReponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "togglerepairestimated", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@repairid", SqlDbType.NVarChar, ParameterDirection.Input, repairId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TSpStatusReponse> ToggleRepairComplete(FwApplicationConfig appConfig, FwUserSession userSession, string repairId)
        {
            TSpStatusReponse response = new TSpStatusReponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "togglerepaircomplete", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@repairid", SqlDbType.NVarChar, ParameterDirection.Input, repairId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TSpStatusReponse> ReleaseRepairItems(FwApplicationConfig appConfig, FwUserSession userSession, string repairId, int quantity)
        {
            TSpStatusReponse response = new TSpStatusReponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "releaserepairitems", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@repairid", SqlDbType.NVarChar, ParameterDirection.Input, repairId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, quantity);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TSpStatusReponse> VoidRepair(FwApplicationConfig appConfig, FwUserSession userSession, string repairId)
        {
            TSpStatusReponse response = new TSpStatusReponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "voidrepair", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@repairid", SqlDbType.NVarChar, ParameterDirection.Input, repairId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                //qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                //qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.success = true;
                response.msg = "";
                //response.success = (qry.GetParameter("@status").ToInt32() == 0);
                //response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string> InsertPackage(FwApplicationConfig appConfig, FwUserSession userSession, OrderItemLogic oi)
        {
            string newOrderItemId = "";
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "insertpackage", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, oi.OrderId);
                qry.AddParameter("@packageid", SqlDbType.NVarChar, ParameterDirection.Input, oi.InventoryId);
                qry.AddParameter("@nestedmasteritemid", SqlDbType.NVarChar, ParameterDirection.Input, "");
                qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, oi.WarehouseId);
                qry.AddParameter("@catalogid", SqlDbType.NVarChar, ParameterDirection.Input, "");
                qry.AddParameter("@rectype", SqlDbType.NVarChar, ParameterDirection.Input, oi.RecType);
                qry.AddParameter("@qty", SqlDbType.NVarChar, ParameterDirection.Input, oi.QuantityOrdered);
                qry.AddParameter("@docheckoutaudit", SqlDbType.NVarChar, ParameterDirection.Input, "F");
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@forcenewrecord", SqlDbType.NVarChar, ParameterDirection.Input, "T");
                qry.AddParameter("@primarymasteritemid", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                newOrderItemId = qry.GetParameter("@primarymasteritemid").ToString();
            }
            return newOrderItemId;
        }
        //-------------------------------------------------------------------------------------------------------

        public static async Task<bool> UpdatePackageQuantities(FwApplicationConfig appConfig, FwUserSession userSession, OrderItemLogic oi)
        {
            bool success = false;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "updatepackageqtys", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, oi.OrderId);
                qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, oi.OrderItemId);
                qry.AddParameter("@newqty", SqlDbType.NVarChar, ParameterDirection.Input, oi.QuantityOrdered);
                qry.AddParameter("@docheckoutaudit", SqlDbType.NVarChar, ParameterDirection.Input, "F");
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@rowsummarized", SqlDbType.NVarChar, ParameterDirection.Input, "F");
                await qry.ExecuteNonQueryAsync(true);
                success = true;
            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string> InsertRepairPackage(FwApplicationConfig appConfig, FwUserSession userSession, RepairPartLogic rpi)
        {
            string newRepairPartId = "";
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "insertrepairpackage", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@repairid", SqlDbType.NVarChar, ParameterDirection.Input, rpi.RepairId);
                qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, rpi.InventoryId);
                qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, rpi.WarehouseId);
                qry.AddParameter("@catalogid", SqlDbType.NVarChar, ParameterDirection.Input, "");
                qry.AddParameter("@packagetype", SqlDbType.NVarChar, ParameterDirection.Input, rpi.ItemClass);
                qry.AddParameter("@qty", SqlDbType.NVarChar, ParameterDirection.Input, rpi.Quantity);
                qry.AddParameter("@primaryrepairpartid", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                newRepairPartId = qry.GetParameter("@primaryrepairpartid").ToString();
            }
            return newRepairPartId;
        }
        //-------------------------------------------------------------------------------------------------------

        public static async Task<ReceiveItemResponse> ReceiveItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId, string purchaseOrderItemId, int quantity)
        {
            ReceiveItemResponse response = new ReceiveItemResponse();
            response.ContractId = contractId;
            response.PurchaseOrderId = purchaseOrderId;
            response.PurchaseOrderItemId = purchaseOrderItemId;
            response.Quantity = quantity;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "receiveitem", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderId);
                qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderItemId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@barcode", SqlDbType.NVarChar, ParameterDirection.Input, "");
                qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, quantity);
                qry.AddParameter("@qtyordered", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@qtyreceived", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@qtyneedbarcode", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@qtycolor", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.QuantityOrdered = qry.GetParameter("@qtyordered").ToDouble();
                response.QuantityReceived = qry.GetParameter("@qtyreceived").ToDouble();
                response.QuantityNeedBarCode = qry.GetParameter("@qtyneedbarcode").ToDouble();
                if (!qry.GetParameter("@qtycolor").IsDbNull())
                {
                    response.QuantityColor = FwConvert.OleColorToHtmlColor(qry.GetParameter("@qtycolor").ToInt32());
                }
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<SelectAllNoneReceiveItemResponse> SelectAllNoneReceiveItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId, bool selectAll)
        {
            SelectAllNoneReceiveItemResponse response = new SelectAllNoneReceiveItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "selectallreceivefromvendor", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@poid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderId);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@selectallnone", SqlDbType.NVarChar, ParameterDirection.Input, (selectAll ? "A" : "N"));
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNoneReceiveItemResponse> SelectAllReceiveItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId)
        {
            return await SelectAllNoneReceiveItem(appConfig, userSession, contractId, purchaseOrderId, true);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNoneReceiveItemResponse> SelectNoneReceiveItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId)
        {
            return await SelectAllNoneReceiveItem(appConfig, userSession, contractId, purchaseOrderId, false);
        }
        //-------------------------------------------------------------------------------------------------------


        public static async Task<ReturnItemResponse> ReturnItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId, string purchaseOrderItemId, int quantity)
        {
            ReturnItemResponse response = new ReturnItemResponse();
            response.ContractId = contractId;
            response.PurchaseOrderId = purchaseOrderId;
            response.PurchaseOrderItemId = purchaseOrderItemId;
            response.Quantity = quantity;
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "returnitem", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderId);
                qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderItemId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@barcode", SqlDbType.NVarChar, ParameterDirection.Input, "");
                qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, quantity);
                qry.AddParameter("@qtyordered", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@qtyreceived", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@qtyreturned", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.QuantityOrdered = qry.GetParameter("@qtyordered").ToDouble();
                response.QuantityReceived = qry.GetParameter("@qtyreceived").ToDouble();
                response.QuantityReturned = qry.GetParameter("@qtyreturned").ToDouble();
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<SelectAllNoneReturnItemResponse> SelectAllNoneReturnItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId, bool selectAll)
        {
            SelectAllNoneReturnItemResponse response = new SelectAllNoneReturnItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "selectallreturntovendor", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@poid", SqlDbType.NVarChar, ParameterDirection.Input, purchaseOrderId);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@selectallnone", SqlDbType.NVarChar, ParameterDirection.Input, (selectAll ? "A" : "N"));
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNoneReturnItemResponse> SelectAllReturnItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId)
        {
            return await SelectAllNoneReturnItem(appConfig, userSession, contractId, purchaseOrderId, true);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SelectAllNoneReturnItemResponse> SelectNoneReturnItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string purchaseOrderId)
        {
            return await SelectAllNoneReturnItem(appConfig, userSession, contractId, purchaseOrderId, false);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TSpStatusReponse> AssignContract(FwApplicationConfig appConfig, FwUserSession userSession, string contractId)
        {
            TSpStatusReponse response = new TSpStatusReponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "assigncontract", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                //qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                //qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                //qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                //response.success = (qry.GetParameter("@status").ToInt32() == 0);
                //response.msg = qry.GetParameter("@msg").ToString();
                response.success = true;
                response.msg = "";
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<List<string>> CreateOutContractsFromReceive(FwApplicationConfig appConfig, FwUserSession userSession, string receiveContractId)
        {
            List<string> contractIds = new List<string>();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                string[] outContractIds = new string[0];
                FwSqlCommand qry = new FwSqlCommand(conn, "createoutcontractsfromreceive", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@receivecontractid", SqlDbType.NVarChar, ParameterDirection.Input, receiveContractId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@outcontractids", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                outContractIds = qry.GetParameter("@outcontractids").ToString().Split(',');
                foreach (string outContractId in outContractIds)
                {
                    contractIds.Add(outContractId);
                }
            }
            return contractIds;
        }
        //-------------------------------------------------------------------------------------------------------            

    }
}
