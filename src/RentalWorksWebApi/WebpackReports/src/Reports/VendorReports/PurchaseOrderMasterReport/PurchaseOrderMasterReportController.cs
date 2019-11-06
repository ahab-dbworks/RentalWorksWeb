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

namespace WebApi.Modules.Reports.VendorReports.PurchaseOrderMasterReport
{
    public class PurchaseOrderMasterReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string WarehouseId { get; set; }
        public string DepartmentId { get; set; }
        public string VendorId { get; set; }
        public SelectedCheckBoxListItems Statuses { get; set; } = new SelectedCheckBoxListItems();
        public SelectedCheckBoxListItems Activities { get; set; } = new SelectedCheckBoxListItems();
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "zJJl8Frw1U7U7")]
    public class PurchaseOrderMasterReportController : AppReportController
    {
        public PurchaseOrderMasterReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        protected override string GetReportFileName() { return "PurchaseOrderMasterReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Purchase Order Master Report"; }
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
            return "PurchaseOrderMasterReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseordermasterreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "VsFLMcFmrowT5")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseordermasterreport/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "lljmgwCshPBQv")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]PurchaseOrderMasterReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseordermasterreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "Tq8lCWmAtOMQ6")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]PurchaseOrderMasterReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PurchaseOrderMasterReportLoader l = new PurchaseOrderMasterReportLoader();
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
