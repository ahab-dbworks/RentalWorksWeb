using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Home.InventoryCompleteKit
{
    public class CreateCompleteResponse
    {
        public string PackageId;
    }
    public static class InventoryCompleteKitFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<CreateCompleteResponse> CreateComplete(FwApplicationConfig appConfig, FwUserSession userSession, string id)
        {
            CreateCompleteResponse response = new CreateCompleteResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "createcompletefromitem", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, id);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@packageid", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.PackageId = qry.GetParameter("@packageid").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
