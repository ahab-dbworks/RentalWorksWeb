using FwStandard.SqlServer;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.AccountServices.Account
{
    public class AccountLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------------------------
        public async Task<ResetPasswordResponse> ValidatePassword(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new ResetPasswordResponse();

            //using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            //{
            //    using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
            //    {
            //        TODO: Load password security settings and validate password matches the complexity
            //    }
            //}

            response.Status = 0;
            return response;
        }
        //------------------------------------------------------------------------------------------------------
        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new ResetPasswordResponse();

            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("update users");
                    qry.Add("   set password = dbo.encrypt(@password), mustchangepwflg = 'F', pwupdated = @today");
                    qry.Add(" where usersid = @usersid");
                    qry.AddParameter("@password", request.Password);
                    qry.AddParameter("@today",    FwDateTime.Now.GetSqlDate());
                    qry.AddParameter("@usersid",  this.UserSession.UsersId);
                    await qry.ExecuteAsync();
                    response.Status = 0;
                }
            }

            return response;
        }
        //------------------------------------------------------------------------------------------------------
    }
}
