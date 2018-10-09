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

namespace WebApi.Modules.Home.CheckOut
{

    public class StageItemRequest
    {
        public string OrderId;
        public string OrderItemId;
        public string Code;
        public int? Quantity;
        public bool? AddItemToOrder;
        public bool? AddCompleteToOrder;
    }

    public class CheckOutAllStagedRequest
    {
        public string OrderId;
    }

    public class CreateOutContractRequest
    {
        public string OrderId;
    }

    public class CreateOutContractResponse : TSpStatusReponse
    {
        public string ContractId;
    }


    public class MoveStagedItemRequest
    {
        public string OrderId;
        public string OrderItemId;
        public string VendorId;
        public string ContractId;
        public string Code;
        public float? Quantity;
    }


    public class MoveStagedItemResponse : TSpStatusReponse
    {
    }





    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class CheckOutController : AppDataController
    {
        public CheckOutController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkout/stageitem
        [HttpPost("stageitem")]
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
                    stageItemResponse = await CheckOutFunc.StageItem(AppConfig, UserSession, request.OrderId, request.OrderItemId, request.Code, request.Quantity, request.AddItemToOrder.GetValueOrDefault(false), request. AddCompleteToOrder.GetValueOrDefault(false));
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
                else {
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