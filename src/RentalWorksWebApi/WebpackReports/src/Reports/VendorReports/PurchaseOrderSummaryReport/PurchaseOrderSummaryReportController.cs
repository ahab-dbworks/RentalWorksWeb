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
using static FwCore.Controllers.FwDataController;
using WebApi.Data;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;
using WebApi.Modules.Agent.Project;
using WebApi.Modules.Agent.Vendor;
using WebApi.Modules.Settings.DepartmentSettings.Department;
using WebApi.Modules.Settings.PoSettings.PoApprovalStatus;

namespace WebApi.Modules.Reports.VendorReports.PurchaseOrderSummaryReport
{
    public class PurchaseOrderSummaryReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string WarehouseId { get; set; }
        public string ProjectId { get; set; }
        public string VendorId { get; set; }
        public string DepartmentId { get; set; }
        public string PoApprovalStatusId { get; set; }
        public SelectedCheckBoxListItems Status { get; set; } = new SelectedCheckBoxListItems();
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "yrvMp4sG8CzF")]
    public class PurchaseOrderSummaryReportController : AppReportController
    {
        public PurchaseOrderSummaryReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(PurchaseOrderSummaryReportLoader); }
        protected override string GetReportFileName(FwReportRenderRequest request) { return "PurchaseOrderSummaryReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Purchase Order Summary Report"; }
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
            return "PurchaseOrderSummaryReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseordersummaryreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "23O945zFVMXg")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseordersummaryreport/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "UI4h7OwUWj3W")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]PurchaseOrderSummaryReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, request);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseordersummaryreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "DkJYaS2uq9g")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]PurchaseOrderSummaryReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PurchaseOrderSummaryReportLoader l = new PurchaseOrderSummaryReportLoader();
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
        // POST api/v1/purchaseordersummaryreport/validatewarehouse/browse 
        [HttpPost("validatewarehouse/browse")]
        [FwControllerMethod(Id: "z0NBeIxzTYDc", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseordersummaryreport/validateproject/browse 
        [HttpPost("validateproject/browse")]
        [FwControllerMethod(Id: "MqPM8K6Qgg5C", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateProjectBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ProjectLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseordersummaryreport/validatevendor/browse 
        [HttpPost("validatevendor/browse")]
        [FwControllerMethod(Id: "1KjrQPUPd9CI", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateVendorBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseordersummaryreport/validatedepartment/browse 
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "zWMBC4Is3Lk1", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseordersummaryreport/validatepoapprovalstatus/browse 
        [HttpPost("validatepoapprovalstatus/browse")]
        [FwControllerMethod(Id: "v79xM6xt5mKE", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateApprovalStatusBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PoApprovalStatusLogic>(browseRequest);
        }
    }
}
