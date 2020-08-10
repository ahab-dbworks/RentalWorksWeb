using FwStandard.AppManager;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.UserProfile
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"DrTcbvvUw92V")]
    public class UserProfileController : AppDataController
    {
        public UserProfileController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(UserProfileLogic); }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/userprofile/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"P6007QSDfm4z", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<UserProfileLogic>> GetOneAsync([FromRoute]string id)  //id = webusersid
        {
            return await DoGetAsync<UserProfileLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/userprofile 
        [HttpPost]
        [FwControllerMethod(Id:"QnhjYTbz5QIs", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<UserProfileLogic>> NewAsync([FromBody]UserProfileLogic l)
        {
            return await DoNewAsync<UserProfileLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/userprofile/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "etTWNoBZMgtXZ", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<UserProfileLogic>> EditAsync([FromRoute] string id, [FromBody]UserProfileLogic l)
        {
            return await DoEditAsync<UserProfileLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
    }
}
