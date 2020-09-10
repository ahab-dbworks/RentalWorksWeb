using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Inventory.InventorySummaryStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "0LZv8tP11itN2")]
    public class InventorySummaryStatusController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public InventorySummaryStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventorySummaryStatusLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysummarystatus/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "0RWc0CZ68dLMC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysummarystatus/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "14xHKqLh7OS0i", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
