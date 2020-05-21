using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Inventory.ContractHistory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "inventory-v1")]
    [FwController(Id: "fY1Au6CjXlodD")]
    public class ContractHistoryController : AppDataController
    {
        public ContractHistoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ContractHistoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/contracthistory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "FYAGKBdIe3KVC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/contracthistory/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "fYBbwRHQ8AYoC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
