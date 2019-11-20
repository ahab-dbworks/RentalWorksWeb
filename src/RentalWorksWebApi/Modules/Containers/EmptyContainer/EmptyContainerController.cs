using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using WebApi.Modules.HomeControls.ContainerItem;

namespace WebApi.Modules.Containers.EmptyContainer
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"tfzkGtEIPTFr")]
    public class EmptyContainerController : AppDataController
    {
        public EmptyContainerController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/emptycontainer/validatecontaineritem/browse 
        [HttpPost("validatecontaineritem/browse")]
        [FwControllerMethod(Id: "TtPJnA2uQJap", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContainerItemLogic>(browseRequest);
        }
    }
}
