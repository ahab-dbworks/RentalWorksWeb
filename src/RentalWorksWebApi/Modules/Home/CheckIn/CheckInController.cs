using FwStandard.AppManager;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System;
using Microsoft.AspNetCore.Http;
//using WebApi.Home.CheckIn;
using WebApi.Logic;
using WebApi.Modules.Home.Contract;
using WebLibrary;

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
        public bool? AddOrderToContract;
        public bool? SwapItem;
    }

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"krnJWTUs4n5U")]
    public class CheckInController : AppDataController
    {
        public CheckInController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }



        //------------------------------------------------------------------------------------ 

        // GET api/v1/checkin/suspendedsessionsexist
        [HttpGet("suspendedsessionsexist")]
        [FwControllerMethod(Id: "gqnWo60RWZUkA")]
        public async Task<ActionResult<bool>> SuspendedSessionsExist()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return await ContractFunc.SuspendedSessionsExist(AppConfig, UserSession, RwConstants.CONTRACT_TYPE_IN, RwConstants.ORDER_TYPE_ORDER);
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
        // GET api/v1/checkin/transfersuspendedsessionsexist
        [HttpGet("transfersuspendedsessionsexist")]
        [FwControllerMethod(Id: "nn5aXjSowjt")]
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
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }

        // POST api/v1/checkin/startcheckincontract
        [HttpPost("startcheckincontract")]
        [FwControllerMethod(Id:"Ns64DDLG1CGr")]
        public async Task<ActionResult<CheckInContractResponse>> StartCheckInContractAsync([FromBody]CheckInContractRequest request)
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
        // POST api/v1/checkin/completecheckincontract
        [HttpPost("completecheckincontract/{id}")]
        [FwControllerMethod(Id:"RAiuwADDxuoJ")]
        public async Task<ActionResult<ContractLogic>> CompleteCheckInContractAsync([FromRoute]string id)
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
        [FwControllerMethod(Id:"H0hnbimbsrSh")]
        public async Task<ActionResult<TCheckInItemResponse>> CheckInItem([FromBody]CheckInItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TCheckInItemResponse checkInItemResponse = new TCheckInItemResponse();
                if ((string.IsNullOrEmpty(request.OrderItemId)) && (string.IsNullOrEmpty(request.Code)))
                {
                    checkInItemResponse.success = false;
                    checkInItemResponse.msg = "Must supply a Code or OrderItemId to check-in items.";
                }
                else
                {
                    checkInItemResponse = await CheckInFunc.CheckInItem(AppConfig, UserSession, request);
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
    }
}
