using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using System.IO;
using FwCore.Controllers;
using System;
using System.Diagnostics;
using FwStandard.Reporting;
using System.Dynamic;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace WebApi.Modules.Reports.ContractReport
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    public class OutContractReportController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public OutContractReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName() { return "OutContractReport"; }
        protected override string GetReportFriendlyName() { return "Out-Contract Report"; }
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
            return request.parameters["contractid"].ToString().TrimEnd();
        }
        //------------------------------------------------------------------------------------ 
        [HttpPost("render")]
        public async Task<IActionResult> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        [HttpGet("{contractid}")]
        public async Task<IActionResult> GetContract([FromRoute] string contractid)
        {
            OutContractReportRepository contractReportRepo = new OutContractReportRepository(this.AppConfig, this.UserSession);
            var contractReport = await contractReportRepo.Get(contractid);
            return new OkObjectResult(contractReport);
        }
        //------------------------------------------------------------------------------------ 
    }
}