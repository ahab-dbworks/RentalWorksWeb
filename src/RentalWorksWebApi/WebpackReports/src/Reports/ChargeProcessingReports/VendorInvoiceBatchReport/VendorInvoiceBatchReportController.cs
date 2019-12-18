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
using WebApi.Modules.Utilities.VendorInvoiceProcessBatch;

namespace WebApi.Modules.Reports.ChargeProcessingReports.VendorInvoiceBatchReport
{
    public class VendorInvoiceBatchReportRequest : AppReportRequest
    {
        public string BatchId { get; set; }
        public string BatchNumber { get; set; }
        public DateTime BatchDate { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "6gc3QbSGD5BX")]
    public class VendorInvoiceBatchReportController : AppReportController
    {
        public VendorInvoiceBatchReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(VendorInvoiceBatchReportLoader); }
        protected override string GetReportFileName() { return "VendorInvoiceBatchReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Vendor Invoice Batch"; }
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
            return "VendorInvoiceBatchReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoicebatchreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "EWPTjonPVzSXD")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoicebatchreport/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "kl6Z9PDkVHBxL")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]VendorInvoiceBatchReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorinvoicebatchreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "2ISh1KpGJEod")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]VendorInvoiceBatchReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                VendorInvoiceBatchReportLoader l = new VendorInvoiceBatchReportLoader();
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
        // POST api/v1/vendorinvoicebatchreport/validatebatch/browse 
        [HttpPost("validatebatch/browse")]
        [FwControllerMethod(Id: "RuYUXWclehqm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateBatchBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorInvoiceProcessBatchLogic>(browseRequest);
        }
    }
}
