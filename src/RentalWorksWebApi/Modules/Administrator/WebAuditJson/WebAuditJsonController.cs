using FwStandard.AppManager;
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
    [FwController(Id:"xepjGBf0rdL")]
    public class WebAuditJsonController : AppDataController
    {
        public WebAuditJsonController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WebAuditJsonLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/webauditjson/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"ougcPGZEIbe")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/webauditjson/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"FO6taFjUx2L")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/webauditjson/12345 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"x1umTxHzMDB")]
        public async Task<ActionResult<WebAuditJsonLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WebAuditJsonLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
