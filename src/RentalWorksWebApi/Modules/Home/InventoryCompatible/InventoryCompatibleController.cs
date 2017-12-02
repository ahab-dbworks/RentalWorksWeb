using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Home.InventoryCompatible
{
    [Route("api/v1/[controller]")]
    public class InventoryCompatibleController : RwDataController
    {
        public InventoryCompatibleController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycompatible/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryCompatibleLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycompatible 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryCompatibleLogic>(pageno, pagesize, sort, typeof(InventoryCompatibleLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycompatible/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryCompatibleLogic>(id, typeof(InventoryCompatibleLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycompatible 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventoryCompatibleLogic l)
        {
            return await DoPostAsync<InventoryCompatibleLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorycompatible/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryCompatibleLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycompatible/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}