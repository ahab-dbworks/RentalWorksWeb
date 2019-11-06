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
using System.Collections.Generic;
using System.Text;
using static FwCore.Controllers.FwDataController;

using WebApi.Data;

namespace WebApi.Modules.Reports.Billing.InvoiceSummaryReport
{

    public class InvoiceSummaryReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string DateType { get; set; }
        public bool? IncludeNoCharge { get; set; }
        public SelectedCheckBoxListItems Statuses { get; set; } = new SelectedCheckBoxListItems();
        public string OfficeLocationId { get; set; }
        public string DepartmentId { get; set; }
        public string CustomerId { get; set; }
        public string DealId { get; set; }
    }

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id:"LeLwkS6yUBfV")]
    public class InvoiceSummaryReportController : AppReportController
    {
        public InvoiceSummaryReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        protected override string GetReportFileName() { return "InvoiceSummaryReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Invoice Summary Report"; }
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
            return "InvoiceSummaryReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoicesummaryreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id:"WOX6IuhVMSRA")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "bsuzBOnZrC1D")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]InvoiceSummaryReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/invoicesummaryreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id:"5deElRqBmsbq")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]InvoiceSummaryReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InvoiceSummaryReportLoader l = new InvoiceSummaryReportLoader();
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
