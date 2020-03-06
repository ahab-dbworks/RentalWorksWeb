using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi.Modules.Agent.Customer;
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Settings.CompanyDepartmentSettings.Department;
using WebApi.Modules.Administrator.User;
using WebApi.Modules.Agent.Order;
using System.Collections.Generic;
using WebApi.Modules.HomeControls.BillingMessage;
using WebApi.Modules.Warehouse.Contract;

namespace WebApi.Modules.Billing.Billing
{
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
        // GET api/v1/billing/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "D6LHhXoFGn9eL", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            //legend.Add("Missing Crew Times", "#FF9D9D");
            //legend.Add("Missing Break Times", "#B7B7FF");
            legend.Add("No Charge", RwGlobals.QUOTE_ORDER_NO_CHARGE_COLOR);
            legend.Add("Outside Order Billing Dates", "#00FF00");
            legend.Add("Flat PO", "#8888FF");
            legend.Add("Repair", RwGlobals.ORDER_REPAIR_COLOR);
            //legend.Add("Rebill Adds", "#F709DF");
            legend.Add("Has Billing Note", "#00FFFF");
            legend.Add("PO Pending", RwGlobals.ORDER_PENDING_PO_COLOR);
            legend.Add("L&D", RwGlobals.ORDER_LOSS_AND_DAMAGE_COLOR);
            legend.Add("Hiatus", "#00B95C");

            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
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
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billing/billingmessage/browse 
        [HttpPost("billingmessage/browse")]
        [FwControllerMethod(Id: "oUxoh1MAIyUm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BillingMessage_BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<BillingMessageLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/billing/contract/legend
        [HttpGet("contract/legend")]
        [FwControllerMethod(Id: "e9n2Jt5URsFJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<Dictionary<string, string>>> Contract_GetLegendAsync()
        {
            ContractLogic contract = (ContractLogic)CreateBusinessLogic(typeof(ContractLogic), this.AppConfig, this.UserSession);
            return new OkObjectResult(await contract.GetLegend());
        }
        //------------------------------------------------------------------------------------ 
    }
}
