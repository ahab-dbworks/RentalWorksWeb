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
using WebApi.Modules.Reports.Shared.InventoryTransactionReport;
using static FwCore.Controllers.FwDataController;

namespace WebApi.Modules.Reports.SalesInventoryReports.SalesInventoryTransactionReport
{

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id:"uUxuUabbhO7ah")]
    public class SalesInventoryTransactionReportController : AppReportController
    {
        public SalesInventoryTransactionReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        protected override string GetReportFileName() { return "SalesInventoryTransactionReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Sales Inventory Transaction Report"; }
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
            return "SalesInventoryTransactionReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventorytransactionreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id:"i1iFq7kwLytBu")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "zUTvH14uUxe3")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]InventoryTransactionReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/salesinventorytransactionreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id:"BqLeqarYNe3hz")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]InventoryTransactionReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SalesInventoryTransactionReportLoader l = new SalesInventoryTransactionReportLoader();
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
