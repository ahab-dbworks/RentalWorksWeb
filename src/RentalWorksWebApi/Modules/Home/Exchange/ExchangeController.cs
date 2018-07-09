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

    public class ExchangeItemRequest
    {
        public string ContractId;
        public string OrderId;
        public string DealId;
        public string DepartmentId;
        public string InCode;
        public int? Quantity;
        public string OutCode;
    }

    public class ExchangeItemResponse
    {
        public string ContractId;
        public string OrderId;
        public string DealId;
        public string DepartmentId;
        public string InCode;
        public int? Quantity;
        public string OutCode;
    }


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class ExchangeController : AppDataController
    {
        public ExchangeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/exchange/exchangeitem
        [HttpPost("exchangeitem")]
        public async Task<IActionResult> ExchangeItem([FromBody]ExchangeItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExchangeItemResponse response = new ExchangeItemResponse();
                response.ContractId = request.ContractId;
                response.OrderId = request.OrderId;
                response.DealId = request.DealId;
                response.DepartmentId = request.DepartmentId;

                if (string.IsNullOrEmpty(request.ContractId))
                {
                    string ContractId = await ExchangeFunc.CreateExchangeContract(AppConfig, UserSession, request.OrderId, request.DealId, request.DepartmentId);
                    response.ContractId = ContractId;
                }
                if (string.IsNullOrEmpty(request.InCode))
                {
                    throw new Exception("Must supply an \"In\" Code.");
                }
                if (string.IsNullOrEmpty(request.OutCode))
                {
                    throw new Exception("Must supply an \"Out\" Code.");
                }


                TExchangeItemReponse exchangeItemResponse = await ExchangeFunc.ExchangeItem(AppConfig, UserSession, request.ContractId, request.InCode, request.Quantity, request.OutCode);

                if (exchangeItemResponse.success)
                {
                return new OkObjectResult(response);
                }
                else
                {
                    throw new Exception(exchangeItemResponse.msg);
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

    }
}