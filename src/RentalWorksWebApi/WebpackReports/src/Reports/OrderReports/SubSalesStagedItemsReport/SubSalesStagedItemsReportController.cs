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
using static FwCore.Controllers.FwDataController;
using WebApi.Data;

namespace WebApi.Modules.Reports.OrderReports.SubSalesStagedItemsReport
{
    public class SubSalesStagedItemsReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string DateType { get; set; }
        public bool? IncludeNoCharge { get; set; }
        // for office/transaction reports 
        public string OfficeLocationId { get; set; }
        public string DepartmentId { get; set; }
        public string AgentId { get; set; }
        public string CustomerId { get; set; }
        public string DealId { get; set; }
        // for inventory reports 
        public string WarehouseId { get; set; }
        public string InventoryTypeId { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string InventoryId { get; set; }
        public SelectedCheckBoxListItems Ranks { get; set; } = new SelectedCheckBoxListItems();
        public SelectedCheckBoxListItems TrackedBys { get; set; } = new SelectedCheckBoxListItems();
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "2GIJvJlbIFQN")]
    public class SubSalesStagedItemsReportController : AppReportController
    {
        public SubSalesStagedItemsReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(SubSalesStagedItemsReportLoader); }
        protected override string GetReportFileName(FwReportRenderRequest request) { return "SubSalesStagedItemsReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Sub-Sales Staged Items Report"; }
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
            return "SubSalesStagedItemsReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subsalesstageditemsreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "dzdPQJ6nkyho ")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subsalesstageditemsreport/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "tfyI9vNfqQ77 ")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]SubSalesStagedItemsReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, request);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subsalesstageditemsreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "X2Luav4KmBHE ")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]SubSalesStagedItemsReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SubSalesStagedItemsReportLoader l = new SubSalesStagedItemsReportLoader();
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
