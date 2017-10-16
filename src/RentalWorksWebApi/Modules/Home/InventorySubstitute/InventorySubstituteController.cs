using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Home.InventorySubstitute
{
    [Route("api/v1/[controller]")]
    public class InventorySubstituteController : RwDataController
    {
        public InventorySubstituteController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysubstitute/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventorySubstituteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorysubstitute 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventorySubstituteLogic>(pageno, pagesize, sort, typeof(InventorySubstituteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorysubstitute/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventorySubstituteLogic>(id, typeof(InventorySubstituteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysubstitute 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventorySubstituteLogic l)
        {
            return await DoPostAsync<InventorySubstituteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysubstitute
        [HttpPost("saveform")]
        public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request)
        {
            return await DoSaveFormAsync<InventorySubstituteLogic>(request, typeof(InventorySubstituteLogic));
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/inventorysubstitute/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventorySubstituteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysubstitute/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}