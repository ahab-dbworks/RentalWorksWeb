using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System;
using Microsoft.AspNetCore.Http;
using WebApi.Home.CheckOut;

namespace WebApi.Modules.Administrator.CheckOut
{

    public class StageItemRequest
    {
        public string OrderId;
        public string OrderItemId;
        public string Code;
        public int? Quantity;
    }


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class CheckOutController : AppDataController
    {
        public CheckOutController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkout/stageitem?orderid=A0000001&
        [HttpPost("stageitem")]
        public async Task<IActionResult> StageItem([FromBody]StageItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (string.IsNullOrEmpty(request.OrderId))
                {
                    throw new Exception("OrderId is required.");
                }
                if ((string.IsNullOrEmpty(request.OrderItemId)) && (string.IsNullOrEmpty(request.Code)))
                {
                    throw new Exception("Must supply a Code or OrderItemId to stage items.");
                }


                TStageItemReponse stageItemResponse = await CheckOutFunc.StageItem(AppConfig, UserSession, request.OrderId, request.Code, request.Quantity);
                if (stageItemResponse.success)
                {
                    return new OkObjectResult(stageItemResponse);
                }
                else
                {
                    throw new Exception(stageItemResponse.msg);
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