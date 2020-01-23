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
using WebApi.Data;
using WebApi.Modules.Warehouse.Contract;

namespace WebApi.Modules.Reports.ContractReports.ExchangeContractReport
{
    public class ExchangeContractReportRequest : AppReportRequest
    {
        public string ContractId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "MAR5QYKd01qwx")]
    public class ExchangeContractReportController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public ExchangeContractReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(ExchangeContractReportLoader); }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName() { return "ExchangeContractReport"; }
        protected override string GetReportFriendlyName() { return "Exchange Contract Report"; }
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
        // POST api/v1/incontractreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "OqOV9oBdPQow1")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest(this.ModelState);
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        //[HttpGet("{contractid}")]
        //[FwControllerMethod(Id:"HCPKa35FPjMY")]
        //public async Task<ActionResult<ExchangeContractReport>> GetContract([FromRoute] string contractid)
        //{
        //    ExchangeContractReportLogic contractReportRepo = new ExchangeContractReportLogic(this.AppConfig, this.UserSession);
        //    var contractReport = await contractReportRepo.Get(contractid);
        //    return new OkObjectResult(contractReport);
        //}
        ////------------------------------------------------------------------------------------ 
        // POST api/v1/incontractreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "Ldny0QjbHeZX1")]
        public async Task<ActionResult<ExchangeContractReportLoader>> RunReportAsync([FromBody]ExchangeContractReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExchangeContractReportLoader l = new ExchangeContractReportLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                return new OkObjectResult(await l.RunReportAsync(request));
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        //------------------------------------------------------------------------------------ 
        // POST api/v1/incontractreport/validatecontract/browse 
        [HttpPost("validatecontract/browse")]
        [FwControllerMethod(Id: "vZWTcW0KSMsRZ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateContractBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContractLogic>(browseRequest);
        }
    }
}
