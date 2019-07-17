using FwStandard.AppManager;
using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using WebApi.Logic;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Home.ContractItemDetail
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"uJtRkkpKi8zT")]
    public class ContractItemDetailController : AppDataController
    {
        public ContractItemDetailController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ContractItemDetailLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/contractitemdetail/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"xppzinq0cvq4")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/contractitemdetail/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"YAvXaedfvmus")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/contractitemdetail/voiditems
        [HttpPost("voiditems")]
        [FwControllerMethod(Id: "pbZeqJ3pd8r")]
        public async Task<ActionResult<TSpStatusResponse>> CancelContract([FromBody]VoidItemsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TSpStatusResponse response = await ContractItemDetailFunc.VoidItems(AppConfig, UserSession, request);
                return response;
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
