using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.GlDistribution
{
    [Route("api/v1/[controller]")]
    public class GlDistributionController : RwDataController
    {
        public GlDistributionController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/gldistribution/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(GlDistributionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/gldistribution 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GlDistributionLogic>(pageno, pagesize, sort, typeof(GlDistributionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/gldistribution/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<GlDistributionLogic>(id, typeof(GlDistributionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/gldistribution 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]GlDistributionLogic l)
        {
            return await DoPostAsync<GlDistributionLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/gldistribution 
        //[HttpPost("saveform")] 
        //public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request) 
        //{ 
        //    return await DoSaveFormAsync<GlDistributionLogic>(request, typeof(GlDistributionLogic)); 
        //} 
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/gldistribution/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(GlDistributionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/gldistribution/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}