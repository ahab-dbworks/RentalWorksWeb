using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Administrator.Update
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "QBpkw2MKnb4yp")]
    public class UpdateController : AppDataController
    {
        public UpdateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) {  }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/update/availableversions/2019.1.2.3
        [HttpGet("availableversions/{currentversion}")]
        [FwControllerMethod(Id: "Q2YnR3ZeEPtlX", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<AvailableVersionsResponse>> GetAvailableVersions(string currentversion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AvailableVersionsResponse response = await UpdateFunc.GetAvailableVersions(AppConfig, UserSession, currentversion);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
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
