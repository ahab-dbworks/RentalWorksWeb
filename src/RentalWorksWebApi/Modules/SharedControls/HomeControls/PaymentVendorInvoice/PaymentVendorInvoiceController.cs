using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.PaymentVendorInvoice
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "ZPBh9DI665Xay")]
    public class PaymentVendorInvoiceController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public PaymentVendorInvoiceController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PaymentVendorInvoiceLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/paymentvendorinvoice/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "zr9yo5tR0pUiJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/paymentvendorinvoice/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "zrGDMwhD2yxFA", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
