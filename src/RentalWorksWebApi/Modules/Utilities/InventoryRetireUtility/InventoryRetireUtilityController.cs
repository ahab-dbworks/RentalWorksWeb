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

namespace WebApi.Modules.Utilities.InventoryRetireUtility
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "KIfiUkxPPwRBr")]
    public class ProcessPaymentController : AppDataController
    {
        public ProcessPaymentController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryretireutility/retireinventory
        [HttpPost("retireinventory")]
        [FwControllerMethod(Id: "nCmN0OJdoK9SZ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<RetireInventoryResponse>> RetireInventory([FromBody]RetireInventoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RetireInventoryResponse response = await InventoryFunc.RetireInventory(AppConfig, UserSession, request);
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
        [FwControllerMethod(Id: "d0PpYQiPX2mWi", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RentalInventoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/removefromcontainer/validateitem/browse 
        [HttpPost("validateitem/browse")]
        [FwControllerMethod(Id: "q7ErB79tGhFSm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateItemBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ItemLogic>(browseRequest);
        }
    }
}
