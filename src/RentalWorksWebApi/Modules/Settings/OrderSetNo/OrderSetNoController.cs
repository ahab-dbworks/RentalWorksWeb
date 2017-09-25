using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.OrderSetNo
{
    [Route("api/v1/[controller]")]
    public class OrderSetNoController : RwDataController
    {
        public OrderSetNoController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersetno/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderSetNoLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordersetno 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderSetNoLogic>(pageno, pagesize, sort, typeof(OrderSetNoLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordersetno/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderSetNoLogic>(id, typeof(OrderSetNoLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersetno 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]OrderSetNoLogic l)
        {
            return await DoPostAsync<OrderSetNoLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordersetno/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderSetNoLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersetno/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}