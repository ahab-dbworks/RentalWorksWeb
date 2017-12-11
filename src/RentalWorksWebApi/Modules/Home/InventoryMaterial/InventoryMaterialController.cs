using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.InventoryMaterial
{
    [Route("api/v1/[controller]")]
    public class InventoryMaterialController : AppDataController
    {
        public InventoryMaterialController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorymaterial/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryMaterialLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorymaterial 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryMaterialLogic>(pageno, pagesize, sort, typeof(InventoryMaterialLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorymaterial/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryMaterialLogic>(id, typeof(InventoryMaterialLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorymaterial 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventoryMaterialLogic l)
        {
            return await DoPostAsync<InventoryMaterialLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorymaterial/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryMaterialLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorymaterial/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}