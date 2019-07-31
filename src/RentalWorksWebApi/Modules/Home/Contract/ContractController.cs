using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Logic;
using System;
using Microsoft.AspNetCore.Http;
using WebLibrary;

namespace WebApi.Modules.Home.Contract
{

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"Z8MlDQp7xOqu")]
    public class ContractController : AppDataController
    {
        public ContractController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ContractLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/contract/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"aO0JWGjjIprZ")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/contract/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "d3Q7wt3ufSHTb")]
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
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"jCKeSS8CiNxw")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/contract 
        [HttpGet]
        [FwControllerMethod(Id:"Fm14zjtMVHLz")]
        public async Task<ActionResult<IEnumerable<ContractLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ContractLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/contract/A0000001 
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ContractLogic))]
        [ProducesResponseType(404)]
        [FwControllerMethod(Id:"jAXbxwqepki6")]
        public async Task<ActionResult<ContractLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ContractLogic>(id);
        }

        //------------------------------------------------------------------------------------ 
        // POST api/v1/contract 
        [HttpPost]
        [FwControllerMethod(Id:"t3psI38R6AMl")]
        public async Task<ActionResult<ContractLogic>> PostAsync([FromBody]ContractLogic l)
        {
            return await DoPostAsync<ContractLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/contract/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"AeKHviMBg3XP")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ContractLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/contract/cancelcontract 
        [HttpPost("cancelcontract")]
        [FwControllerMethod(Id: "S8ybdjuN7MU")]
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
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/contract/voidcontract 
        [HttpPost("voidcontract")]
        [FwControllerMethod(Id: "bwrnjBpQv1P")]
        public async Task<ActionResult<TSpStatusResponse>> VoidContract([FromBody]VoidContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TSpStatusResponse response = await ContractFunc.VoidContract(AppConfig, UserSession, request);
                return response;
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
