using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
using System;

namespace WebApi.Modules.Home.GLDistribution
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
        [FwControllerMethod(Id: "xTCi5ChMTPIV")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/gldistribution/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "vTgh32ZRwrxO")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/gldistribution/refresh
        [HttpPost("refresh")]
        [FwControllerMethod(Id: "89F50711MXa")]
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
