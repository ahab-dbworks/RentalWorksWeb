using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.Building
{
    [Route("api/v1/[controller]")]
    public class BuildingController : RwDataController
    {
        public BuildingController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/building/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(BuildingLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/building 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<BuildingLogic>(pageno, pagesize, sort, typeof(BuildingLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/building/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<BuildingLogic>(id, typeof(BuildingLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/building 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]BuildingLogic l)
        {
            return await DoPostAsync<BuildingLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/building/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(BuildingLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/building/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}