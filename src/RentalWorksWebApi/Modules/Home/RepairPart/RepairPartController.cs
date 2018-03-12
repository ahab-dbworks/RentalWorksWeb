using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.RepairPart
{
    [Route("api/v1/[controller]")]
    public class RepairPartController : AppDataController
    {
        public RepairPartController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairpart/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(RepairPartLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repairpart 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RepairPartLogic>(pageno, pagesize, sort, typeof(RepairPartLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repairpart/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<RepairPartLogic>(id, typeof(RepairPartLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairpart 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]RepairPartLogic l)
        {
            return await DoPostAsync<RepairPartLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/repairpart/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(RepairPartLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairpart/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
