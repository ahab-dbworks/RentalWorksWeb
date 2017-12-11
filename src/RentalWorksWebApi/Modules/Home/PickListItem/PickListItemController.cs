using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.PickListItem
{
    [Route("api/v1/[controller]")]
    public class PickListItemController : AppDataController
    {
        public PickListItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistitem/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PickListItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/picklistitem 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PickListItemLogic>(pageno, pagesize, sort, typeof(PickListItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/picklistitem/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<PickListItemLogic>(id, typeof(PickListItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistitem 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PickListItemLogic l)
        {
            return await DoPostAsync<PickListItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/picklistitem/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PickListItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistitem/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}