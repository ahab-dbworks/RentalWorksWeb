using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using WebApi.Modules.Home.Contract;
using WebApi.Logic;
using System.Collections.Generic;

namespace WebApi.Modules.Home.PurchaseOrder
{


    public class ReceiveContractRequest
    {
        public string PurchaseOrderId;
    }

    public class ReceiveContractResponse
    {
        public string ContractId;
    }

    public class CompleteReceiveContractRequest
    {
        public bool? CreateOutContracts;
    }


    public class ReturnContractRequest
    {
        public string PurchaseOrderId;
    }

    public class ReturnContractResponse
    {
        public string ContractId;
    }

    public class PurchaseOrderReceiveBarCodeAddItemsRequest
    {
        public string PurchaseOrderId;
        public string ContractId;
    }

    public class PurchaseOrderReceiveBarCodeAddItemsResponse : TSpStatusReponse
    {
        public int ItemsAdded;
    }


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class PurchaseOrderController : AppDataController
    {
        public PurchaseOrderController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PurchaseOrderLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorder/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorder/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchaseorder 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PurchaseOrderLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchaseorder/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PurchaseOrderLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorder 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PurchaseOrderLogic l)
        {
            return await DoPostAsync<PurchaseOrderLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/purchaseorder/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorder/startreceivecontract
        [HttpPost("startreceivecontract")]
        public async Task<IActionResult> StartReceiveContractAsync([FromBody]ReceiveContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PurchaseOrderLogic l = new PurchaseOrderLogic();
                l.SetDependencies(this.AppConfig, this.UserSession);
                l.PurchaseOrderId = request.PurchaseOrderId;
                if (await l.LoadAsync<PurchaseOrderLogic>())
                {
                    string ContractId = await l.CreateReceiveContract();
                    ReceiveContractResponse response = new ReceiveContractResponse();
                    response.ContractId = ContractId;
                    return new OkObjectResult(response);
                }
                else
                {
                    return NotFound();
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
        // POST api/v1/purchaseorder/completereceivecontract
        [HttpPost("completereceivecontract/{id}")]
        public async Task<IActionResult> CompleteReceiveContractAsync([FromRoute]string id, [FromBody] CompleteReceiveContractRequest request)
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
                    List<ContractLogic> contracts = new List<ContractLogic>();
                    ContractLogic contract = new ContractLogic();
                    contract.SetDependencies(AppConfig, UserSession);
                    contract.ContractId = id;
                    await contract.LoadAsync<ContractLogic>();
                    contracts.Add(contract);
                    if (request.CreateOutContracts.GetValueOrDefault(false))
                    {
                        List<string> outContractIds = await PurchaseOrderFunc.CreateOutContractsFromReceive(AppConfig, UserSession, id);
                        foreach (string outContractId in outContractIds)
                        {
                            contract = new ContractLogic();
                            contract.SetDependencies(AppConfig, UserSession);
                            contract.ContractId = outContractId;
                            await contract.LoadAsync<ContractLogic>();
                            contracts.Add(contract);
                        }
                    }
                    return new OkObjectResult(contracts);
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




        // POST api/v1/purchaseorder/startreturncontract
        [HttpPost("startreturncontract")]
        public async Task<IActionResult> StartReturnContractAsync([FromBody]ReturnContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PurchaseOrderLogic l = new PurchaseOrderLogic();
                l.SetDependencies(this.AppConfig, this.UserSession);
                l.PurchaseOrderId = request.PurchaseOrderId;
                if (await l.LoadAsync<PurchaseOrderLogic>())
                {
                    string ContractId = await l.CreateReturnContract();
                    ReturnContractResponse response = new ReturnContractResponse();
                    response.ContractId = ContractId;
                    return new OkObjectResult(response);
                }
                else
                {
                    return NotFound();
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
        // POST api/v1/purchaseorder/completereturncontract
        [HttpPost("completereturncontract/{id}")]
        public async Task<IActionResult> CompleteReturnContractAsync([FromRoute]string id)
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



        // POST api/v1/purchaseorder/receivebarcodeadditems
        [HttpPost("receivebarcodeadditems")]
        public async Task<IActionResult> ReceiveBarCodeAddItems([FromBody] PurchaseOrderReceiveBarCodeAddItemsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PurchaseOrderReceiveBarCodeAddItemsResponse response = await ContractFunc.AddInventoryFromReceive(AppConfig, UserSession, request.PurchaseOrderId, request.ContractId);
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

    }
}
