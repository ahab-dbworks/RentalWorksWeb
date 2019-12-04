using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebLibrary;

namespace WebApi.Modules.HomeControls.ExchangeItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "Azkpehs1tvl")]
    public class ExchangeItemController : AppDataController
    {
        public ExchangeItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ExchangeItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/exchangeitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "ESm9Cw30r3m", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/exchangeitem/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "aYzxhc3i8TY", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/exchangeitem/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "y9uKxecOus6LK", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Pending Exchange", RwGlobals.PENDING_EXCHANGE_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
    }
}
