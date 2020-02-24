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

namespace WebApi.Modules.Reports.ContractReports.TransferReceiptReport
{
    public class TransferReceiptReportRequest : AppReportRequest
    {
        public string ContractId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "F6XtBsrOH4cjm")]
    public class TransferReceiptReportController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public TransferReceiptReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(TransferReceiptReportLoader); }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName() { return "TransferReceiptReport"; }
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
        // POST api/v1/transferreceiptreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "ZtZcIsvTh2iA5")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        //[HttpGet("{contractid}")]
        //[FwControllerMethod(Id:"HCPKa35FPjMY")]
        //public async Task<ActionResult<TransferReceiptReport>> GetContract([FromRoute] string contractid)
        //{
        //    TransferReceiptReportLogic contractReportRepo = new TransferReceiptReportLogic(this.AppConfig, this.UserSession);
        //    var contractReport = await contractReportRepo.Get(contractid);
        //    return new OkObjectResult(contractReport);
        //}
        ////------------------------------------------------------------------------------------ 
        // POST api/v1/transferreceiptreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "KRcMT7QPVMb9t")]
        public async Task<ActionResult<TransferReceiptReportLoader>> RunReportAsync([FromBody]TransferReceiptReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TransferReceiptReportLoader l = new TransferReceiptReportLoader();
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
        // POST api/v1/transferreceiptreport/validatecontract/browse 
        [HttpPost("validatecontract/browse")]
        [FwControllerMethod(Id: "KdooVKhTCi4qq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateContractBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContractLogic>(browseRequest);
        }
    }
}
