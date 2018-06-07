using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.CoverLetter
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class CoverLetterController : AppDataController
    {
        public CoverLetterController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CoverLetterLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/coverletter/browse
        [HttpPost("browse")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CoverLetterLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/coverletter
        [HttpGet]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CoverLetterLogic>(pageno, pagesize, sort, typeof(CoverLetterLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/coverletter/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<CoverLetterLogic>(id, typeof(CoverLetterLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/coverletter
        [HttpPost]
        [Authorize(Policy = "")]
        public async Task<IActionResult> PostAsync([FromBody]CoverLetterLogic l)
        {
            return await DoPostAsync<CoverLetterLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/coverletter/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(CoverLetterLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/coverletter/validateduplicate
        [HttpPost("validateduplicate")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request) => await DoValidateDuplicateAsync(request);
        //------------------------------------------------------------------------------------
    }
}