using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.InventoryGroup
{
    [Route("api/v1/[controller]")]
    public class InventoryGroupController : RwDataController
    {
        public InventoryGroupController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorygroup/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryGroupLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorygroup 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryGroupLogic>(pageno, pagesize, sort, typeof(InventoryGroupLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorygroup/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryGroupLogic>(id, typeof(InventoryGroupLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorygroup 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventoryGroupLogic l)
        {
            return await DoPostAsync<InventoryGroupLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/inventorygroup 
        //[HttpPost("saveform")] 
        //public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request) 
        //{ 
        //    return await DoSaveFormAsync<InventoryGroupLogic>(request, typeof(InventoryGroupLogic)); 
        //} 
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorygroup/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryGroupLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorygroup/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}