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
using WebApi.Modules.Settings.InventorySettings.InventoryType;
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;
using WebApi.Modules.Agent.Vendor;
using WebApi.Modules.Agent.Customer;

namespace WebApi.Modules.Reports.SalesInventoryReports.SalesBackorderReport
{
    public class SalesBackorderReportRequest : AppReportRequest
    {
        public string VendorId { get; set; }
        public string DealId { get; set; }
        public string WarehouseId { get; set; }
        public string InventoryTypeId { get; set; }
        public string CustomerId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "KMdFiy9SEIQEU")]
    public class SalesBackorderReportController : AppReportController
    {
        public SalesBackorderReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(SalesBackorderReportLoader); }
        protected override string GetReportFileName() { return "SalesBackorderReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Sales Backorder Report"; }
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
            return "SalesBackorderReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesbackorderreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "qbCxnhEoMvlGf")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesbackorderreport/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "cUnoYEMA7McmG")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]SalesBackorderReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesbackorderreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "WO5Q9nOaklCaJ")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]SalesBackorderReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SalesBackorderReportLoader l = new SalesBackorderReportLoader();
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
        // POST api/v1/salesbackorderreport/validateinventorytype/browse 
        [HttpPost("validateinventorytype/browse")]
        [FwControllerMethod(Id: "QHzHHm08DFOl", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesbackorderreport/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "aty599SKV4dA", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesbackorderreport/validatewarehouse/browse 
        [HttpPost("validatewarehouse/browse")]
        [FwControllerMethod(Id: "I2jrwAX34FNQ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesbackorderreport/validatevendor/browse 
        [HttpPost("validatevendor/browse")]
        [FwControllerMethod(Id: "PO5sAOWf72Jt", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateVendorBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesbackorderreport/validatecustomer/browse 
        [HttpPost("validatecustomer/browse")]
        [FwControllerMethod(Id: "mtb8AsmWE89O", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCustomerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerLogic>(browseRequest);
        }
    }
}
