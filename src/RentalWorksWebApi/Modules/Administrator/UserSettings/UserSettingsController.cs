using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;

namespace WebApi.Modules.Administrator.UserSettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    public class UserSettingsController : AppDataController
    {
        public UserSettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(UserSettingsLogic); }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/usersettings/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<UserSettingsLogic>> GetOneAsync([FromRoute]string id)  //id = webusersid
        {
            return await DoGetAsync<UserSettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/usersettings 
        [HttpPost]
        public async Task<ActionResult<UserSettingsLogic>> PostAsync([FromBody]UserSettingsLogic l)
        {
            return await DoPostAsync<UserSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
    }
}