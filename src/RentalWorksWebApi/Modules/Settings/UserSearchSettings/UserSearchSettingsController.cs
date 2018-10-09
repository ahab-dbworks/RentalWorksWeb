using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Settings.UserSearchSettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class UserSearchSettingsController : AppDataController
    {
        public UserSearchSettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(UserSearchSettingsLogic); }
        //------------------------------------------------------------------------------------
        // GET api/v1/usersearchsettings/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UserSearchSettingsLogic l = new UserSearchSettingsLogic();
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
        // POST api/v1/usersearchsettings 
        [HttpPost]
        public async Task<ActionResult<UserSearchSettingsLogic>> PostAsync([FromBody]UserSearchSettingsLogic l)
        {
            l.SetDbConfig(this.AppConfig.DatabaseSettings);
            l.SetDependencies(this.AppConfig, this.UserSession);
            return await DoPostAsync<UserSearchSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
    }
}