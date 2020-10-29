using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Inventory.PurchaseRetired
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "inventory-v1")]
    [FwController(Id: "0F5WIgzZ01nd")]
    public class PurchaseRetiredController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public PurchaseRetiredController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PurchaseRetiredLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseretired/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "0F9qEC4h7kB0h  ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseretired/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "0fFHORU7GveJ0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
