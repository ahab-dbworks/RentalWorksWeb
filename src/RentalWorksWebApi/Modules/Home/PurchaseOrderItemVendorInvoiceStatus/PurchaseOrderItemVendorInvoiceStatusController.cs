using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Agent.PurchaseOrderItemVendorInvoiceStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "1u1xfN9Ur5g8")]
    public class PurchaseOrderItemVendorInvoiceStatusController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public PurchaseOrderItemVendorInvoiceStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PurchaseOrderItemVendorInvoiceStatusLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderitemvendorinvoicestatus/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "1ujMsn8hoCKV", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderitemvendorinvoicestatus/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "1UzQitVOnFHj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
