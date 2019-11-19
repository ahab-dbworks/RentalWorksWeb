using FwStandard.AppManager;
using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.AvailableWidget
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "Ji2Jpet9c6oZ5")]
    public class AvailableWidgetController : AppDataController
    {
        public AvailableWidgetController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(AvailableWidgetLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/availablewidget/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "Tp2E3Tyyl1xwz", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/availablewidget/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "B1GQ3pvfPjzGn", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
