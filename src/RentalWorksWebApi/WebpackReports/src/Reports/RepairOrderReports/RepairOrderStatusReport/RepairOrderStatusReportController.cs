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

namespace WebApi.Modules.Reports.RepairOrderReports.RepairOrderStatusReport
{
    public class RepairOrderStatusReportRequest : AppReportRequest
    {
        public SelectedCheckBoxListItems RepairOrderStatus { get; set; } = new SelectedCheckBoxListItems();
        public SelectedCheckBoxListItems Priority { get; set; } = new SelectedCheckBoxListItems();
        public bool? Billable { get; set; } // true, false, null=all
        public bool? Billed { get; set; } // true, false, null=all
        public bool? Owned { get; set; } // true, false, null=all
        //public bool? IsSummary { get; set; }
        public int? DaysInRepair { get; set; }
        public string DaysInRepairFilterMode { get; set; } = "ALL";  // ALL/LTE/GT
        public bool? IncludeOutsideRepairsOnly { get; set; }
        public bool? IncludeDamageNotes { get; set; }
        public string WarehouseId { get; set; }
        public string DepartmentId { get; set; }
        public string InventoryTypeId { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string InventoryId { get; set; }
        public string RepairItemStatusId { get; set; }
        public string VendorId { get; set; }
        public string VendorRepairItemStatusId { get; set; }
        public string DealId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "KFP6Nq17ZuDPO")]
    public class RepairOrderStatusReportController : AppReportController
    {
        public RepairOrderStatusReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        protected override string GetReportFileName() { return "RepairOrderStatusReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Repair Order Status Report"; }
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
            return "RepairOrderStatusReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairorderstatusreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "adYNAXvamoVal")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairorderstatusreport/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "y3F1LE5C4DX2Y")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]RepairOrderStatusReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairorderstatusreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "Tstbs20Q0YZQX")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]RepairOrderStatusReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RepairOrderStatusReportLoader l = new RepairOrderStatusReportLoader();
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
