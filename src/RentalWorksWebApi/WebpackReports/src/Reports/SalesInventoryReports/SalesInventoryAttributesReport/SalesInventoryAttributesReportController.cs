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
using WebApi.Modules.Reports.RentalInventoryReports.RentalInventoryAttributesReport;
using WebApi.Modules.Reports.Shared.InventoryAttributesReport;
using WebApi.Modules.Settings.Category;
using WebApi.Modules.Settings.InventorySettings.InventoryType;
using WebApi.Modules.Settings.SubCategory;
using static FwCore.Controllers.FwDataController;

namespace WebApi.Modules.Reports.SalesInventoryReports.SalesInventoryAttributesReport
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id:"IKtnIOnt3tS")]
    public class SalesInventoryAttributesReportController : AppReportController
    {
        public SalesInventoryAttributesReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(SalesInventoryAttributesReportLoader); }
        protected override string GetReportFileName() { return "SalesInventoryAttributesReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Sales Inventory Attributes Report"; }
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
            return "SalesInventoryAttributesReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventoryattributesreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id:"2sWC6aPlLok")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest();
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "0dkHaSnWajTD")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]InventoryAttributesReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalinventoryattributesreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id:"6d6IflcrFZ5")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]InventoryAttributesReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SalesInventoryAttributesReportLoader l = new SalesInventoryAttributesReportLoader();
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
        // POST api/v1/rentalinventoryattributesreport/validateinventorytype/browse 
        [HttpPost("validateinventorytype/browse")]
        [FwControllerMethod(Id: "R0X2fY5yv1Wy", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventoryattributesreport/validatecategory/browse 
        [HttpPost("validatecategory/browse")]
        [FwControllerMethod(Id: "VyvAfKFnLLP1", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventoryattributesreport/validateattribute/browse 
        [HttpPost("validateattribute/browse")]
        [FwControllerMethod(Id: "xBgKDTIyOsVY", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAttributeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<AttributeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventoryattributesreport/validateinventory/browse 
        [HttpPost("validateinventory/browse")]
        [FwControllerMethod(Id: "8nxzjQ91ygLV", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SalesInventoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventoryattributesreport/validatesubcategory/browse 
        [HttpPost("validatesubcategory/browse")]
        [FwControllerMethod(Id: "5Tx2Asufk6FN", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SubCategoryLogic>(browseRequest);
        }
    }
}
