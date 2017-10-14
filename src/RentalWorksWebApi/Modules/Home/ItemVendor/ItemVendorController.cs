using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Home.ItemVendor
{
    [Route("api/v1/[controller]")]
    public class ItemVendorController : RwDataController
    {
        public ItemVendorController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemvendor/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ItemVendorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemvendor 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ItemVendorLogic>(pageno, pagesize, sort, typeof(ItemVendorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemvendor/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<ItemVendorLogic>(id, typeof(ItemVendorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemvendor 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ItemVendorLogic l)
        {
            return await DoPostAsync<ItemVendorLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemvendor 
        [HttpPost("saveform")]
        public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request)
        {
            return await DoSaveFormAsync<ItemVendorLogic>(request, typeof(ItemVendorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/itemvendor/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ItemVendorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemvendor/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}