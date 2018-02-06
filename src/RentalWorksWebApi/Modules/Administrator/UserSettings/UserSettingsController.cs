using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;

namespace WebApi.Modules.Administrator.UserSettings
{
    [Route("api/v1/[controller]")]
    public class UserSettingsController : AppDataController
    {
        public UserSettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/usersettings/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)  //id = webusersid
        {
            return await DoGetAsync<UserSettingsLogic>(id, typeof(UserSettingsLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/usersettings 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]UserSettingsLogic l)
        {
            return await DoPostAsync<UserSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
    }
}