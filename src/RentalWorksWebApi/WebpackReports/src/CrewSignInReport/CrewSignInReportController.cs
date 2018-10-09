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

namespace WebApi.Modules.Reports.CrewSignInReport
{
    public class CrewSignInReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string OfficeLocationId { get; set; }
        public string DepartmentId { get; set; }
        public string CustomerId { get; set; }
        public string DealId { get; set; }
        public string OrderId { get; set; }
    }


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    public class CrewSignInReportController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public CrewSignInReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName() { return "CrewSignInReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Crew Sign-In Report"; }
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
            return request.parameters["crewcontactid"].ToString().TrimEnd();
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crewsigninreport/render 
        [HttpPost("render")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crewsigninreport/runreport 
        [HttpPost("runreport")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]CrewSignInReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CrewSignInReportLoader l = new CrewSignInReportLoader();
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