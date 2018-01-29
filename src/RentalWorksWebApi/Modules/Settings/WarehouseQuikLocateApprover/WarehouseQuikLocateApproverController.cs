using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WarehouseQuikLocateApprover
{
    [Route("api/v1/[controller]")]
    public class WarehouseQuikLocateApproverController : AppDataController
    {
        public WarehouseQuikLocateApproverController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousequiklocateapprover/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WarehouseQuikLocateApproverLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehousequiklocateapprover 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WarehouseQuikLocateApproverLogic>(pageno, pagesize, sort, typeof(WarehouseQuikLocateApproverLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehousequiklocateapprover/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<WarehouseQuikLocateApproverLogic>(id, typeof(WarehouseQuikLocateApproverLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousequiklocateapprover 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WarehouseQuikLocateApproverLogic l)
        {
            return await DoPostAsync<WarehouseQuikLocateApproverLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/warehousequiklocateapprover/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(WarehouseQuikLocateApproverLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousequiklocateapprover/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}