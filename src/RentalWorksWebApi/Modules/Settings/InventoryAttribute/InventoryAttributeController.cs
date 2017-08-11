using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.InventoryAttribute
{
    [Route("api/v1/[controller]")]
    public class InventoryAttributeController : RwDataController
    {
        public InventoryAttributeController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventoryattribute/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryAttributeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventoryattribute
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryAttributeLogic>(pageno, pagesize, sort, typeof(InventoryAttributeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventoryattribute/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryAttributeLogic>(id, typeof(InventoryAttributeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventoryattribute
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventoryAttributeLogic l)
        {
            return await DoPostAsync<InventoryAttributeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/inventoryattribute/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryAttributeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventoryattribute/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}