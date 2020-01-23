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
using WebApi.Data;
using static FwCore.Controllers.FwDataController;
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Agent.Vendor;
using WebApi.Modules.Agent.PurchaseOrder;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi.Modules.Settings.CompanyDepartmentSettings.Department;

namespace WebApi.Modules.Reports.VendorReports.VendorInvoiceSummaryReport
{
    public class VendorInvoiceSummaryReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string DateType { get; set; }
        public SelectedCheckBoxListItems Statuses { get; set; } = new SelectedCheckBoxListItems();

        public bool? IncludeAccruals { get; set; }
        public DateTime? AccrualFromDate { get; set; }
        public DateTime? AccrualToDate { get; set; }
        public bool? AccrualsOnly { get; set; }

        public string OfficeLocationId { get; set; }
        public string DepartmentId { get; set; }
        public string DealId { get; set; }
        public string VendorId { get; set; }
        public string PurchaseOrderId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "J2Lczm4sL14Ze")]
    public class VendorInvoiceSummaryReportController : AppReportController
    {
        public VendorInvoiceSummaryReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(VendorInvoiceSummaryReportLoader); }
        protected override string GetReportFileName() { return "VendorInvoiceSummaryReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Vendor Invoice Summary Report"; }
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
            return "VendorInvoiceSummaryReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoicesummaryreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "WaYso7URypcL")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest(this.ModelState);
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoicesummaryreport/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "Q96WIEe2tcR0b")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]VendorInvoiceSummaryReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoicesummaryreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "hw6dFHhZCFQa")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]VendorInvoiceSummaryReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                VendorInvoiceSummaryReportLoader l = new VendorInvoiceSummaryReportLoader();
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
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoicesummaryreport/validatepurchaseorder/browse 
        [HttpPost("validatepurchaseorder/browse")]
        [FwControllerMethod(Id: "j79HGXnwQpcD", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePurchaseOrderBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PurchaseOrderLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoicesummaryreport/validateofficelocation/browse 
        [HttpPost("validateofficelocation/browse")]
        [FwControllerMethod(Id: "ukOjr5Vxnwlm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoicesummaryreport/validatedepartment/browse 
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "gyom7RYsy1uE", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoicesummaryreport/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "6xNHShJDsvAN", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoicesummaryreport/validatevendor/browse 
        [HttpPost("validatevendor/browse")]
        [FwControllerMethod(Id: "j42lw28VXWAj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateVendorBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
    }
}
