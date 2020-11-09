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
namespace WebApi.Modules.Reports.DeliveryLabel 
{ 
public class DeliveryLabelRequest : AppReportRequest 
{ 
public DateTime FromDate {get;set;} 
public DateTime ToDate {get;set;} 
public string DateType {get;set;} 
public bool? IncludeNoCharge {get;set;} 
// for office/transaction reports 
public string OfficeLocationId {get;set;} 
public string DepartmentId {get;set;} 
public string AgentId {get;set;} 
public string CustomerId {get;set;} 
public string DealId {get;set;} 
// for inventory reports 
public string WarehouseId {get;set;} 
public string InventoryTypeId {get;set;} 
public string CategoryId {get;set;} 
public string SubCategoryId {get;set;} 
public string InventoryId {get;set;} 
public SelectedCheckBoxListItems Ranks { get; set; } = new SelectedCheckBoxListItems(); 
public SelectedCheckBoxListItems TrackedBys { get; set; } = new SelectedCheckBoxListItems(); 
} 
[Route("api/v1/[controller]")] 
[ApiExplorerSettings(GroupName = "reports-v1")] 
[FwController(Id: "9OT5LSvhJaTk3")] 
public class DeliveryLabelController : AppReportController 
{ 
//------------------------------------------------------------------------------------ 
public DeliveryLabelController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(DeliveryLabelLoader);} 
//------------------------------------------------------------------------------------ 
protected override string GetReportFileName(FwReportRenderRequest request) { return "DeliveryLabel"; } 
//------------------------------------------------------------------------------------ 
protected override string GetReportFriendlyName() { return "Outgoing / Incoming Delivery Label"; } 
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
return "DeliveryLabel"; 
} 
//------------------------------------------------------------------------------------ 
// POST api/v1/deliverylabel/render 
[HttpPost("render")] 
[FwControllerMethod(Id: "9R8mDztxHNYCS")] 
public async Task<ActionResult<FwReportRenderResponse>> Render([FromBody]FwReportRenderRequest request) 
{ 
if (!this.ModelState.IsValid) return BadRequest(); 
ActionResult<FwReportRenderResponse> actionResult = await DoRender(request); 
return actionResult; 
} 
//------------------------------------------------------------------------------------ 
// POST api/v1/deliverylabel/exportexcelxlsx 
[HttpPost("exportexcelxlsx")] 
[FwControllerMethod(Id: "9sNsQpI2AfdDO")] 
public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]DeliveryLabelRequest request) 
{ 
ActionResult<FwJsonDataTable> actionResult = await RunReportAsync(request); 
FwJsonDataTable dt = (FwJsonDataTable)((OkObjectResult)(actionResult.Result)).Value; 
return await DoExportExcelXlsxFileAsync(dt, includeIdColumns: request.IncludeIdColumns); 
} 
//------------------------------------------------------------------------------------ 
// POST api/v1/deliverylabel/runreport 
[HttpPost("runreport")] 
[FwControllerMethod(Id: "9WDYFashyRsBU")] 
public async Task<ActionResult<FwJsonDataTable>> RunReportAsync([FromBody]DeliveryLabelRequest request) 
{ 
if (!ModelState.IsValid) 
{ 
return BadRequest(ModelState); 
} 
try 
{ 
DeliveryLabelLoader l = new DeliveryLabelLoader(); 
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
} 
} 
