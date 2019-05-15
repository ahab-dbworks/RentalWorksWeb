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
namespace WebApi.Modules.Reports.BillingAnalysisReport
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
        public BillingAnalysisReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
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
        // POST api/v1/billinganalysisreport/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "xBmBwO1MAHoD ")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BillingAnalysisReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt);
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
                return new OkObjectResult(dt);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
