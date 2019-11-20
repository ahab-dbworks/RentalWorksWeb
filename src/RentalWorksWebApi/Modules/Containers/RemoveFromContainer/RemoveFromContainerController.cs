using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using WebApi.Modules.Inventory.Asset;
using WebApi.Modules.HomeControls.ContainerItem;
using WebApi.Modules.Inventory.RentalInventory;

namespace WebApi.Modules.Containers.RemoveFromContainer
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"J9BTE3hOYuEd")]
    public class RemoveFromContainerController : AppDataController
    {
        public RemoveFromContainerController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        //------------------------------------------------------------------------------------ 
        // POST api/v1/removefromcontainer/validatecontaineritem/browse 
        [HttpPost("validatecontaineritem/browse")]
        [FwControllerMethod(Id: "f2tTEP1S0YVc", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateContainerItemBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContainerItemLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/removefromcontainer/validateitem/browse 
        [HttpPost("validateitem/browse")]
        [FwControllerMethod(Id: "x9gqP0pfKBmG", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateItemBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ItemLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/removefromcontainer/validateinventory/browse 
        [HttpPost("validateinventory/browse")]
        [FwControllerMethod(Id: "9HTT6E2zTHn9", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RentalInventoryLogic>(browseRequest);
        }
    }
    
}
