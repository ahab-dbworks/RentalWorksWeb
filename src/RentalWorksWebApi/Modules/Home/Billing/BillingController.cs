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

namespace WebApi.Modules.Home.Billing
{
    public class PopulateBillingRequest
    {
        public DateTime BillAsOfDate { get; set; }
        public string OfficeLocationId { get; set; }
        public string CustomerId { get; set; }
        public string DealId { get; set; }
        public string DepartmentId { get; set; }
        public string AgentId { get; set; }
        public string OrderId { get; set; }
        public bool? ShowOrdersWithPendingPO { get; set; }
        public bool? BillIfComplete { get; set; }
        public bool? CombinePeriods { get; set; }

    }

    public class PopulateBillingResponse : TSpStatusResponse
    {
        public string SessionId { get; set; }
        public int BillingMessages { get; set; }
    }

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "67cZ8IUbw53c")]
    public class BillingController : AppDataController
    {
        public BillingController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(BillingLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billing/populate 
        [HttpPost("populate")]
        [FwControllerMethod(Id: "IkJY2GybFJCXf")]
        public async Task<ActionResult<PopulateBillingResponse>> Populate([FromBody]PopulateBillingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PopulateBillingResponse response = await BillingFunc.Populate(AppConfig, UserSession, request);
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
        // POST api/v1/billing/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "IailV9PQbfGc")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billing/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "wkAGDeyykITiq")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/billing/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "oUL3WodVBURC")]
        public async Task<ActionResult<BillingLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<BillingLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/billing/createinvoices 
        [HttpPost("createinvoices")]
        [FwControllerMethod(Id: "wgZGAuKCJv4Y")]
        public async Task<ActionResult<CreateInvoicesResponse>> CreateInvoices([FromBody]CreateInvoicesRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CreateInvoicesResponse response = await BillingFunc.CreateInvoices(AppConfig, UserSession, request);
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
