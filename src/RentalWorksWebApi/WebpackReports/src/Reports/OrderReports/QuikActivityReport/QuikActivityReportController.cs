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

namespace WebApi.Modules.Reports.OrderReports.QuikActivityReport
{
    public class QuikActivityReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        //public bool? IncludeInUse { get; set; }
        public bool? OnlySubs { get; set; }
        //public string IsSummary { get; set; }
        public string RecType { get; set; }
        public string DepartmentId { get; set; }
        public string AgentId { get; set; }
        public string WarehouseId { get; set; }
        public string InventoryTypeId { get; set; }
        public string ActivityType { get; set; }
        public SelectedCheckBoxListItems OrderTypes { get; set; } = new SelectedCheckBoxListItems();
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "4hamhMOWKXD9")]
    public class QuikActivityReportController : AppReportController
    {
        public QuikActivityReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(QuikActivityReportLoader); }
        protected override string GetReportFileName() { return "QuikActivityReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "QuikActivity Report"; }
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
            return "QuikActivityReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/quikactivityreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "n4sPboI7Burl")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/quikactivityreport/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "wx2pWrULyPZA ")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]QuikActivityReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/quikactivityreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "MzpdTsKvrlk2")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]QuikActivityReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                QuikActivityReportLoader l = new QuikActivityReportLoader();
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
    }
}
