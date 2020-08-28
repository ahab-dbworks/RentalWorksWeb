using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Data.SqlClient;
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

            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(appConfig.DatabaseSettings.ConnectionString);
            string dbworksConnectionString = "Server=" + connectionStringBuilder.DataSource + ";Database=" + connectionStringBuilder.InitialCatalog + ";User Id=dbworks;Password=db2424;";  // user/pass hard-coded for now

            ApplyProposedCurrenciesResponse response = new ApplyProposedCurrenciesResponse();
            using (SqlConnection conn = new SqlConnection(dbworksConnectionString))
            {
                SqlCommand qry = new SqlCommand("setmissingcurrency", conn);
                SqlParameter p1 = new SqlParameter("@status", SqlDbType.Int, 10, ParameterDirection.Output, true, 1, 1, "", DataRowVersion.Current, null);
                qry.Parameters.Add(p1);
                SqlParameter p2 = new SqlParameter("@msg", SqlDbType.NVarChar, 255, ParameterDirection.Output, true, 1, 1, "", DataRowVersion.Current, null);
                qry.Parameters.Add(p2);
                conn.Open();
                await qry.ExecuteNonQueryAsync();
                conn.Close();
                response.status = FwConvert.ToInt32(qry.Parameters[0].Value);
                response.success = (response.status == 0);
                response.msg = qry.Parameters[1].ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
