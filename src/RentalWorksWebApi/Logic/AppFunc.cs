
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Modules.Home.OrderItem;

namespace WebApi.Logic
{

    public class TSpStatusReponse
    {
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
        public static async Task<string> GetNextCounterAsync(FwApplicationConfig appConfig, FwUserSession userSession, string moduleName)
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
    }
}
