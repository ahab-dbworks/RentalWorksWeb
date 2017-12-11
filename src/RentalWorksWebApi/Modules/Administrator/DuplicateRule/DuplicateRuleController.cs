using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Administrator.DuplicateRule
{
    [Route("api/v1/[controller]")]
    public class DuplicateRuleController : AppDataController
    {
        public DuplicateRuleController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/duplicaterule/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DuplicateRuleLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/duplicaterule 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DuplicateRuleLogic>(pageno, pagesize, sort, typeof(DuplicateRuleLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/duplicaterule/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<DuplicateRuleLogic>(id, typeof(DuplicateRuleLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/duplicaterule 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]DuplicateRuleLogic l)
        {
            return await DoPostAsync<DuplicateRuleLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/duplicaterule/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(DuplicateRuleLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/duplicaterule/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}