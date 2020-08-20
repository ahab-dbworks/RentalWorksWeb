using FwStandard.Models;
using FwStandard.SqlServer;
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
                FwSqlCommand qry = new FwSqlCommand(conn, "xx", appConfig.DatabaseSettings.QueryTimeout);
                //qry.AddParameter("@code", SqlDbType.NVarChar, ParameterDirection.Input, request.Code);
                await qry.ExecuteNonQueryAsync();
                response.success = true;
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
