using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.OrderType
{
    [Route("api/v1/[controller]")]
    public class OrderTypeController : RwDataController
    {
        public OrderTypeController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertype/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertype 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderTypeLogic>(pageno, pagesize, sort, typeof(OrderTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertype/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderTypeLogic>(id, typeof(OrderTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertype 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]OrderTypeLogic l)
        {
            return await DoPostAsync<OrderTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordertype/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertype/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}