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

namespace WebApi.Modules.Utilities.ChangeICodeUtility
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "S794cPbgUARcH")]
    public class ChangeICodeUtilityController : AppDataController
    {
        public ChangeICodeUtilityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/changeicodeutility/changeicode
        [HttpPost("changeicode")]
        [FwControllerMethod(Id: "CuRrj2cCbvph2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<ChangeICodeResponse>> ChangeICode([FromBody]ChangeICodeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ChangeICodeResponse response = await InventoryFunc.ChangeICode(AppConfig, UserSession, request);
                return new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/changeicodeutility/validateinventory/browse
        [HttpPost("validateinventory/browse")]
        [FwControllerMethod(Id: "GoupJ6UG6EwGu", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RentalInventoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/changeicodeutility/validateitem/browse 
        [HttpPost("validateitem/browse")]
        [FwControllerMethod(Id: "ov2FPkKn2O2CZ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateItemBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ItemLogic>(browseRequest);
        }
    }
}
