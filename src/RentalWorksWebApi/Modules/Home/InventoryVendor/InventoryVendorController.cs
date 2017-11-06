using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Home.InventoryVendor
{
    [Route("api/v1/[controller]")]
    public class InventoryVendorController : RwDataController
    {
        public InventoryVendorController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryvendor/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryVendorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryvendor 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryVendorLogic>(pageno, pagesize, sort, typeof(InventoryVendorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryvendor/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryVendorLogic>(id, typeof(InventoryVendorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryvendor 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventoryVendorLogic l)
        {
            return await DoPostAsync<InventoryVendorLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventoryvendor/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryVendorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryvendor/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}