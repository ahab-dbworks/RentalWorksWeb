using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.ProductionType
{
    [Route("api/v1/[controller]")]
    public class ProductionTypeController : RwDataController
    {
        public ProductionTypeController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/productiontype/browse
        [HttpPost("browse")]
        public async Task<IActionResult> Browse([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ProductionTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/productiontype
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProductionTypeLogic>(pageno, pagesize, sort, typeof(ProductionTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/productiontype/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProductionTypeLogic>(id, typeof(ProductionTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/productiontype
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ProductionTypeLogic l)
        {
            return await DoPostAsync<ProductionTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/productiontype/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ProductionTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/productiontype/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}