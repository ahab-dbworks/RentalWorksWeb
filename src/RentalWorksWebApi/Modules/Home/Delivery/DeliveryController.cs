using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.Delivery
{
    [Route("api/v1/[controller]")]
    public class DeliveryController : AppDataController
    {
        public DeliveryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/delivery/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DeliveryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/delivery 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DeliveryLogic>(pageno, pagesize, sort, typeof(DeliveryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/delivery/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<DeliveryLogic>(id, typeof(DeliveryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/delivery 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]DeliveryLogic l)
        {
            return await DoPostAsync<DeliveryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/delivery/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(DeliveryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/delivery/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
