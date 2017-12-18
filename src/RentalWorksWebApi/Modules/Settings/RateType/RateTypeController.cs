using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.RateType
{
    [Route("api/v1/[controller]")]
    public class RateTypeController : AppDataController
    {
        public RateTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ratetype/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(RateTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ratetype 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RateTypeLogic>(pageno, pagesize, sort, typeof(RateTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ratetype/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<RateTypeLogic>(id, typeof(RateTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/ratetype 
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody]RateTypeLogic l)
        //{
        //    return await DoPostAsync<RateTypeLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/ratetype/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id, typeof(RateTypeLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/ratetype/validateduplicate 
        //[HttpPost("validateduplicate")]
        //public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        //{
        //    return await DoValidateDuplicateAsync(request);
        //}
        //------------------------------------------------------------------------------------ 
    }
}