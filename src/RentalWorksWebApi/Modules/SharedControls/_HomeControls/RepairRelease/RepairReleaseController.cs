using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.HomeControls.RepairRelease
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"O2lL9RZYzdjNg")]
    public class RepairReleaseController : AppDataController
    {
        public RepairReleaseController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RepairReleaseLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairrelease/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"r5OW5cEY7vNvv", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"0x3s2ZpzaBwiL", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repairrelease 
        [HttpGet]
        [FwControllerMethod(Id:"QqR9fe09CVN6w", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<RepairReleaseLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RepairReleaseLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repairrelease/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"jMspODXjsJxLo", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<RepairReleaseLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RepairReleaseLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
