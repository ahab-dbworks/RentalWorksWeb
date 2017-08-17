using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.GeneratorFuelType
{
    [Route("api/v1/[controller]")]
    public class GeneratorFuelTypeController : RwDataController
    {
        public GeneratorFuelTypeController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatorfueltype/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(GeneratorFuelTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatorfueltype
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GeneratorFuelTypeLogic>(pageno, pagesize, sort, typeof(GeneratorFuelTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatorfueltype/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<GeneratorFuelTypeLogic>(id, typeof(GeneratorFuelTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatorfueltype
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]GeneratorFuelTypeLogic l)
        {
            return await DoPostAsync<GeneratorFuelTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/generatorfueltype/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(GeneratorFuelTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatorfueltype/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}