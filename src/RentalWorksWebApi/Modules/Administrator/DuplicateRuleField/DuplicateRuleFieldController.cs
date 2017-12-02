using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Administrator.DuplicateRuleField
{
    [Route("api/v1/[controller]")]
    public class DuplicateRuleFieldController : RwDataController
    {
        public DuplicateRuleFieldController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/duplicaterulefield/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DuplicateRuleFieldLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/duplicaterulefield 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DuplicateRuleFieldLogic>(pageno, pagesize, sort, typeof(DuplicateRuleFieldLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/duplicaterulefield/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<DuplicateRuleFieldLogic>(id, typeof(DuplicateRuleFieldLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/duplicaterulefield 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]DuplicateRuleFieldLogic l)
        {
            return await DoPostAsync<DuplicateRuleFieldLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/duplicaterulefield/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(DuplicateRuleFieldLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/duplicaterulefield/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}