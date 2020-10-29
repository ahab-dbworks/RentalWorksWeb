using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Inventory.InventoryQuantityHistory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "inventory-v1")]
    [FwController(Id: "0NnVn0knqSjPO")]
    public class InventoryQuantityHistoryController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public InventoryQuantityHistoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryQuantityHistoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryquantityhistory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "0NshrseqZrqkE  ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryquantityhistory/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "0NTOEAHNw5XKw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
