using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Utilities.CurrencyMissingUtility
{
    public class ApplyProposedCurrenciesRequest { }

    public class ApplyProposedCurrenciesResponse : TSpStatusResponse { }

    public static class CurrencyMissingUtilityFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ApplyProposedCurrenciesResponse> ApplyProposedCurrencies(FwApplicationConfig appConfig, FwUserSession userSession, ApplyProposedCurrenciesRequest request)
        {
            ApplyProposedCurrenciesResponse response = new ApplyProposedCurrenciesResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "setmissingcurrency", appConfig.DatabaseSettings.QueryTimeout);
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
    }
}
