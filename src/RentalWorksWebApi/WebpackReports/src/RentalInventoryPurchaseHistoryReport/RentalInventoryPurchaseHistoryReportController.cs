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
using WebApi.Modules.Reports.InventoryPurchaseHistoryReport;

namespace WebApi.Modules.Reports.RentalInventoryPurchaseHistoryReport
{
    //public class RentalInventoryPurchaseHistoryReportRequest
    //{
    //    public DateTime? PurchasedFromDate { get; set; }
    //    public DateTime? PurchasedToDate { get; set; }
    //    public DateTime? ReceivedFromDate { get; set; }
    //    public DateTime? ReceivedToDate { get; set; }
    //    public SelectedCheckBoxListItems TrackedBys { get; set; } = new SelectedCheckBoxListItems();
    //    public SelectedCheckBoxListItems Ranks { get; set; } = new SelectedCheckBoxListItems();
    //    public string WarehouseId { get; set; }
    //    public string InventoryTypeId { get; set; }
    //    public string CategoryId { get; set; }
    //    public string SubCategoryId { get; set; }
    //    public string InventoryId { get; set; }
    //}
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id:"kI5HgFqlPzr")]
    public class RentalInventoryPurchaseHistoryReportController : AppReportController
    {
        public RentalInventoryPurchaseHistoryReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        protected override string GetReportFileName() { return "RentalInventoryPurchaseHistoryReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Rental Inventory Purchase History Report"; }
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
            return "RentalInventoryPurchaseHistoryReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventorypurchasehistoryreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id:"Dro2zkdnIyr")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventorypurchasehistoryreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id:"azR5VwuQTaf")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]InventoryPurchaseHistoryReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RentalInventoryPurchaseHistoryReportLoader l = new RentalInventoryPurchaseHistoryReportLoader();
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
