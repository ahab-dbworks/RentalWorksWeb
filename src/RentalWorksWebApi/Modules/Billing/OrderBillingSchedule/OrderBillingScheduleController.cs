using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WebApi.Controllers;
namespace WebApi.Modules.Billing.OrderBillingSchedule
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "uOnqzcfEDJnJ")]
    public class OrderBillingScheduleController : AppDataController
    {
        public OrderBillingScheduleController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderBillingScheduleLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderbillingschedule/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "SMwUGjBH51oC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderbillingschedule/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "wpgDJppjDuNC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
