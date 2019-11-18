using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.InvoiceStatusHistory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "3bf1WgNHvIyF")]
    public class InvoiceStatusHistoryController : AppDataController
    {
        public InvoiceStatusHistoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InvoiceStatusHistoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoicestatushistory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "ad7aGrlghjl5V")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoicestatushistory/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "79s0dJOdoaxR8")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
