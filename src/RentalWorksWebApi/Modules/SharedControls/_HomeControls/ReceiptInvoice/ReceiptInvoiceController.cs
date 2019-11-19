using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;

namespace WebApi.Modules.HomeControls.ReceiptInvoice
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "VNl76Q0KpJk31")]
    public class ReceiptInvoiceController : AppDataController
    {
        public ReceiptInvoiceController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ReceiptInvoiceLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receiptinvoice/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "FQKNknibRlh")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receiptinvoice/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "TxHBxvExNQV")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 

    }
}
