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

namespace WebApi.Modules.Reports.ProjectManagerBillingReport
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    public class ProjectManagerBillingReportController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public ProjectManagerBillingReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        public override string GetReportFileName() { return "ProjectManagerBillingReport"; }
        //------------------------------------------------------------------------------------ 
        public override string GetReportFriendlyName() { return "Project Manager Billing Report"; }
        //------------------------------------------------------------------------------------ 
        public override PdfOptions GetPdfOptions()
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
            return "ProjectManagerBillingReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectmanagerbillingreport/render 
        [HttpPost("render")]
        public async Task<IActionResult> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectmanagerbillingreport/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            //return await DoBrowseAsync(browseRequest);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProjectManagerBillingReportLogic l = new ProjectManagerBillingReportLogic();
                l.SetDependencies(this.AppConfig, this.UserSession);
                FwJsonDataTable dt = await l.BrowseAsync(browseRequest);
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