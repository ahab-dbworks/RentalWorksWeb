using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Logic;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi.Modules.Agent.Customer;
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Settings.CompanyDepartmentSettings.Department;
using WebApi.Modules.Administrator.User;
using WebApi.Modules.Agent.Order;

namespace WebApi.Modules.Billing.Billing
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
        public bool? IncludeTotals { get; set; }
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
        [FwControllerMethod(Id: "IkJY2GybFJCXf", FwControllerActionTypes.Browse)]
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
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billing/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "IailV9PQbfGc", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billing/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "wkAGDeyykITiq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/billing/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "oUL3WodVBURC", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<BillingLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<BillingLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/billing/createinvoices 
        [HttpPost("createinvoices")]
        [FwControllerMethod(Id: "wgZGAuKCJv4Y", ActionType: FwControllerActionTypes.Option, Caption: "Create Invoices")]
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
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billing/validateofficelocation/browse 
        [HttpPost("validateofficelocation/browse")]
        [FwControllerMethod(Id: "MulMGf46Tq7w", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billing/validatecustomer/browse 
        [HttpPost("validatecustomer/browse")]
        [FwControllerMethod(Id: "4IlaDdUragKb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCustomerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billing/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "P9555AHhocHp", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billing/validatedepartment/browse 
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "Ohao6ElKp5Hp", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billing/validateuser/browse 
        [HttpPost("validateuser/browse")]
        [FwControllerMethod(Id: "dIdFCkOGSj6G", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateUserBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billing/validateorder/browse 
        [HttpPost("validateorder/browse")]
        [FwControllerMethod(Id: "k9atS0j2b5ww", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOrderBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderLogic>(browseRequest);
        }
    }
}
