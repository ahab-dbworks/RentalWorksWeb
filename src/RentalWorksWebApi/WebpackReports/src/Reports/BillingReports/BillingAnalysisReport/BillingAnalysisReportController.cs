using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.Reporting;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PuppeteerSharp;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using static FwCore.Controllers.FwDataController;
using WebApi.Data;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi.Modules.Agent.Customer;
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Agent.Project;
using WebApi.Modules.Administrator.User;

namespace WebApi.Modules.Reports.Billing.BillingAnalysisReport
{
    public class BillingAnalysisReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string OfficeLocationId { get; set; }
        public string CustomerId { get; set; }
        public string DealId { get; set; }
        public string ProjectId { get; set; }
        public string AgentId { get; set; }
        public SelectedCheckBoxListItems Status { get; set; } = new SelectedCheckBoxListItems();
        public SelectedCheckBoxListItems IncludeFilter { get; set; } = new SelectedCheckBoxListItems();
        public SelectedCheckBoxListItems IncludeTaxFilter { get; set; } = new SelectedCheckBoxListItems();
        public bool? ExcludeOrdersBilledInTotal { get; set; }
        public bool? IncludeProjectStatus { get; set; }
        public bool? IncludeCreditsInvoiced { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "c2AwOP9UmJFw ")]
    public class BillingAnalysisReportController : AppReportController
    {
        public BillingAnalysisReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(BillingAnalysisReportLoader); }
        protected override string GetReportFileName() { return "BillingAnalysisReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Billing Analysis Report"; }
        //------------------------------------------------------------------------------------ 
        protected override PdfOptions GetPdfOptions()
        {
            // Configures Chromium for printing. Some of these properties are better to set in the @page section in CSS. 
            PdfOptions pdfOptions = new PdfOptions();
            pdfOptions.DisplayHeaderFooter = true;
            return pdfOptions;
        }
        //------------------------------------------------------------------------------------ 
        protected override string GetUniqueId(FwReportRenderRequest request)
        {
            //return request.parameters["xxxxid"].ToString().TrimEnd(); 
            return "BillingAnalysisReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billinganalysisreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "zhMdzRvkdIjq ")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billinganalysisreport/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "xBmBwO1MAHoD ")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BillingAnalysisReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billinganalysisreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "fuLdJvJXRTvAC")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]BillingAnalysisReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BillingAnalysisReportLoader l = new BillingAnalysisReportLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                FwJsonDataTable dt = await l.RunReportAsync(request);
                l.HideDetailColumnsInSummaryDataTable(request, dt);
                return new OkObjectResult(dt);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billinganalysisreport/validateofficelocation/browse 
        [HttpPost("validateofficelocation/browse")]
        [FwControllerMethod(Id: "0gH4X2vSLf7f", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        // POST api/v1/billinganalysisreport/validatecustomer/browse 
        [HttpPost("validatecustomer/browse")]
        [FwControllerMethod(Id: "1WIow2SfmvXj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCustomerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerLogic>(browseRequest);
        }
        // POST api/v1/billinganalysisreport/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "n7fAGOw3lWhB", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
        // POST api/v1/billinganalysisreport/validateproject/browse 
        [HttpPost("validateproject/browse")]
        [FwControllerMethod(Id: "QeYIaMeV6Gbj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateProjectBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ProjectLogic>(browseRequest);
        }
        // POST api/v1/billinganalysisreport/validateagent/browse 
        [HttpPost("validateagent/browse")]
        [FwControllerMethod(Id: "eKoJIcKFsyR0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAgentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
    }
}
