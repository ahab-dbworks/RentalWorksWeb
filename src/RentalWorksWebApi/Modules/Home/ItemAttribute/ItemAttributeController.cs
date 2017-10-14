using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Home.ItemAttribute
{
    [Route("api/v1/[controller]")]
    public class ItemAttributeController : RwDataController
    {
        public ItemAttributeController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemattribute/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ItemAttributeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemattribute 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ItemAttributeLogic>(pageno, pagesize, sort, typeof(ItemAttributeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemattribute/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<ItemAttributeLogic>(id, typeof(ItemAttributeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemattribute 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ItemAttributeLogic l)
        {
            return await DoPostAsync<ItemAttributeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemattribute 
        [HttpPost("saveform")]
        public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request)
        {
            return await DoSaveFormAsync<ItemAttributeLogic>(request, typeof(ItemAttributeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/itemattribute/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ItemAttributeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemattribute/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}