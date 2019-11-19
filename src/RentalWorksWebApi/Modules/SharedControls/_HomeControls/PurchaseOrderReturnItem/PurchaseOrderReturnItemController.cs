using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Logic;
using WebApi.Modules.Agent.PurchaseOrder;

namespace WebApi.Modules.HomeControls.PurchaseOrderReturnItem
{
    public class ReturnItemRequest
    {
        [Required]
        public string ContractId { get; set; }
        [Required]
        public string PurchaseOrderId { get; set; }
        //[Required]
        public string PurchaseOrderItemId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string BarCode { get; set; }
    }


    public class ReturnItemResponse : TSpStatusResponse
    {
        public string ContractId;
        public string PurchaseOrderId;
        public string PurchaseOrderItemId;
        public int Quantity;
        public double QuantityOrdered;
        public double QuantityReceived;
        public double QuantityReturned;
    }

    public class SelectAllNoneReturnItemRequest
    {
        [Required]
        public string ContractId { get; set; }
        [Required]
        public string PurchaseOrderId { get; set; }
    }


    public class SelectAllNoneReturnItemResponse : TSpStatusResponse
    {
    }


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"wND2psEV3OEia")]
    public class PurchaseOrderReturnItemController : AppDataController
    {
        public PurchaseOrderReturnItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PurchaseOrderReturnItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreturnitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"Q9Vkiqy0kpc9j", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreturnitem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"ZXQmTbT88kNtY", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreturnitem/returnitems
        [HttpPost("returnitems")]
        [FwControllerMethod(Id:"Qq7dEVLcAm4vQ", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<ReturnItemResponse>> ReturnItems([FromBody] ReturnItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if ((request.Quantity == 0) && (string.IsNullOrEmpty(request.BarCode)))
                {
                    throw new Exception("Must supply a non-zero Quantity or a Bar Code.");
                }
                else if (string.IsNullOrEmpty(request.PurchaseOrderItemId) && (string.IsNullOrEmpty(request.BarCode)))
                {
                    throw new Exception("Must supply a PO line-item ID or a Bar Code.");
                }
                else
                {
                    ReturnItemResponse response = await PurchaseOrderFunc.ReturnItem(AppConfig, UserSession, request.ContractId, request.PurchaseOrderId, request.PurchaseOrderItemId, request.Quantity, request.BarCode);
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
        // POST api/v1/purchaseorderreturnitem/selectall
        [HttpPost("selectall")]
        [FwControllerMethod(Id:"DbE8sDn09QI1", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<SelectAllNoneReturnItemResponse>> SelectAll([FromBody] SelectAllNoneReturnItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SelectAllNoneReturnItemResponse response = await PurchaseOrderFunc.SelectAllReturnItem(AppConfig, UserSession, request.ContractId, request.PurchaseOrderId);
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

        // POST api/v1/purchaseorderreturnitem/selectnone
        [HttpPost("selectnone")]
        [FwControllerMethod(Id:"x7iB3kcAYOOuH", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<SelectAllNoneReturnItemResponse>> SelectNone([FromBody] SelectAllNoneReturnItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SelectAllNoneReturnItemResponse response = await PurchaseOrderFunc.SelectNoneReturnItem(AppConfig, UserSession, request.ContractId, request.PurchaseOrderId);
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
