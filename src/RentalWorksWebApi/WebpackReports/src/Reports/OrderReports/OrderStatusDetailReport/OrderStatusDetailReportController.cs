

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
using static FwCore.Controllers.FwDataController;

namespace WebApi.Modules.Reports.OrderStatusDetailReport
{
    public class OrderStatusDetailReportRequest : AppReportRequest
    {
        public string OrderId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "EY9uBXnssjv1")]
    public class OrderStatusDetailReportController : AppReportController
    {
        public OrderStatusDetailReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        protected override string GetReportFileName() { return "OrderStatusReport"; }
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
            return "OrderStatusReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderstatusreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "OmsVMrVdBH02")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            ActionResult<FwReportRenderResponse> response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderstatusreport/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "7Byk9maM4f9x")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]OrderStatusDetailReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderstatusreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "HWv77z8LURHY")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]OrderStatusDetailReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OrderStatusDetailReportLoader l = new OrderStatusDetailReportLoader();
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
    }
}
