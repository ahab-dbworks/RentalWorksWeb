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

namespace WebApi.Modules.Reports.AgentBillingReport
{

    public class AgentBillingReportRequest
    {
        public DateTime FromDate;
        public DateTime ToDate;
        public string DateType;
        public bool? IncludeNoCharge;
        public string OfficeLocationId;
        public string DepartmentId;
        public string AgentId;
        public string CustomerId;
        public string DealId;
    }


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    public class AgentBillingReportController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public AgentBillingReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName() { return "AgentBillingReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Agent Billing Report"; }
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
            //return request.parameters["contractid"].ToString().TrimEnd();
            return "AgentBillingReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/agentbillingreport/render 
        [HttpPost("render")]
        public async Task<IActionResult> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/agentbillingreport/runreport 
        [HttpPost("runreport")]
        public async Task<IActionResult> RunReportAsync([FromBody]AgentBillingReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgentBillingReportLoader l = new AgentBillingReportLoader();
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