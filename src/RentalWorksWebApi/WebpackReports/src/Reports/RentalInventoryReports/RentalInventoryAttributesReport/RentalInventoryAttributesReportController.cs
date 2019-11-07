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
using WebApi.Modules.Reports.Shared.InventoryAttributesReport;
using static FwCore.Controllers.FwDataController;

namespace WebApi.Modules.Reports.RentalInventoryReports.RentalInventoryAttributesReport
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id:"CgvKJwiD2ew")]
    public class RentalInventoryAttributesReportController : AppReportController
    {
        public RentalInventoryAttributesReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        protected override string GetReportFileName() { return "RentalInventoryAttributesReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Rental Inventory Attributes Report"; }
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
            return "RentalInventoryAttributesReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventoryattributesreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id:"bjJzED9Fqb8")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "JzPIXExe78vP")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]InventoryAttributesReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalinventoryattributesreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id:"VpjB4XzHY5C")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]InventoryAttributesReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RentalInventoryAttributesReportLoader l = new RentalInventoryAttributesReportLoader();
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