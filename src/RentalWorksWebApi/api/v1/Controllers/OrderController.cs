using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class OrderController : RwDataController
    {
        public OrderController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/browse
        [HttpPost("browse")]
        public IActionResult Browse([FromBody]BrowseRequestDto browseRequest)
        {
            return doBrowse(browseRequest, typeof(OrderLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/order
        [HttpGet]
        public IActionResult Get(int pageno, int pagesize)
        {
            return doGet<OrderLogic>(pageno, pagesize, typeof(OrderLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/order/A0000001
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return doGet<OrderLogic>(id, typeof(OrderLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order
        [HttpPost]
        public IActionResult Post([FromBody]OrderLogic l)
        {
            return doPost<OrderLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/order/A0000001
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return doDelete(id, typeof(OrderLogic));
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