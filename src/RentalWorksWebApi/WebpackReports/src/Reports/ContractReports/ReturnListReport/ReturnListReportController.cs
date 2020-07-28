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
using WebApi.Modules.Reports.ContractReports.ContractReport;
using WebApi.Modules.Warehouse.Contract;
using WebApi.Data;
using WebApi.Modules.Inventory.RentalInventory;
using WebApi.Logic;
using static WebApi.Modules.AccountServices.Account.AccountController;
using FwCore.Controllers;


namespace WebApi.Modules.Reports.ContractReports.ReturnListReport
{
    public class ReturnListReportRequest : AppReportRequest
    {
        public string DealId { get; set; }
        public string DepartmentId { get; set; }
        public string SortBy { get; set; }
        public string PrintBarcodeMode { get; set; }
        public string IncludeSales { get; set; }
        public string WarehouseId { get; set; }
        public string ContractId { get; set; }
        public string OrderIds { get; set; }
        public bool? IncludeTrackedByBarcode { get; set; }
        public bool? PrintSerialNumbers { get; set; }
        public bool? PrintBarCodes { get; set; }
        public bool? PrintContainersToRefillReport { get; set; }
        public bool? PaginateByInventoryType { get; set; }
        public bool? AddBoxforMeterReading { get; set; }
        public bool? PrintICodeColumn { get; set; }
        public bool? PrintAisleShelf { get; set; }
        public bool? PrintOut{ get; set; }
        public bool? PrintIn{ get; set; }
    }

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "cYzbGgLOUMRz")]
    public class ReturnListReportController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public ReturnListReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(ReturnListReportLoader); }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName(FwReportRenderRequest request) { return "ReturnListReport"; }

        protected override string GetReportFriendlyName() { return "Return List Report"; }
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
            return request.parameters["ContractId"].ToString().TrimEnd();
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/returnlistreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "jDejxCh2MCdV")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/returnlistreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "s8uLh17OgMwI")]
        public async Task<ActionResult<ReturnListReportLoader>> RunReportAsync([FromBody]ReturnListReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReturnListReportLoader l = new ReturnListReportLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                return new OkObjectResult(await l.RunReportAsync(request));
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/returnlistreport/validateinventory/browse 
        [HttpPost("validateinventory/browse")]
        [FwControllerMethod(Id: "BmM4C6QFteF2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RentalInventoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
