using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.PresentationLayerActivityOverride
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class PresentationLayerActivityOverrideController : AppDataController
    {
        public PresentationLayerActivityOverrideController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PresentationLayerActivityOverrideLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayeractivityoverride/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PresentationLayerActivityOverrideLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayeractivityoverride 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PresentationLayerActivityOverrideLogic>(pageno, pagesize, sort, typeof(PresentationLayerActivityOverrideLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayeractivityoverride/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<PresentationLayerActivityOverrideLogic>(id, typeof(PresentationLayerActivityOverrideLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayeractivityoverride 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PresentationLayerActivityOverrideLogic l)
        {
            return await DoPostAsync<PresentationLayerActivityOverrideLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/presentationlayeractivityoverride/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PresentationLayerActivityOverrideLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayeractivityoverride/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}