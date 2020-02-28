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
using WebApi.Modules.Reports.ContractReports.ContractReport;
using WebApi.Modules.Warehouse.Contract;

namespace WebApi.Modules.Reports.ContractReports.LostContractReport
{
    public class LostContractReportRequest : ContractReportRequest { }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "F6uNUdehIUVSB")]
    public class LostContractReportController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public LostContractReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(LostContractReportLoader); }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName() { return "LostContractReport"; }
        protected override string GetReportFriendlyName() { return "Lost Contract Report"; }
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
        [FwControllerMethod(Id: "PrX4swTihkuJm")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        //[HttpGet("{contractid}")]
        //[FwControllerMethod(Id:"HCPKa35FPjMY")]
        //public async Task<ActionResult<LostContractReport>> GetContract([FromRoute] string contractid)
        //{
        //    LostContractReportLogic contractReportRepo = new LostContractReportLogic(this.AppConfig, this.UserSession);
        //    var contractReport = await contractReportRepo.Get(contractid);
        //    return new OkObjectResult(contractReport);
        //}
        ////------------------------------------------------------------------------------------ 
        // POST api/v1/incontractreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "RTGqGGzJCbKNz")]
        public async Task<ActionResult<LostContractReportLoader>> RunReportAsync([FromBody]LostContractReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                LostContractReportLoader l = new LostContractReportLoader();
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
        [FwControllerMethod(Id: "YY8FtCbRopzPx", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateContractBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContractLogic>(browseRequest);
        }
    }
}
