using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using FwStandard.AppManager;
using static WebApi.Modules.Utilities.QuikActivity.QuikActivityFunc;
using FwStandard.SqlServer;

namespace WebApi.Modules.Utilities.QuikActivity
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "yhYOLhLE92IT")]
    public class QuikActivityController : AppDataController
    {
        public QuikActivityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(QuikActivityLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/quikactivity/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "U48xqYD5BCIsF", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/quikactivity/calendardata 
        [HttpGet("calendardata")]
        [FwControllerMethod(Id: "RhaSuoafWaVn0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<TQuikActivityCalendarResponse>> GetCalendarDataAsync(string WarehouseId, DateTime FromDate, DateTime ToDate, bool IncludeTimes, string ActivityType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TQuikActivityCalendarResponse response = await QuikActivityFunc.GetQuikActivityCalendarData(AppConfig, UserSession, WarehouseId, FromDate, ToDate, IncludeTimes, ActivityType);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}