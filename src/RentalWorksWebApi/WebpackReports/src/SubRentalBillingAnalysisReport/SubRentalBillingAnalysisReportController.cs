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

namespace WebApi.Modules.Reports.SubRentalBillingAnalysisReport
{
    public class SubRentalBillingAnalysisReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string OfficeLocationId { get; set; }
        public string DepartmentId { get; set; }
        public string DealId { get; set; }
        public string VendorId { get; set; }
        public string PoClassificationId { get; set; }
        public string PurchaseOrderId { get; set; }
        public string InventoryId { get; set; }
        public SelectedCheckBoxListItems InvoiceStatus { get; set; } = new SelectedCheckBoxListItems();
        public SelectedCheckBoxListItems PurchaseOrderStatus { get; set; } = new SelectedCheckBoxListItems();
        public bool? IncludeVendorTax { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "KIE1O1i2tvtsu")]
    public class SubRentalBillingAnalysisReportController : AppReportController
    {
        public SubRentalBillingAnalysisReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        protected override string GetReportFileName() { return "SubRentalBillingAnalysisReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Sub-Rental Billing Analysis Report"; }
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
            return "SubRentalBillingAnalysisReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subrentalbillinganalysisreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "LcFu22KourA0 ")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subrentalbillinganalysisreport/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "pIeArCKE037N ")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]SubRentalBillingAnalysisReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subrentalbillinganalysisreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "MVV3v96UE4jV ")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]SubRentalBillingAnalysisReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SubRentalBillingAnalysisReportLoader l = new SubRentalBillingAnalysisReportLoader();
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
