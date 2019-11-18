using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.SuspendedSession
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "AeUawGKvyGQ6")]
    public class SuspendedSessionController : AppDataController
    {
        public SuspendedSessionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SuspendedSessionLogic); }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/suspendedsession/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "kmJGmu1xRWcaT")]
        public async Task<ActionResult<SuspendedSessionLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SuspendedSessionLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/suspendedsession/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "PuCNKwQS7OJ0")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/suspendedsession/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "3rlkom7XymvVX")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
