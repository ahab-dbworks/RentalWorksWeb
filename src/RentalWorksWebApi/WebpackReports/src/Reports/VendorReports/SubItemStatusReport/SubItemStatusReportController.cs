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
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi.Modules.Agent.Customer;
using WebApi.Modules.Agent.Order;
using WebApi.Modules.Agent.Vendor;
using WebApi.Modules.Settings.PoSettings.PoClassification;

namespace WebApi.Modules.Reports.VendorReports.SubItemStatusReport
{
    public class SubItemStatusReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string DateType { get; set; }
        public string OfficeLocationId { get; set; }
        public string CustomerId { get; set; }
        public string DealId { get; set; }
        public string OrderId { get; set; }
        public string VendorId { get; set; }
        public string PoClassificationId { get; set; }

        public SelectedCheckBoxListItems RecType { get; set; } = new SelectedCheckBoxListItems();
        public SelectedCheckBoxListItems Statuses { get; set; } = new SelectedCheckBoxListItems();
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "fL9dlJfzzJf8U")]
    public class SubItemStatusReportController : AppReportController
    {
        public SubItemStatusReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(SubItemStatusReportLoader); }
        protected override string GetReportFileName(FwReportRenderRequest request) { return "SubItemStatusReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Sub Item Status Report"; }
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
            return "SubItemStatusReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subitemstatusreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "cofoTjgvuSM9N")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subitemstatusreport/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "oTSxp23T4Ubij")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]SubItemStatusReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, request);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subitemstatusreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "PzwGOPOYRtltW")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]SubItemStatusReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SubItemStatusReportLoader l = new SubItemStatusReportLoader();
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
        // POST api/v1/subitemstatusreport/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "pU2k4MzMf3rp", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subitemstatusreport/validateofficelocation/browse 
        [HttpPost("validateofficelocation/browse")]
        [FwControllerMethod(Id: "EABMEkfzrthg", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subitemstatusreport/validatecustomer/browse 
        [HttpPost("validatecustomer/browse")]
        [FwControllerMethod(Id: "9Klhvh7uH9Wm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCustomerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subitemstatusreport/validateorder/browse 
        [HttpPost("validateorder/browse")]
        [FwControllerMethod(Id: "LQhsI5XgbSRm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOrderBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subitemstatusreport/validatevendor/browse 
        [HttpPost("validatevendor/browse")]
        [FwControllerMethod(Id: "mJCaO0c5MUkt", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateVendorBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subitemstatusreport/validatepoclassification/browse 
        [HttpPost("validatepoclassification/browse")]
        [FwControllerMethod(Id: "Css857RENpbX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePoClassificationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PoClassificationLogic>(browseRequest);
        }
    }
}
