using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.PoRejectReason
{
    [Route("api/v1/[controller]")]
    public class PoRejectReasonController : AppDataController
    {
        public PoRejectReasonController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PoRejectReasonLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/porejectreason/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PoRejectReasonLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/porejectreason 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PoRejectReasonLogic>(pageno, pagesize, sort, typeof(PoRejectReasonLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/porejectreason/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<PoRejectReasonLogic>(id, typeof(PoRejectReasonLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/porejectreason 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PoRejectReasonLogic l)
        {
            return await DoPostAsync<PoRejectReasonLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/porejectreason/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PoRejectReasonLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/porejectreason/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
} 
