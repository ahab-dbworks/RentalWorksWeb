using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Inventory.Asset;
using WebApi.Modules.Inventory.Inventory;
using WebApi.Modules.Inventory.RentalInventory;

namespace WebApi.Modules.Utilities.InventoryUnretireUtility
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "K1yCLThxh8VX8")]
    public class InventoryUnretireUtilityController : AppDataController
    {
        public InventoryUnretireUtilityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryunretireutility/unretireinventory
        [HttpPost("unretireinventory")]
        [FwControllerMethod(Id: "le8RuhuVYGQoX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<RetireInventoryResponse>> UnretireInventory([FromBody]UnretireInventoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UnretireInventoryResponse response = await InventoryFunc.UnretireInventory(AppConfig, UserSession, request);
                return new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorypurchaseutility/validateinventory/browse
        [HttpPost("validateinventory/browse")]
        [FwControllerMethod(Id: "lx2uSeX4TLjlu", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RentalInventoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/removefromcontainer/validateitem/browse 
        [HttpPost("validateitem/browse")]
        [FwControllerMethod(Id: "vkc4pPrH74I7i", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateItemBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ItemLogic>(browseRequest);
        }
    }
}
