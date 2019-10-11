using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;

namespace WebApi.Modules.Transfers.TransferOrder
{
    public static class TransferOrderFunc
    {
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
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
