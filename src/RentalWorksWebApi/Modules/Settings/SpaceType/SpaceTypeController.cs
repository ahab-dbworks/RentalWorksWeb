using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.SpaceType
{
    [Route("api/v1/[controller]")]
    public class SpaceTypeController : RwDataController
    {
        public SpaceTypeController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/spacetype/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(SpaceTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/spacetype 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SpaceTypeLogic>(pageno, pagesize, sort, typeof(SpaceTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/spacetype/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<SpaceTypeLogic>(id, typeof(SpaceTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/spacetype 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SpaceTypeLogic l)
        {
            return await DoPostAsync<SpaceTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/spacetype/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(SpaceTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/spacetype/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}