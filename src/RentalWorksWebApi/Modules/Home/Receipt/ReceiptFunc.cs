using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;

namespace WebApi.Modules.Home.Receipt
{

    public static class ReceiptFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> PostGlForReceipt(FwApplicationConfig appConfig, FwUserSession userSession, string receiptId, FwSqlConnection conn = null)
        {
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "postglforar", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@arid", SqlDbType.NVarChar, ParameterDirection.Input, receiptId);
            await qry.ExecuteNonQueryAsync();
            return true;
        }
        //-------------------------------------------------------------------------------------------------------    
        public static async Task<bool> DeleteReceipt(FwApplicationConfig appConfig, FwUserSession userSession, string receiptId, FwSqlConnection conn = null)
        {
            bool success = false;
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "arcancel", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@arid", SqlDbType.NVarChar, ParameterDirection.Input, receiptId);
            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
            qry.AddParameter("@success", SqlDbType.NVarChar, ParameterDirection.Output);
            await qry.ExecuteNonQueryAsync();
            success = FwConvert.ToBoolean(qry.GetParameter("@success").ToString());
            return success;
        }
        //-------------------------------------------------------------------------------------------------------    
    }
}
