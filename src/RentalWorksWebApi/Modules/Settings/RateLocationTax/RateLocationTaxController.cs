using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.RateLocationTax
{
    [Route("api/v1/[controller]")]
    public class RateLocationTaxController : RwDataController
    {
        public RateLocationTaxController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ratelocationtax/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(RateLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ratelocationtax 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RateLocationTaxLogic>(pageno, pagesize, sort, typeof(RateLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ratelocationtax/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<RateLocationTaxLogic>(id, typeof(RateLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ratelocationtax 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]RateLocationTaxLogic l)
        {
            return await DoPostAsync<RateLocationTaxLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ratelocationtax
        [HttpPost("saveform")]
        public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request)
        {
            return await DoSaveFormAsync<RateLocationTaxLogic>(request, typeof(RateLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/ratelocationtax/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(RateLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ratelocationtax/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}