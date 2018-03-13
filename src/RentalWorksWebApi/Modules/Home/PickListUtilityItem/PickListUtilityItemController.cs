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
        public PickListUtilityItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistutilityitem/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PickListUtilityItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/picklistutilityitem 
        //[HttpGet]
        //public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<PickListUtilityItemLogic>(pageno, pagesize, sort, typeof(PickListUtilityItemLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        //// GET api/v1/picklistutilityitem/A0000001~A0000002~A0000003  //sessionid~orderid~masteritemid
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetAsync([FromRoute]string id)
        //{
        //    return await DoGetAsync<PickListUtilityItemLogic>(id, typeof(PickListUtilityItemLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        // POST api/v1/picklistutilityitem 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PickListUtilityItemLogic l)
        {
            return await DoPostAsync<PickListUtilityItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/picklistutilityitem/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PickListUtilityItemLogic));
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
