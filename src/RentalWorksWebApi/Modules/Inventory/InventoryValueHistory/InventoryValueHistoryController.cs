using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Inventory.InventoryValueHistory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "inventory-v1")]
    [FwController(Id: "0NnVn0knqSjPO")]
    public class InventoryValueHistoryController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public InventoryValueHistoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryValueHistoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryvaluehistory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "0NshrseqZrqkE", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryvaluehistory/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "0NTOEAHNw5XKw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
