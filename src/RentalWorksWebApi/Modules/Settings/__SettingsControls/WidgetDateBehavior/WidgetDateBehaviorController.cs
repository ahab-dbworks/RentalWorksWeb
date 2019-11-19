using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;

namespace WebApi.Modules.Settings.WidgetDateBehavior
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "OgvBedVmWbO5d")]
    public class WidgetDateBehaviorController : AppDataController
    {
        public WidgetDateBehaviorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WidgetDateBehaviorLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/widgetdatebehavior/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "I6NRcnLWzujs", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/widgetdatebehavior/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "tQeFrHtW1FzO", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
