using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.MarketSegmentJob
{
    [Route("api/v1/[controller]")]
    public class MarketSegmentJobController : AppDataController
    {
        public MarketSegmentJobController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/marketsegmentjob/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(MarketSegmentJobLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/marketsegmentjob 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<MarketSegmentJobLogic>(pageno, pagesize, sort, typeof(MarketSegmentJobLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/marketsegmentjob/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<MarketSegmentJobLogic>(id, typeof(MarketSegmentJobLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/marketsegmentjob 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]MarketSegmentJobLogic l)
        {
            return await DoPostAsync<MarketSegmentJobLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/marketsegmentjob/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(MarketSegmentJobLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/marketsegmentjob/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
