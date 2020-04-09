using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Utilities.Update
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "QBpkw2MKnb4yp")]
    public class UpdateController : AppDataController
    {
        public UpdateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) {  }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/update/applyupdate
        [HttpPost("applyupdate")]
        [FwControllerMethod(Id: "TxeVikkpb8dws", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<ApplyUpdateResponse>> ApplyUpdate([FromBody]ApplyUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ApplyUpdateResponse response = await UpdateFunc.ApplyUpdate(AppConfig, UserSession, request);
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
