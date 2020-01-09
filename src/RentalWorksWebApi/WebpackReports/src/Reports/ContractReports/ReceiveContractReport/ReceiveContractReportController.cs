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

namespace WebApi.Modules.Reports.ContractReports.ReceiveContractReport
{
    public class ReceiveContractReportRequest : AppReportRequest
    {
        public string ContractId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "KXKz4J1x0t71K")]
    public class ReceiveContractReportController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public ReceiveContractReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(ReceiveContractReportLoader); }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName() { return "ReceiveContractReport"; }
        protected override string GetReportFriendlyName() { return "Receive Contract Report"; }
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
        // POST api/v1/receivecontractreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "Nghwz0H0ZWFjA")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        //[HttpGet("{contractid}")]
        //[FwControllerMethod(Id:"HCPKa35FPjMY")]
        //public async Task<ActionResult<ReceiveContractReport>> GetContract([FromRoute] string contractid)
        //{
        //    ReceiveContractReportLogic contractReportRepo = new ReceiveContractReportLogic(this.AppConfig, this.UserSession);
        //    var contractReport = await contractReportRepo.Get(contractid);
        //    return new OkObjectResult(contractReport);
        //}
        ////------------------------------------------------------------------------------------ 
        // POST api/v1/receivecontractreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "56xcZtBERGYdT")]
        public async Task<ActionResult<ReceiveContractReportLoader>> RunReportAsync([FromBody]ReceiveContractReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReceiveContractReportLoader l = new ReceiveContractReportLoader();
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
        // POST api/v1/receivecontractreport/validatecontract/browse 
        [HttpPost("validatecontract/browse")]
        [FwControllerMethod(Id: "YySK38ojRU11t", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateContractBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContractLogic>(browseRequest);
        }
    }
}
