using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.InventoryCategory
{
    [Route("api/v1/[controller]")]
    public class InventoryCategoryController : RwDataController
    {
        public InventoryCategoryController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorycategory/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorycategory
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryCategoryLogic>(pageno, pagesize, sort, typeof(InventoryCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorycategory/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryCategoryLogic>(id, typeof(InventoryCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorycategory
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventoryCategoryLogic l)
        {
            return await DoPostAsync<InventoryCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/inventorycategory/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorycategory/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}