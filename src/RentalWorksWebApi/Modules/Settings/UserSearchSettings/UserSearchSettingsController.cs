using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.UserSearchSettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class UserSearchSettingsController : AppDataController
    {
        public UserSearchSettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(UserSearchSettingsLogic); }
        //------------------------------------------------------------------------------------
        // GET api/v1/usersearchsettings/A0000001  (id = webusersid)
        [HttpGet("{id}")]
        public async Task<ActionResult<UserSearchSettingsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<UserSearchSettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/usersearchsettings 
        [HttpPost]
        public async Task<ActionResult<UserSearchSettingsLogic>> PostAsync([FromBody]UserSearchSettingsLogic l)
        {
            return await DoPostAsync<UserSearchSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
    }
}