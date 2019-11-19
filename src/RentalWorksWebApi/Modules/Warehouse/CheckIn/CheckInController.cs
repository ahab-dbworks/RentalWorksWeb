using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Logic;
using WebApi.Modules.Warehouse.Contract;
using WebLibrary;

namespace WebApi.Modules.Warehouse.CheckIn
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"krnJWTUs4n5U")]
    public class CheckInController : AppDataController
    {
        public CheckInController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/checkin/suspendedsessionsexist
        [HttpGet("suspendedsessionsexist")]
        [FwControllerMethod(Id: "gqnWo60RWZUkA", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<bool>> SuspendedSessionsExist(string warehouseId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return await ContractFunc.SuspendedSessionsExist(AppConfig, UserSession, RwConstants.CONTRACT_TYPE_IN, warehouseId);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/checkin/transfersuspendedsessionsexist
        [HttpGet("transfersuspendedsessionsexist")]
        [FwControllerMethod(Id: "nn5aXjSowjt", ActionType: FwControllerActionTypes.Browse)]
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
                string ContractId = await CheckInFunc.CreateCheckInContract(AppConfig, UserSession, request);
                CheckInContractResponse response = new CheckInContractResponse();
                response.ContractId = ContractId;
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkin/completecheckincontract
        [HttpPost("completecheckincontract/{id}")]
        [FwControllerMethod(Id:"RAiuwADDxuoJ", ActionType: FwControllerActionTypes.Browse, Caption: "Complete Check-In Contract")]
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
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkin/checkinitem
        [HttpPost("checkinitem")]
        [FwControllerMethod(Id:"H0hnbimbsrSh", ActionType: FwControllerActionTypes.Browse, Caption: "Check-In Item")]
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
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
    }
}
