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
using WebApi.Modules.Agent.Order;

namespace WebApi.Modules.Reports.OrderReports.OrderReport
{
    public class OrderReportRequest : AppReportRequest
    {
        public string OrderId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "Q89Ni6FvVL92")]
    public class OrderReportController : AppReportController
    {
        public OrderReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(OrderReportLoader); }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName(FwReportRenderRequest request)
        {
            //return "OrderReport"; 
            OrderLogic order = new OrderLogic();
            order.SetDependencies(AppConfig, UserSession);
            order.OrderId = GetUniqueId(request);
            bool b = order.LoadAsync<OrderLogic>().Result;
            string fileName = order.OrderNumber + " " + order.Description;
            return fileName;
        }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Order Report"; }
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
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "T8c0DKhRVZhF")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "lZEgjy5NkBDm")]
        public async Task<ActionResult<OrderReportLoader>> RunReportAsync([FromBody]OrderReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OrderReportLoader l = new OrderReportLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                return new OkObjectResult(await l.RunReportAsync(request));
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderreport/validateorder/browse 
        [HttpPost("validateorder/browse")]
        [FwControllerMethod(Id: "y72TPuX9WyF1", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOrderBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
