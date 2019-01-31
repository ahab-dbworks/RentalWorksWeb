using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;

namespace WebApi.Modules.Home.Receipt
{

    public static class ReceiptFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> PostGlForReceipt(FwApplicationConfig appConfig, FwUserSession userSession, string receiptId)
        {
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "postglforar", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@arid", SqlDbType.NVarChar, ParameterDirection.Input, receiptId);
                await qry.ExecuteNonQueryAsync(true);
            }
            return true;
        }
        //-------------------------------------------------------------------------------------------------------    
    }
}
