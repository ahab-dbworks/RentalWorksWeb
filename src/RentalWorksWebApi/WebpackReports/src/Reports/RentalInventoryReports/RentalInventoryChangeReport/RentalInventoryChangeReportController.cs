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
using WebApi.Modules.Reports.Shared.InventoryChangeTransactionType;
using WebApi.Modules.Settings.InventorySettings.InventoryType;
using WebApi.Modules.Settings.Category;
using WebApi.Modules.Settings.SubCategory;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;
using WebApi.Modules.Inventory.RentalInventory;
using WebApi.Modules.Settings.InventorySettings.RentalCategory;
using WebApi.Modules.Reports.InventoryChangeReport;

namespace WebApi.Modules.Reports.RentalInventoryChangeReport
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "38oJf9KIuq4x")]
    public class RentalInventoryChangeReportController : AppReportController
    {
        //------------------------------------------------------------------------------------ 
        public RentalInventoryChangeReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(RentalInventoryChangeReportLoader); }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFileName(FwReportRenderRequest request) { return "RentalInventoryChangeReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Rental Inventory Change Report"; }
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
            return "RentalInventoryChangeReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventorychangereport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "kWqWWselO6L2")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventorychangereport/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "G1soa2DZ3UmS")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]InventoryChangeReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, request);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventorychangereport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "8Av0EWfRVBqe")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]InventoryChangeReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RentalInventoryChangeReportLoader l = new RentalInventoryChangeReportLoader();
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
        // POST api/v1/rentalinventorychangereport/validateinventorytype/browse 
        [HttpPost("validateinventorytype/browse")]
        [FwControllerMethod(Id: "0OTR9H35bqQm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventorychangereport/validatecategory/browse 
        [HttpPost("validatecategory/browse")]
        [FwControllerMethod(Id: "GOvSjOtG4XDm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RentalCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventorychangereport/validatesubcategory/browse 
        [HttpPost("validatesubcategory/browse")]
        [FwControllerMethod(Id: "D9Y2ng65xKeJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SubCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventorychangereport/validatetransactiontype/browse 
        [HttpPost("validatetransactiontype/browse")]
        [FwControllerMethod(Id: "AW7dodYKpSrf", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateTransactionTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryChangeTransactionTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventorychangereport/validatewarehouse/browse 
        [HttpPost("validatewarehouse/browse")]
        [FwControllerMethod(Id: "628qKhTcmIo9", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventorychangereport/validateinventory/browse 
        [HttpPost("validateinventory/browse")]
        [FwControllerMethod(Id: "SbR5uVY5JUtw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RentalInventoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
