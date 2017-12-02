using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.Source
{
    [Route("api/v1/[controller]")]
    public class SourceController : RwDataController
    {
        public SourceController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/source/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(SourceLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/source
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SourceLogic>(pageno, pagesize, sort, typeof(SourceLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/source/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<SourceLogic>(id, typeof(SourceLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/source
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SourceLogic l)
        {
            return await DoPostAsync<SourceLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/source/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(SourceLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/source/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}