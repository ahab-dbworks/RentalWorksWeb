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
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi.Modules.Settings.DepartmentSettings.Department;
using WebApi.Modules.Agent.Customer;
using WebApi.Modules.Settings.DealSettings.DealType;
using WebApi.Modules.Settings.DealSettings.DealStatus;
using WebApi.Modules.Agent.Deal;

namespace WebApi.Modules.Reports.DealReports.OrdersByDealReport
{
    public class OrdersByDealReportRequest : AppReportRequest
    {
        public bool? FilterDatesOrderCreate { get; set; }
        public DateTime? OrderCreateFromDate { get; set; }
        public DateTime? OrderCreateToDate { get; set; }
        public bool? FilterDatesOrderStart { get; set; }
        public DateTime? OrderStartFromDate { get; set; }
        public DateTime? OrderStartToDate { get; set; }
        public bool? FilterDatesDealCredit { get; set; }
        public DateTime? DealCreditFromDate { get; set; }
        public DateTime? DealCreditToDate { get; set; }
        public bool? FilterDatesDealInsurance { get; set; }
        public DateTime? DealInsuranceFromDate { get; set; }
        public DateTime? DealInsuranceToDate { get; set; }
        public string OfficeLocationId { get; set; }
        public string DepartmentId { get; set; }
        public string CustomerId { get; set; }
        public string DealTypeId { get; set; }
        public string DealStatusId { get; set; }
        public string DealId { get; set; }
        public string NoCharge { get; set; } //ALL, NoChargeOnly, ExcludeNoCharge
        public SelectedCheckBoxListItems OrderType { get; set; } = new SelectedCheckBoxListItems();
        public SelectedCheckBoxListItems QuoteStatus { get; set; } = new SelectedCheckBoxListItems();
        public SelectedCheckBoxListItems OrderStatus { get; set; } = new SelectedCheckBoxListItems();
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "ltXuVM54H5dYe")]
    public class OrdersByDealReportController : AppReportController
    {
        public OrdersByDealReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(OrdersByDealReportLoader); }
        protected override string GetReportFileName(FwReportRenderRequest request) { return "OrdersByDealReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Orders By Deal Report"; }
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
            return "OrdersByDealReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersbydealreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "CST7rtTyPWnj0")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersbydealreport/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "Vg9UDtqFo6N0y")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]OrdersByDealReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersbydealreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "e2jiEJTL8KL1i")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]OrdersByDealReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OrdersByDealReportLoader l = new OrdersByDealReportLoader();
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
        // POST api/v1/ordersbydealreport/validateofficelocation/browse 
        [HttpPost("validateofficelocation/browse")]
        [FwControllerMethod(Id: "fc6HC4UCjfg1", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersbydealreport/validatedepartment/browse 
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "ynAvz3V9CpHb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersbydealreport/validatecustomer/browse 
        [HttpPost("validatecustomer/browse")]
        [FwControllerMethod(Id: "ACQs14oyCnlq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCustomerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersbydealreport/validatedealtype/browse 
        [HttpPost("validatedealtype/browse")]
        [FwControllerMethod(Id: "SDTUThht0bvC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersbydealreport/validatedealstatus/browse 
        [HttpPost("validatedealstatus/browse")]
        [FwControllerMethod(Id: "3CJ3MFz0F4ga", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealStatusBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealStatusLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersbydealreport/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "nDLp2SymckM5", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
    }
}
