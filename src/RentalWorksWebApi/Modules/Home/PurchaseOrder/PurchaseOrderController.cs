using FwStandard.AppManager;
using FwStandard.SqlServer;
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
using WebLibrary;

namespace WebApi.Modules.Home.PurchaseOrder
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"9a0xOMvBM7Uh9")]
    public class PurchaseOrderController : AppDataController
    {
        public PurchaseOrderController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PurchaseOrderLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorder/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"CT5m1NLaLhzuD")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchaseorder/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "RuAxro4XLWdOW")]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Not Approved", RwGlobals.PO_NEEDS_APPROVAL_COLOR);
            legend.Add("Drop Ship", RwGlobals.PO_DROP_SHIP_COLOR);
            legend.Add("Items in Holding", RwGlobals.PO_ITEMS_IN_HOLDING_COLOR);
            legend.Add("Items Needing Bar Code / Serial / RFID", RwGlobals.PO_ITEMS_NEED_BARCODE_COLOR);
            legend.Add("Foreign Currency", RwGlobals.FOREIGN_CURRENCY_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorder/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"UoKvbRlRbt1bF")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchaseorder 
        [HttpGet]
        [FwControllerMethod(Id:"LIKzNq6S5IzDy")]
        public async Task<ActionResult<IEnumerable<PurchaseOrderLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PurchaseOrderLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchaseorder/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"RFk1UEbjNbkyG")]
        public async Task<ActionResult<PurchaseOrderLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PurchaseOrderLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorder 
        [HttpPost]
        [FwControllerMethod(Id:"IRGS1gIXKz13P")]
        public async Task<ActionResult<PurchaseOrderLogic>> PostAsync([FromBody]PurchaseOrderLogic l)
        {
            return await DoPostAsync<PurchaseOrderLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/purchaseorder/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id:"slEQG9pj9a")]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await <PurchaseOrderLogic>DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 


        // POST api/v1/purchaseorder/applybottomlinedaysperweek
        [HttpPost("applybottomlinedaysperweek")]
        [FwControllerMethod(Id:"kS5BrDluy5bBu")]
        public async Task<ActionResult<bool>> ApplyBottomLineDaysPerWeek([FromBody] ApplyBottomLineDaysPerWeekRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = new string[] { request.PurchaseOrderId };

                PurchaseOrderLogic l = new PurchaseOrderLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<PurchaseOrderLogic>(ids))
                {
                    bool applied = await l.ApplyBottomLineDaysPerWeek(request);
                    return new OkObjectResult(true);
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
        // POST api/v1/purchaseorder/applybottomlinediscountpercent
        [HttpPost("applybottomlinediscountpercent")]
        [FwControllerMethod(Id:"i9rBnq4qjg6HC")]
        public async Task<ActionResult<bool>> ApplyBottomLineDiscountPercent([FromBody] ApplyBottomLineDiscountPercentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = new string[] { request.PurchaseOrderId };

                PurchaseOrderLogic l = new PurchaseOrderLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<PurchaseOrderLogic>(ids))
                {
                    bool applied = await l.ApplyBottomLineDiscountPercent(request);
                    return new OkObjectResult(true);
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
        // POST api/v1/purchaseorder/applybottomlinetotal
        [HttpPost("applybottomlinetotal")]
        [FwControllerMethod(Id:"vV0pObeeL5y4K")]
        public async Task<ActionResult<bool>> ApplyBottomLineTotal([FromBody] ApplyBottomLineTotalRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = new string[] { request.PurchaseOrderId };

                PurchaseOrderLogic l = new PurchaseOrderLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<PurchaseOrderLogic>(ids))
                {
                    bool applied = await l.ApplyBottomLineTotal(request);
                    return new OkObjectResult(true);
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


        // GET api/v1/purchaseorder/receivesuspendedsessionsexist
        [HttpGet("receivesuspendedsessionsexist")]
        [FwControllerMethod(Id: "RyFgNYsAQk5p9")]
        public async Task<ActionResult<bool>> RecevieSuspendedSessionsExist(string warehouseId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return await ContractFunc.SuspendedSessionsExist(AppConfig, UserSession, RwConstants.CONTRACT_TYPE_RECEIVE, warehouseId);
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


        // POST api/v1/purchaseorder/startreceivecontract
        [HttpPost("startreceivecontract")]
        [FwControllerMethod(Id:"Xs4EV6zXN8jsa")]
        public async Task<ActionResult<ReceiveContractResponse>> StartReceiveContractAsync([FromBody]ReceiveContractRequest request)
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
        [FwControllerMethod(Id:"SFnrq53IYU6HS")]
        public async Task<ActionResult<List<ContractLogic>>> CompleteReceiveContractAsync([FromRoute]string id, [FromBody] CompleteReceiveContractRequest request)
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


        // GET api/v1/purchaseorder/returnsuspendedsessionsexist
        [HttpGet("returnsuspendedsessionsexist")]
        [FwControllerMethod(Id: "zPuvlEmQXvmog")]
        public async Task<ActionResult<bool>> ReturnSuspendedSessionsExist(string warehouseId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return await ContractFunc.SuspendedSessionsExist(AppConfig, UserSession, RwConstants.CONTRACT_TYPE_RETURN, warehouseId);
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
        [FwControllerMethod(Id:"IHQC7YuIlyflM")]
        public async Task<ActionResult<ReturnContractResponse>> StartReturnContractAsync([FromBody]ReturnContractRequest request)
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
        [FwControllerMethod(Id:"Yu4sDt9BpjVrt")]
        public async Task<ActionResult<ContractLogic>> CompleteReturnContractAsync([FromRoute]string id)
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
        // POST api/v1/purchaseorder/receivebarcodeadditems
        [HttpPost("receivebarcodeadditems")]
        [FwControllerMethod(Id:"x7nZuntw3E0dk")]
        public async Task<ActionResult<PurchaseOrderReceiveBarCodeAddItemsResponse>> ReceiveBarCodeAddItems([FromBody] PurchaseOrderReceiveBarCodeAddItemsRequest request)
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
        // POST api/v1/purchaseorder/assignbarcodesfromreceive
        [HttpPost("assignbarcodesfromreceive")]
        [FwControllerMethod(Id:"RFMr1ZCHMVvo4")]
        public async Task<ActionResult<PurchaseOrderReceiveAssignBarCodesResponse>> AssignBarCodesFromReceive([FromBody] PurchaseOrderReceiveAssignBarCodesRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PurchaseOrderReceiveAssignBarCodesResponse response = await ContractFunc.AssignBarCodesFromReceive(AppConfig, UserSession, request.PurchaseOrderId, request.ContractId);
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
        // POST api/v1/purchaseorder/void/A0000001
        [HttpPost("void/{id}")]
        [FwControllerMethod(Id: "u5eAwyixomSFN")]
        public async Task<ActionResult<PurchaseOrderLogic>> Void([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                PurchaseOrderLogic l = new PurchaseOrderLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<PurchaseOrderLogic>(ids))
                {
                    VoidPurchaseOrderRequest response = await l.Void(id);
                    if (response.success)
                    {
                        await l.LoadAsync<PurchaseOrderLogic>(ids);
                        return new OkObjectResult(l);
                    }
                    else
                    {
                        throw new Exception(response.msg);
                    }

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
        // GET api/v1/purchaseorder/nextvendorinvoicedefaultdates/A0000001
        [HttpGet("nextvendorinvoicedefaultdates/{PurchaseOrderId}")]
        [FwControllerMethod(Id: "e4lReUTArJ5Kg")]
        public async Task<ActionResult<NextVendorInvoiceDefaultDatesResponse>> GetNextVendorInvoiceDefaultDates([FromRoute] string PurchaseOrderId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                NextVendorInvoiceDefaultDatesResponse response = await PurchaseOrderFunc.GetNextVendorInvoiceDefaultDates(AppConfig, UserSession, PurchaseOrderId);
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
