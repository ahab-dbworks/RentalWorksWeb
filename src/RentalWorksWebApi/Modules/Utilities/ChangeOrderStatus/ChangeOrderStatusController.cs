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

namespace WebApi.Modules.Utilities.ChangeOrderStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "SjkAsallYxwNq")]
    public class ChangeOrderStatusController : AppDataController
    {
        public ChangeOrderStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/changeorderstatus/changestatus
        [HttpPost("changestatus")]
        [FwControllerMethod(Id: "wZzG1jIURw0Bv", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<ChangeOrderStatusResponse>> RetireInventory([FromBody]ChangeOrderStatusRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ChangeOrderStatusResponse response = await InventoryFunc.ChangeStatus(AppConfig, UserSession, request);
                return new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/changeorderstatus/validateinventory/browse
        [HttpPost("validateinventory/browse")]
        [FwControllerMethod(Id: "lDAXjrmLSzVnX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RentalInventoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/removefromcontainer/validateitem/browse 
        [HttpPost("validateitem/browse")]
        [FwControllerMethod(Id: "iZtnA96ku0t16", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateItemBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ItemLogic>(browseRequest);
        }
    }
}
