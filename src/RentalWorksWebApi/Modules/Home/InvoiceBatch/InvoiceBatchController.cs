using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Billing.InvoiceBatch
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "0NB2O1dDY0Y6")]
    public class InvoiceBatchController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public InvoiceBatchController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InvoiceBatchLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoicebatch/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "0nXEpL9S2fqp", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoicebatch/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "0O3RijMFrYMk", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
