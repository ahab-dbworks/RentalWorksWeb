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

namespace WebApi.Modules.Reports.WarehouseReports.WarehouseInboundReport
{
    public class WarehouseInboundReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        //public string OfficeLocationId { get; set; }
        public string WarehouseId { get; set; }
        public string DepartmentId { get; set; }
        public string AgentId { get; set; }
        public string ActivityTypeId { get; set; }
        //public SelectedCheckBoxListItems SortBy { get; set; } = new SelectedCheckBoxListItems();
        //public SelectedCheckBoxListItems OrderTypes { get; set; } = new SelectedCheckBoxListItems();
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "gQLKq8L9zC32b")]
    public class WarehouseInboundReportController : AppReportController
    {
        public WarehouseInboundReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        protected override string GetReportFileName() { return "WarehouseInboundReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Warehouse Inbound Report"; }
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
            return "WarehouseInboundReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehouseinboundreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "gqVHY3jo0yDhF")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehouseinboundreport/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "GrrESd2CGEuvM")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]WarehouseInboundReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehouseinboundreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "GRv4Y5Uevs9TG")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]WarehouseInboundReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                WarehouseInboundReportLoader l = new WarehouseInboundReportLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                FwJsonDataTable dt = await l.RunReportAsync(request);
                //l.HideSummaryColumnsInDataTable(request, dt);
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
