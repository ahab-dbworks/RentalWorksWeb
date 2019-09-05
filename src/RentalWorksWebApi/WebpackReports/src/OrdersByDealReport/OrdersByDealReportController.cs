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

namespace WebApi.Modules.Reports.OrdersByDealReport
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
        public OrdersByDealReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        protected override string GetReportFileName() { return "OrdersByDealReport"; }
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
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersbydealreport/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
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
    }
}
