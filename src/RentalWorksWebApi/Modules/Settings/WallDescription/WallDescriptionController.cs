using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.WallDescription
{
    [Route("api/v1/[controller]")]
    public class WallDescriptionController : RwDataController
    {
        public WallDescriptionController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/walldescription/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WallDescriptionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/walldescription 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WallDescriptionLogic>(pageno, pagesize, sort, typeof(WallDescriptionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/walldescription/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<WallDescriptionLogic>(id, typeof(WallDescriptionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/walldescription 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WallDescriptionLogic l)
        {
            return await DoPostAsync<WallDescriptionLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/walldescription/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(WallDescriptionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/walldescription/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}