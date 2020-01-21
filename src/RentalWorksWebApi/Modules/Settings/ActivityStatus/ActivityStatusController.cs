using FwStandard.Models;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks; 
using FwStandard.SqlServer; 
using System.Collections.Generic; 
using FwStandard.AppManager; 
namespace WebApi.Modules.Settings.ActivityStatus 
{ 
[Route("api/v1/[controller]")] 
[ApiExplorerSettings(GroupName = "settings-v1")] 
[FwController(Id: "E7cf8EVeQXuUY")] 
public class ActivityStatusController : AppDataController 
{ 
public ActivityStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ActivityStatusLogic);} 
//------------------------------------------------------------------------------------ 
// POST api/v1/activitystatus/browse 
[HttpPost("browse")] 
[FwControllerMethod(Id: "E7qumbVk0bYaI", ActionType: FwControllerActionTypes.Browse)] 
public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest) 
{ 
return await DoBrowseAsync(browseRequest); 
} 
//------------------------------------------------------------------------------------ 
// POST api/v1/activitystatus/exportexcelxlsx/filedownloadname 
[HttpPost("exportexcelxlsx/{fileDownloadName}")] 
[FwControllerMethod(Id: "E7uwCASTicdI2", ActionType: FwControllerActionTypes.Browse)] 
public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest) 
{ 
return await DoExportExcelXlsxFileAsync(browseRequest); 
} 
//------------------------------------------------------------------------------------ 
// GET api/v1/activitystatus 
[HttpGet] 
[FwControllerMethod(Id: "E9AZX55vlDrUP", ActionType: FwControllerActionTypes.Browse)] 
public async Task<ActionResult<IEnumerable<ActivityStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort) 
{ 
return await DoGetAsync<ActivityStatusLogic>(pageno, pagesize, sort); 
} 
//------------------------------------------------------------------------------------ 
// GET api/v1/activitystatus/A0000001 
[HttpGet("{id}")] 
[FwControllerMethod(Id: "e9Ymv32s8IMyE", ActionType: FwControllerActionTypes.View)] 
public async Task<ActionResult<ActivityStatusLogic>> GetOneAsync([FromRoute]string id) 
{ 
return await DoGetAsync<ActivityStatusLogic>(id); 
} 
//------------------------------------------------------------------------------------ 
// POST api/v1/activitystatus 
[HttpPost] 
[FwControllerMethod(Id: "eA1bg7vNmaMIr", ActionType: FwControllerActionTypes.New)] 
public async Task<ActionResult<ActivityStatusLogic>> NewAsync([FromBody]ActivityStatusLogic l) 
{ 
return await DoNewAsync<ActivityStatusLogic>(l); 
} 
//------------------------------------------------------------------------------------ 
// PUT api/v1/activitystatus/A0000001 
[HttpPut("{id}")] 
[FwControllerMethod(Id: "Ea2k7ks9gznW0", ActionType: FwControllerActionTypes.Edit)] 
public async Task<ActionResult<ActivityStatusLogic>> EditAsync([FromRoute] string id, [FromBody]ActivityStatusLogic l) 
{ 
return await DoEditAsync<ActivityStatusLogic>(l); 
} 
//------------------------------------------------------------------------------------ 
// DELETE api/v1/activitystatus/A0000001 
[HttpDelete("{id}")] 
[FwControllerMethod(Id: "ea70Y8S3e8hgT", ActionType: FwControllerActionTypes.Delete)] 
public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id) 
{ 
return await DoDeleteAsync<ActivityStatusLogic>(id); 
} 
//------------------------------------------------------------------------------------ 
} 
} 
