using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Logic;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Home.PurchaseOrderReturnItem
{
    public class ReturnItemRequest
    {
        public string ContractId;
        public string PurchaseOrderId;
        public string PurchaseOrderItemId;
        public int Quantity;
    }


    public class ReturnItemResponse : TSpStatusReponse
    {
        public string ContractId;
        public string PurchaseOrderId;
        public string PurchaseOrderItemId;
        public int Quantity;
        public double QuantityOrdered;
        public double QuantityReceived;
        public double QuantityReturned;
    }

    public class SelectAllNoneReturnItemResponse : TSpStatusReponse
    {
    }


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class PurchaseOrderReturnItemController : AppDataController
    {
        public PurchaseOrderReturnItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PurchaseOrderReturnItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreturnitem/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreturnitem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/purchaseorderreturnitem 
        //[HttpGet]
        //public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<PurchaseOrderReturnItemLogic>(pageno, pagesize, sort);
        //}
        ////------------------------------------------------------------------------------------ 
        //// GET api/v1/purchaseorderreturnitem/A0000001 
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        //{
        //    return await DoGetAsync<PurchaseOrderReturnItemLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/purchaseorderreturnitem 
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody]PurchaseOrderReturnItemLogic l)
        //{
        //    return await DoPostAsync<PurchaseOrderReturnItemLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/purchaseorderreturnitem/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id);
        //}
        //------------------------------------------------------------------------------------ 


        // POST api/v1/purchaseorderreturnitem/returnitems
        [HttpPost("returnitems")]
        public async Task<IActionResult> ReturnItems([FromBody] ReturnItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (string.IsNullOrEmpty(request.ContractId))
                {
                    throw new Exception("ContractId is required.");
                }
                else if (string.IsNullOrEmpty(request.PurchaseOrderId))
                {
                    throw new Exception("PurchaseOrderId is required.");
                }
                else if (string.IsNullOrEmpty(request.PurchaseOrderItemId))
                {
                    throw new Exception("PurchaseOrderItemId is required.");
                }
                else if (request.Quantity == 0)
                {
                    throw new Exception("Quantity cannot be zero.");
                }
                else
                {
                    ReturnItemResponse response = await AppFunc.ReturnItem(AppConfig, UserSession, request.ContractId, request.PurchaseOrderId, request.PurchaseOrderItemId, request.Quantity);
                    //if (response.success)
                    //{
                    //    return new OkObjectResult(true);
                    //}
                    //else
                    //{
                    //    throw new Exception(response.msg);
                    //}
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
        public async Task<IActionResult> SelectAll([FromBody] ReturnItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (string.IsNullOrEmpty(request.ContractId))
                {
                    throw new Exception("ContractId is required.");
                }
                else if (string.IsNullOrEmpty(request.PurchaseOrderId))
                {
                    throw new Exception("PurchaseOrderId is required.");
                }
                else
                {
                    SelectAllNoneReturnItemResponse response = await AppFunc.SelectAllReturnItem(AppConfig, UserSession, request.ContractId, request.PurchaseOrderId);
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

        // POST api/v1/purchaseorderreturnitem/selectnone
        [HttpPost("selectnone")]
        public async Task<IActionResult> SelectNone([FromBody] ReturnItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (string.IsNullOrEmpty(request.ContractId))
                {
                    throw new Exception("ContractId is required.");
                }
                else if (string.IsNullOrEmpty(request.PurchaseOrderId))
                {
                    throw new Exception("PurchaseOrderId is required.");
                }
                else
                {
                    SelectAllNoneReturnItemResponse response = await AppFunc.SelectNoneReturnItem(AppConfig, UserSession, request.ContractId, request.PurchaseOrderId);
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


    }
}
