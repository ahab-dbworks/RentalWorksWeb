using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Home.ItemAttributeValue
{
    [Route("api/v1/[controller]")]
    public class ItemAttributeValueController : RwDataController
    {
        public ItemAttributeValueController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemattributevalue/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ItemAttributeValueLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemattributevalue 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ItemAttributeValueLogic>(pageno, pagesize, sort, typeof(ItemAttributeValueLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemattributevalue/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<ItemAttributeValueLogic>(id, typeof(ItemAttributeValueLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemattributevalue 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ItemAttributeValueLogic l)
        {
            return await DoPostAsync<ItemAttributeValueLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/itemattributevalue/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ItemAttributeValueLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemattributevalue/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}