using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.Modules.Administrator.WebAuditJson;

namespace WebApi.Modules.Administrator.WebAuditJson
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    public class WebAuditJsonController : AppDataController
    {
        public WebAuditJsonController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WebAuditJsonLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/webauditjson/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/webauditjson/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/webauditjson 
        //[HttpGet]
        //public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<WebAuditJsonLogic>(pageno, pagesize, sort);
        //}
        ////------------------------------------------------------------------------------------ 
        // GET api/v1/webauditjson/12345 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WebAuditJsonLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/webauditjson 
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody]WebAuditJsonLogic l)
        //{
        //    return await DoPostAsync<WebAuditJsonLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/webauditjson/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
