using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using FwStandard.AppManager;
using static WebApi.Modules.Utilities.QuikActivity.QuikActivityFunc;
using FwStandard.SqlServer;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;

namespace WebApi.Modules.Utilities.QuikActivity
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "yhYOLhLE92IT")]
    public class QuikActivityController : AppDataController
    {
        public QuikActivityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(QuikActivityLogic); }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/quikactivity/calendardata 
        [HttpPost("calendardata")]
        [FwControllerMethod(Id: "RhaSuoafWaVn0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<TQuikActivityCalendarResponse>> GetCalendarDataAsync([FromBody]QuikActivityCalendarRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TQuikActivityCalendarResponse response = await QuikActivityFunc.GetQuikActivityCalendarData(AppConfig, UserSession, request);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/quikactivity/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "U48xqYD5BCIsF", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/quikactivity/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "w1UwGTR6LxorC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/quikactivity/A0000001 
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "jGm20mzeOp7Qi", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<QuikActivityLogic>> EditAsync([FromRoute] string id, [FromBody]QuikActivityLogic l)
        {
            return await DoEditAsync<QuikActivityLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/quikactivity/validatewarehouse/browse
        [HttpPost("validatewarehouse/browse")]
        [FwControllerMethod(Id: "zLGR9juMsCx9", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
