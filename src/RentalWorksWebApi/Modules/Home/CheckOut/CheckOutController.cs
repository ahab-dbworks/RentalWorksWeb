using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System;
using Microsoft.AspNetCore.Http;
//using WebApi.Home.CheckOut;
using WebApi.Logic;
using WebApi.Modules.Home.Contract;
using WebLibrary;

namespace WebApi.Modules.Home.CheckOut
{

    public class StageItemRequest
    {
        public string OrderId { get; set; }
        public string OrderItemId { get; set; }
        public string Code { get; set; }
        public int? Quantity { get; set; }
        public bool? UnstageItem { get; set; }
        public bool? AddItemToOrder { get; set; }
        public bool? AddCompleteToOrder { get; set; }
    }

    public class CheckOutAllStagedRequest
    {
        public string OrderId { get; set; }
    }

    public class CreateOutContractRequest
    {
        public string OrderId { get; set; }
    }

    public class CreateOutContractResponse : TSpStatusReponse
    {
        public string ContractId { get; set; }
    }


    public class MoveStagedItemRequest
    {
        public string OrderId { get; set; }
        public string OrderItemId { get; set; }
        public string VendorId { get; set; }
        public string ContractId { get; set; }
        public string Code { get; set; }
        public float? Quantity { get; set; }
    }


    public class MoveStagedItemResponse : TSpStatusReponse
    {
    }





    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"H0sf3MFhL0VK")]
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
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkout/stageitem
        [HttpPost("stageitem")]
        [FwControllerMethod(Id:"cjSZS0HLutCV")]
        public async Task<ActionResult<StageItemReponse>> StageItem([FromBody]StageItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                StageItemReponse stageItemResponse = new StageItemReponse();
                if (string.IsNullOrEmpty(request.OrderId))
                {
                    stageItemResponse.success = false;
                    stageItemResponse.msg = "OrderId is required.";
                }
                else if ((string.IsNullOrEmpty(request.OrderItemId)) && (string.IsNullOrEmpty(request.Code)))
                {
                    stageItemResponse.success = false;
                    stageItemResponse.msg = "Must supply a Code or OrderItemId to stage items.";
                }
                else
                {
                    stageItemResponse = await CheckOutFunc.StageItem(AppConfig, UserSession, request);
                }

                return new OkObjectResult(stageItemResponse);

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
        // POST api/v1/checkout/checkoutallstaged
        [HttpPost("checkoutallstaged")]
        [FwControllerMethod(Id:"3Ocr6r5He3xF")]
        public async Task<ActionResult<CheckOutAllStagedResponse>> CheckOutAllStaged([FromBody]CheckOutAllStagedRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CheckOutAllStagedResponse checkOutAllStagedResponse = new CheckOutAllStagedResponse();
                if (string.IsNullOrEmpty(request.OrderId))
                {
                    checkOutAllStagedResponse.success = false;
                    checkOutAllStagedResponse.msg = "OrderId is required.";
                }
                else
                {
                    checkOutAllStagedResponse = await CheckOutFunc.CheckOutAllStaged(AppConfig, UserSession, request.OrderId);
                }

                return new OkObjectResult(checkOutAllStagedResponse);

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
        // POST api/v1/checkout/startcheckoutcontract
        [HttpPost("startcheckoutcontract")]
        [FwControllerMethod(Id:"O6ibb6WOwzzg")]
        public async Task<ActionResult<CreateOutContractResponse>> StartCheckOutContract([FromBody]CreateOutContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CreateOutContractResponse response = new CreateOutContractResponse();
                if (string.IsNullOrEmpty(request.OrderId))
                {
                    response.success = false;
                    response.msg = "OrderId is required.";
                }
                else
                {
                    response = await CheckOutFunc.CreateOutContract(AppConfig, UserSession, request.OrderId);
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
        // POST api/v1/checkout/movestageditemtoout
        [HttpPost("movestageditemtoout")]
        [FwControllerMethod(Id:"fbWJYkPbqpBE")]
        public async Task<ActionResult<MoveStagedItemResponse>> MoveStagedItemToOut([FromBody]MoveStagedItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MoveStagedItemResponse response = new MoveStagedItemResponse();
                if (string.IsNullOrEmpty(request.OrderId))
                {
                    response.success = false;
                    response.msg = "OrderId is required.";
                }
                else if (string.IsNullOrEmpty(request.ContractId))
                {
                    response.success = false;
                    response.msg = "ContractId is required.";
                }
                else if (string.IsNullOrEmpty(request.Code))
                {
                    response.success = false;
                    response.msg = "Code is required.";
                }
                else
                {
                    response = await CheckOutFunc.MoveStagedItemToOut(AppConfig, UserSession, request.OrderId, request.OrderItemId, request.VendorId, request.ContractId, request.Code, request.Quantity);
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
        // POST api/v1/checkout/moveoutitemtostaged
        [HttpPost("moveoutitemtostaged")]
        [FwControllerMethod(Id:"b705dpUOY3rJ")]
        public async Task<ActionResult<MoveStagedItemResponse>> MoveOutItemToStaged([FromBody]MoveStagedItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MoveStagedItemResponse response = new MoveStagedItemResponse();
                if (string.IsNullOrEmpty(request.OrderId))
                {
                    response.success = false;
                    response.msg = "OrderId is required.";
                }
                else if (string.IsNullOrEmpty(request.ContractId))
                {
                    response.success = false;
                    response.msg = "ContractId is required.";
                }
                else if (string.IsNullOrEmpty(request.Code))
                {
                    response.success = false;
                    response.msg = "Code is required.";
                }
                else
                {
                    response = await CheckOutFunc.MoveOutItemToStaged(AppConfig, UserSession, request.OrderId, request.OrderItemId, request.VendorId, request.ContractId, request.Code, request.Quantity);
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
        // POST api/v1/checkout/completecheckoutcontract
        [HttpPost("completecheckoutcontract/{id}")]
        [FwControllerMethod(Id:"b1UmILugTF0F")]
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
