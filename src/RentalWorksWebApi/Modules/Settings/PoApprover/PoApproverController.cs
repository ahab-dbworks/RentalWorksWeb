using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.PoApprover
{
    [Route("api/v1/[controller]")]
    public class PoApproverController : AppDataController
    {
        public PoApproverController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PoApproverLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/poapprover/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PoApproverLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/poapprover 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PoApproverLogic>(pageno, pagesize, sort, typeof(PoApproverLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/poapprover/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<PoApproverLogic>(id, typeof(PoApproverLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/poapprover 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PoApproverLogic l)
        {
            return await DoPostAsync<PoApproverLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/poapprover/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PoApproverLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/poapprover/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}