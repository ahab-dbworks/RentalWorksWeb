using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.GeneratorTypeWarehouse
{
    [Route("api/v1/[controller]")]
    public class GeneratorTypeWarehouseController : RwDataController
    {
        public GeneratorTypeWarehouseController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatortypewarehouse/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(GeneratorTypeWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatortypewarehouse
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GeneratorTypeWarehouseLogic>(pageno, pagesize, sort, typeof(GeneratorTypeWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatortypewarehouse/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<GeneratorTypeWarehouseLogic>(id, typeof(GeneratorTypeWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatortypewarehouse
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]GeneratorTypeWarehouseLogic l)
        {
            return await DoPostAsync<GeneratorTypeWarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/generatortypewarehouse/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(GeneratorTypeWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatortypewarehouse/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}