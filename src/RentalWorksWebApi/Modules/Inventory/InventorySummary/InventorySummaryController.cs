using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Inventory.RentalInventory;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;

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
        public async Task<ActionResult<bool>> DoNothing([FromBody] object request)
        {
            await Task.CompletedTask;
            return BadRequest(ModelState);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdateutility/validateinventory/browse
        [HttpPost("validateinventory/browse")]
        [FwControllerMethod(Id: "xoAQEoLsfqtJj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RentalInventoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdateutility/validatewarehouse/browse
        [HttpPost("validatewarehouse/browse")]
        [FwControllerMethod(Id: "ueWjSZctjW5aA", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
    }
}