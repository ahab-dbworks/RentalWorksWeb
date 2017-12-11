using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.EventType
{
    [Route("api/v1/[controller]")]
    public class EventTypeController : AppDataController
    {
        public EventTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/eventtype/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(EventTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/eventtype 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<EventTypeLogic>(pageno, pagesize, sort, typeof(EventTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/eventtype/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<EventTypeLogic>(id, typeof(EventTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/eventtype 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]EventTypeLogic l)
        {
            return await DoPostAsync<EventTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/eventtype/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(EventTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/eventtype/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}