using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WallType
{
    [Route("api/v1/[controller]")]
    public class WallTypeController : AppDataController
    {
        public WallTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/walltype/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WallTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/walltype 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WallTypeLogic>(pageno, pagesize, sort, typeof(WallTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/walltype/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<WallTypeLogic>(id, typeof(WallTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/walltype 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WallTypeLogic l)
        {
            return await DoPostAsync<WallTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/walltype/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(WallTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/walltype/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}