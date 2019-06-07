using FwStandard.AppManager;
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


    public class StartLossAndDamageSessionResponse : TSpStatusResponse
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


    public class UpdateLossAndDamageItemResponse : TSpStatusResponse
    {
        public int? NewQuantity;
    }



    public class CompleteLossAndDamageSessionRequest
    {
        public string SourceOrderId;
        public string SessionId;
    }


    public class CompleteLossAndDamageSessionResponse : TSpStatusResponse
    {
    }


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"BqFEiztXXexb")]
    public class LossAndDamageController : AppDataController
    {
        public LossAndDamageController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/lossanddamage/startsession
        [HttpPost("startsession")]
        [FwControllerMethod(Id:"ZScJnowrNcdU")]
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
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 

        // POST api/v1/lossanddamage/updateitem
        [HttpPost("updateitem")]
        [FwControllerMethod(Id:"ZScJnowrNcdU")]
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
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/lossanddamage/selectall
        [HttpPost("selectall")]
        [FwControllerMethod(Id: "GE33IAKqVKgTA")]
        public async Task<ActionResult<SelectAllNoneLossAndDamageItemResponse>> SelectAll([FromBody] SelectAllNoneLossAndDamageItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SelectAllNoneLossAndDamageItemResponse response = await LossAndDamageFunc.SelectAllLossAndDamageItem(AppConfig, UserSession, request.SessionId);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/lossanddamage/selectnone
        [HttpPost("selectnone")]
        [FwControllerMethod(Id: "xAPYeO2q21xcc")]
        public async Task<ActionResult<SelectAllNoneLossAndDamageItemResponse>> SelectNone([FromBody] SelectAllNoneLossAndDamageItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SelectAllNoneLossAndDamageItemResponse response = await LossAndDamageFunc.SelectNoneLossAndDamageItem (AppConfig, UserSession, request.SessionId);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------      
        // POST api/v1/lossanddamage/completesession
        [HttpPost("completesession")]
        [FwControllerMethod(Id:"tQvfBhyuXk7i")]
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
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------     
        // POST api/v1/lossanddamage/retire
        [HttpPost("retire")]
        [FwControllerMethod(Id: "mEEALs7s4ruoY")]
        public async Task<ActionResult<RetireLossAndDamageItemResponse>> Retire([FromBody] RetireLossAndDamageItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RetireLossAndDamageItemResponse response = await LossAndDamageFunc.RetireLossAndDamageItem(AppConfig, UserSession, request.OrderId);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------      


    }
}
