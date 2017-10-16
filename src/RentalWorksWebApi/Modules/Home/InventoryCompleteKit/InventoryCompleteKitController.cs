using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Home.InventoryCompleteKit
{
    [Route("api/v1/[controller]")]
    public class InventoryCompleteKitController : RwDataController
    {
        public InventoryCompleteKitController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycompletekit/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryCompleteKitLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycompletekit 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryCompleteKitLogic>(pageno, pagesize, sort, typeof(InventoryCompleteKitLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycompletekit/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryCompleteKitLogic>(id, typeof(InventoryCompleteKitLogic));
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/inventorycompletekit 
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody]InventoryCompleteKitLogic l)
        //{
        //    return await DoPostAsync<InventoryCompleteKitLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/inventorycompletekit/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id, typeof(InventoryCompleteKitLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/inventorycompletekit/validateduplicate 
        //[HttpPost("validateduplicate")]
        //public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        //{
        //    return await DoValidateDuplicateAsync(request);
        //}
        //------------------------------------------------------------------------------------ 
    }
}