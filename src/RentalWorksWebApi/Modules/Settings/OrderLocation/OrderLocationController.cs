using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.OrderLocation
{
    [Route("api/v1/[controller]")]
    public class OrderLocationController : AppDataController
    {
        public OrderLocationController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderlocation/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/orderlocation 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderLocationLogic>(pageno, pagesize, sort, typeof(OrderLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/orderlocation/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderLocationLogic>(id, typeof(OrderLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderlocation 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]OrderLocationLogic l)
        {
            return await DoPostAsync<OrderLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/orderlocation/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderlocation/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}