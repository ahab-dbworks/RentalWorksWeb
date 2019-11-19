using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.VendorInvoiceStatusHistory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "laMVsOwWI4Wkj")]
    public class VendorInvoiceStatusHistoryController : AppDataController
    {
        public VendorInvoiceStatusHistoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorInvoiceStatusHistoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoicestatushistory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "V1TAyCAxmSxQ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoicestatushistory/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "orho7zzEMcp", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
