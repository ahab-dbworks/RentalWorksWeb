using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.WardrobeSource
{
    [Route("api/v1/[controller]")]
    public class WardrobeSourceController : RwDataController
    {
        public WardrobeSourceController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobesource/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WardrobeSourceLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobesource
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobeSourceLogic>(pageno, pagesize, sort, typeof(WardrobeSourceLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobesource/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobeSourceLogic>(id, typeof(WardrobeSourceLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobesource
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WardrobeSourceLogic l)
        {
            return await DoPostAsync<WardrobeSourceLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobesource/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(WardrobeSourceLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobesource/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}