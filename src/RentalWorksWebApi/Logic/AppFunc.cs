
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;

namespace WebApi.Logic
{
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

    }

}
