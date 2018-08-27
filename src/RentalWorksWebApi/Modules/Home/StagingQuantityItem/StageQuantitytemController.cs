using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Logic;
using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using WebApi.Modules.Home.PurchaseOrder;

namespace WebApi.Modules.Home.StageQuantityItem
{
    //public class SelectAllNoneStageQuantityItemRequest
    //{
    //    [Required]
    //    public string ContractId { get; set; }
    //    [Required]
    //    public string PurchaseOrderId { get; set; }
    //}


    //public class SelectAllNoneStageQuantityItemResponse : TSpStatusReponse
    //{
    //}


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class StageQuantityItemController : AppDataController
    {
        public StageQuantityItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(StageQuantityItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/stagequantityitem/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/stagequantityitem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/stagequantityitem/selectall
        //[HttpPost("selectall")]
        //public async Task<IActionResult> SelectAll([FromBody] SelectAllNoneReceiveItemRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        SelectAllNoneReceiveItemResponse response = await PurchaseOrderFunc.SelectAllReceiveItem(AppConfig, UserSession, request.ContractId, request.PurchaseOrderId);
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

        //// POST api/v1/stagequantityitem/selectnone
        //[HttpPost("selectnone")]
        //public async Task<IActionResult> SelectNone([FromBody] SelectAllNoneReceiveItemRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        SelectAllNoneReceiveItemResponse response = await PurchaseOrderFunc.SelectNoneReceiveItem(AppConfig, UserSession, request.ContractId, request.PurchaseOrderId);
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
