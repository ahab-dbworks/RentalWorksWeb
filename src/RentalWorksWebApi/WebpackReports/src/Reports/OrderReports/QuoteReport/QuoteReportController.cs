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
using WebApi.Modules.Agent.Quote;
using WebApi.Modules.Reports.OrderReports.OrderReport;

namespace WebApi.Modules.Reports.OrderReports.QuoteReport
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "SZ80uvR5NjI7")]
    public class QuoteReportController : AppReportController
    {
        public QuoteReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(QuoteReportLoader); }
        protected override string GetReportFileName() { return "QuoteReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Quote Report"; }
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
            return request.parameters["OrderId"].ToString().TrimEnd(); 
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/quotereport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "WRFc9aKUUkRq")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/quotereport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "uSkWW0LRLNXF")]
        public async Task<ActionResult<QuoteReportLoader>> RunReportAsync([FromBody]OrderReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                QuoteReportLoader l = new QuoteReportLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                return new OkObjectResult(await l.RunReportAsync(request));               
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/quotereport/validatequote/browse 
        [HttpPost("validatequote/browse")]
        [FwControllerMethod(Id: "W8etU3923lAW", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateQuoteBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<QuoteLogic>(browseRequest);
        }
    }
}
