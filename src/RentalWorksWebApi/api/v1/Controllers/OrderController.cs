using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class OrderController : RwDataController
    {
        public OrderController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/browse
        [HttpPost("browse")]
        public async Task<IActionResult> Browse([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/order
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize)
        {
            return await DoGetAsync<OrderLogic>(pageno, pagesize, typeof(OrderLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/order/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
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
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(OrderLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validateduplicate
        [HttpPost("validateduplicate")]
        public IActionResult ValidateDuplicate(ValidateDuplicateRequest request)
        {
            return doValidateDuplicate(request);
        }
        //------------------------------------------------------------------------------------
    }
}