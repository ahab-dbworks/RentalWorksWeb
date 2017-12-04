using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Home.OrderStatusSummary
{
    [Route("api/v1/[controller]")]
    public class OrderStatusSummaryController : RwDataController
    {
        public OrderStatusSummaryController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderstatussummary/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderStatusSummaryLogic));
        }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/orderstatussummary 
        //[HttpGet]
        //public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<OrderStatusSummaryLogic>(pageno, pagesize, sort, typeof(OrderStatusSummaryLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        //// GET api/v1/orderstatussummary/A0000001 
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetAsync([FromRoute]string id)
        //{
        //    return await DoGetAsync<OrderStatusSummaryLogic>(id, typeof(OrderStatusSummaryLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/orderstatussummary 
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody]OrderStatusSummaryLogic l)
        //{
        //    return await DoPostAsync<OrderStatusSummaryLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/orderstatussummary/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id, typeof(OrderStatusSummaryLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/orderstatussummary/validateduplicate 
        //[HttpPost("validateduplicate")]
        //public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        //{
        //    return await DoValidateDuplicateAsync(request);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}