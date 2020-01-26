using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Logic;
using WebApi.Modules.Warehouse.Contract;
using WebApi;
using WebApi.Modules.Transfers.TransferOrder;
using WebApi.Modules.Agent.Order;
using WebApi.Modules.HomeControls.ContainerItem;
using WebApi.Modules.Containers.Container;

namespace WebApi.Modules.Warehouse.CheckOut
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "H0sf3MFhL0VK")]
    public class CheckOutController : AppDataController
    {
        public CheckOutController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/checkout/suspendedsessionsexist
        [HttpGet("suspendedsessionsexist")]
        [FwControllerMethod(Id: "bcGmOQgVzeDpc", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<bool>> SuspendedSessionsExist(string warehouseId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return await ContractFunc.SuspendedSessionsExist(AppConfig, UserSession, RwConstants.CONTRACT_TYPE_OUT, warehouseId);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/checkout/transfersuspendedsessionsexist
        [HttpGet("transfersuspendedsessionsexist")]
        [FwControllerMethod(Id: "enHr6q0s4X9", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<bool>> TransferSuspendedSessionsExist(string warehouseId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return await ContractFunc.SuspendedSessionsExist(AppConfig, UserSession, RwConstants.CONTRACT_TYPE_MANIFEST, warehouseId);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/checkout/containersuspendedsessionsexist
        [HttpGet("containersuspendedsessionsexist")]
        [FwControllerMethod(Id: "QnA31soyCzm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<bool>> ContainerSuspendedSessionsExist(string warehouseId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return await ContractFunc.SuspendedSessionsExist(AppConfig, UserSession, RwConstants.CONTRACT_TYPE_FILL, warehouseId);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/checkout/stagingtabs?OrderId&WarehouseId
        [HttpGet("stagingtabs")]
        [FwControllerMethod(Id: "2EfNs9npvIhkL", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<StagingTabsResponse>> GetStagingTabs(string OrderId, string WarehouseId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                StagingTabsResponse response = await CheckOutFunc.GetStagingTabs(AppConfig, UserSession, OrderId, WarehouseId);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------       
        // POST api/v1/checkout/stageitem
        [HttpPost("stageitem")]
        [FwControllerMethod(Id: "cjSZS0HLutCV", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<StageItemResponse>> StageItem([FromBody]StageItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                StageItemResponse stageItemResponse = await CheckOutFunc.StageItem(AppConfig, UserSession, request);
                return new OkObjectResult(stageItemResponse);

            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkout/unstageitem
        [HttpPost("unstageitem")]
        [FwControllerMethod(Id: "clTIdJRRV1jEy", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<UnstageItemResponse>> UnstageItem([FromBody]UnstageItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UnstageItemResponse unstageItemResponse = await CheckOutFunc.UnstageItem(AppConfig, UserSession, request);
                return new OkObjectResult(unstageItemResponse);

            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkout/checkoutallstaged
        [HttpPost("checkoutallstaged")]
        [FwControllerMethod(Id: "3Ocr6r5He3xF", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<CheckOutAllStagedResponse>> CheckOutAllStaged([FromBody]CheckOutAllStagedRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CheckOutAllStagedResponse checkOutAllStagedResponse = await CheckOutFunc.CheckOutAllStaged(AppConfig, UserSession, request);
                return new OkObjectResult(checkOutAllStagedResponse);

            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkout/startcheckoutcontract
        [HttpPost("startcheckoutcontract")]
        [FwControllerMethod(Id: "O6ibb6WOwzzg", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<CreateOutContractResponse>> StartCheckOutContract([FromBody]CreateOutContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CreateOutContractResponse response = await CheckOutFunc.CreateOutContract(AppConfig, UserSession, request);
                return new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkout/movestageditemtoout
        [HttpPost("movestageditemtoout")]
        [FwControllerMethod(Id: "fbWJYkPbqpBE", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<MoveStagedItemResponse>> MoveStagedItemToOut([FromBody]MoveStagedItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MoveStagedItemResponse response = await CheckOutFunc.MoveStagedItemToOut(AppConfig, UserSession, request.OrderId, request.OrderItemId, request.VendorId, request.ContractId, request.Code, request.Quantity);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkout/moveoutitemtostaged
        [HttpPost("moveoutitemtostaged")]
        [FwControllerMethod(Id: "b705dpUOY3rJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<MoveStagedItemResponse>> MoveOutItemToStaged([FromBody]MoveStagedItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MoveStagedItemResponse response = await CheckOutFunc.MoveOutItemToStaged(AppConfig, UserSession, request.OrderId, request.OrderItemId, request.VendorId, request.ContractId, request.Code, request.Quantity);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkout/completecheckoutcontract/A0000001
        [HttpPost("completecheckoutcontract/{id}")]
        [FwControllerMethod(Id: "b1UmILugTF0F", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<ContractLogic>> CompleteCheckOutContractAsync([FromRoute]string id)
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
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkout/validateorder/browse 
        [HttpPost("validateorder/browse")]
        [FwControllerMethod(Id: "bbKYV7nAz5WO", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkout/validatetransfer/browse 
        [HttpPost("validatetransfer/browse")]
        [FwControllerMethod(Id: "dSDPhw8NbPIO", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateTransferBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<TransferOrderLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkout/validatecontaineritem/browse 
        [HttpPost("validatecontaineritem/browse")]
        [FwControllerMethod(Id: "zF66EFjFX0Gl", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateContainerItemBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContainerItemLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkout/validatecontainer/browse 
        [HttpPost("validatecontainer/browse")]
        [FwControllerMethod(Id: "j06Fok0NSyuW", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateContainerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContainerLogic>(browseRequest);
        }

    }
}
