using FwStandard.SqlServer;
using System.Collections.Generic;
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
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/webauditjson/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/webauditjson 
        //[HttpGet]
        //public aync <IEnumerable<WebAuditJsonLogic>>Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<WebAuditJsonLogic>(pageno, pagesize, sort);
        //}
        ////------------------------------------------------------------------------------------ 
        // GET api/v1/webauditjson/12345 
        [HttpGet("{id}")]
        public async Task<ActionResult<WebAuditJsonLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WebAuditJsonLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
