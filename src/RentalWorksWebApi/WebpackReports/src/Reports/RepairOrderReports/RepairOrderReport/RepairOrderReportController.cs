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
namespace WebApi.Modules.Reports.RepairReports.RepairOrderReport
{
    public class RepairOrderReportRequest : AppReportRequest
    {
        public string RepairId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "tqBQZCgmOUDcE")]
    public class RepairOrderReportController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public RepairOrderReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(RepairOrderReportLoader); }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName(FwReportRenderRequest request) { return "RepairOrderReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Repair Order"; }
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
            return "RepairOrderReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairorderreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "TqricDb6FkHxA")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairorderreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "TS3A3aA0f92hQ")]
        public async Task<ActionResult<RepairOrderReportLoader>> RunReportAsync([FromBody]RepairOrderReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RepairOrderReportLoader l = new RepairOrderReportLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                return new OkObjectResult(await l.RunReportAsync(request));
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
