using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.Floor
{
    [Route("api/v1/[controller]")]
    public class FloorController : RwDataController
    {
        public FloorController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/floor/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(FloorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/floor 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FloorLogic>(pageno, pagesize, sort, typeof(FloorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/floor/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<FloorLogic>(id, typeof(FloorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/floor 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]FloorLogic l)
        {
            return await DoPostAsync<FloorLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/floor/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(FloorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/floor/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}