using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Utilities.InventoryPurchaseUtility
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "sOxbXBmCPc9y")]
    public class InventoryPurchaseUtilityController : AppDataController
    {
        public InventoryPurchaseUtilityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorypurchaseutility/startsession
        [HttpPost("startsession")]
        [FwControllerMethod(Id: "MswwSEhfz761H", ActionType: FwControllerActionTypes.Option, Caption: "Start Session")]
        public async Task<ActionResult<StartInventoryPurchaseSessionResponse>> StartSession ([FromBody]StartInventoryPurchaseSessionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                StartInventoryPurchaseSessionResponse response = await InventoryPurchaseUtilityFunc.StartSession(AppConfig, UserSession, request);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorypurchaseutility/updatesession
        [HttpPost("updatesession")]
        [FwControllerMethod(Id: "7PRcr9vgVyP38", ActionType: FwControllerActionTypes.Option, Caption: "Update Session")]
        public async Task<ActionResult<UpdateInventoryPurchaseSessionResponse>> UpdateSession([FromBody]UpdateInventoryPurchaseSessionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UpdateInventoryPurchaseSessionResponse response = await InventoryPurchaseUtilityFunc.UpdateSession(AppConfig, UserSession, request);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/inventorypurchaseutility/assignbarcodes
        [HttpPost("assignbarcodes")]
        [FwControllerMethod(Id: "tMnf44QPZSZJ1", ActionType: FwControllerActionTypes.Option, Caption: "Assign Bar Codes")]
        public async Task<ActionResult<InventoryPurchaseAssignBarCodesResponse>> AssignBarCodes([FromBody]InventoryPurchaseAssignBarCodesRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InventoryPurchaseAssignBarCodesResponse response = await InventoryPurchaseUtilityFunc.AssignBarCodes(AppConfig, UserSession, request);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/inventorypurchaseutility/completesession
        [HttpPost("completesession")]
        [FwControllerMethod(Id: "niBKGqW4TdGbC", ActionType: FwControllerActionTypes.Option, Caption: "Complete Session")]
        public async Task<ActionResult<InventoryPurchaseCompleteSessionResponse>> CompleteSession([FromBody]InventoryPurchaseCompleteSessionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InventoryPurchaseCompleteSessionResponse response = await InventoryPurchaseUtilityFunc.CompleteSession(AppConfig, UserSession, request);
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
