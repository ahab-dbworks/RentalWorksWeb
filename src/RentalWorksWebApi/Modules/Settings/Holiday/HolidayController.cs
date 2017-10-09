using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.Holiday
{
    [Route("api/v1/[controller]")]
    public class HolidayController : RwDataController
    {
        public HolidayController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/holiday/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(HolidayLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/holiday 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<HolidayLogic>(pageno, pagesize, sort, typeof(HolidayLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/holiday/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<HolidayLogic>(id, typeof(HolidayLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/holiday 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]HolidayLogic l)
        {
            return await DoPostAsync<HolidayLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/holiday/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(HolidayLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/holiday/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}