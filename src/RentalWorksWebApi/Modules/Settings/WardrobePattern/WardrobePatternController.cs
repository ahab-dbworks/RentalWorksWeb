using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.WardrobePattern
{
    [Route("api/v1/[controller]")]
    public class WardrobePatternController : AppDataController
    {
        public WardrobePatternController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobepattern/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WardrobePatternLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobepattern
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobePatternLogic>(pageno, pagesize, sort, typeof(WardrobePatternLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobepattern/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobePatternLogic>(id, typeof(WardrobePatternLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobepattern
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WardrobePatternLogic l)
        {
            return await DoPostAsync<WardrobePatternLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobepattern/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(WardrobePatternLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobepattern/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}