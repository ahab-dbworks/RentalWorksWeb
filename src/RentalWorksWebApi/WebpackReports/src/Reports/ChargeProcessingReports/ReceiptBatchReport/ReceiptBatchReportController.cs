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
using WebApi.Modules.Utilities.ReceiptProcessBatch;

namespace WebApi.Modules.Reports.ChargeProcessingReports.ReceiptBatchReport
{
    public class ReceiptBatchReportRequest : AppReportRequest
    {
        public string BatchId { get; set; }
        public string BatchNumber { get; set; }
        public DateTime BatchDate { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "jB7p9OvmCibhx")]
    public class ReceiptBatchReportController : AppReportController
    {
        public ReceiptBatchReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(ReceiptBatchReportLoader); }
        protected override string GetReportFileName() { return "ReceiptBatchReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Receipt Batch"; }
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
            return "ReceiptBatchReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receiptbatchreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "26lgBmURqwiE")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest(this.ModelState);
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receiptbatchreport/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "trPTReKnJmWm4")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]ReceiptBatchReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/receiptbatchreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "by05ybTkfJFg")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]ReceiptBatchReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReceiptBatchReportLoader l = new ReceiptBatchReportLoader();
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
        // POST api/v1/receiptbatchreport/validatebatch/browse 
        [HttpPost("validatebatch/browse")]
        [FwControllerMethod(Id: "OxnNHyNB2sJv", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateBatchBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ReceiptProcessBatchLogic>(browseRequest);
        }
    }
}
