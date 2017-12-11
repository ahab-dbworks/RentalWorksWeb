using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.GeneratorRating
{
    [Route("api/v1/[controller]")]
    public class GeneratorRatingController : AppDataController
    {
        public GeneratorRatingController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatorrating/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{915BC77B-88C6-4DF7-9AC8-9B2CC0B2ECDB}")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(GeneratorRatingLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatorrating
        [HttpGet]
        [Authorize(Policy = "{D014CE24-1518-4022-9D66-E779DF284AB4}")]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GeneratorRatingLogic>(pageno, pagesize, sort, typeof(GeneratorRatingLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatorrating/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{10B3F329-73CE-4359-8DF2-CF86DF76C9E4}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<GeneratorRatingLogic>(id, typeof(GeneratorRatingLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatorrating
        [HttpPost]
        [Authorize(Policy = "{53DD87EE-08DA-4FE5-A2CC-EE8C38DA40C8}")]
        public async Task<IActionResult> PostAsync([FromBody]GeneratorRatingLogic l)
        {
            return await DoPostAsync<GeneratorRatingLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/generatorrating/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{7E849D41-933F-4715-9F77-4FDB6278A138}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(GeneratorRatingLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatorrating/validateduplicate
        [HttpPost("validateduplicate")]
        [Authorize(Policy = "{63D65189-639E-450E-964A-06566FA671DB}")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
    //------------------------------------------------------------------------------------
}
}