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
using WebApi.Modules.Inventory.PartsInventory;
using WebApi.Modules.Reports.Shared.InventoryAttributesReport;
using WebApi.Modules.Settings.InventorySettings.Attribute;
using WebApi.Modules.Settings.InventorySettings.InventoryType;
using WebApi.Modules.Settings.InventorySettings.PartsCategory;
using WebApi.Modules.Settings.SubCategory;
using static FwCore.Controllers.FwDataController;

namespace WebApi.Modules.Reports.PartsInventoryReports.PartsInventoryAttributesReport
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id:"qyi6CvOObXr")]
    public class PartsInventoryAttributesReportController : AppReportController
    {
        public PartsInventoryAttributesReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(PartsInventoryAttributesReportLoader); }
        protected override string GetReportFileName() { return "PartsInventoryAttributesReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Parts Inventory Attributes Report"; }
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
            return "PartsInventoryAttributesReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventoryattributesreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id:"oyK7EnzOO8z")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest(this.ModelState);
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "Gkfcs3g2raNG")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]InventoryAttributesReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalinventoryattributesreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id:"0pE4aN83tQ9")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]InventoryAttributesReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PartsInventoryAttributesReportLoader l = new PartsInventoryAttributesReportLoader();
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
        [FwControllerMethod(Id: "R4J5VCt55Kmj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventoryattributesreport/validatecategory/browse 
        [HttpPost("validatecategory/browse")]
        [FwControllerMethod(Id: "xGKqdzAhaBR5", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PartsCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventoryattributesreport/validatesubcategory/browse 
        [HttpPost("validatesubcategory/browse")]
        [FwControllerMethod(Id: "yWRQ4vzgIwvk", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SubCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventoryattributesreport/validateinventory/browse 
        [HttpPost("validateinventory/browse")]
        [FwControllerMethod(Id: "TkEYkHSvSbcf", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PartsInventoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventoryattributesreport/validateattribute/browse 
        [HttpPost("validateattribute/browse")]
        [FwControllerMethod(Id: "PSTwpa5qmNH9", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAttributeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<AttributeLogic>(browseRequest);
        }
    }
}
