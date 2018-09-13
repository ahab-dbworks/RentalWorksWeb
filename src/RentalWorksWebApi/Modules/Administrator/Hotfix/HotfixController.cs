using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Administrator.Hotfix
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    public class HotfixController : AppDataController
    {
        public HotfixController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(HotfixLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/hotfix/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/hotfix/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/hotfix 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<HotfixLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/hotfix/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<HotfixLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/hotfix 
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody]HotfixLogic l)
        //{
        //    return await DoPostAsync<HotfixLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/hotfix/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
