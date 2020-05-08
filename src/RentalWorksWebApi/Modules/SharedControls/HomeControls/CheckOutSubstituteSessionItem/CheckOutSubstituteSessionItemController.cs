using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.AgentX.CheckOutSubstituteSessionItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "agentx-v1")]
    [FwController(Id: "qCquw4GIfqRW5")]
    public class CheckOutSubstituteSessionItemController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public CheckOutSubstituteSessionItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CheckOutSubstituteSessionItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkoutsubstitutesessionitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "qctJVn2VrftXo", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkoutsubstitutesessionitem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "QCU1rjHSBKBWE", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
