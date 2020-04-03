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
using WebApi.Modules.Warehouse.PickList;

namespace WebApi.Modules.Reports.OrderReports.PickListReport
{
    public class PickListReportRequest : AppReportRequest
    {
        public string PickListId { get; set; }
    }

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "Rk38wHmvgXTg")]
    public class PickListReportController : AppReportController
    {
        public PickListReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(PickListReportLoader); }
        protected override string GetReportFileName(FwReportRenderRequest request) { return "PickListReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Pick List Report"; }
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
            return request.parameters["PickListId"].ToString().TrimEnd();
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "9Cxmjc8ym2oy")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "bfAeFhKAd4iP")]
        public async Task<ActionResult<PickListReportLoader>> RunReportAsync([FromBody]PickListReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PickListReportLoader l = new PickListReportLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                return new OkObjectResult(await l.RunReportAsync(request));
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistreport/validatepicklist/browse 
        [HttpPost("validatepicklist/browse")]
        [FwControllerMethod(Id: "q8dPnRbYfEP7", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePickListBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PickListLogic>(browseRequest);
        }
    }
}
