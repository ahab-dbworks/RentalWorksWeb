using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Inventory.InventorySummary
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home")]
    [FwController(Id: "84eSG3zrmtitY")]
    public class InventorySummaryController : AppDataController
    {
        public InventorySummaryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorysummary/donothing
        [HttpPost("donothing")]
        [FwControllerMethod(Id: "Wr0oIn9yVDXI1", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<bool>> DoNothing([FromBody]object request)
        {
            await Task.CompletedTask;
            return BadRequest(ModelState);
        }
        //------------------------------------------------------------------------------------
    }
}