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
namespace WebApi.Modules.Reports.PickListReport
{
    public class PickListReportRequest
    {
        public string PickListId { get; set; }
    }

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    public class PickListReportController : AppReportController
    {
        public PickListReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        protected override string GetReportFileName() { return "PickListReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Pick List Report"; }
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
            return "PickListReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistreport/render 
        [HttpPost("render")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistreport/runreport 
        [HttpPost("runreport")]
        public async Task<ActionResult<PickListReportLoader>> RunReportAsync([FromBody]PickListReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PickListReportLoader l = new PickListReportLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                return new OkObjectResult(await l.RunReportAsync(request));
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
