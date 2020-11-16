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

namespace WebApi.Modules.Reports.OrderValueSheetReport
{
    public class OrderValueSheetReportRequest : AppReportRequest
    {
        public string OrderId { get; set; }
        public string RentalValue { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "8lSfSBPXlYh5")]
    public class OrderValueSheetReportController : AppReportController
    {
        public OrderValueSheetReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(OrderValueSheetReportLoader); }
        protected override string GetReportFileName(FwReportRenderRequest request) { return "OrderValueSheetReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "OrderValueSheet"; }
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
            return "OrderValueSheetReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordervaluesheet/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "wj6jqn59eJEy")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            //ActionResult<FwReportRenderResponse> response = await DoRender(request);
            //return new OkObjectResult(response);
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/ordervaluesheet/exportexcelxlsx
        //[HttpPost("exportexcelxlsx")]
        //[FwControllerMethod(Id: "fBLkHuI0p4EA")]
        //public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]OrderValueSheetReportRequest request)
        //{
        //    ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
        //    FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
        //    return await DoExportExcelXlsxFileAsync(dt, request);
        //}
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordervaluesheet/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "gQbBa1zghqTv")]
        public async Task<ActionResult<OrderValueSheetReportLoader>> RunReportAsync([FromBody]OrderValueSheetReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OrderValueSheetReportLoader l = new OrderValueSheetReportLoader();
                l.SetDependencies(this.AppConfig, UserSession);
                OrderValueSheetReportLoader Order = await l.RunReportAsync(request);
                //l.HideSummaryColumnsInDataTable(request, dt);
                return new OkObjectResult(Order);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordervaluesheet/validateorder/browse 
        [HttpPost("validateorder/browse")]
        [FwControllerMethod(Id: "Rldp65t3h8LM", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOrderBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
