using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Home.InventoryConsignor
{
    [Route("api/v1/[controller]")]
    public class InventoryConsignorController : RwDataController
    {
        public InventoryConsignorController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryconsignor/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryConsignorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryconsignor 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryConsignorLogic>(pageno, pagesize, sort, typeof(InventoryConsignorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryconsignor/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryConsignorLogic>(id, typeof(InventoryConsignorLogic));
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/inventoryconsignor 
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody]inventoryconsignorLogic l)
        //{
        //    return await DoPostAsync<inventoryconsignorLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/inventoryconsignor/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id, typeof(inventoryconsignorLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryconsignor/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}