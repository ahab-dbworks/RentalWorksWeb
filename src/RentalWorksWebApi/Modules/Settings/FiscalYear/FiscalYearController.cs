using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.FiscalYear
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class FiscalYearController : AppDataController
    {
        public FiscalYearController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FiscalYearLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/fiscalyear/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(FiscalYearLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/fiscalyear 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FiscalYearLogic>(pageno, pagesize, sort, typeof(FiscalYearLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/fiscalyear/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<FiscalYearLogic>(id, typeof(FiscalYearLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/fiscalyear 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]FiscalYearLogic l)
        {
            return await DoPostAsync<FiscalYearLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/fiscalyear/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(FiscalYearLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/fiscalyear/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}