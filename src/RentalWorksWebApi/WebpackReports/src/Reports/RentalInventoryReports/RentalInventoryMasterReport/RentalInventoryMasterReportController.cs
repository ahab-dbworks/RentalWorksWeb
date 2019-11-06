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

namespace WebApi.Modules.Reports.RentalInventoryReports.RentalInventoryMasterReport
{
    public class RentalInventoryMasterReportRequest : AppReportRequest
    {
        public string WarehouseId { get; set; }
        public string InventoryTypeId { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string InventoryId { get; set; }
        public SelectedCheckBoxListItems Ranks { get; set; } = new SelectedCheckBoxListItems();
        public SelectedCheckBoxListItems TrackedBys { get; set; } = new SelectedCheckBoxListItems();
        public SelectedCheckBoxListItems Ownerships { get; set; } = new SelectedCheckBoxListItems();
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "hVovGdrneLrzq")]
    public class RentalInventoryMasterReportController : AppReportController
    {
        public RentalInventoryMasterReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        protected override string GetReportFileName() { return "RentalInventoryMasterReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Rental Inventory Master Report"; }
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
            return "RentalInventoryMasterReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventorymasterreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "ZjbtwHnaOEYve")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventorymasterreport/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "wPX8elYm2wjAN")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]RentalInventoryMasterReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventorymasterreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "vMMvP50FtuQYw")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]RentalInventoryMasterReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RentalInventoryMasterReportLoader l = new RentalInventoryMasterReportLoader();
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
