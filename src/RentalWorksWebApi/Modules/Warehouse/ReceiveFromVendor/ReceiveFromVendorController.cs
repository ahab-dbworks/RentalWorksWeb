using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using WebApi.Modules.Agent.PurchaseOrder;
using WebApi.Modules.Warehouse.Contract;
using System;
using System.Collections.Generic;
using WebApi.Logic;

namespace WebApi.Modules.Warehouse.ReceiveFromVendor
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"MtgBxCKWVl7m")]
    public class ReceiveFromVendorController : AppDataController
    {
        public ReceiveFromVendorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/receivefromvendor/suspendedsessionsexist
        [HttpGet("suspendedsessionsexist")]
        [FwControllerMethod(Id: "RyFgNYsAQk5p9", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<bool>> ReceiveSuspendedSessionsExist(string warehouseId)
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
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receivefromvendor/startsession
        [HttpPost("startsession")]
        [FwControllerMethod(Id: "Xs4EV6zXN8jsa", ActionType: FwControllerActionTypes.Option, Caption: "Start Receive Contract")]
        public async Task<ActionResult<ReceiveContractResponse>> StartReceiveContractAsync([FromBody]ReceiveContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReceiveContractResponse response = await PurchaseOrderFunc.CreateReceiveContract(AppConfig, UserSession, request);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/receivefromvendor/receiveitems
        [HttpPost("receiveitems")]
        [FwControllerMethod(Id: "wfjNbUnPwmx3k", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<ReceiveItemResponse>> ReceiveItems([FromBody] ReceiveItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (request.Quantity == 0)
                {
                    throw new Exception("Quantity cannot be zero.");
                }
                else
                {
                    ReceiveItemResponse response = await PurchaseOrderFunc.ReceiveItem(AppConfig, UserSession, request.ContractId, request.PurchaseOrderId, request.PurchaseOrderItemId, request.Quantity);
                    return new OkObjectResult(response);
                }
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/receivefromvendor/selectall
        [HttpPost("selectall")]
        [FwControllerMethod(Id: "vdlG8WoddsbA", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<SelectAllNoneReceiveItemResponse>> SelectAll([FromBody] SelectAllNoneReceiveItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SelectAllNoneReceiveItemResponse response = await PurchaseOrderFunc.SelectAllReceiveItem(AppConfig, UserSession, request.ContractId, request.PurchaseOrderId, request.WarehouseId);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/receivefromvendor/selectnone
        [HttpPost("selectnone")]
        [FwControllerMethod(Id: "MFcTZfrvaCGis", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<SelectAllNoneReceiveItemResponse>> SelectNone([FromBody] SelectAllNoneReceiveItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SelectAllNoneReceiveItemResponse response = await PurchaseOrderFunc.SelectNoneReceiveItem(AppConfig, UserSession, request.ContractId, request.PurchaseOrderId, request.WarehouseId);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receivefromvendor/completecontract
        [HttpPost("completecontract/{id}")]
        [FwControllerMethod(Id: "SFnrq53IYU6HS", ActionType: FwControllerActionTypes.Option, Caption: "Complete Receive Contract")]
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
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------       
        // POST api/v1/receivefromvendor/cancelcontract
        [HttpPost("cancelcontract")]
        [FwControllerMethod(Id: "I3eLaSxzy86FK", ActionType: FwControllerActionTypes.Option, Caption: "Cancel Contract")]
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
        // POST api/v1/receivefromvendor/validatepurchaseorder/browse 
        [HttpPost("validatepurchaseorder/browse")]
        [FwControllerMethod(Id: "hxUfPhdlF2uj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePurchaseOrderBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PurchaseOrderLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
