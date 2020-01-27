using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Inventory.Inventory;

namespace WebApi.Modules.Utilities.InventoryRetireUtility
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "KIfiUkxPPwRBr")]
    public class InventoryRetireUtilityController : AppDataController
    {
        public InventoryRetireUtilityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
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
    }
}
