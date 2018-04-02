using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Home.PickList;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Home.PickListUtilityItem
{
    [Route("api/v1/[controller]")]
    public class PickListUtilityItemController : AppDataController
    {
        public PickListUtilityItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistutilityitem/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PickListUtilityItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistutilityitem 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PickListUtilityItemLogic l)
        {
            return await DoPostAsync<PickListUtilityItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistutilityitem/createpicklist
        [HttpPost("createpicklist")]
        public async Task<IActionResult> CreatePickList([FromBody]BrowseRequest browseRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PickListLogic l = new PickListLogic();
                l.SetDependencies(this.AppConfig, this.UserSession);
                bool b = await l.LoadFromSession(browseRequest);
                return new OkObjectResult(l);
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistutilityitem/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
