using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.SpaceRate
{
    [Route("api/v1/[controller]")]
    public class SpaceRateController : RwDataController
    {
        public SpaceRateController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/spacerate/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(SpaceRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/spacerate 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SpaceRateLogic>(pageno, pagesize, sort, typeof(SpaceRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/spacerate/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<SpaceRateLogic>(id, typeof(SpaceRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/spacerate 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SpaceRateLogic l)
        {
            return await DoPostAsync<SpaceRateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/spacerate/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(SpaceRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/spacerate/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}