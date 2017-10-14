using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Home.ItemConsignor
{
    [Route("api/v1/[controller]")]
    public class ItemConsignorController : RwDataController
    {
        public ItemConsignorController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemconsignor/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ItemConsignorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemconsignor 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ItemConsignorLogic>(pageno, pagesize, sort, typeof(ItemConsignorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemconsignor/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<ItemConsignorLogic>(id, typeof(ItemConsignorLogic));
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/itemconsignor 
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody]ItemConsignorLogic l)
        //{
        //    return await DoPostAsync<ItemConsignorLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/itemconsignor/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id, typeof(ItemConsignorLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        // POST api/v1/itemconsignor/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}