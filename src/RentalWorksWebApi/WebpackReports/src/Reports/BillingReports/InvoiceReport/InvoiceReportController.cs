using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using FwStandard.Reporting;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using WebApi.Data;
using WebApi.Modules.Billing.Invoice;

namespace WebApi.Modules.Reports.Billing.InvoiceReport
{
    public class InvoiceReportRequest : AppReportRequest
    {
        public string InvoiceId { get; set; }
    }

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "o5nbWmTr7xy0n")]
    public class InvoiceReportController : AppReportController
    {
        public InvoiceReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(InvoiceReportLoader); }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName(FwReportRenderRequest request) {
            //return "InvoiceReport"; 
            InvoiceLogic invoice = new InvoiceLogic();
            invoice.SetDependencies(AppConfig, UserSession);
            invoice.InvoiceId = GetUniqueId(request);
            bool b = invoice.LoadAsync<InvoiceLogic>().Result;
            string fileName = invoice.InvoiceNumber + " " + invoice.InvoiceDescription;
            return fileName;
        }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Invoice Report"; }
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
            return request.parameters["InvoiceId"].ToString().TrimEnd();
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "kI7jYtaWlf8ue")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "jj2IKqYy5ZpPN")]
        public async Task<ActionResult<InvoiceReportLoader>> RunReportAsync([FromBody]InvoiceReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InvoiceReportLoader l = new InvoiceReportLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                return new OkObjectResult(await l.RunReportAsync(request));
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderreport/validateinvoice/browse 
        [HttpPost("validateinvoice/browse")]
        [FwControllerMethod(Id: "mneC0a6tg45H", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInvoiceBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InvoiceLogic>(browseRequest);
        }
    }
}
