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
using FwStandard.AppManager;
using static FwCore.Controllers.FwDataController;
using WebApi.Data;

namespace WebApi.Modules.Reports.DealReports.CustomerRevenueByMonthReport
{
    public class CustomerRevenueByMonthReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string OfficeLocationId { get; set; }
        public string DepartmentId { get; set; }
        public string CustomerTypeId { get; set; }
        public string CustomerId { get; set; }
        public string DealTypeId { get; set; }
        public string DealId { get; set; }
        public string InventoryTypeId { get; set; }
        //public bool? IsSummary { get; set; }
        public SelectedCheckBoxListItems RevenueTypes { get; set; } = new SelectedCheckBoxListItems();
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "40SdfVGkZPtA6")]
    public class CustomerRevenueByMonthReportController : AppReportController
    {
        public CustomerRevenueByMonthReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        protected override string GetReportFileName() { return "CustomerRevenueByMonthReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Customer Revenue By Month Report"; }
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
            return "CustomerRevenueByMonthReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customerrevenuebymonthreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "y0XR4t6KrO077")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customerrevenuebymonthreport/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "watIKCwBjv70t")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]CustomerRevenueByMonthReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customerrevenuebymonthreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "V1ccd7hozdm1G")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]CustomerRevenueByMonthReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CustomerRevenueByMonthReportLoader l = new CustomerRevenueByMonthReportLoader();
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
