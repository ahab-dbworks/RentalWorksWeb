using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Home.ContainerItem
{
    public class EmptyContainerItemResponse : TSpStatusReponse
    {
        public string InContractId = "";
    }

    public static class ContainerItemFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<EmptyContainerItemResponse> EmptyContainer(FwApplicationConfig appConfig, FwUserSession userSession, string id)
        {
            EmptyContainerItemResponse response = new EmptyContainerItemResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "emptycontainer", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@containerrentalitemid", SqlDbType.NVarChar, ParameterDirection.Input, id);
                qry.AddParameter("@deleteall", SqlDbType.NVarChar, ParameterDirection.Input, "F");
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@incontractid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.InContractId = qry.GetParameter("@incontractid").ToString();
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
