using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.HomeControls.StageHoldingItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "i7EMskpGXvByc")]
    public class StageHoldingItemController : AppDataController
    {
        public StageHoldingItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(StageHoldingItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/stageholdingitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "Tg1JfTktaINAN", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/stageholdingitem/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "CBxkT6tcJWbKX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/stageholdingitem/selectall
        //[HttpPost("selectall")]
        //[FwControllerMethod(Id:"lsOufDQo0Mew", ActionType: FwControllerActionTypes.Option)]
        //public async Task<ActionResult<SelectAllNoneStageHoldingItemResponse>> SelectAll([FromBody] SelectAllNoneStageHoldingItemRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        SelectAllNoneStageHoldingItemResponse response = await CheckOutFunc.SelectAllStageHoldingItem(AppConfig, UserSession, request.OrderId);
        //        return new OkObjectResult(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        FwApiException jsonException = new FwApiException();
        //        jsonException.StatusCode = StatusCodes.Status500InternalServerError;
        //        jsonException.Message = ex.Message;
        //        jsonException.StackTrace = ex.StackTrace;
        //        return StatusCode(jsonException.StatusCode, jsonException);
        //    }
        //}
        ////------------------------------------------------------------------------------------        

        //// POST api/v1/stageholdingitem/selectnone
        //[HttpPost("selectnone")]
        //[FwControllerMethod(Id:"0nqu4VvVBAxY", ActionType: FwControllerActionTypes.Option)]
        //public async Task<ActionResult<SelectAllNoneStageHoldingItemResponse>> SelectNone([FromBody] SelectAllNoneStageHoldingItemRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        SelectAllNoneStageHoldingItemResponse response = await CheckOutFunc.SelectNoneStageHoldingItem(AppConfig, UserSession, request.OrderId);
        //        return new OkObjectResult(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        FwApiException jsonException = new FwApiException();
        //        jsonException.StatusCode = StatusCodes.Status500InternalServerError;
        //        jsonException.Message = ex.Message;
        //        jsonException.StackTrace = ex.StackTrace;
        //        return StatusCode(jsonException.StatusCode, jsonException);
        //    }
        //}
        ////------------------------------------------------------------------------------------        

    }
}
