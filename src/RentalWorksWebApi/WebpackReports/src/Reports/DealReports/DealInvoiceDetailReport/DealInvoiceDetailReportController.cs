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
using WebApi.Modules.Settings.CompanyDepartmentSettings.Department;

namespace WebApi.Modules.Reports.DealReports.DealInvoiceDetailReport
{
    public class DealInvoiceDetailReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string DateType { get; set; }
        public bool? IncludeNoCharge { get; set; }
        public bool? DeductVendorItemCost { get; set; }
        public string OfficeLocationId { get; set; }
        public string DepartmentId { get; set; }
        public string CustomerId { get; set; }
        public string DealId { get; set; }
        public string NoCharge { get; set; }
        public string BilledHiatus { get; set; }
        public string BillableFlat { get; set; }
        public SelectedCheckBoxListItems Statuses { get; set; } = new SelectedCheckBoxListItems();
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "u5mTLIfmUKWfH")]
    public class DealInvoiceDetailReportController : AppReportController
    {
        public DealInvoiceDetailReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(DealInvoiceDetailReportLoader); }
        protected override string GetReportFileName() { return "DealInvoiceDetailReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Deal Invoice Detail Report"; }
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
            return "DealInvoiceDetailReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dealinvoicedetailreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "E8bx7PuwBAIbN")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest(this.ModelState);
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dealinvoicedetailreport/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "To36mbIzSdZF7")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]DealInvoiceDetailReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dealinvoicedetailreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "vK7QDovlEJ0OJ")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]DealInvoiceDetailReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DealInvoiceDetailReportLoader l = new DealInvoiceDetailReportLoader();
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
        // POST api/v1/dealinvoicedetailreport/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "jJLaWEpOv5vb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
        // POST api/v1/dealinvoicedetailreport/validateofficelocation/browse 
        [HttpPost("validateofficelocation/browse")]
        [FwControllerMethod(Id: "KKGgS5JtSjFF", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        // POST api/v1/dealinvoicedetailreport/validatecustomer/browse 
        [HttpPost("validatecustomer/browse")]
        [FwControllerMethod(Id: "chEUWUwyGfsT", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCustomerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerLogic>(browseRequest);
        }
        // POST api/v1/dealinvoicedetailreport/validatedepartment/browse 
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "HB0c3RH9ef7P", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
    }
}
