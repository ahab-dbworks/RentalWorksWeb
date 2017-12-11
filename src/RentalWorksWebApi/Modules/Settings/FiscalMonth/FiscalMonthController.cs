using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.FiscalMonth
{
    [Route("api/v1/[controller]")]
    public class FiscalMonthController : AppDataController
    {
        public FiscalMonthController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/fiscalmonth/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(FiscalMonthLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/fiscalmonth 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FiscalMonthLogic>(pageno, pagesize, sort, typeof(FiscalMonthLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/fiscalmonth/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<FiscalMonthLogic>(id, typeof(FiscalMonthLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/fiscalmonth 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]FiscalMonthLogic l)
        {
            return await DoPostAsync<FiscalMonthLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/fiscalmonth/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(FiscalMonthLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/fiscalmonth/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}