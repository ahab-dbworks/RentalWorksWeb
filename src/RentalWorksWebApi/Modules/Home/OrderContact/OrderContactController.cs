using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.OrderContact
{
    [Route("api/v1/[controller]")]
    public class OrderContactController : AppDataController
    {
        public OrderContactController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordercontact/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderContactLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordercontact 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderContactLogic>(pageno, pagesize, sort, typeof(OrderContactLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordercontact/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderContactLogic>(id, typeof(OrderContactLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordercontact 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]OrderContactLogic l)
        {
            return await DoPostAsync<OrderContactLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordercontact/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderContactLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordercontact/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
