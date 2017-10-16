using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.CrewPosition
{
    [Route("api/v1/[controller]")]
    public class CrewPositionController : RwDataController
    {
        public CrewPositionController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crewposition/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CrewPositionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/crewposition 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CrewPositionLogic>(pageno, pagesize, sort, typeof(CrewPositionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/crewposition/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<CrewPositionLogic>(id, typeof(CrewPositionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crewposition 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CrewPositionLogic l)
        {
            return await DoPostAsync<CrewPositionLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/crewposition/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(CrewPositionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crewposition/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
} 
