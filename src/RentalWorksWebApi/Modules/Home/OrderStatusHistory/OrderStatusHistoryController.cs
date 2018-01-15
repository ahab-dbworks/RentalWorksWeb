using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.OrderStatusHistory
{
    [Route("api/v1/[controller]")]
    public class OrderStatusHistoryController : AppDataController
    {
        public OrderStatusHistoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderstatushistory/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderStatusHistoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/orderstatushistory 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderStatusHistoryLogic>(pageno, pagesize, sort, typeof(OrderStatusHistoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/orderstatushistory/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderStatusHistoryLogic>(id, typeof(OrderStatusHistoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/orderstatushistory 
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody]OrderStatusHistoryLogic l)
        //{
        //    return await DoPostAsync<OrderStatusHistoryLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/orderstatushistory/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id, typeof(OrderStatusHistoryLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        // POST api/v1/orderstatushistory/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}