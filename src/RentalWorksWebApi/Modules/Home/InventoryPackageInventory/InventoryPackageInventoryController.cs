using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Home.InventoryPackageInventory
{
    [Route("api/v1/[controller]")]
    public class InventoryPackageInventoryController : RwDataController
    {
        public InventoryPackageInventoryController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorypackageinventory/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryPackageInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorypackageinventory 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryPackageInventoryLogic>(pageno, pagesize, sort, typeof(InventoryPackageInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorypackageinventory/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryPackageInventoryLogic>(id, typeof(InventoryPackageInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorypackageinventory 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventoryPackageInventoryLogic l)
        {
            return await DoPostAsync<InventoryPackageInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorypackageinventory/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryPackageInventoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorypackageinventory/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}