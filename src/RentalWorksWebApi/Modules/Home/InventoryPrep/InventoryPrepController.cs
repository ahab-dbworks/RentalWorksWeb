using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Home.InventoryPrep
{
    [Route("api/v1/[controller]")]
    public class InventoryPrepController : RwDataController
    {
        public InventoryPrepController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryprep/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryPrepLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryprep 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryPrepLogic>(pageno, pagesize, sort, typeof(InventoryPrepLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryprep/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryPrepLogic>(id, typeof(InventoryPrepLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryprep 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventoryPrepLogic l)
        {
            return await DoPostAsync<InventoryPrepLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryprep 
        [HttpPost("saveform")]
        public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request)
        {
            return await DoSaveFormAsync<InventoryPrepLogic>(request, typeof(InventoryPrepLogic));
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventoryprep/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryPrepLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryprep/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}