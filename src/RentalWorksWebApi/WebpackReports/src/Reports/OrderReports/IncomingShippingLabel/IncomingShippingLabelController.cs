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
namespace WebApi.Modules.Reports.IncomingShippingLabel
{
    public class IncomingShippingLabelRequest : AppReportRequest
    {
        public string OrderId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "U2RQ1fjYwkIZ6")]
    public class IncomingShippingLabelController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public IncomingShippingLabelController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(IncomingShippingLabelLoader); }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName(FwReportRenderRequest request) { return "IncomingShippingLabel"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Incoming Shipping Label"; }
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
            return "IncomingShippingLabel";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/incomingshippinglabel/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "U2rXfWApGDmZf")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/incomingshippinglabel/exportexcelxlsx 
        //[HttpPost("exportexcelxlsx")]
        //[FwControllerMethod(Id: "U2yLAUzJo1p1D")]
        //public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]IncomingShippingLabelRequest request)
        //{
        //    ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
        //    FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
        //    return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        //}
        //------------------------------------------------------------------------------------ 
        // POST api/v1/incomingshippinglabel/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "u4XetZnLWwpio")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]IncomingShippingLabelRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IncomingShippingLabelLoader l = new IncomingShippingLabelLoader();
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
