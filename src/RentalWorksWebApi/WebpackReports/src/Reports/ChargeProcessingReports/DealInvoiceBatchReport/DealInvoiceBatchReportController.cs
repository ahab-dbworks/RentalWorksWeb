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
using WebApi.Modules.Utilities.InvoiceProcessBatch;

namespace WebApi.Modules.Reports.ChargeProcessingReports.DealInvoiceBatchReport
{
    public class DealInvoiceBatchReportRequest : AppReportRequest
    {
        public string BatchId { get; set; }
        public string BatchNumber { get; set; }
        public DateTime BatchDate { get; set; }
    }

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "t3byQWNa3hZ4H")]
    public class DealInvoiceBatchReportController : AppReportController
    {
        public DealInvoiceBatchReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(DealInvoiceBatchReportLoader); }
        protected override string GetReportFileName(FwReportRenderRequest request) { return "DealInvoiceBatchReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Deal Invoice Batch"; }
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
            return "DealInvoiceBatchReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dealinvoicebatchreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "TayhUDJZ1gqjx")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dealinvoicebatchreport/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "OKkpJWgtUgU99")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]DealInvoiceBatchReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, request);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealinvoicebatchreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "yvXJUqbHRbd09")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]DealInvoiceBatchReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DealInvoiceBatchReportLoader l = new DealInvoiceBatchReportLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                FwJsonDataTable dt = await l.RunReportAsync(request);
                l.HideDetailColumnsInSummaryDataTable(request, dt);
                return new OkObjectResult(dt);
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------ 
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dealinvoicebatchreport/validatebatch/browse 
        [HttpPost("validatebatch/browse")]
        [FwControllerMethod(Id: "Tc9g3eHJ7Cbq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateBatchBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InvoiceProcessBatchLogic>(browseRequest);
        }
    }
}
