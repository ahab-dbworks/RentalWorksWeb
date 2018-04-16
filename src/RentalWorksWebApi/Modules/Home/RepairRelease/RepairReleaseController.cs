using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.RepairRelease
{
    [Route("api/v1/[controller]")]
    public class RepairReleaseController : AppDataController
    {
        public RepairReleaseController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairrelease/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(RepairReleaseLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repairrelease 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RepairReleaseLogic>(pageno, pagesize, sort, typeof(RepairReleaseLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repairrelease/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<RepairReleaseLogic>(id, typeof(RepairReleaseLogic));
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/repairrelease 
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody]RepairReleaseLogic l)
        //{
        //    return await DoPostAsync<RepairReleaseLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/repairrelease/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id, typeof(RepairReleaseLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        // POST api/v1/repairrelease/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
