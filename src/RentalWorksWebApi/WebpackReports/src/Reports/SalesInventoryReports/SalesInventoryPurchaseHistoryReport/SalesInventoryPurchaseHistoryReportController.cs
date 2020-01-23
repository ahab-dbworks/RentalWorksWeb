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
using WebApi.Modules.Inventory.SalesInventory;
using WebApi.Modules.Reports.Shared.InventoryPurchaseHistoryReport;
using WebApi.Modules.Settings.Category;
using WebApi.Modules.Settings.InventorySettings.InventoryType;
using WebApi.Modules.Settings.InventorySettings.SalesCategory;
using WebApi.Modules.Settings.SubCategory;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;
using static FwCore.Controllers.FwDataController;

namespace WebApi.Modules.Reports.SalesInventoryReports.SalesInventoryPurchaseHistoryReport
{

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id:"mSjHvRjNvI0")]
    public class SalesInventoryPurchaseHistoryReportController : AppReportController
    {
        public SalesInventoryPurchaseHistoryReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(SalesInventoryPurchaseHistoryReportLoader); }
        protected override string GetReportFileName() { return "SalesInventoryPurchaseHistoryReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Sales Inventory Purchase History Report"; }
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
            return "SalesInventoryPurchaseHistoryReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventorypurchasehistoryreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id:"ltjJg9UNSo6")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest(this.ModelState);
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "aMbLEDkTPeSs")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]InventoryPurchaseHistoryReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/salesinventorypurchasehistoryreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id:"3PlvmBRfuB2")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]InventoryPurchaseHistoryReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SalesInventoryPurchaseHistoryReportLoader l = new SalesInventoryPurchaseHistoryReportLoader();
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
        // POST api/v1/salesinventorypurchasehistoryreport/validateinventorytype/browse 
        [HttpPost("validateinventorytype/browse")]
        [FwControllerMethod(Id: "XQbbLVyqMX6w", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventorypurchasehistoryreport/validatecategory/browse 
        [HttpPost("validatecategory/browse")]
        [FwControllerMethod(Id: "Yu5ggyCNA9v4", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SalesCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventorypurchasehistoryreport/validatesubcategory/browse 
        [HttpPost("validatesubcategory/browse")]
        [FwControllerMethod(Id: "uPWuDNS39E6Q", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SubCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventorypurchasehistoryreport/validatewarehouse/browse 
        [HttpPost("validatewarehouse/browse")]
        [FwControllerMethod(Id: "UUeSMY6T3FYY", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/salesinventorypurchasehistoryreport/validateinventory/browse 
        [HttpPost("validateinventory/browse")]
        [FwControllerMethod(Id: "59wOWh1YvhQ6", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SalesInventoryLogic>(browseRequest);
        }
    }
}
