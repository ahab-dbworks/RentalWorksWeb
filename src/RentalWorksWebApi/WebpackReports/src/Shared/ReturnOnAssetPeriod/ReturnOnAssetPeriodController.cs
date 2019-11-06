using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Reports.Shared.ReturnOnAssetPeriod
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "7GRHZMC4iFV0")]
    public class ReturnOnAssetPeriodController : AppDataController
    {
        public ReturnOnAssetPeriodController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ReturnOnAssetPeriodLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/returnonassetperiod/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "Us1Igvm4W9As")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/returnonassetperiod/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "twIbejgMs1ff")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
