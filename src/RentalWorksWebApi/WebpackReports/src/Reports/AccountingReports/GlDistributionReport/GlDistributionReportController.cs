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
using WebApi.Modules.Settings.AccountingSettings.GlAccount;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi.Modules.Agent.Deal;

namespace WebApi.Modules.Reports.AccountingReports.GlDistributionReport
{

    public class GlDistributionReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string OfficeLocationId { get; set; }
        public string GlAccountId { get; set; }
        public string ExcludeGlAccountId { get; set; }
        public string DealId { get; set; }
        public bool? IsSomeDetail { get; set; }
        public bool? IsFullDetail { get; set; }
    }


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id:"ClMQ5QkZq4PY")]
    public class GlDistributionReportController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public GlDistributionReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(GlDistributionReportLoader); }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName(FwReportRenderRequest request) { return "GlDistributionReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "G/L Distribution Report"; }
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
            return "GlDistributionReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/gldistributionreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id:"rV3HPxEFt02l")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "kaPx70Y2LZuZ")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]GlDistributionReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, request);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/gldistributionreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id:"qCURx0Zt8fVW")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]GlDistributionReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GlDistributionReportLoader l = new GlDistributionReportLoader();
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
        // POST api/v1/gldistributionreport/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "7as7EIEvaBcS", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
        // POST api/v1/gldistributionreport/validateofficelocation/browse 
        [HttpPost("validateofficelocation/browse")]
        [FwControllerMethod(Id: "AcFmMkGydda8", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        // POST api/v1/gldistributionreport/validateglaccount/browse 
        [HttpPost("validateglaccount/browse")]
        [FwControllerMethod(Id: "96MO7yf7nMnP", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateGlAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
    }
}
