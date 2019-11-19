using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Utilities.GLDistribution
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "UuKB0PPalR9p")]
    public class GLDistributionController : AppDataController
    {
        public GLDistributionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GLDistributionLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/gldistribution/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "xTCi5ChMTPIV", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/gldistribution/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "vTgh32ZRwrxO", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/gldistribution/refresh
        [HttpPost("refresh")]
        [FwControllerMethod(Id: "89F50711MXa", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<RefreshGLHistoryResponse>> RefreshGLHistory([FromBody]RefreshGLHistoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RefreshGLHistoryResponse response = await GLDistributionFunc.RefreshGLHistory(AppConfig, UserSession, request);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
