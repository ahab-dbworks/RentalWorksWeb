using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Logic;
using WebApi.Modules.Warehouse.Contract;
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Settings.CompanyDepartmentSettings.Department;
using WebApi.Modules.Settings.RateType;

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
        public string SessionId { get; set; }
        public bool? MigrateToNewOrder { get; set; }
        public string NewOrderOfficeLocationId { get; set; }
        public string NewOrderWarehouseId { get; set; }
        public string NewOrderDealId { get; set; }
        public string NewOrderDescription { get; set; }
        public string NewOrderRateType { get; set; }
        public DateTime? NewOrderFromDate { get; set; }
        public string NewOrderFromTime { get; set; }
        public DateTime? NewOrderToDate { get; set; }
        public string NewOrderToTime { get; set; }
        public DateTime? NewOrderBillingStopDate { get; set; }
        public bool? NewOrderPendingPO { get; set; }
        public bool? NewOrderFlatPO { get; set; }
        public string NewOrderPurchaseOrderNumber { get; set; }
        public decimal? NewOrderPurchaseOrderAmount { get; set; }
        public bool? MigrateToExistingOrder { get; set; }
        public string ExistingOrderId { get; set; }
        //public string MigrateDealId { get; set; }  // not used
        public string InventoryFulfillIncrement { get; set; }   // FULFILL / INCREMENT
        public string InventoryCheckedOrStaged { get; set; }    // CHECKED / STAGED
        public bool? CopyLineItemNotes { get; set; }
        public bool? CopyOrderNotes { get; set; }
        public bool? CopyRentalRates { get; set; }
        public bool? UpdateBillingStopDate { get; set; }
        public DateTime? BillingStopDate { get; set; }
    }


    public class CompleteMigrateSessionResponse : TSpStatusResponse
    {
        public List<ContractLogic> Contracts = new List<ContractLogic>();
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
    }
}
