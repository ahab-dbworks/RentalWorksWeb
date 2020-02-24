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
using WebApi.Modules.Reports.ContractReports.ContractReport;
using WebApi.Modules.Warehouse.Contract;

namespace WebApi.Modules.Reports.ContractReports.InContractReport
{
    public class InContractReportRequest : ContractReportRequest { }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "DQ0kEL13GYyEg")]
    public class InContractReportController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public InContractReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(InContractReportLoader); }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName() { return "InContractReport"; }
        protected override string GetReportFriendlyName() { return "In Contract Report"; }
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
        [FwControllerMethod(Id: "CWjXSGKDbFGba")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/incontractreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "wj8eIyalZ53t2")]
        public async Task<ActionResult<InContractReportLoader>> RunReportAsync([FromBody]InContractReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InContractReportLoader l = new InContractReportLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                return new OkObjectResult(await l.RunReportAsync(request));
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/incontractreport/validatecontract/browse 
        [HttpPost("validatecontract/browse")]
        [FwControllerMethod(Id: "kxl1P4o2DOhSB", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateContractBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContractLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
