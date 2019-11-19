using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Administrator.Hotfix
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id:"yeqaGIUYfYNX")]
    public class HotfixController : AppDataController
    {
        public HotfixController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(HotfixLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/hotfix/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"oLM5HAWqXKYK", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/hotfix/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"orzWOp5ywsAh", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/hotfix 
        [HttpGet]
        [FwControllerMethod(Id:"OjHNXPRvFVam", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<HotfixLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<HotfixLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/hotfix/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"C0lTLbp3FIQl", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<HotfixLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<HotfixLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
