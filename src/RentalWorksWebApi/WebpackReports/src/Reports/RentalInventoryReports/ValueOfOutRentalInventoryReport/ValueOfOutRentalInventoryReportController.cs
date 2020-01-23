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
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;

namespace WebApi.Modules.Reports.RentalInventoryReports.ValueOfOutRentalInventoryReport
{
    public class ValueOfOutRentalInventoryReportRequest : AppReportRequest
    {
        public DateTime AsOfDate { get; set; }
        public string WarehouseId { get; set; }
        public string InventoryTypeId { get; set; }
        public SelectedCheckBoxListItems OwnerShips { get; set; } = new SelectedCheckBoxListItems();
        public SelectedCheckBoxListItems TrackedBys { get; set; } = new SelectedCheckBoxListItems();

    }

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "n4gdQ6awebnX")]
    public class ValueOfOutRentalInventoryReportController : AppReportController
    {
        public ValueOfOutRentalInventoryReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(ValueOfOutRentalInventoryReportLoader); }
        protected override string GetReportFileName() { return "ValueOfOutRentalInventoryReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "Value of Out Rental Inventory"; }
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
            return "ValueOfOutRentalInventoryReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/valueofoutrentalinventoryreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "Py37HjYuIqdM")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request)
        {
            if (!this.ModelState.IsValid) return BadRequest(this.ModelState);
            FwReportRenderResponse response = await DoRender(request);
            return new OkObjectResult(response);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/valueofoutrentalinventoryreport/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "7YKbGZFVUWKh")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]ValueOfOutRentalInventoryReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/valueofoutrentalinventoryreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "QOjMswc4wO61")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]ValueOfOutRentalInventoryReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ValueOfOutRentalInventoryReportLoader l = new ValueOfOutRentalInventoryReportLoader();
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
        // POST api/v1/valueofoutrentalinventoryreport/validateinventorytype/browse 
        [HttpPost("validateinventorytype/browse")]
        [FwControllerMethod(Id: "xIjCjyNgdmfh", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/valueofoutrentalinventoryreport/validatewarehouse/browse 
        [HttpPost("validatewarehouse/browse")]
        [FwControllerMethod(Id: "tQM1G5jb9a14", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
    }
}
