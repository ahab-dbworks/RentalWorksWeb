using FwStandard.SqlServer;
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

namespace WebApi.Modules.Home.PurchaseOrderReceiveItem
{
    public class ReceiveItemRequest
    {
        [Required]
        public string ContractId { get; set; }
        [Required]
        public string PurchaseOrderId { get; set; }
        [Required]
        public string PurchaseOrderItemId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }

    public class ReceiveItemResponse : TSpStatusReponse
    {
        public string ContractId;
        public string PurchaseOrderId;
        public string PurchaseOrderItemId;
        public int Quantity;
        public double QuantityOrdered;
        public double QuantityReceived;
        public double QuantityNeedBarCode;
        public string QuantityColor;
    }


    public class SelectAllNoneReceiveItemRequest
    {
        [Required]
        public string ContractId { get; set; }
        [Required]
        public string PurchaseOrderId { get; set; }
    }


    public class SelectAllNoneReceiveItemResponse : TSpStatusReponse
    {
    }


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class PurchaseOrderReceiveItemController : AppDataController
    {
        public PurchaseOrderReceiveItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PurchaseOrderReceiveItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreceiveitem/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreceiveitem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreceiveitem/receiveitems
        [HttpPost("receiveitems")]
        public async Task<ActionResult<ReceiveItemResponse>> ReceiveItems([FromBody] ReceiveItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (request.Quantity == 0)
                {
                    throw new Exception("Quantity cannot be zero.");
                }
                else
                {
                    ReceiveItemResponse response = await PurchaseOrderFunc.ReceiveItem(AppConfig, UserSession, request.ContractId, request.PurchaseOrderId, request.PurchaseOrderItemId, request.Quantity);
                    return new OkObjectResult(response);
                }
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
        // POST api/v1/purchaseorderreceiveitem/selectall
        [HttpPost("selectall")]
        public async Task<ActionResult<SelectAllNoneReceiveItemResponse>> SelectAll([FromBody] SelectAllNoneReceiveItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SelectAllNoneReceiveItemResponse response = await PurchaseOrderFunc.SelectAllReceiveItem(AppConfig, UserSession, request.ContractId, request.PurchaseOrderId);
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

        // POST api/v1/purchaseorderreceiveitem/selectnone
        [HttpPost("selectnone")]
        public async Task<ActionResult<SelectAllNoneReceiveItemResponse>> SelectNone([FromBody] SelectAllNoneReceiveItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SelectAllNoneReceiveItemResponse response = await PurchaseOrderFunc.SelectNoneReceiveItem(AppConfig, UserSession, request.ContractId, request.PurchaseOrderId);
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
