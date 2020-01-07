using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Logic;
using WebApi.Modules.Warehouse.Contract;
using WebApi;
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Agent.Order;

namespace WebApi.Modules.Warehouse.Exchange
{

    //------------------------------------------------------------------------------------ 
    public class ExchangeContractRequest
    {
        public string OrderId;
        public string DealId;
        public string DepartmentId;
    }

    public class ExchangeContractResponse
    {
        public string ContractId;
    }
    //------------------------------------------------------------------------------------ 



    //------------------------------------------------------------------------------------ 
    public class ExchangeItemInRequest
    {
        public string ContractId;
        public string OrderId;
        public string DealId;
        public string DepartmentId;
        public string InCode;
        public int? Quantity;
    }

    public class ExchangeItemInResponse : ExchangeItemSpStatusResponse
    {
        public string ContractId;
        public string InCode;
        public int? Quantity;
    }
    //------------------------------------------------------------------------------------ 



    //------------------------------------------------------------------------------------ 
    public class ExchangeItemOutRequest
    {
        public string ContractId;
        public string OrderId;
        public string DealId;
        public string DepartmentId;
        public string InCode;
        public int? Quantity;
        public string OutCode;
    }

    public class ExchangeItemOutResponse : ExchangeItemSpStatusResponse
    {
        public string ContractId;
        public string InCode;
        public int? Quantity;
        public string OutCode;
    }
    //------------------------------------------------------------------------------------ 


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"IQS4rxzIVFl")]
    public class ExchangeController : AppDataController
    {
        public ExchangeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }


        //------------------------------------------------------------------------------------ 


        // GET api/v1/exchange/suspendedsessionsexist
        [HttpGet("suspendedsessionsexist")]
        [FwControllerMethod(Id: "DetgPNBfyCRRX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<bool>> SuspendedSessionsExist(string warehouseId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return await ContractFunc.SuspendedSessionsExist(AppConfig, UserSession, RwConstants.CONTRACT_TYPE_EXCHANGE, warehouseId);
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


        // POST api/v1/exchange/exchangeitemin
        [HttpPost("exchangeitemin")]
        [FwControllerMethod(Id:"XOvqlNHRQs3", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<ExchangeItemInResponse>> ExchangeItemIn([FromBody]ExchangeItemInRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (string.IsNullOrEmpty(request.InCode))
                {
                    throw new Exception("Must supply an \"In\" Code.");
                }

                ExchangeItemInResponse response = new ExchangeItemInResponse();
                response.ContractId = request.ContractId;
                response.OrderId = request.OrderId;
                response.DealId = request.DealId;
                response.DepartmentId = request.DepartmentId;
                response.InCode = request.InCode;

                if (string.IsNullOrEmpty(request.ContractId))
                {
                    if (((!string.IsNullOrEmpty(request.OrderId)) || (!string.IsNullOrEmpty(request.DealId))) && (!string.IsNullOrEmpty(request.DepartmentId)))
                    {
                        string ContractId = await ExchangeFunc.CreateExchangeContract(AppConfig, UserSession, request.OrderId, request.DealId, request.DepartmentId);
                        response.ContractId = ContractId;
                    }
                }

                ExchangeItemSpStatusResponse exchangeItemResponse = await ExchangeFunc.ExchangeItem(AppConfig, UserSession, request.ContractId, /*request.OrderId, request.DealId, request.DepartmentId, */request.InCode, request.Quantity, "");
                response.Deal = exchangeItemResponse.Deal;
                response.OrderNumber = exchangeItemResponse.OrderNumber;
                response.OrderDescription = exchangeItemResponse.OrderDescription;
                response.ItemStatus = exchangeItemResponse.ItemStatus;
                response.success = exchangeItemResponse.success;
                response.msg = exchangeItemResponse.msg;

                if ((response.success) && (string.IsNullOrEmpty(request.ContractId)))
                {
                    if (((!string.IsNullOrEmpty(exchangeItemResponse.OrderId)) || (!string.IsNullOrEmpty(exchangeItemResponse.DealId))) && (!string.IsNullOrEmpty(exchangeItemResponse.DepartmentId)))
                    {
                        string ContractId = await ExchangeFunc.CreateExchangeContract(AppConfig, UserSession, exchangeItemResponse.OrderId, exchangeItemResponse.DealId, exchangeItemResponse.DepartmentId);
                        response.ContractId = ContractId;
                        response.OrderId = exchangeItemResponse.OrderId;
                        response.DealId = exchangeItemResponse.DealId;
                        response.DepartmentId = exchangeItemResponse.DepartmentId;
                    }
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


        //------------------------------------------------------------------------------------ 
        // POST api/v1/exchange/exchangeitemout
        [HttpPost("exchangeitemout")]
        [FwControllerMethod(Id:"5QUCeNc5L0u", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<ExchangeItemOutResponse>> ExchangeItemOut([FromBody]ExchangeItemOutRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExchangeItemOutResponse response = new ExchangeItemOutResponse();

                if (string.IsNullOrEmpty(request.OutCode)) // temporary.  completing a pending exchange should allow a blank Out Bar Code
                {
                    response.success = false;
                    response.msg = "Must supply an \"Out\" Code.";
                }
                //// temporary.  pending exchanges need to allow a blank InCode
                //else if (string.IsNullOrEmpty(request.InCode))
                //{
                //    response.success = false;
                //    response.msg = "Must supply an \"In\" Code.";
                //}
                else
                {

                    response.ContractId = request.ContractId;
                    response.OrderId = request.OrderId;
                    response.DealId = request.DealId;
                    response.DepartmentId = request.DepartmentId;
                    response.InCode = request.InCode;
                    response.OutCode = request.OutCode;

                    if (string.IsNullOrEmpty(request.ContractId))
                    {
                        if (((!string.IsNullOrEmpty(request.OrderId)) || (!string.IsNullOrEmpty(request.DealId))) && (!string.IsNullOrEmpty(request.DepartmentId)))
                        {
                            string ContractId = await ExchangeFunc.CreateExchangeContract(AppConfig, UserSession, request.OrderId, request.DealId, request.DepartmentId);
                            response.ContractId = ContractId;
                        }
                    }


                    ExchangeItemSpStatusResponse exchangeItemResponse = await ExchangeFunc.ExchangeItem(AppConfig, UserSession, request.ContractId, /*request.OrderId, request.DealId, request.DepartmentId,*/ request.InCode, request.Quantity, request.OutCode);
                    response.Deal = exchangeItemResponse.Deal;
                    response.OrderNumber = exchangeItemResponse.OrderNumber;
                    response.OrderDescription = exchangeItemResponse.OrderDescription;
                    response.ItemStatus = exchangeItemResponse.ItemStatus;
                    response.success = exchangeItemResponse.success;
                    response.msg = exchangeItemResponse.msg;
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



        // POST api/v1/exchange/startexchangecontract
        [HttpPost("startexchangecontract")]
        [FwControllerMethod(Id:"m7wzxum9Fjk", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<ExchangeContractResponse>> StartExchangeContractAsync([FromBody]ExchangeContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string ContractId = await ExchangeFunc.CreateExchangeContract(AppConfig, UserSession, request.OrderId, request.DealId, request.DepartmentId);
                ExchangeContractResponse response = new ExchangeContractResponse();
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
        // POST api/v1/exchange/completeexchangecontract
        [HttpPost("completeexchangecontract/{id}")]
        [FwControllerMethod(Id:"ETE3Ab2TsnT", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<ContractLogic>> CompleteExchangeContractAsync([FromRoute]string id)
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
        // POST api/v1/exchange/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "GSYnHvCvxO1N", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDeliveryToCountryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/exchange/validateorder/browse 
        [HttpPost("validateorder/browse")]
        [FwControllerMethod(Id: "Stg1hDOyLwo6", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOrderBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderLogic>(browseRequest);
        }

    }
}
