using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.Crew
{
    [Route("api/v1/[controller]")]
    public class CrewController : AppDataController
    {
        public CrewController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crew/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CrewLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/crew 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CrewLogic>(pageno, pagesize, sort, typeof(CrewLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/crew/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<CrewLogic>(id, typeof(CrewLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crew 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CrewLogic l)
        {
            return await DoPostAsync<CrewLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/crew/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(CrewLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crew/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}