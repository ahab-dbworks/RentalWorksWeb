using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Logic;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Utilities.Migrate
{

    public class StartMigrateSessionRequest
    {
        public string DealId { get; set; }
        public string DepartmentId { get; set; }
        public string OrderIds { get; set; }
    }


    public class StartMigrateSessionResponse : TSpStatusResponse
    {
        public string SessionId;
    }


    public class UpdateMigrateItemRequest
    {
        public string SessionId;
        public string OrderId;
        public string OrderItemId;
        public string BarCode;
        public int? Quantity;
    }


    public class UpdateMigrateItemResponse : TSpStatusResponse
    {
        public int? NewQuantity;
    }



    public class CompleteMigrateSessionRequest
    {
        //public string SourceOrderId;
        //public string SessionId;
    }


    public class CompleteMigrateSessionResponse : TSpStatusResponse
    {
    }


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "8NYSNibMVoO")]
    public class MigrateController : AppDataController
    {
        public MigrateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/migrate/startsession
        [HttpPost("startsession")]
        [FwControllerMethod(Id: "vuCrJ6PMa3n")]
        public async Task<ActionResult<StartMigrateSessionResponse>> StartSession([FromBody]StartMigrateSessionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                StartMigrateSessionResponse response = new StartMigrateSessionResponse();
                if (string.IsNullOrEmpty(request.DealId))
                {
                    response.success = false;
                    response.msg = "DealId is required.";
                }
                else if (string.IsNullOrEmpty(request.DepartmentId))
                {
                    response.success = false;
                    response.msg = "DepartmentId is required.";
                }
                else
                {
                    response = await MigrateFunc.StartSession(AppConfig, UserSession, request);
                }

                return new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/migrate/updateitem
        [HttpPost("updateitem")]
        [FwControllerMethod(Id: "H3vFKzK6VTZ")]
        public async Task<ActionResult<UpdateMigrateItemResponse>> UpdateItem ([FromBody]UpdateMigrateItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UpdateMigrateItemResponse response = new UpdateMigrateItemResponse();
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
                    response = await MigrateFunc.UpdateItem(AppConfig, UserSession, request);
                }

                return new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/migrate/selectall
        [HttpPost("selectall")]
        [FwControllerMethod(Id: "6nxMKPPccQq")]
        public async Task<ActionResult<SelectAllNoneMigrateItemResponse>> SelectAll([FromBody] SelectAllNoneMigrateItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SelectAllNoneMigrateItemResponse response = await MigrateFunc.SelectAllMigrateItem(AppConfig, UserSession, request.SessionId);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/migrate/selectnone
        [HttpPost("selectnone")]
        [FwControllerMethod(Id: "VvtDKiyfXyh")]
        public async Task<ActionResult<SelectAllNoneMigrateItemResponse>> SelectNone([FromBody] SelectAllNoneMigrateItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SelectAllNoneMigrateItemResponse response = await MigrateFunc.SelectNoneMigrateItem (AppConfig, UserSession, request.SessionId);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------      
        // POST api/v1/migrate/completesession
        [HttpPost("completesession")]
        [FwControllerMethod(Id: "PWJiNSDvo8Z")]
        public async Task<ActionResult<CompleteMigrateSessionResponse>> CompleteSession([FromBody]CompleteMigrateSessionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CompleteMigrateSessionResponse response = new CompleteMigrateSessionResponse();
                //if (string.IsNullOrEmpty(request.SourceOrderId))
                //{
                //    response.success = false;
                //    response.msg = "SourceOrderId is required.";
                //}
                //else if (string.IsNullOrEmpty(request.SessionId))
                //{
                //    response.success = false;
                //    response.msg = "SessionId is required.";
                //}
                //else
                //{
                //    response = await MigrateFunc.CompleteSession(AppConfig, UserSession, request);
                //}

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
