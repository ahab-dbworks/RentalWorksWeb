using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using FwStandard.Reporting;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using static FwCore.Controllers.FwDataController;

using WebApi.Data;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi.Modules.Settings.DepartmentSettings.Department;
using WebApi.Modules.Agent.Customer;
using WebApi.Modules.Settings.DealSettings.DealType;
using WebApi.Modules.Settings.DealSettings.DealStatus;
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Administrator.User;

namespace WebApi.Modules.Reports.Billing.ProjectManagerBillingReport
{

    public class ProjectManagerBillingReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string DateType { get; set; }
        public bool? IncludeNoCharge { get; set; }
        public string OfficeLocationId { get; set; }
        public string DepartmentId { get; set; }
        public string ProjectManagerId { get; set; }
        public string CustomerId { get; set; }
        public string DealId { get; set; }
    }



    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "lTDdAGi63jRVL")]
    public class ProjectManagerBillingReportController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public ProjectManagerBillingReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(ProjectManagerBillingReportLoader); }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName(FwReportRenderRequest request) { return "ProjectManagerBillingReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Project Manager Billing Report"; }
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
            //return request.parameters["contractid"].ToString().TrimEnd();
            return "ProjectManagerBillingReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectmanagerbillingreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "fS9GiroGDzpgI")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "lofSA5NpsaIc")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]ProjectManagerBillingReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, request);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/projectmanagerbillingreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "zhzqeRxcjE0oa")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]ProjectManagerBillingReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProjectManagerBillingReportLoader l = new ProjectManagerBillingReportLoader();
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
        // POST api/v1/projectmanagerbillingreport/validateofficelocation/browse 
        [HttpPost("validateofficelocation/browse")]
        [FwControllerMethod(Id: "ykzCUaAgS45t", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectmanagerbillingreport/validatedepartment/browse 
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "EyGX7U2zRtFm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectmanagerbillingreport/validatecustomer/browse 
        [HttpPost("validatecustomer/browse")]
        [FwControllerMethod(Id: "kXE9QPfA4pgq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCustomerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectmanagerbillingreport/validateprojectmanager/browse 
        [HttpPost("validateprojectmanager/browse")]
        [FwControllerMethod(Id: "kpFk5Y4C7DXO", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateProjectManagerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectmanagerbillingreport/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "PxHSUghfPns6", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
    }
}
