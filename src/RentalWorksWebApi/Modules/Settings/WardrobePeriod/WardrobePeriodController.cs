using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.WardrobePeriod
{
    [Route("api/v1/[controller]")]
    public class WardrobePeriodController : AppDataController
    {
        public WardrobePeriodController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobeperiod/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WardrobePeriodLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobeperiod
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobePeriodLogic>(pageno, pagesize, sort, typeof(WardrobePeriodLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobeperiod/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobePeriodLogic>(id, typeof(WardrobePeriodLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobeperiod
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WardrobePeriodLogic l)
        {
            return await DoPostAsync<WardrobePeriodLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobeperiod/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(WardrobePeriodLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobeperiod/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}