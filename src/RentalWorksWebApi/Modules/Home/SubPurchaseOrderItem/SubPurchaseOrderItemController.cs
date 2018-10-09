using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WebApi.Logic;
using WebApi.Modules.Home.Order;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Home.SubPurchaseOrderItem
{


    public class SelectAllNonePoWorksheetItemRequest
    {
        [Required]
        public string SessionId { get; set; }
    }


    public class SelectAllNonePoWorksheetItemResponse : TSpStatusReponse
    {
    }

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class SubPurchaseOrderItemController : AppDataController
    {
        public SubPurchaseOrderItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SubPurchaseOrderItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subpurchaseorderitem/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subpurchaseorderitem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/subpurchaseorderitem 
        //[HttpGet]
        //public aync <IEnumerable<SubPurchaseOrderItemLogic>>Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<SubPurchaseOrderItemLogic>(pageno, pagesize, sort);
        //}
        ////------------------------------------------------------------------------------------ 
        // GET api/v1/subpurchaseorderitem/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<SubPurchaseOrderItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SubPurchaseOrderItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subpurchaseorderitem 
        [HttpPost]
        public async Task<ActionResult<SubPurchaseOrderItemLogic>> PostAsync([FromBody]SubPurchaseOrderItemLogic l)
        {
            return await DoPostAsync<SubPurchaseOrderItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/subpurchaseorderitem/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subpurchaseorderitem/selectall
        [HttpPost("selectall")]
        public async Task<IActionResult> SelectAll([FromBody] SelectAllNonePoWorksheetItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SelectAllNonePoWorksheetItemResponse response = await OrderFunc.SelectAllPoWorksheetItem(AppConfig, UserSession, request.SessionId);
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
        // POST api/v1/subpurchaseorderitem/selectnone
        [HttpPost("selectnone")]
        public async Task<IActionResult> SelectNone([FromBody] SelectAllNonePoWorksheetItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SelectAllNonePoWorksheetItemResponse response = await OrderFunc.SelectNonePoWorksheetItem(AppConfig, UserSession, request.SessionId);
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
