using FwStandard.AppManager;
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
using static FwCore.Controllers.FwDataController;
using WebApi.Data;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi.Modules.Agent.Customer;
using WebApi.Modules.Settings.DealSettings.DealType;
using WebApi.Modules.Administrator.User;
using WebApi.Modules.Agent.Deal;

namespace WebApi.Modules.Reports.AccountingReports.ArAgingReport
{

    public class ArAgingReportRequest : AppReportRequest
    {
        public DateTime AsOfDate { get; set; }
        public string OfficeLocationId { get; set; }
        public string CustomerId { get; set; }
        public string DealCsrId { get; set; }
        public string DealTypeId { get; set; }
        public string DealId { get; set; }
    }



    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "KHw5yX5TubQ")]
    public class ArAgingReportController : AppReportController
    {
        public ArAgingReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(ArAgingReportLoader); }
        protected override string GetReportFileName(FwReportRenderRequest request) { return "ArAgingReport"; }
        //------------------------------------------------------------------------------------ 
        protected override string GetReportFriendlyName() { return "A/R Aging Report"; }
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
            return "ArAgingReport";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/aragingreport/render 
        [HttpPost("render")]
        [FwControllerMethod(Id: "7vZrn15t6u1")]
        public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody] FwReportRenderRequest request)
        {
            ActionResult<FwReportRenderResponse> actionResult = await DoRender(request);
            return actionResult;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "XZXyv8kucaW5")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody] ArAgingReportRequest request)
        {
            ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request);
            FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value;
            return await DoExportExcelXlsxFileAsync(dt, request);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/aragingreport/runreport 
        [HttpPost("runreport")]
        [FwControllerMethod(Id: "4Q6hNH42lCg")]
        public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody] ArAgingReportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ArAgingReportLoader l = new ArAgingReportLoader();
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
        //------------------------------------------------------------------------------------ 
        // POST api/v1/aragingreport/validateofficelocation/browse 
        [HttpPost("validateofficelocation/browse")]
        [FwControllerMethod(Id: "pQsmIbQ5VtBq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/aragingreport/validatecustomer/browse 
        [HttpPost("validatecustomer/browse")]
        [FwControllerMethod(Id: "OJYLnReORowK", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCustomerBrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/aragingreport/validatedealtype/browse 
        [HttpPost("validatedealtype/browse")]
        [FwControllerMethod(Id: "BreUEn8JCeYk", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealTypeBrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/aragingreport/validatedealcsr/browse 
        [HttpPost("validatedealcsr/browse")]
        [FwControllerMethod(Id: "RXNnUK050vYS", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealCSRBrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/aragingreport/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "qvUAIVbauQTs", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealBrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
    }
}
