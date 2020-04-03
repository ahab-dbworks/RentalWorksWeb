using FwStandard.AppManager;
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
using static FwCore.Controllers.FwDataController;

using WebApi.Data;
using WebApi.Modules.HomeControls.InvoiceCreationBatch;

namespace WebApi.Modules.Reports.Billing.CreateInvoiceProcessReport
{

    public class CreateInvoiceProcessReportRequest : AppReportRequest
    {
        public string InvoiceCreationBatchId { get; set; }
        public bool? ExceptionsOnly { get; set; }
    }


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id:"qhb1dkFRrS6T")]
    public class CreateInvoiceProcessReportController : AppReportController
    {
        public CreateInvoiceProcessReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(CreateInvoiceProcessReportLoader); }
        protected override string GetReportFileName(FwReportRenderRequest request) { return "CreateInvoiceProcessReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Create Invoice Process Report"; }
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
            return "CreateInvoiceProcessReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/createinvoiceprocessreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id:"2ZwDxRu7WuaI")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "oWX59kojq768")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]CreateInvoiceProcessReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/createinvoiceprocessreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id:"I3gMmg5S5PlA")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]CreateInvoiceProcessReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CreateInvoiceProcessReportLoader l = new CreateInvoiceProcessReportLoader();
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
        // POST api/v1/createinvoiceprocessreport/validateinvoicecreationbatch/browse 
        [HttpPost("validateinvoicecreationbatch/browse")]
        [FwControllerMethod(Id: "nMRLxjkWbqrN", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInvoiceCreationBatchBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InvoiceCreationBatchLogic>(browseRequest);
        }
    }
}
