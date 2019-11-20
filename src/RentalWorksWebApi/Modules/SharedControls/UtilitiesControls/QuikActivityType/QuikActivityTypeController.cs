using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using FwStandard.AppManager;
using WebApi.Modules.Utilities.QuikActivityType;

namespace WebApi.Modules.UtilitiesControls.QuikActivityType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "LDBjvAnswvvY")]
    public class QuikActivityTypeController : AppDataController
    {
        public QuikActivityTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(QuikActivityTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/quikactivitytype/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "nn6L7D18xH93", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/auikactivitytype/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "0RUvheRQ5Bgn", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
