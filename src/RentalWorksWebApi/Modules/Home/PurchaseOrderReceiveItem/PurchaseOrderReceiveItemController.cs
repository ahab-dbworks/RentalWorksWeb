using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Logic;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Home.PurchaseOrderReceiveItem
{
    public class ReceiveItemRequest
    {
        public string ContractId;
        public string PurchaseOrderId;
        public string PurchaseOrderItemId;
        public int Quantity;
    }

    public class ReceiveItemResponse : TSpStatusReponse
    {
        public string ContractId;
        public string PurchaseOrderId;
        public string PurchaseOrderItemId;
        public int Quantity;
        public double QuantityOrdered;
        public double QuantityReceived;
    }


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class PurchaseOrderReceiveItemController : AppDataController
    {
        public PurchaseOrderReceiveItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PurchaseOrderReceiveItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreceiveitem/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreceiveitem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/purchaseorderreceiveitem 
        //[HttpGet]
        //public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<PurchaseOrderReceiveItemLogic>(pageno, pagesize, sort);
        //}
        ////------------------------------------------------------------------------------------ 
        //// GET api/v1/purchaseorderreceiveitem/A0000001 
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        //{
        //    return await DoGetAsync<PurchaseOrderReceiveItemLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/purchaseorderreceiveitem 
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody]PurchaseOrderReceiveItemLogic l)
        //{
        //    return await DoPostAsync<PurchaseOrderReceiveItemLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/purchaseorderreceiveitem/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id);
        //}
        //------------------------------------------------------------------------------------ 


        // POST api/v1/purchaseorderreceiveitem/receiveitems
        [HttpPost("receiveitems")]
        public async Task<IActionResult> ReceiveItems([FromBody] ReceiveItemRequest request)
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
                    ReceiveItemResponse response = await AppFunc.ReceiveItem(AppConfig, UserSession, request.ContractId, request.PurchaseOrderId, request.PurchaseOrderItemId, request.Quantity);
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

    }
}
