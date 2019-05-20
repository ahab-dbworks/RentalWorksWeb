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
namespace WebApi.Modules.Reports.SalesBackorderReport
{
    public class SalesBackorderReportRequest : AppReportRequest
    {
        public string VendorId { get; set; }
        public string DealId { get; set; }
        public string WarehouseId { get; set; }
        public string InventoryTypeId { get; set; }
        public string CustomerId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "KMdFiy9SEIQEU")]
    public class SalesBackorderReportController : AppReportController
    {
        public SalesBackorderReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        protected override string GetReportFileName() { return "SalesBackorderReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Sales Backorder Report"; }
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
            return "SalesBackorderReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesbackorderreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "qbCxnhEoMvlGf")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesbackorderreport/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "cUnoYEMA7McmG")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]SalesBackorderReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesbackorderreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "WO5Q9nOaklCaJ")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]SalesBackorderReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SalesBackorderReportLoader l = new SalesBackorderReportLoader();
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
