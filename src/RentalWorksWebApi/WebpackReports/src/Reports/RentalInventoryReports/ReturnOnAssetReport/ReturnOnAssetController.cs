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

namespace WebApi.Modules.Reports.RentalInventoryReports.ReturnOnAssetReport
{
    public class ReturnOnAssetReportRequest : AppReportRequest
    {
        public bool? UsePeriodSelector { get; set; }
        public string ReportYear { get; set; }
        public string ReportPeriod { get; set; }
        public bool? UseDateRange { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string WarehouseId { get; set; }
        public string InventoryTypeId { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string InventoryId { get; set; }
        public SelectedCheckBoxListItems Ranks { get; set; } = new SelectedCheckBoxListItems();
        public SelectedCheckBoxListItems TrackedBys { get; set; } = new SelectedCheckBoxListItems();
        public bool? IncludeZeroCurrentOwned { get; set; }
        public bool? IncludeZeroAverageOwned { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "15TIjoDzY09G")]
    public class ReturnOnAssetReportController : AppReportController
    {
        public ReturnOnAssetReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        protected override string GetReportFileName() { return "ReturnOnAssetReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Return On Asset"; }
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
            return "ReturnOnAssetReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/returnonassetreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "vxkDs5fdH9xK")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "PbOEHrTE6udG")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]ReturnOnAssetReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/returnonassetreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "ZoQDFHjFP2pe")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]ReturnOnAssetReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FwJsonDataTable dt = null;
                if (request.UseDateRange.GetValueOrDefault(false))
                {
                    ReturnOnAssetByDateRangeReportLoader l = new ReturnOnAssetByDateRangeReportLoader();
                    l.SetDependencies(this.AppConfig, this.UserSession);
                    dt = await l.RunReportAsync(request);
                l.HideDetailColumnsInSummaryDataTable(request, dt);
                }
                else
                {
                    ReturnOnAssetByPeriodReportLoader l = new ReturnOnAssetByPeriodReportLoader();
                    l.SetDependencies(this.AppConfig, this.UserSession);
                    dt = await l.RunReportAsync(request);
                }
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
