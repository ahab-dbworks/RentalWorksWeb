using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Agent.Project
{
    public class CreateQuoteFromProjectRequest
    {
        public string ProjectId { get; set; }
        public string OfficeLocationId { get; set; }
    }

    public class CreateQuoteFromProjectResponse : TSpStatusResponse
    {
        public string QuoteId { get; set; }
    }

    public static class ProjectFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<CreateQuoteFromProjectResponse> CreateQuoteFromProject(FwApplicationConfig appConfig, FwUserSession userSession, CreateQuoteFromProjectRequest request)
        {
            CreateQuoteFromProjectResponse response = new CreateQuoteFromProjectResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "leadtoquote2", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@leadid", SqlDbType.NVarChar, ParameterDirection.Input, request.ProjectId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@locationid", SqlDbType.NVarChar, ParameterDirection.Input, request.OfficeLocationId);
                qry.AddParameter("@quoteid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.QuoteId = qry.GetParameter("@quoteid").ToString();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
