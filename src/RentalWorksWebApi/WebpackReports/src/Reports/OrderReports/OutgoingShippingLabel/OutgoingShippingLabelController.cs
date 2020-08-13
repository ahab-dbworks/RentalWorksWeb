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
using FwStandard.AppManager;
using static FwCore.Controllers.FwDataController;
using WebApi.Data;
using WebApi.Modules.Agent.Order;

namespace WebApi.Modules.Reports.OrderReports.OutgoingShippingLabel
{
    public class OutgoingShippingLabelRequest : AppReportRequest
    {
        public string OrderId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "tzTGi6kzrelFp")]
    public class OutgoingShippingLabelController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public OutgoingShippingLabelController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(OutgoingShippingLabelLoader); }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName(FwReportRenderRequest request) { return "OutgoingShippingLabel"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Outgoing Shipping Label"; }
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
            return "OutgoingShippingLabel";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/outgoingshippinglabel/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "tzuKwxAykNJeI")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/outgoingshippinglabel/exportexcelxlsx 
        //[HttpPost("exportexcelxlsx")]
        //[FwControllerMethod(Id: "TzxzQcaaEVGYn")]
        //public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]OutgoingShippingLabelRequest request)
        //{
        //    ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
        //    FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
        //    return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        //}
        //------------------------------------------------------------------------------------ 
        // POST api/v1/outgoingshippinglabel/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "TZYCj9JKnIhKp")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]OutgoingShippingLabelRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OutgoingShippingLabelLoader l = new OutgoingShippingLabelLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                FwJsonDataTable dt = await l.RunReportAsync(request);
                l.HideDetailColumnsInSummaryDataTable(request, dt);
                return new OkObjectResult(dt);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/outgoingshippinglabel/validateorder/browse 
        [HttpPost("validateorder/browse")]
        [FwControllerMethod(Id: "L5V8ydaMST3Iv", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOrderBrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
