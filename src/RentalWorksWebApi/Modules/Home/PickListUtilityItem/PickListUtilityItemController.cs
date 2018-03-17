using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.PickListUtilityItem
{
    [Route("api/v1/[controller]")]
    public class PickListUtilityItemController : AppDataController
    {
        //justin - temporary
        public class PickListResponse
        {
            public string PickListId;
        }

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
        // POST api/v1/picklistutilityitem/createpicklist/A0000001 (sessionid)
        [HttpPost("createpicklist/{sessionId}")]
        //public async Task<IActionResult> CreatePickList([FromRoute]string sessionId)
        public IActionResult CreatePickList([FromRoute]string sessionId)
        {
            //return await DoPostAsync<PickListUtilityItemLogic>(l);

            //justin - temporary
            PickListResponse r = new PickListResponse();
            r.PickListId = sessionId;
            return new OkObjectResult(r);
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
