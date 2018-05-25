using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.PoType
{
    [Route("api/v1/[controller]")]
    public class PoTypeController : AppDataController
    {
        public PoTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PoTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/potype/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PoTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/potype 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PoTypeLogic>(pageno, pagesize, sort, typeof(PoTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/potype/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<PoTypeLogic>(id, typeof(PoTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/potype 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PoTypeLogic l)
        {
            return await DoPostAsync<PoTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/potype/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PoTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/potype/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}