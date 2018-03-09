using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.RepairOrder
{
    [Route("api/v1/[controller]")]
    public class RepairOrderController : AppDataController
    {
        public RepairOrderController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairorder/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(RepairOrderLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repairorder 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RepairOrderLogic>(pageno, pagesize, sort, typeof(RepairOrderLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repairorder/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<RepairOrderLogic>(id, typeof(RepairOrderLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairorder 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]RepairOrderLogic l)
        {
            return await DoPostAsync<RepairOrderLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/repairorder/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(RepairOrderLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairorder/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
