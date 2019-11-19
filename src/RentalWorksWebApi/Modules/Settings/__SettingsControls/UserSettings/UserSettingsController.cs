using FwStandard.AppManager;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using System;

namespace WebApi.Modules.Settings.UserSettings.UserSettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"xCMUamQmbO0yh")]
    public class UserSettingsController : AppDataController
    {
        public UserSettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(UserSettingsLogic); }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/usersettings/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"dODYJZuvVxum5", ValidateSecurityGroup: false)]
        public async Task<ActionResult<UserSettingsLogic>> GetOneAsync([FromRoute]string id)  //id = webusersid
        {
            return await DoGetAsync<UserSettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/usersettings 
        [HttpPost]
        [FwControllerMethod(Id: "nuWsJOCX1jpvm", ActionType: FwControllerActionTypes.Save)]
        public async Task<ActionResult<UserSettingsLogic>> PostAsync([FromBody]UserSettingsLogic l)
        {
            //return await DoPostAsync<UserSettingsLogic>(l);
            if (l.UserId.Equals(UserSession.WebUsersId)) 
            {
                return await DoPostAsync<UserSettingsLogic>(l);
            }
            else
            {
                return GetApiExceptionResult(new Exception("User can only modify own settings here."));
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
