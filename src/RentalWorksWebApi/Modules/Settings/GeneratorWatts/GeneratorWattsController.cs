using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.GeneratorWatts
{
    [Route("api/v1/[controller]")]
    public class GeneratorWattsController : RwDataController
    {
        public GeneratorWattsController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatorwatts/browse
        [HttpPost("browse")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(GeneratorWattsLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatorwatts
        [HttpGet]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<GeneratorWattsLogic>(pageno, pagesize, sort, typeof(GeneratorWattsLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatorwatts/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<GeneratorWattsLogic>(id, typeof(GeneratorWattsLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatorwatts
        [HttpPost]
        [Authorize(Policy = "")]
        public async Task<IActionResult> PostAsync([FromBody]GeneratorWattsLogic l)
        {
            return await DoPostAsync<GeneratorWattsLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/generatorwatts/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(GeneratorWattsLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatorwatts/validateduplicate
        [HttpPost("validateduplicate")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}