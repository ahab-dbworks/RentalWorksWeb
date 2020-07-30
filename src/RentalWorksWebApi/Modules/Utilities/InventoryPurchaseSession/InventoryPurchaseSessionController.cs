using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Utilities.InventoryPurchaseSession
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "TvlNyYBNKI36V")]
    public class InventoryPurchaseSessionController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public InventoryPurchaseSessionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryPurchaseSessionLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorypurchasesession/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "tVNCLEjjp8GZw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorypurchasesession/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "TvNFPpXxIHi0N", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorypurchasesession/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "0rXIuaQdNVLfU", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryPurchaseSessionLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
