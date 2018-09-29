using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System;
using Microsoft.AspNetCore.Http;
using WebApi.Logic;

namespace WebApi.Modules.Utilities.UtilityFunctions
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    public class UtilityFunctionsController : AppDataController
    {
        public UtilityFunctionsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/utilityfunctions/newsessionid
        [HttpGet("newsessionid")]
        public async Task<ActionResult<string>> NewSessionId()
        {
            try
            {
                return new OkObjectResult(await AppFunc.GetNextIdAsync(AppConfig));
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}