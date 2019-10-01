using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.Reporting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PuppeteerSharp;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Data;

namespace WebApi.Modules.Reports.OutContractReport
{
    public class OutContractReportRequest : AppReportRequest
    {
        public string ContractId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id:"p2tH0HSDc930")]
    public class OutContractReportController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public OutContractReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName() { return "OutContractReport"; }
        protected override string GetReportFriendlyName() { return "Out Contract Report"; }
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
        // POST api/v1/outcontractreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id:"LtlWdMVpDoe4")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        //[HttpGet("{contractid}")]
        //[FwControllerMethod(Id:"HCPKa35FPjMY")]
        //public async Task<ActionResult<OutContractReport>> GetContract([FromRoute] string contractid)
        //{
        //    OutContractReportLogic contractReportRepo = new OutContractReportLogic(this.AppConfig, this.UserSession);
        //    var contractReport = await contractReportRepo.Get(contractid);
        //    return new OkObjectResult(contractReport);
        //}
        ////------------------------------------------------------------------------------------ 
        // POST api/v1/outcontractreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "HCPKa35FPjMY")]
        public async Task<ActionResult<OutContractReportLoader>> RunReportAsync([FromBody]OutContractReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OutContractReportLoader l = new OutContractReportLoader();
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
