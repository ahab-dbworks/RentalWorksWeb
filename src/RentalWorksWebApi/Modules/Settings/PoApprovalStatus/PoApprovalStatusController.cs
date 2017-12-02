using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.PoApprovalStatus
{
    [Route("api/v1/[controller]")]
    public class PoApprovalStatusController : RwDataController
    {
        public PoApprovalStatusController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/poapprovalstatus/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PoApprovalStatusLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/poapprovalstatus 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PoApprovalStatusLogic>(pageno, pagesize, sort, typeof(PoApprovalStatusLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/poapprovalstatus/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<PoApprovalStatusLogic>(id, typeof(PoApprovalStatusLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/poapprovalstatus 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PoApprovalStatusLogic l)
        {
            return await DoPostAsync<PoApprovalStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/poapprovalstatus/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PoApprovalStatusLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/poapprovalstatus/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}