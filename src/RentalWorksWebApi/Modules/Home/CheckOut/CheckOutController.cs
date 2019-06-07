using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using WebApi.Logic;
using WebApi.Modules.Home.Contract;
using WebLibrary;

namespace WebApi.Modules.Home.CheckOut
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
        [FwControllerMethod(Id: "bcGmOQgVzeDpc")]
        public async Task<ActionResult<bool>> SuspendedSessionsExist()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return await ContractFunc.SuspendedSessionsExist(AppConfig, UserSession, RwConstants.CONTRACT_TYPE_OUT, RwConstants.ORDER_TYPE_ORDER);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/checkout/transfersuspendedsessionsexist
        [HttpGet("transfersuspendedsessionsexist")]
        [FwControllerMethod(Id: "enHr6q0s4X9")]
        public async Task<ActionResult<bool>> TransferSuspendedSessionsExist()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return await ContractFunc.SuspendedSessionsExist(AppConfig, UserSession, RwConstants.CONTRACT_TYPE_MANIFEST, RwConstants.ORDER_TYPE_TRANSFER);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/checkout/containersuspendedsessionsexist
        [HttpGet("containersuspendedsessionsexist")]
        [FwControllerMethod(Id: "QnA31soyCzm")]
        public async Task<ActionResult<bool>> ContainerSuspendedSessionsExist()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return await ContractFunc.SuspendedSessionsExist(AppConfig, UserSession, RwConstants.CONTRACT_TYPE_FILL, RwConstants.ORDER_TYPE_CONTAINER);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/checkout/stagingtabs?OrderId&WarehouseId
        [HttpGet("stagingtabs")]
        [FwControllerMethod(Id: "2EfNs9npvIhkL")]
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
        [FwControllerMethod(Id: "cjSZS0HLutCV")]
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
        // POST api/v1/checkout/checkoutallstaged
        [HttpPost("checkoutallstaged")]
        [FwControllerMethod(Id: "3Ocr6r5He3xF")]
        public async Task<ActionResult<CheckOutAllStagedResponse>> CheckOutAllStaged([FromBody]CheckOutAllStagedRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CheckOutAllStagedResponse checkOutAllStagedResponse = await CheckOutFunc.CheckOutAllStaged(AppConfig, UserSession, request.OrderId);
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
        [FwControllerMethod(Id: "O6ibb6WOwzzg")]
        public async Task<ActionResult<CreateOutContractResponse>> StartCheckOutContract([FromBody]CreateOutContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CreateOutContractResponse response = await CheckOutFunc.CreateOutContract(AppConfig, UserSession, request.OrderId);
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
        [FwControllerMethod(Id: "fbWJYkPbqpBE")]
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
        [FwControllerMethod(Id: "b705dpUOY3rJ")]
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
        [FwControllerMethod(Id: "b1UmILugTF0F")]
        public async Task<ActionResult<ContractLogic>> CompleteCheckOutContractAsync([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TSpStatusReponse response = await ContractFunc.AssignContract(AppConfig, UserSession, id);
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
    }
}
