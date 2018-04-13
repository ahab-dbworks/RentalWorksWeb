using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Settings.UserDashboardSettings
{
    [Route("api/v1/[controller]")]
    public class UserDashboardSettingsController : AppDataController
    {
        public UserDashboardSettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // GET api/v1/userdashboardsettings/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UserDashboardSettingsLogic l = new UserDashboardSettingsLogic();
                l.SetDbConfig(this.AppConfig.DatabaseSettings);
                l.SetDependencies(this.AppConfig, this.UserSession);
                await l.LoadAsync(id);
                return new OkObjectResult(l);
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
        // POST api/v1/userdashboardsettings 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]UserDashboardSettingsLogic l)
        {
            l.SetDbConfig(this.AppConfig.DatabaseSettings);
            l.SetDependencies(this.AppConfig, this.UserSession);
            return await DoPostAsync<UserDashboardSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
    }
}