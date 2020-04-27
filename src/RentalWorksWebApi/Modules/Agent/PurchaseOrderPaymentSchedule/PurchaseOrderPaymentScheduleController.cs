using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Agent.PurchaseOrderPaymentSchedule
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "agent-v1")]
    [FwController(Id: "NhVLHR4uMbkRQ")]
    public class PurchaseOrderPaymentScheduleController : AppDataController
    {
        public PurchaseOrderPaymentScheduleController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PurchaseOrderPaymentScheduleLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderpaymentschedule/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "NINn6NuM0wIjf", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderpaymentschedule/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "niXfO9tQv26Es", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
