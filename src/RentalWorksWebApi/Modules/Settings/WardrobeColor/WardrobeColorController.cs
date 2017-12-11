using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.WardrobeColor
{
    [Route("api/v1/[controller]")]
    public class WardrobeColorController : AppDataController
    {
        public WardrobeColorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobecolor/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WardrobeColorLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobecolor
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobeColorLogic>(pageno, pagesize, sort, typeof(WardrobeColorLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobecolor/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobeColorLogic>(id, typeof(WardrobeColorLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobecolor
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WardrobeColorLogic l)
        {
            return await DoPostAsync<WardrobeColorLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobecolor/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(WardrobeColorLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobecolor/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}