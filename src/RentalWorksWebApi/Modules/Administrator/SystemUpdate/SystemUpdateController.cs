using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Administrator.SystemUpdate
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "QBpkw2MKnb4yp")]
    public class SystemUpdateController : AppDataController
    {
        public SystemUpdateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/systemupdate/availableversions
        [HttpPost("availableversions")]
        [FwControllerMethod(Id: "Q2YnR3ZeEPtlX", ActionType: FwControllerActionTypes.View)]
        //public async Task<ActionResult<AvailableVersionsResponse>> GetAvailableVersions([FromBody] AvailableVersionsRequest request)
        public ActionResult<AvailableVersionsResponse> GetAvailableVersions([FromBody] AvailableVersionsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //AvailableVersionsResponse response = await SystemUpdateFunc.GetAvailableVersions(AppConfig, UserSession, request);
                AvailableVersionsResponse response = SystemUpdateFunc.GetAvailableVersions(AppConfig, UserSession, request);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/systemupdate/builddocuments
        [HttpPost("builddocuments")]
        [FwControllerMethod(Id: "MNxZry7QZjjRa", ActionType: FwControllerActionTypes.View)]
        public ActionResult<BuildDocumentsResponse> GetBuildDocuments([FromBody] BuildDocumentsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BuildDocumentsResponse response = SystemUpdateFunc.GetBuildDocuments(AppConfig, UserSession, request);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/systemupdate/applyupdate
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
                ApplyUpdateResponse response = await SystemUpdateFunc.ApplyUpdate(AppConfig, UserSession, request);
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
