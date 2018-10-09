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
namespace WebApi.Modules.Reports.ArAgingReport
{

    public class ArAgingReportRequest
    {
        public DateTime AsOfDate { get; set; }
        public string OfficeLocationId { get; set; }
        public string CustomerId { get; set; }
        public string DealCsrId { get; set; }
        public string DealTypeId { get; set; }
        public string DealId { get; set; }
    }



    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    public class ArAgingReportController : AppReportController
    {
        public ArAgingReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        protected override string GetReportFileName() { return "ArAgingReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "A/R Aging Report"; }
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
            return "ArAgingReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/aragingreport/render 
        [HttpPost("render")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/aragingreport/runreport 
        [HttpPost("runreport")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]ArAgingReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ArAgingReportLoader l = new ArAgingReportLoader();
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
