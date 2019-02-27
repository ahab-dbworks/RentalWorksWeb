using FwStandard.AppManager;
﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Settings.UserDashboardSettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"0DhKjUvwylglQ")]
    public class UserDashboardSettingsController : AppDataController
    {
        public UserDashboardSettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(UserDashboardSettingsLogic); }
        //------------------------------------------------------------------------------------
        // GET api/v1/userdashboardsettings/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"c79UfgZ1Jat5a")]
        public async Task<ActionResult<UserDashboardSettingsLogic>> GetOneAsync([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UserDashboardSettingsLogic l = new UserDashboardSettingsLogic();
                l.SetDependencies(this.AppConfig, this.UserSession);
                await l.LoadAsync<UserDashboardSettingsLogic>(new object[] { id });
                return new OkObjectResult(l);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/userdashboardsettings 
        [HttpPost]
        [FwControllerMethod(Id:"c9DKCtKZ05H2T")]
        public async Task<ActionResult<UserDashboardSettingsLogic>> PostAsync([FromBody]UserDashboardSettingsLogic l)
        {
            l.SetDependencies(this.AppConfig, this.UserSession);
            return await DoPostAsync<UserDashboardSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
    }
}
