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
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi.Modules.Administrator.User;
using WebApi.Modules.Settings.CompanyDepartmentSettings.Department;
using WebApi.Modules.Agent.Customer;
using WebApi.Modules.Agent.Contact;

namespace WebApi.Modules.Reports.Billing.SalesRepresentativeBillingReport
{

    public class SalesRepresentativeBillingReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string DateType { get; set; }
        public bool? IncludeNoCharge { get; set; }
        public string OfficeLocationId { get; set; }
        public string DepartmentId { get; set; }
        public string SalesRepresentativeId { get; set; }
        public string CustomerId { get; set; }
        public string DealId { get; set; }
    }

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "SgF8AMJVjKARN")]
    public class SalesRepresentativeBillingReportController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public SalesRepresentativeBillingReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(SalesRepresentativeBillingReportLoader); }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName() { return "SalesRepresentativeBillingReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Sales Representative Billing Report"; }
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
            return "SalesRepresentativeBillingReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesrepresentativebillingreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "EsTDsoD2wwzjJ")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)

        {
            if (!this.ModelState.IsValid) return BadRequest(this.ModelState);
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "fTvTmHR0kHYe")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]SalesRepresentativeBillingReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/salesrepresentativebillingreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "ot8i27Z2PZZCV")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]SalesRepresentativeBillingReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SalesRepresentativeBillingReportLoader l = new SalesRepresentativeBillingReportLoader();
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
        // POST api/v1/salesrepresentativebillingreport/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "0U1xGYABDYV9", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesrepresentativebillingreport/validateofficelocation/browse 
        [HttpPost("validateofficelocation/browse")]
        [FwControllerMethod(Id: "StjReaN9aebt", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesrepresentativebillingreport/validatesalesrepresentative/browse 
        [HttpPost("validatesalesrepresentative/browse")]
        [FwControllerMethod(Id: "syTNhC2shQyl", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSalesRepresentativeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContactLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesrepresentativebillingreport/validatedepartment/browse 
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "lkbvbXCygvn9", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesrepresentativebillingreport/validatecustomer/browse 
        [HttpPost("validatecustomer/browse")]
        [FwControllerMethod(Id: "yYnhz7WVTHej", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCustomerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerLogic>(browseRequest);
        }
    }
}
