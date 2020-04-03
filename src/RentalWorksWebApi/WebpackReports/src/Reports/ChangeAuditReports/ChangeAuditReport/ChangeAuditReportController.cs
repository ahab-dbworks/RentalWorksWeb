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
using WebApi.Modules.Administrator.User;

namespace WebApi.Modules.Reports.ChangeAuditReports.ChangeAuditReport
{
    public class ChangeAuditReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string ModuleName { get; set; }
        public string WebUsersId { get; set; }
        public string Keyword { get; set; }

    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "hfM1GHEQCWBJ")]
    public class ChangeAuditReportController : AppReportController
    {
        public ChangeAuditReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(ChangeAuditReportLoader); }
        protected override string GetReportFileName(FwReportRenderRequest request) { return "ChangeAuditReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Change Audit Report"; }
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
            return "ChangeAuditReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/changeauditreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "Xz7qD10rB7BQ")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/changeauditreport/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "O6cK89TJOP8cs")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]ChangeAuditReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/changeauditreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "OV1FlQ0bmMmQ")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]ChangeAuditReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ChangeAuditReportLoader l = new ChangeAuditReportLoader();
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
        // POST api/v1/changeauditreport/validatewebusers/browse 
        [HttpPost("validatewebusers/browse")]
        [FwControllerMethod(Id: "5SHOctdEFvN8", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWebUsersBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
    }
}
