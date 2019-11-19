using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;

namespace WebApi.Modules.HomeControls.ReceiptCredit
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "NUrTXlXrPpZHZ")]
    public class ReceiptCreditController : AppDataController
    {
        public ReceiptCreditController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ReceiptCreditLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receiptcredit/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "l32UVfhkcJ50N")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receiptcredit/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "xVJRTR3frhJfJ")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
