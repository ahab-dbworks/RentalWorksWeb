using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Agent.Order;

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
        public async Task<ActionResult<ChangeOrderStatusResponse>> ChangeOrderStatus([FromBody]ChangeOrderStatusRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ChangeOrderStatusResponse response = await OrderFunc.ChangeStatus(AppConfig, UserSession, request);
                return new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/changeorderstatus/validateorder/browse 
        [HttpPost("validateorder/browse")]
        [FwControllerMethod(Id: "0tU4gS7pQkgZY", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOrderBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderLogic>(browseRequest);
        }
    }
}
