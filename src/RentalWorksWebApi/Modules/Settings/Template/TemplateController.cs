using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.Template
{
    [Route("api/v1/[controller]")]
    public class TemplateController : AppDataController
    {
        public TemplateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/template/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(TemplateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/template 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<TemplateLogic>(pageno, pagesize, sort, typeof(TemplateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/template/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<TemplateLogic>(id, typeof(TemplateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/template 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]TemplateLogic l)
        {
            return await DoPostAsync<TemplateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/template/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(TemplateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/template/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}