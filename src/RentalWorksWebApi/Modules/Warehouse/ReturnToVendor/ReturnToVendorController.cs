using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using WebApi.Modules.Agent.PurchaseOrder;
using System;
using WebApi.Modules.Warehouse.Contract;
using WebApi.Logic;

namespace WebApi.Modules.Warehouse.ReturnToVendor
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"cCxoTvTCDTcm")]
    public class ReturnToVendorController : AppDataController
    {
        public ReturnToVendorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------  
        // GET api/v1/returntovendor/suspendedsessionsexist
        [HttpGet("suspendedsessionsexist")]
        [FwControllerMethod(Id: "zPuvlEmQXvmog", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<bool>> ReturnSuspendedSessionsExist(string warehouseId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return await ContractFunc.SuspendedSessionsExist(AppConfig, UserSession, RwConstants.CONTRACT_TYPE_RETURN, warehouseId);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/returntovendor/startsession
        [HttpPost("startsession")]
        [FwControllerMethod(Id: "IHQC7YuIlyflM", ActionType: FwControllerActionTypes.Option, Caption: "Start Return Contract")]
        public async Task<ActionResult<ReturnContractResponse>> StartReturnContractAsync([FromBody]ReturnContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReturnContractResponse response = await PurchaseOrderFunc.CreateReturnContract(AppConfig, UserSession, request);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/returntovendor/returnitems
        [HttpPost("returnitems")]
        [FwControllerMethod(Id: "Qq7dEVLcAm4vQ", ActionType: FwControllerActionTypes.Option)]
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
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/returntovendor/selectall
        [HttpPost("selectall")]
        [FwControllerMethod(Id: "DbE8sDn09QI1", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<SelectAllNoneReturnItemResponse>> SelectAll([FromBody] SelectAllNoneReturnItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SelectAllNoneReturnItemResponse response = await PurchaseOrderFunc.SelectAllReturnItem(AppConfig, UserSession, request.ContractId, request.PurchaseOrderId, request.WarehouseId);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/returntovendor/selectnone
        [HttpPost("selectnone")]
        [FwControllerMethod(Id: "x7iB3kcAYOOuH", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<SelectAllNoneReturnItemResponse>> SelectNone([FromBody] SelectAllNoneReturnItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SelectAllNoneReturnItemResponse response = await PurchaseOrderFunc.SelectNoneReturnItem(AppConfig, UserSession, request.ContractId, request.PurchaseOrderId, request.WarehouseId);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------      
        // POST api/v1/returntovendor/completecontract
        [HttpPost("completecontract/{id}")]
        [FwControllerMethod(Id: "Yu4sDt9BpjVrt", ActionType: FwControllerActionTypes.Option, Caption: "Complete Return Contract")]
        public async Task<ActionResult<ContractLogic>> CompleteReturnContractAsync([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                TSpStatusResponse response = await ContractFunc.AssignContract(AppConfig, UserSession, id);
                if (response.success)
                {

                    ContractLogic contract = new ContractLogic();
                    contract.SetDependencies(AppConfig, UserSession);
                    contract.ContractId = id;
                    bool x = await contract.LoadAsync<ContractLogic>();
                    return new OkObjectResult(contract);
                }
                else
                {
                    throw new Exception(response.msg);
                }

            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------  
        // POST api/v1/returntovendor/cancelcontract
        [HttpPost("cancelcontract")]
        [FwControllerMethod(Id: "YLZZjcDgV479S", ActionType: FwControllerActionTypes.Option, Caption: "Cancel Contract")]
        public async Task<ActionResult<TSpStatusResponse>> CancelContract([FromBody]CancelContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TSpStatusResponse response = await ContractFunc.CancelContract(AppConfig, UserSession, request);
                return response;
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/returntovendor/validatepurchaseorder/browse 
        [HttpPost("validatepurchaseorder/browse")]
        [FwControllerMethod(Id: "Um0qP8FxPAGF", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePurchaseOrderBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PurchaseOrderLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------  
    }
}
