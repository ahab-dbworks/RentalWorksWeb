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
using WebApi.Modules.Utilities.RateUpdateBatch;

namespace WebApi.Modules.Reports.RateUpdateReport
{
    public class RateUpdateReportRequest : AppReportRequest
    {
        public bool? PendingModificationsOnly { get; set; }
        public string RateUpdateBatchId { get; set; }    // this field is ignored if PendingModificationsOnly is true
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "Nxb4NonfG10c9")]
    public class RateUpdateReportController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public RateUpdateReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RateUpdateReportLoader); }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName(FwReportRenderRequest request) { return "RateUpdateReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Rate Update Report"; }
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
            return "RateUpdateReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdatereport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "nxJnd37ypwait")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdatereport/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "nXpmAayJpq2HZ")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]RateUpdateReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdatereport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "NxZOgUmgj0ErH")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]RateUpdateReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RateUpdateReportLoader l = new RateUpdateReportLoader();
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
        // POST api/v1/rateupdatereport/validatebatch/browse
        [HttpPost("validatebatch/browse")]
        [FwControllerMethod(Id: "ErntnasdVvEI", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateBatchBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RateUpdateBatchLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
    }
}
