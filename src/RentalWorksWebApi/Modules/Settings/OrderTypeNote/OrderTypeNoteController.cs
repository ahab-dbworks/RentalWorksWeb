using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Settings.OrderTypeNote
{
    [Route("api/v1/[controller]")]
    public class OrderTypeNoteController : RwDataController
    {
        public OrderTypeNoteController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypenote/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderTypeNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypenote 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderTypeNoteLogic>(pageno, pagesize, sort, typeof(OrderTypeNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypenote/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderTypeNoteLogic>(id, typeof(OrderTypeNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypenote 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]OrderTypeNoteLogic l)
        {
            return await DoPostAsync<OrderTypeNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordertypenote/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderTypeNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypenote/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}