using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Logic;
using WebApi.Modules.Warehouse.CheckIn;
using WebApi.Modules.Warehouse.Contract;
using WebApi;
using WebApi.Modules.Transfers.TransferOrder;

//dummy-security-controller 
namespace WebApi.Modules.Transfers.TransferIn
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"aVOT6HR8knES")]
    public class TransferInController : AppDataController
    {
        public TransferInController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // GET api/v1/transferin/suspendedsessionsexist
        [HttpGet("suspendedsessionsexist")]
        [FwControllerMethod(Id: "QxU5b6nzJKG5", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<bool>> SuspendedSessionsExist(string warehouseId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return await ContractFunc.SuspendedSessionsExist(AppConfig, UserSession, RwConstants.CONTRACT_TYPE_RECEIPT, warehouseId);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/transferin/startcheckincontract
        [HttpPost("startcheckincontract")]
        [FwControllerMethod(Id:"yfGMYlzcLcpY", ActionType: FwControllerActionTypes.Browse)]
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
        // POST api/v1/transferin/completecheckincontract
        [HttpPost("completecheckincontract/{id}")]
        [FwControllerMethod(Id:"75g0XkCeozjv", ActionType: FwControllerActionTypes.Browse)]
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
        // POST api/v1/transferin/checkinitem
        [HttpPost("checkinitem")]
        [FwControllerMethod(Id:"MxSXXbGC8TG6", ActionType: FwControllerActionTypes.Browse)]
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
        // POST api/v1/transferin/cancelcontract
        [HttpPost("cancelcontract")]
        [FwControllerMethod(Id: "q9pERMOQZkwOl", ActionType: FwControllerActionTypes.Option, Caption: "Cancel Contract")]
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
        // POST api/v1/transferin/validatetransfer/browse 
        [HttpPost("validatetransfer/browse")]
        [FwControllerMethod(Id: "dvWPAAj5GMnn", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateTransferBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<TransferOrderLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
    }
}

