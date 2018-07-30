using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System;
using Microsoft.AspNetCore.Http;
using WebApi.Home.CheckIn;
using WebApi.Logic;
using WebApi.Modules.Home.Contract;

namespace WebApi.Modules.Home.CheckIn
{


    public class CheckInContractRequest
    {
        public string OrderId;
        public string DealId;
        public string DepartmentId;
    }

    public class CheckInContractResponse
    {
        public string ContractId;
    }
    
    public class CheckInItemRequest
    {
        public string ContractId;
        public string Code;
        public string OrderId;
        public string OrderItemId;
        public int? Quantity;
        public bool? AddItemToOrder;
        public bool? AddCompleteToOrder;
    }


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class CheckInController : AppDataController
    {
        public CheckInController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }


        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkin/startcheckincontract
        [HttpPost("startcheckincontract")]
        public async Task<IActionResult> StartCheckInContractAsync([FromBody]CheckInContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string ContractId = await CheckInFunc.CreateCheckInContract(AppConfig, UserSession, request.OrderId, request.DealId, request.DepartmentId);
                CheckInContractResponse response = new CheckInContractResponse();
                response.ContractId = ContractId;
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
        // POST api/v1/purchaseorder/completecheckincontract
        [HttpPost("completecheckincontract/{id}")]
        public async Task<IActionResult> CompleteCheckInContractAsync([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                TSpStatusReponse response = await AppFunc.AssignContract(AppConfig, UserSession, id);
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




        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkin/checkinitem
        [HttpPost("checkinitem")]
        public async Task<IActionResult> CheckInItem([FromBody]CheckInItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TCheckInItemReponse checkInItemResponse = new TCheckInItemReponse();
                if ((string.IsNullOrEmpty(request.OrderItemId)) && (string.IsNullOrEmpty(request.Code)))
                {
                    checkInItemResponse.success = false;
                    checkInItemResponse.msg = "Must supply a Code or OrderItemId to check-in items.";
                }
                else
                {
                    checkInItemResponse = await CheckInFunc.CheckInItem(AppConfig, UserSession, request.ContractId, request.Code, request.Quantity);
                }

                return new OkObjectResult(checkInItemResponse);

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