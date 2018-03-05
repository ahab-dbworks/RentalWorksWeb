using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;
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
                Dictionary<string, object> uniqueIds = new Dictionary<string, object>();
                uniqueIds.Add("WebUsersId", id);

                BrowseRequest request = new BrowseRequest();
                request.uniqueids = uniqueIds;

                UserDashboardSettingsLogic l = new UserDashboardSettingsLogic();
                l.SetDbConfig(_appConfig.DatabaseSettings);
                IEnumerable<UserDashboardSettingsLogic> records = await l.SelectAsync<UserDashboardSettingsLogic>(request);
                return new OkObjectResult(records);
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