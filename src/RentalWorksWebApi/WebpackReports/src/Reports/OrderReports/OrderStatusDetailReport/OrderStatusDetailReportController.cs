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

namespace WebApi.Modules.Reports.OrderReports.OrderStatusDetailReport
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
        public OrderStatusDetailReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(OrderStatusDetailReportLoader); }
        protected override string GetReportFileName(FwReportRenderRequest request) { return "OrderStatusDetailReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Order Status Detail"; }
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
            return request.parameters["OrderId"].ToString().TrimEnd();
            //return "OrderStatusDetailReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderstatusdetailreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "OmsVMrVdBH02")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderstatusdetailreport/runreport 
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
                OrderStatusDetailHeaderLoader Order = await l.RunReportAsync(request);
                //l.HideSummaryColumnsInDataTable(request, dt);
                return new OkObjectResult(Order);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderstatusdetailreport/validateorder/browse 
        [HttpPost("validateorder/browse")]
        [FwControllerMethod(Id: "mXCvb9NSeGVL", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOrderBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderLogic>(browseRequest);
        }
    }
}
