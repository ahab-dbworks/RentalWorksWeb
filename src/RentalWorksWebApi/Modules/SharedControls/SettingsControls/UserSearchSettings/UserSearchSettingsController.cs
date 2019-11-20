using FwStandard.AppManager;
﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.UserSearchSettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"KBVTR3OwqYz0x")]
    public class UserSearchSettingsController : AppDataController
    {
        public UserSearchSettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(UserSearchSettingsLogic); return404IfGetNotFound = false; }
        //------------------------------------------------------------------------------------
        // GET api/v1/usersearchsettings/A0000001  (id = webusersid)
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"4NbXXJMhBCvXA", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<UserSearchSettingsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<UserSearchSettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/usersearchsettings 
        [HttpPost]
        [FwControllerMethod(Id:"F0ocsO9lqX1lP", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<UserSearchSettingsLogic>> NewAsync([FromBody]UserSearchSettingsLogic l)
        {
            return await DoNewAsync<UserSearchSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/usersearchsettings/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "QJjEcnRd7LCSr", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<UserSearchSettingsLogic>> EditAsync([FromRoute] string id, [FromBody]UserSearchSettingsLogic l)
        {
            return await DoEditAsync<UserSearchSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
    }
}
