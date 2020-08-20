using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Utilities.CurrencyMissing
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "WxhDqX5BH6yzv")]
    public class CurrencyMissingController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public CurrencyMissingController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CurrencyMissingLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/currencymissing/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "WXHjblwA56ZUm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/currencymissing/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "WZQJH01jkpDmt", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
