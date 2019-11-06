using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Reports.Shared.ReturnOnAssetYear
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "Udgb5ZYgGpFvl")]
    public class ReturnOnAssetYearController : AppDataController
    {
        public ReturnOnAssetYearController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ReturnOnAssetYearLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/returnonassetyear/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "vv3us5MMvKeT")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/returnonassetyear/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "qEYHCvlGeklG")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
