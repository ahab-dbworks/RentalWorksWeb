using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;

namespace WebApi.Modules.Billing.Payment
{

    public static class PaymentFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> PostGlForPayment(FwApplicationConfig appConfig, FwUserSession userSession, string paymentId, FwSqlConnection conn = null)
        {
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "postglforpayment", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@paymentid", SqlDbType.NVarChar, ParameterDirection.Input, paymentId);
            await qry.ExecuteNonQueryAsync();
            return true;
        }
        //-------------------------------------------------------------------------------------------------------    
    }
}
