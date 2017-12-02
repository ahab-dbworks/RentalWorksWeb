using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Home.Order
{
    [Route("api/v1/[controller]")]
    public class OrderController : RwDataController
    {
        public OrderController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/browse
        [HttpPost("browse")]
        public async Task<IActionResult> Browse([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/order
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderLogic>(pageno, pagesize, sort, typeof(OrderLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/order/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderLogic>(id, typeof(OrderLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]OrderLogic l)
        {
            return await DoPostAsync<OrderLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/order/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}