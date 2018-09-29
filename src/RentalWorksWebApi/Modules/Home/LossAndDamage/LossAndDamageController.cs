using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Logic;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Home.LossAndDamage
{

    public class StartLossAndDamageSessionRequest
    {
        public string DealId;
        public string WarehouseId;
        public string OrderIds;
    }


    public class StartLossAndDamageSessionResponse : TSpStatusReponse
    {
        public string SessionId;
    }


    public class UpdateLossAndDamageItemRequest
    {
        public string SessionId;
        public string OrderId;
        public string OrderItemId;
        public string BarCode;
        public int? Quantity;
    }


    public class UpdateLossAndDamageItemResponse : TSpStatusReponse
    {
        public int? NewQuantity;
    }



    public class CompleteLossAndDamageSessionRequest
    {
        public string SourceOrderId;
        public string SessionId;
    }


    public class CompleteLossAndDamageSessionResponse : TSpStatusReponse
    {
    }


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class LossAndDamageController : AppDataController
    {
        public LossAndDamageController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/lossanddamage/startsession
        [HttpPost("startsession")]
        public async Task<ActionResult<StartLossAndDamageSessionResponse>> StartSession([FromBody]StartLossAndDamageSessionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                StartLossAndDamageSessionResponse response = new StartLossAndDamageSessionResponse();
                if (string.IsNullOrEmpty(request.DealId))
                {
                    response.success = false;
                    response.msg = "DealId is required.";
                }
                else if (string.IsNullOrEmpty(request.WarehouseId))
                {
                    response.success = false;
                    response.msg = "WarehouseId is required.";
                }
                else
                {
                    response = await LossAndDamageFunc.StartSession(AppConfig, UserSession, request);
                }

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

        // POST api/v1/lossanddamage/updateitem
        [HttpPost("updateitem")]
        public async Task<ActionResult<UpdateLossAndDamageItemResponse>> StartSession([FromBody]UpdateLossAndDamageItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UpdateLossAndDamageItemResponse response = new UpdateLossAndDamageItemResponse();
                if (string.IsNullOrEmpty(request.SessionId))
                {
                    response.success = false;
                    response.msg = "SessionId is required.";
                }
                else if (string.IsNullOrEmpty(request.OrderId))
                {
                    response.success = false;
                    response.msg = "OrderId is required.";
                }
                else if (string.IsNullOrEmpty(request.OrderItemId))
                {
                    response.success = false;
                    response.msg = "OrderItemId is required.";
                }
                else
                {
                    response = await LossAndDamageFunc.UpdateItem(AppConfig, UserSession, request);
                }

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
        // POST api/v1/lossanddamage/completesession
        [HttpPost("completesession")]
        public async Task<ActionResult<CompleteLossAndDamageSessionResponse>> CompleteSession([FromBody]CompleteLossAndDamageSessionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CompleteLossAndDamageSessionResponse response = new CompleteLossAndDamageSessionResponse();
                if (string.IsNullOrEmpty(request.SourceOrderId))
                {
                    response.success = false;
                    response.msg = "SourceOrderId is required.";
                }
                else if (string.IsNullOrEmpty(request.SessionId))
                {
                    response.success = false;
                    response.msg = "SessionId is required.";
                }
                else
                {
                    response = await LossAndDamageFunc.CompleteSession(AppConfig, UserSession, request);
                }

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
