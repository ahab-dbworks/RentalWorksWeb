using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.PresentationLayerActivity
{
    [Route("api/v1/[controller]")]
    public class PresentationLayerActivityController : AppDataController
    {
        public PresentationLayerActivityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayeractivity/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PresentationLayerActivityLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayeractivity 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PresentationLayerActivityLogic>(pageno, pagesize, sort, typeof(PresentationLayerActivityLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayeractivity/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<PresentationLayerActivityLogic>(id, typeof(PresentationLayerActivityLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayeractivity 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PresentationLayerActivityLogic l)
        {
            return await DoPostAsync<PresentationLayerActivityLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/presentationlayeractivity/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PresentationLayerActivityLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayeractivity/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}