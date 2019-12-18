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
using WebApi.Modules.Settings.SubCategory;
using WebApi.Modules.Settings.InventorySettings.RetiredReason;
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Agent.Customer;

namespace WebApi.Modules.Reports.RentalInventoryReports.RetiredRentalInventoryReport
{
    public class RetiredRentalInventoryReportRequest : AppReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool? IncludeUnretired { get; set; }
        public string WarehouseId { get; set; }
        public string InventoryTypeId { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string InventoryId { get; set; }
        public string CustomerId { get; set; }
        public string DealId { get; set; }
        public string RetiredReasonId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "L8qgvB6nFhAV")]
    public class RetiredRentalInventoryReportController : AppReportController
    {
        public RetiredRentalInventoryReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(RetiredRentalInventoryReportLoader); }
        protected override string GetReportFileName() { return "RetiredRentalInventoryReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Retired Rental Inventory Report"; }
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
            return "RetiredRentalInventoryReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/retiredrentalinventoryreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "VJ159JOnP0ge")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "XmbTAtErnupH")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]RetiredRentalInventoryReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/retiredrentalinventoryreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "fkp2hgpw8zmD")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]RetiredRentalInventoryReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RetiredRentalInventoryReportLoader l = new RetiredRentalInventoryReportLoader();
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
        // POST api/v1/repairorderstatusreport/validateinventorytype/browse 
        [HttpPost("validateinventorytype/browse")]
        [FwControllerMethod(Id: "2SMYTF9QnmDm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairorderstatusreport/validatecategory/browse 
        [HttpPost("validatecategory/browse")]
        [FwControllerMethod(Id: "roscS7obl0re", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairorderstatusreport/validatewarehouse/browse 
        [HttpPost("validatewarehouse/browse")]
        [FwControllerMethod(Id: "S5XPCnp2rH9x", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairorderstatusreport/validateinventory/browse 
        [HttpPost("validateinventory/browse")]
        [FwControllerMethod(Id: "68FXKfCYjfMh", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RentalInventoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairorderstatusreport/validatesubcategory/browse 
        [HttpPost("validatesubcategory/browse")]
        [FwControllerMethod(Id: "WlLrwd0cCIgX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SubCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairorderstatusreport/validateretiredreason/browse 
        [HttpPost("validateretiredreason/browse")]
        [FwControllerMethod(Id: "vTP6HYfzMHmI", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateRetiredReasonBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RetiredReasonLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairorderstatusreport/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "M6EsP9YlUOHD", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairorderstatusreport/validatecustomer/browse 
        [HttpPost("validatecustomer/browse")]
        [FwControllerMethod(Id: "DwH1gJXpuKVO", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCustomerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerLogic>(browseRequest);
        }
    }
}
