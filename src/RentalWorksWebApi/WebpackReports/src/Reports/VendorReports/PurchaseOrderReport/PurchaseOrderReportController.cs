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
using WebApi.Modules.Agent.PurchaseOrder;

namespace WebApi.Modules.Reports.VendorReports.PurchaseOrderReport
{
    public class PurchaseOrderReportRequest : AppReportRequest
    {
        public string PurchaseOrderId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "ZcNjp0seMeWi")]
    public class PurchaseOrderReportController : AppReportController
    {
        public PurchaseOrderReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(PurchaseOrderReportLoader); }
        protected override string GetReportFileName(FwReportRenderRequest request) { return "PurchaseOrderReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Purchase Order Report"; }
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
            return request.parameters["PurchaseOrderId"].ToString().TrimEnd(); 
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "IUpNEr7hVNc8")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "r0iYHUryiJej")]
        public async Task<ActionResult<PurchaseOrderReportLoader>> RunReportAsync([FromBody]PurchaseOrderReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PurchaseOrderReportLoader l = new PurchaseOrderReportLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                return new OkObjectResult(await l.RunReportAsync(request));               
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreport/validatepurchaseorder/browse 
        [HttpPost("validatepurchaseorder/browse")]
        [FwControllerMethod(Id: "jv65WF0jFFgE", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOrderBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PurchaseOrderLogic>(browseRequest);
        }
    }
}
