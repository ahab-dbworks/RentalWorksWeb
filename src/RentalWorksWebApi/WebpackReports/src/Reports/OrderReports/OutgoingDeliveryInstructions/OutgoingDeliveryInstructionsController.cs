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
namespace WebApi.Modules.Reports.OutgoingDeliveryInstructions
{
    public class OutgoingDeliveryInstructionsRequest : AppReportRequest
    {
        public string OutgoingDeliveryId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "meEd0XqsUjqNZ")]
    public class OutgoingDeliveryInstructionsController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public OutgoingDeliveryInstructionsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(OutgoingDeliveryInstructionsLoader); }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName(FwReportRenderRequest request) { return "OutgoingDeliveryInstructions"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Outgoing Delivery Instructions"; }
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
            return "OutgoingDeliveryInstructions";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/outgoingdeliveryinstructions/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "O5Wz1qWSw3TOZ")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody] FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/outgoingdeliveryinstructions/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "IeLq2coVepwiQ")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody] OutgoingDeliveryInstructionsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OutgoingDeliveryInstructionsLoader l = new OutgoingDeliveryInstructionsLoader();
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
