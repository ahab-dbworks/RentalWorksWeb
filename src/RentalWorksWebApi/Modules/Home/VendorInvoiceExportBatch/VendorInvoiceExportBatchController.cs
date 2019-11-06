using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.VendorInvoiceExportBatch
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "QriRQnYpPbxn")]
    public class VendorInvoiceExportBatchController : AppDataController
    {
        public VendorInvoiceExportBatchController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorInvoiceExportBatchLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceexportbatch/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "ybf8DYKHJ4mX")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceexportbatch/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "yyQ0W8cSiVq9")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
