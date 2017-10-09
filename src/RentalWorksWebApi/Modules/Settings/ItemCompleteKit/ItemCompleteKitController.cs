using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.ItemCompleteKit
{
    [Route("api/v1/[controller]")]
    public class ItemCompleteKitController : RwDataController
    {
        public ItemCompleteKitController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemcompletekit/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ItemCompleteKitLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemcompletekit 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ItemCompleteKitLogic>(pageno, pagesize, sort, typeof(ItemCompleteKitLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemcompletekit/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<ItemCompleteKitLogic>(id, typeof(ItemCompleteKitLogic));
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/itemcompletekit 
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody]ItemCompleteKitLogic l)
        //{
        //    return await DoPostAsync<ItemCompleteKitLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/itemcompletekit/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id, typeof(ItemCompleteKitLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/itemcompletekit/validateduplicate 
        //[HttpPost("validateduplicate")]
        //public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        //{
        //    return await DoValidateDuplicateAsync(request);
        //}
        //------------------------------------------------------------------------------------ 
    }
}