using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.PresentationLayerForm
{
    [Route("api/v1/[controller]")]
    public class PresentationLayerFormController : AppDataController
    {
        public PresentationLayerFormController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PresentationLayerFormLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayerform/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PresentationLayerFormLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayerform 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PresentationLayerFormLogic>(pageno, pagesize, sort, typeof(PresentationLayerFormLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayerform/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<PresentationLayerFormLogic>(id, typeof(PresentationLayerFormLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayerform 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PresentationLayerFormLogic l)
        {
            return await DoPostAsync<PresentationLayerFormLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/presentationlayerform/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PresentationLayerFormLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayerform/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}