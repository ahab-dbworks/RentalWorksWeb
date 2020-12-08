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
using WebApi.Modules.Agent.Order;
using static FwCore.Controllers.FwDataController;

namespace WebApi.Modules.Reports.OrderDepletingDepositReceiptReport
{
    public class OrderDepletingDepositReceiptReportRequest : AppReportRequest
    {
        public string OrderId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "PVylYE8XDyxP")]
    public class OrderDepletingDepositReceiptReportController : AppReportController
    {
        public OrderDepletingDepositReceiptReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(OrderDepletingDepositReceiptReportLoader); }
        protected override string GetReportFileName(FwReportRenderRequest request) { return "OrderDepletingDepositReceiptReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "OrderDepletingDepositReceipt"; }
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
            return "OrderDepletingDepositReceiptReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderdepletingdepositreceiptreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "tWRwEmIdlh6h")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            //ActionResult<FwReportRenderResponse> response = await DoRender(request);
            //return new OkObjectResult(response);
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/orderdepletingdepositreceiptreport/exportexcelxlsx
        //[HttpPost("exportexcelxlsx")]
        //[FwControllerMethod(Id: "fBLkHuI0p4EA")]
        //public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]OrderValueSheetReportRequest request)
        //{
        //    ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
        //    FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
        //    return await DoExportExcelXlsxFileAsync(dt, request);
        //}
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderdepletingdepositreceiptreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "9S5OSEPO0RQJ")]
        public async Task<ActionResult<OrderDepletingDepositReceiptReportLoader>> RunReportAsync([FromBody] OrderDepletingDepositReceiptReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OrderDepletingDepositReceiptReportLoader l = new OrderDepletingDepositReceiptReportLoader();
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
        // POST api/v1/orderdepletingdepositreceiptreport/validateorder/browse 
        [HttpPost("validateorder/browse")]
        [FwControllerMethod(Id: "X0lDVsxbqfsX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOrderBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
