using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Logic;
using WebApi.Modules.HomeControls.Manifest;
using WebApi.Modules.Warehouse.Contract;
using WebLibrary;

//dummy-security-controller 
namespace WebApi.Modules.Transfers.TransferReceipt
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"VSn3weoOHqLc")]
    public class TransferReceiptController : AppDataController
    {
        public TransferReceiptController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ManifestLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/transferreceipt/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id: "zJJzkDtTE5SX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ManifestLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/transferreceipt/legend
        [HttpGet("legend")]
        [FwControllerMethod(Id: "VbFnWuW86m9C", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            //legend.Add("Unassigned Items", RwGlobals.QUOTE_ORDER_ON_HOLD_COLOR);
            //legend.Add("Pending Exchanges", RwGlobals.QUOTE_ORDER_NO_CHARGE_COLOR);
            legend.Add("Migrated", RwGlobals.CONTRACT_MIGRATED_COLOR);
            //legend.Add("Inactive Deal", RwGlobals.ORDER_LATE_COLOR);
            //legend.Add("Truck (No Charge)", RwGlobals.ORDER_LATE_COLOR);
            legend.Add("Adjusted Billing Date", RwGlobals.CONTRACT_BILLING_DATE_ADJUSTED_COLOR);
            legend.Add("Voided Items", RwGlobals.CONTRACT_ITEM_VOIDED_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/transferreceipt/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "BLpKFW1ozkAp", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync<ManifestLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/transferreceipt
        [HttpGet]
        [FwControllerMethod(Id: "Lc3Tgy1RN4bV", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<ManifestLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ManifestLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/transferreceipt/A0000001
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ManifestLogic))]
        [ProducesResponseType(404)]
        [FwControllerMethod(Id: "FuON7H9oJg7J", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<ManifestLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ManifestLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/transferreceipt
        [HttpPost]
        [FwControllerMethod(Id: "pRpQdFzLLrKT", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<ManifestLogic>> NewAsync([FromBody]ContractLogic l)
        {
            return await DoNewAsync<ManifestLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/transferreceipt/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "Bc2HvrYGJCem", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<ManifestLogic>> EditAsync([FromRoute] string id, [FromBody]ManifestLogic l)
        {
            return await DoEditAsync<ManifestLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/transferreceipt/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "hoq1knOEfuEz", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ManifestLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/transferreceipt/voidcontract
        [HttpPost("voidcontract")]
        [FwControllerMethod(Id: "OB2ssAaVMdIY", ActionType: FwControllerActionTypes.Option, Caption: "Void Contract")]
        public async Task<ActionResult<TSpStatusResponse>> VoidContractAsync([FromBody]VoidContractRequest request)
        {
            await Task.CompletedTask;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                throw new NotImplementedException();
                //TSpStatusResponse response = await ContractFunc.VoidContract(AppConfig, UserSession, request);
                //return response;
            }
            catch (Exception ex)
            {
                //FwApiException jsonException = new FwApiException();
                //jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                //jsonException.Message = ex.Message;
                //jsonException.StackTrace = ex.StackTrace;
                //return StatusCode(jsonException.StatusCode, jsonException);
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
    }
}

