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
using WebApi.Modules.Settings.Category;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;
using WebApi.Modules.Inventory.RentalInventory;
using WebApi.Modules.Settings.InventorySettings.RetiredReason;
using WebApi.Modules.Agent.Customer;
using WebApi.Modules.Settings.InventorySettings.UnretiredReason;
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Settings.InventorySettings.RentalCategory;

namespace WebApi.Modules.Reports.RentalInventoryReports.RentalLostAndDamagedBillingHistoryReport
{
    public class RentalLostAndDamagedBillingHistoryReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool? ExcludeRetiredItems { get; set; }
        public bool? ExcludeUnretiredItems { get; set; }
        public string WarehouseId { get; set; }
        public string InventoryTypeId { get; set; }
        public string CategoryId { get; set; }
        public string InventoryId { get; set; }
        public string CustomerId { get; set; }
        public string DealId { get; set; }
        public string RetiredReasonId { get; set; }
        public string UnretiredReasonId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "37O4QUBc8nM8t")]
    public class RentalLostAndDamagedBillingHistoryReportController : AppReportController
    {
        public RentalLostAndDamagedBillingHistoryReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(RentalLostAndDamagedBillingHistoryReportLoader); }
        protected override string GetReportFileName(FwReportRenderRequest request) { return "RentalLostAndDamagedBillingHistoryReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Rental Lost and Damaged Billing History Report"; }
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
            return "RentalLostAndDamagedBillingHistoryReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentallostanddamagedbillinghistoryreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "CqLN3PFVB3Xh")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentallostanddamagedbillinghistoryreport/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "XhQ9o7CGN4Yj")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]RentalLostAndDamagedBillingHistoryReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, request);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentallostanddamagedbillinghistoryreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "tmfHNutdQ8Tf")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]RentalLostAndDamagedBillingHistoryReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RentalLostAndDamagedBillingHistoryReportLoader l = new RentalLostAndDamagedBillingHistoryReportLoader();
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
        // POST api/v1/rentallostanddamagedbillinghistoryreport/validateinventorytype/browse 
        [HttpPost("validateinventorytype/browse")]
        [FwControllerMethod(Id: "qazrvFHnYd0z", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentallostanddamagedbillinghistoryreport/validatecategory/browse 
        [HttpPost("validatecategory/browse")]
        [FwControllerMethod(Id: "HO28TCGPBJnY", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RentalCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentallostanddamagedbillinghistoryreport/validatewarehouse/browse 
        [HttpPost("validatewarehouse/browse")]
        [FwControllerMethod(Id: "h5QYPDquskL8", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentallostanddamagedbillinghistoryreport/validateinventory/browse 
        [HttpPost("validateinventory/browse")]
        [FwControllerMethod(Id: "JKlm5VWNNsEw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RentalInventoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentallostanddamagedbillinghistoryreport/validateretiredreason/browse 
        [HttpPost("validateretiredreason/browse")]
        [FwControllerMethod(Id: "G69To13QPqOL", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateRetiredReasonBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RetiredReasonLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentallostanddamagedbillinghistoryreport/validatecustomer/browse 
        [HttpPost("validatecustomer/browse")]
        [FwControllerMethod(Id: "50vV1N6B50r9", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCustomerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentallostanddamagedbillinghistoryreport/validateunretiredreason/browse 
        [HttpPost("validateunretiredreason/browse")]
        [FwControllerMethod(Id: "CpuqJyqYBvsS", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateUnretiredReasonBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UnretiredReasonLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentallostanddamagedbillinghistoryreport/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "DGgAr5su6Iqf", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
    }
}
