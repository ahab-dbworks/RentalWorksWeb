using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Reports.InventoryChangeTransactionType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "ouvRAXhgm8fGQ")]
    public class InventoryChangeTransactionTypeController : AppDataController
    {
        public InventoryChangeTransactionTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryChangeTransactionTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorychangetransactiontype/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "TIJqaehByFLaG")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorychangetransactiontype/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "s6JfJuHjCWMRe")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
