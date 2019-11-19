using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Warehouse.CheckOut;

namespace WebApi.Modules.HomeControls.StageQuantityItem
{

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"0m0QMviBYWVYm")]
    public class StageQuantityItemController : AppDataController
    {
        public StageQuantityItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(StageQuantityItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/stagequantityitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"uUlS77AJ2aviZ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/stagequantityitem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"Sj7c8TbywPDKs", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/stagequantityitem/selectall
        [HttpPost("selectall")]
        [FwControllerMethod(Id:"EpllUTJNEtPV", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<SelectAllNoneStageQuantityItemResponse>> SelectAll([FromBody] SelectAllNoneStageQuantityItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SelectAllNoneStageQuantityItemResponse response = await CheckOutFunc.SelectAllStageQuantityItem(AppConfig, UserSession, request.OrderId);
                return new OkObjectResult(response);
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

        // POST api/v1/stagequantityitem/selectnone
        [HttpPost("selectnone")]
        [FwControllerMethod(Id:"khDDULtg1hwsQ", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<SelectAllNoneStageQuantityItemResponse>> SelectNone([FromBody] SelectAllNoneStageQuantityItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SelectAllNoneStageQuantityItemResponse response = await CheckOutFunc.SelectNoneStageQuantityItem(AppConfig, UserSession, request.OrderId);
                return new OkObjectResult(response);
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
