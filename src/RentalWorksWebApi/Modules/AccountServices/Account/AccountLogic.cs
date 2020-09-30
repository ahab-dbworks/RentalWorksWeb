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
            await Task.CompletedTask;
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
                    qry.AddParameter("@password", request.Password.ToUpper());
                    qry.AddParameter("@today",    FwDateTime.Now.GetSqlDate());
                    qry.AddParameter("@usersid",  this.UserSession.UsersId);
                    await qry.ExecuteAsync();
                    response.Status = 0;
                }

                using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("update webusers");
                    qry.Add("   set webpassword = dbo.encrypt(@password)");
                    qry.Add(" where webusersid = @webusersid");
                    qry.AddParameter("@password",   request.Password.ToUpper());
                    qry.AddParameter("@webusersid", this.UserSession.WebUsersId);
                    await qry.ExecuteAsync();
                    response.Status = 0;
                }
            }

            return response;
        }
        //------------------------------------------------------------------------------------------------------
        public async Task<SystemSettingsResponse> GetSystemSettingsAsync()
        {
            SystemSettingsResponse response = new SystemSettingsResponse();

            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select quikscanstagebysession = dbo.quikscanstagebysession()");
                    await qry.ExecuteAsync();
                    response.QuikScanStageBySession = qry.GetField("quikscanstagebysession").ToBoolean();
                }

                using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddColumn("barcodeskip", false);
                    qry.Add("select barcodeskip");
                    qry.Add("from  barcodeskip with (nolock)");
                    response.BarcodeSkipPrefixes = await qry.QueryToFwJsonTableAsync();
                }
            }

            return response;
        }
        //------------------------------------------------------------------------------------------------------
    }
}
