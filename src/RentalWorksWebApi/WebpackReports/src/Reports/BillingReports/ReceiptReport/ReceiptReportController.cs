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
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Agent.Order;
using WebApi.Modules.Billing.Invoice;
using WebApi.Modules.Billing.Receipt;
using static FwCore.Controllers.FwDataController;

namespace WebApi.Modules.Reports.OrderDepletingDepositReceiptReport
{
    public class ReceiptReportRequest : AppReportRequest
    {
        public string OrderId { get; set; }
        public string DealId { get; set; }
        public string ReceiptId { get; set; }
    }

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "PVylYE8XDyxP")]
    public class ReceiptReportController : AppReportController
    {
        public ReceiptReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(ReceiptReportLoader); }
        protected override string GetReportFileName(FwReportRenderRequest request) { return "Receipt"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Receipt"; }
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
            //return request.parameters["xxxxid"].ToString().TrimEnd(); 
            return "Receipt";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receiptreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "tWRwEmIdlh6h")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/receiptreport/exportexcelxlsx
        //[HttpPost("exportexcelxlsx")]
        //[FwControllerMethod(Id: "fBLkHuI0p4EA")]
        //public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]OrderValueSheetReportRequest request)
        //{
        //    ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
        //    FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
        //    return await DoExportExcelXlsxFileAsync(dt, request);
        //}
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receiptreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "9S5OSEPO0RQJ")]
        public async Task<ActionResult<ReceiptReportLoader>> RunReportAsync([FromBody] ReceiptReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReceiptReportLoader l = new ReceiptReportLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                var report = await l.RunReportAsync(request);
                return new OkObjectResult(report);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receiptreport/validateorder/browse 
        [HttpPost("validateorder/browse")]
        [FwControllerMethod(Id: "X0lDVsxbqfsX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOrderBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receiptreport/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "9pYpFn7dv8zv", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealBrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receiptreport/validatereceipt/browse 
        [HttpPost("validatereceipt/browse")]
        [FwControllerMethod(Id: "SNJzDbE57GFp", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateReceiptBrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ReceiptLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
