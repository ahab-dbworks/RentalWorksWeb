using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.Reporting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PuppeteerSharp;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Reports.OutContractReport
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id:"p2tH0HSDc930")]
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
            return request.parameters["ContractId"].ToString().TrimEnd();
        }
        //------------------------------------------------------------------------------------ 
        [HttpPost("render")]
        [FwControllerMethod(Id:"LtlWdMVpDoe4")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        [HttpGet("{contractid}")]
        [FwControllerMethod(Id:"HCPKa35FPjMY")]
        public async Task<ActionResult<OutContractReport>> GetContract([FromRoute] string contractid)
        {
            OutContractReportLogic contractReportRepo = new OutContractReportLogic(this.AppConfig, this.UserSession);
            var contractReport = await contractReportRepo.Get(contractid);
            return new OkObjectResult(contractReport);
        }
        //------------------------------------------------------------------------------------ 
    }
}
