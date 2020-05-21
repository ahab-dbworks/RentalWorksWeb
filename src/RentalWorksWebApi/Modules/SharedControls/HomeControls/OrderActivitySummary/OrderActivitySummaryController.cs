using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.OrderActivitySummary
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "agent-v1")]
    [FwController(Id: "anBvrz1T2ipsv")]
    public class OrderActivitySummaryController : AppDataController
    {
        public OrderActivitySummaryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderActivitySummaryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderactivitysummary/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "aNdT4NZGuXaBU", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderactivitysummary/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "ANiSJKk4OzaKq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
