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
namespace WebApi.Modules.Reports.IncomingDeliveryInstructions
{
    public class IncomingDeliveryInstructionsRequest : AppReportRequest
    {
        public string InDeliveryId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "9OT5LSvhJaTk3")]
    public class IncomingDeliveryInstructionsController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public IncomingDeliveryInstructionsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(IncomingDeliveryInstructionsLoader); }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName(FwReportRenderRequest request) { return "IncomingDeliveryInstructions"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Incoming Delivery Label"; }
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
            return "IncomingDeliveryInstructions";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/incomingdeliveryinstructions/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "9R8mDztxHNYCS")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody] FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/incomingdeliveryinstructions/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "9WDYFashyRsBU")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody] IncomingDeliveryInstructionsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IncomingDeliveryInstructionsLoader l = new IncomingDeliveryInstructionsLoader();
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
