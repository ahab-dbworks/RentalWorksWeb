using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.InventoryAttributeValue
{
    [Route("api/v1/[controller]")]
    public class InventoryAttributeValueController : AppDataController
    {
        public InventoryAttributeValueController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryattributevalue/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryAttributeValueLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryattributevalue 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryAttributeValueLogic>(pageno, pagesize, sort, typeof(InventoryAttributeValueLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryattributevalue/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryAttributeValueLogic>(id, typeof(InventoryAttributeValueLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryattributevalue 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventoryAttributeValueLogic l)
        {
            return await DoPostAsync<InventoryAttributeValueLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventoryattributevalue/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryAttributeValueLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryattributevalue/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}