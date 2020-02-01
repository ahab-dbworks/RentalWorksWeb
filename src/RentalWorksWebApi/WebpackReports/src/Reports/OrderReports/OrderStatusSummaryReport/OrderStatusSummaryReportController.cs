

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

namespace WebApi.Modules.Reports.OrderStatusSummaryReport
{
    public class OrderStatusSummaryReportRequest : AppReportRequest
    {
        public string OrderId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "44jjIwel6TP0d")]
    public class OrderStatusSummaryReportController : AppReportController
    {
        public OrderStatusSummaryReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        protected override string GetReportFileName() { return "OrderStatusSummaryReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Order Status"; }
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
            return "OrderStatusSummaryReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderstatussummaryreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "4cMLxL68N5OVv")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            ActionResult<FwReportRenderResponse> response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderstatussummaryreport/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "4mzI4jEF8FD2C")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]OrderStatusSummaryReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderstatussummaryreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "4VsjqrccU1KZ2")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]OrderStatusSummaryReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OrderStatusSummaryReportLoader l = new OrderStatusSummaryReportLoader();
                l.SetDependencies(this.AppConfig, UserSession);
                FwJsonDataTable dt = await l.RunReportAsync(request);
                //l.HideSummaryColumnsInDataTable(request, dt);
                return new OkObjectResult(dt);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 

        // POST api/v1/orderreport/validateorder/browse 
        [HttpPost("validateorder/browse")]
        [FwControllerMethod(Id: "YyAtJqo69gwa", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOrderBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderLogic>(browseRequest);
        }
    }
}
