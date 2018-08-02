using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System;
using Microsoft.AspNetCore.Http;
using WebApi.Home.Exchange;
using WebApi.Logic;
using WebApi.Modules.Home.Contract;

namespace WebApi.Modules.Administrator.Exchange
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

    public class ExchangeItemInResponse : ExchangeItemSpStatusReponse
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

    public class ExchangeItemOutResponse : ExchangeItemSpStatusReponse
    {
        public string ContractId;
        public string InCode;
        public int? Quantity;
        public string OutCode;
    }
    //------------------------------------------------------------------------------------ 


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class ExchangeController : AppDataController
    {
        public ExchangeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }


        //------------------------------------------------------------------------------------ 
        // POST api/v1/exchange/exchangeitemin
        [HttpPost("exchangeitemin")]
        public async Task<IActionResult> ExchangeItemIn([FromBody]ExchangeItemInRequest request)
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

                ExchangeItemSpStatusReponse exchangeItemResponse = await ExchangeFunc.ExchangeItem(AppConfig, UserSession, request.ContractId, request.OrderId, request.DealId, request.DepartmentId, request.InCode, request.Quantity, "");
                response.Deal = exchangeItemResponse.Deal;
                response.OrderNumber = exchangeItemResponse.OrderNumber;
                response.OrderDescription = exchangeItemResponse.OrderDescription;
                response.InventoryId = exchangeItemResponse.InventoryId;
                response.ICode = exchangeItemResponse.ICode;
                response.ItemDescription = exchangeItemResponse.ItemDescription;
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
        public async Task<IActionResult> ExchangeItemOut([FromBody]ExchangeItemOutRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (string.IsNullOrEmpty(request.OutCode))
                {
                    throw new Exception("Must supply an \"Out\" Code.");
                }

                // temporary.  pending exchanges need to allow a blank InCode
                if (string.IsNullOrEmpty(request.InCode))
                {
                    throw new Exception("Must supply an \"In\" Code.");
                }

                ExchangeItemOutResponse response = new ExchangeItemOutResponse();
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


                ExchangeItemSpStatusReponse exchangeItemResponse = await ExchangeFunc.ExchangeItem(AppConfig, UserSession, request.ContractId, request.OrderId, request.DealId, request.DepartmentId, request.InCode, request.Quantity, request.OutCode);
                response.Deal = exchangeItemResponse.Deal;
                response.OrderNumber = exchangeItemResponse.OrderNumber;
                response.OrderDescription = exchangeItemResponse.OrderDescription;
                response.InventoryId = exchangeItemResponse.InventoryId;
                response.ICode = exchangeItemResponse.ICode;
                response.ItemDescription = exchangeItemResponse.ItemDescription;
                response.success = exchangeItemResponse.success;
                response.msg = exchangeItemResponse.msg;


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
        public async Task<IActionResult> StartExchangeContractAsync([FromBody]ExchangeContractRequest request)
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
        public async Task<IActionResult> CompleteExchangeContractAsync([FromRoute]string id)
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