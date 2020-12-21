using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Settings.DepartmentLocation;
using WebApi.Modules.Settings.DepartmentSettings.Department;
using WebApi.Modules.Settings.RateType;

namespace WebApi.Modules.Utilities.Migrate
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "8NYSNibMVoO")]
    public class MigrateController : AppDataController
    {
        public MigrateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/migrate/startsession
        [HttpPost("startsession")]
        [FwControllerMethod(Id: "vuCrJ6PMa3n", ActionType: FwControllerActionTypes.Browse)]
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
        [FwControllerMethod(Id: "H3vFKzK6VTZ", ActionType: FwControllerActionTypes.Browse)]
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
        [FwControllerMethod(Id: "6nxMKPPccQq", ActionType: FwControllerActionTypes.Browse)]
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
        [FwControllerMethod(Id: "VvtDKiyfXyh", ActionType: FwControllerActionTypes.Browse)]
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
        [FwControllerMethod(Id: "PWJiNSDvo8Z", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<CompleteMigrateSessionResponse>> CompleteSession([FromBody]CompleteMigrateSessionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CompleteMigrateSessionResponse response = await MigrateFunc.CompleteSession(AppConfig, UserSession, request);
                return new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/migrate/completesession2
        [HttpPost("completesession2")]
        [FwControllerMethod(Id: "p8gGwTu7l6lHp", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<CompleteMigrateSessionResponse>> CompleteSession2([FromBody]CompleteMigrateSessionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CompleteMigrateSessionResponse response = await MigrateFunc.CompleteSession2(AppConfig, UserSession, request);
                return new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/migrate/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "JykKJ71hmyMm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/migrate/validatedepartment/browse 
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "ebKqlLmWTEEI", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/migrate/validatecreatenewdeal/browse 
        [HttpPost("validatecreatenewdeal/browse")]
        [FwControllerMethod(Id: "Y9WnvNSws5ea", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/migrate/validateratetype/browse 
        [HttpPost("validateratetype/browse")]
        [FwControllerMethod(Id: "EYYVUQuMIwkx", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RateTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/migrate/department/{departmentid}/location/{locationid}
        [HttpGet("department/{departmentid}/location/{locationid}")]
        [FwControllerMethod(Id: "uv7YF2yRry9bC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DepartmentLocationLogic>> DepartmentLocation_GetOneAsync([FromRoute] string departmentid, [FromRoute] string locationid)
        {
            return await DoGetAsync<DepartmentLocationLogic>($"{departmentid}~{locationid}", typeof(DepartmentLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}
