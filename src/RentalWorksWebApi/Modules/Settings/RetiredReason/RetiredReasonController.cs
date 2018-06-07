using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.RetiredReason
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class RetiredReasonController : AppDataController
    {
        public RetiredReasonController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RetiredReasonLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/retiredreason/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(RetiredReasonLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/retiredreason
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RetiredReasonLogic>(pageno, pagesize, sort, typeof(RetiredReasonLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/retiredreason/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<RetiredReasonLogic>(id, typeof(RetiredReasonLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/retiredreason
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]RetiredReasonLogic l)
        {
            return await DoPostAsync<RetiredReasonLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/retiredreason/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(RetiredReasonLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/retiredreason/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}