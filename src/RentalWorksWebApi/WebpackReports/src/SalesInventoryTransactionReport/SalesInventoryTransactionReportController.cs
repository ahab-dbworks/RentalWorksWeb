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
namespace WebApi.Modules.Reports.SalesInventoryTransactionReport
{

    public class SalesInventoryTransactionReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public SelectedCheckBoxListItems TransactionTypes { get; set; } = new SelectedCheckBoxListItems();
        public string WarehouseId { get; set; }
        public string InventoryTypeId { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string InventoryId { get; set; }
    }

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
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
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventorytransactionreport/runreport 
        [HttpPost("runreport")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]SalesInventoryTransactionReportRequest request)
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
    }
}
