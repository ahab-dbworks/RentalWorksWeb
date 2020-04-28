using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Utilities.InventorySequenceUtility
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "NY5nvYtS0WnEj")]
    public class InventorySequenceUtilityController : AppDataController
    {
        public InventorySequenceUtilityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorysequenceutility/donothing
        [HttpPost("donothing")]
        [FwControllerMethod(Id: "6s16oYeo4BzHI", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<bool>> DoNothing([FromBody]object request)
        {
            return BadRequest(ModelState);
        }
        //------------------------------------------------------------------------------------
    }
}