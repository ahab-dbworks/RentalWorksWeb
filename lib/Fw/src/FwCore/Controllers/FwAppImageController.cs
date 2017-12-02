using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace FwCore.Controllers
{
    [Route("api/v1/[controller]")]
    public class AttributeController : Controller
    {
        public AttributeController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/attribute/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(AttributeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/attribute
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AttributeLogic>(pageno, pagesize, sort, typeof(AttributeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/attribute/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<AttributeLogic>(id, typeof(AttributeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/attribute
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]AttributeLogic l)
        {
            return await DoPostAsync<AttributeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/attribute/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(AttributeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/attribute/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}
