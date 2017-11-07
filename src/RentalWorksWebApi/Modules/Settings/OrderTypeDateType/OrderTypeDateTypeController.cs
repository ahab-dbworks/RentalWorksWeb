using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.OrderTypeDateType
{
    [Route("api/v1/[controller]")]
    public class OrderTypeDateTypeController : RwDataController
    {
        public OrderTypeDateTypeController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypedatetype/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderTypeDateTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypedatetype 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderTypeDateTypeLogic>(pageno, pagesize, sort, typeof(OrderTypeDateTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypedatetype/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderTypeDateTypeLogic>(id, typeof(OrderTypeDateTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypedatetype 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]OrderTypeDateTypeLogic l)
        {
            return await DoPostAsync<OrderTypeDateTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordertypedatetype/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderTypeDateTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypedatetype/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}