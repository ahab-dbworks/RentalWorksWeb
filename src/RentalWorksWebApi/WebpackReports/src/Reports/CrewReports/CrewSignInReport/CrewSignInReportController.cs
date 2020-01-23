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
using WebApi.Modules.Agent.Customer;
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Agent.Order;
using WebApi.Modules.Settings.CompanyDepartmentSettings.Department;

namespace WebApi.Modules.Reports.CrewReports.CrewSignInReport
{
    public class CrewSignInReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string OfficeLocationId { get; set; }
        public string DepartmentId { get; set; }
        public string CustomerId { get; set; }
        public string DealId { get; set; }
        public string OrderId { get; set; }
    }


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id:"kvDH1delOiUT")]
    public class CrewSignInReportController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public CrewSignInReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(CrewSignInReportLoader); }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName() { return "CrewSignInReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Crew Sign-In Report"; }
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
            return request.parameters["crewcontactid"].ToString().TrimEnd();
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crewsigninreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id:"aOiK050HXPgX")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest(this.ModelState);
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "BdFBdVcDbewE")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]CrewSignInReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/crewsigninreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id:"rQVKJ0Cz2uKH")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]CrewSignInReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CrewSignInReportLoader l = new CrewSignInReportLoader();
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
        // POST api/v1/crewsigninreport/validateofficelocation/browse 
        [HttpPost("validateofficelocation/browse")]
        [FwControllerMethod(Id: "nM8c5WLn3Qpk", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        // POST api/v1/crewsigninreport/validatecustomer/browse 
        [HttpPost("validatecustomer/browse")]
        [FwControllerMethod(Id: "hhPQOqHDN0wK", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCustomerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerLogic>(browseRequest);
        }
        // POST api/v1/crewsigninreport/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "GrJQ5tFEdf1E", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
        // POST api/v1/crewsigninreport/validateorder/browse 
        [HttpPost("validateorder/browse")]
        [FwControllerMethod(Id: "wJftGTXkjK0S", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOrderBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderLogic>(browseRequest);
        }
        // POST api/v1/crewsigninreport/validatedepartment/browse 
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "eyVpvPrwryGN", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
    }
}
