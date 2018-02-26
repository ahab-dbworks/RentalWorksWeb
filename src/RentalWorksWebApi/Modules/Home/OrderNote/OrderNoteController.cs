using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.OrderNote
{
    [Route("api/v1/[controller]")]
    public class OrderNoteController : AppDataController
    {
        public OrderNoteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordernote/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordernote 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderNoteLogic>(pageno, pagesize, sort, typeof(OrderNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordernote/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderNoteLogic>(id, typeof(OrderNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordernote 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]OrderNoteLogic l)
        {
            return await DoPostAsync<OrderNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordernote/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordernote/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}