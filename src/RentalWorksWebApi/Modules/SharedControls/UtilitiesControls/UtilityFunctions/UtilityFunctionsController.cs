using FwStandard.AppManager;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System;
using Microsoft.AspNetCore.Http;
using WebApi.Logic;
using System.Net.Mail;
using System.Net;

namespace WebApi.Modules.UtilitiesControls.UtilityFunctions
{

    public class NewSessionIdResponse
    { 
        public string SessionId { get; set; }
    }

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id:"PNUWZqaFb8W0r")]
    public class UtilityFunctionsController : AppDataController
    {
        public UtilityFunctionsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/utilityfunctions/istraining
        [HttpGet("istraining")]
        [FwControllerMethod(Id: "axktpsOUUxgVC", ValidateSecurityGroup: false)]
        public async Task<ActionResult<bool>> GetIsTraining()
        {
            try
            {
                return new OkObjectResult(await FwSqlData.IsTraining(AppConfig.DatabaseSettings));
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/utilityfunctions/newsessionid
        [HttpGet("newsessionid")]
        [FwControllerMethod(Id:"cDT0iXnq4OCgX")]
        public async Task<ActionResult<NewSessionIdResponse>> NewSessionId()
        {
            try
            {
                NewSessionIdResponse response = new NewSessionIdResponse();
                response.SessionId = await AppFunc.GetNextIdAsync(AppConfig);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 


            // TEMPORARY

        // POST api/v1/utilityfunctions/sendmail
        [HttpPost("sendmail")]
        [FwControllerMethod(Id: "eBf4i3QqhOPAf", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<bool>> SendEmailAsync(string from, string to, string cc, string subject, string body)
        {
            var message = new MailMessage(from, to, subject, body);
            message.IsBodyHtml = true;
            string accountname = string.Empty, accountpassword = string.Empty, authtype = string.Empty, host = string.Empty, domain = "";
            int port = 25;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select top 1 *");
                    qry.Add("from emailreportcontrol with (nolock)");
                    await qry.ExecuteAsync();
                    accountname = qry.GetField("accountname").ToString().TrimEnd();
                    accountpassword = qry.GetField("accountpassword").ToString().TrimEnd();
                    authtype = qry.GetField("authtype").ToString().TrimEnd();
                    host = qry.GetField("host").ToString().TrimEnd();
                    port = qry.GetField("port").ToInt32();
                }
            }
            var client = new SmtpClient(host, port);
            client.Credentials = new NetworkCredential(accountname, accountpassword, domain);
            await client.SendMailAsync(message);
            return true;
        }

    }
}
