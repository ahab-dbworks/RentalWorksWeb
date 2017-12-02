using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Home.InventoryContainerItem
{
    [Route("api/v1/[controller]")]
    public class InventoryContainerItemController : RwDataController
    {
        public InventoryContainerItemController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycontaineritem/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryContainerItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycontaineritem 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryContainerItemLogic>(pageno, pagesize, sort, typeof(InventoryContainerItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycontaineritem/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryContainerItemLogic>(id, typeof(InventoryContainerItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycontaineritem 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventoryContainerItemLogic l)
        {
            return await DoPostAsync<InventoryContainerItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorycontaineritem/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryContainerItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycontaineritem/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}