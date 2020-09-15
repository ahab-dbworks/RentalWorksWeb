using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Inventory.InventorySummaryRetiredHistory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "5LpDkxSK6jqMz")]
    public class InventorySummaryRetiredHistoryController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public InventorySummaryRetiredHistoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventorySummaryRetiredHistoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysummaryretiredhistory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "5REwQvlLDLHQM", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysummaryretiredhistory/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "5rFLQoPT4KpoR", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
