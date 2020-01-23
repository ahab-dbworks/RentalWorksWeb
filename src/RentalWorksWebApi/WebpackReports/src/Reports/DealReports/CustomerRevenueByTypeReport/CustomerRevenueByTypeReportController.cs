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
using static FwCore.Controllers.FwDataController;

using WebApi.Data;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi.Modules.Agent.Customer;
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Settings.DealSettings.DealType;
using WebApi.Modules.Settings.CompanyDepartmentSettings.Department;
using WebApi.Modules.Settings.OrderSettings.OrderType;

namespace WebApi.Modules.Reports.DealReports.CustomerRevenueByTypeReport
{

    public class CustomerRevenueByTypeReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string DateType { get; set; }
        public string OfficeLocationId { get; set; }
        public string DepartmentId { get; set; }
        public string CustomerId { get; set; }
        public string DealTypeId { get; set; }
        public string DealId { get; set; }
        public string OrderTypeId { get; set; }
    }



    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "mIieqY1nHrJP")]
    public class CustomerRevenueByTypeReportController : AppReportController
    {
        public CustomerRevenueByTypeReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(CustomerRevenueByTypeReportLoader); }
        protected override string GetReportFileName() { return "CustomerRevenueByTypeReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Customer Revenue By Type Report"; }
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
            return "CustomerRevenueByTypeReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customerrevenuebytypereport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "Vn78OJzbD0kJ")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest(this.ModelState);
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "fMRmiMRlY7mR")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]CustomerRevenueByTypeReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerrevenuebytypereport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "YbGP2l61lTkk")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]CustomerRevenueByTypeReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CustomerRevenueByTypeReportLoader l = new CustomerRevenueByTypeReportLoader();
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
        // POST api/v1/customerrevenuebytypereport/validateofficelocation/browse 
        [HttpPost("validateofficelocation/browse")]
        [FwControllerMethod(Id: "lhaCqTtRjdWS", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        // POST api/v1/customerrevenuebytypereport/validatecustomer/browse 
        [HttpPost("validatecustomer/browse")]
        [FwControllerMethod(Id: "JP4QNw9w6E45", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCustomerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerLogic>(browseRequest);
        }
        // POST api/v1/customerrevenuebytypereport/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "gI7IrnSJfXL0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
        // POST api/v1/customerrevenuebytypereport/validatedealtype/browse 
        [HttpPost("validatedealtype/browse")]
        [FwControllerMethod(Id: "xJCxcTPhpUeS", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealTypeLogic>(browseRequest);
        }
        // POST api/v1/customerrevenuebytypereport/validatedepartment/browse 
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "0Wn1csEPoevR", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        // POST api/v1/customerrevenuebytypereport/validateordertype/browse 
        [HttpPost("validateordertype/browse")]
        [FwControllerMethod(Id: "WUA5voMUddtd", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOrderTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderTypeLogic>(browseRequest);
        }
    }
}
