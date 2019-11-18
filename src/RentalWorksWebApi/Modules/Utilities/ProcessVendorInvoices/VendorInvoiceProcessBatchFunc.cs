using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;

namespace WebApi.Modules.Utilities.VendorInvoiceProcessBatch
{
    public static class VendorInvoiceProcessBatchFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<VendorInvoiceProcessBatchResponse> CreateBatch(FwApplicationConfig appConfig, FwUserSession userSession, VendorInvoiceProcessBatchRequest request)
        {
            VendorInvoiceProcessBatchResponse response = new VendorInvoiceProcessBatchResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "createvendorinvoicechargebatch", appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@chgbatchid", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.BatchId = qry.GetParameter("@chgbatchid").ToString();
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
