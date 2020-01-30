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

namespace WebApi.Modules.Reports.ContractReports.TransferManifestReport
{
    public class TransferManifestReportRequest : AppReportRequest
    {
        public string ContractId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "wf68uZe0JPXDt")]
    public class TransferManifestReportController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public TransferManifestReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(TransferManifestReportLoader); }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName() { return "TransferManifestReport"; }
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
        // POST api/v1/transfermanifestreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "Z3oUT6u6I6oeN")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        //[HttpGet("{contractid}")]
        //[FwControllerMethod(Id:"HCPKa35FPjMY")]
        //public async Task<ActionResult<TransferManifestReport>> GetContract([FromRoute] string contractid)
        //{
        //    TransferManifestReportLogic contractReportRepo = new TransferManifestReportLogic(this.AppConfig, this.UserSession);
        //    var contractReport = await contractReportRepo.Get(contractid);
        //    return new OkObjectResult(contractReport);
        //}
        ////------------------------------------------------------------------------------------ 
        // POST api/v1/transfermanifestreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "yKbd9OjSTWvz2")]
        public async Task<ActionResult<TransferManifestReportLoader>> RunReportAsync([FromBody]TransferManifestReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TransferManifestReportLoader l = new TransferManifestReportLoader();
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
        // POST api/v1/transfermanifestreport/validatecontract/browse 
        [HttpPost("validatecontract/browse")]
        [FwControllerMethod(Id: "9NHaRhxCm0mal", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateContractBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContractLogic>(browseRequest);
        }
    }
}
