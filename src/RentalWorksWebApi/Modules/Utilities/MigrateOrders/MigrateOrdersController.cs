using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Logic;
using System;
using Microsoft.AspNetCore.Http;
using WebApi.Modules.Home.LossAndDamage;

namespace WebApi.Modules.Utilities.MigrateOrders
{

    public class StartMigrateOrdersSessionRequest
    {
        public string DealId;
        public string WarehouseId;
        public string OrderIds;
    }


    public class StartMigrateOrdersSessionResponse : TSpStatusResponse
    {
        public string SessionId;
    }


    public class UpdateMigrateOrdersItemRequest
    {
        public string SessionId;
        public string OrderId;
        public string OrderItemId;
        public string BarCode;
        public int? Quantity;
    }


    public class UpdateMigrateOrdersItemResponse : TSpStatusResponse
    {
        public int? NewQuantity;
    }



    public class CompleteMigrateOrdersSessionRequest
    {
        public string SourceOrderId;
        public string SessionId;
    }


    public class CompleteMigrateOrdersSessionResponse : TSpStatusResponse
    {
    }


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "8NYSNibMVoO")]
    public class MigrateOrdersController : AppDataController
    {
        public MigrateOrdersController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/migrateorders/startsession
        [HttpPost("startsession")]
        [FwControllerMethod(Id: "vuCrJ6PMa3n")]
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

        // POST api/v1/migrateorders/updateitem
        [HttpPost("updateitem")]
        [FwControllerMethod(Id: "H3vFKzK6VTZ")]
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
        // POST api/v1/migrateorders/selectall
        [HttpPost("selectall")]
        [FwControllerMethod(Id: "6nxMKPPccQq")]
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
        // POST api/v1/migrateorders/selectnone
        [HttpPost("selectnone")]
        [FwControllerMethod(Id: "VvtDKiyfXyh")]
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
        // POST api/v1/migrateorders/completesession
        [HttpPost("completesession")]
        [FwControllerMethod(Id: "PWJiNSDvo8Z")]
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
        // POST api/v1/migrateorders/retire
        [HttpPost("retire")]
        [FwControllerMethod(Id: "fAJpKrRhUGg")]
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
