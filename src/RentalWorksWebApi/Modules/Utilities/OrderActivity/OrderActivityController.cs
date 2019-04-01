using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using FwStandard.AppManager;
using static WebApi.Modules.Utilities.OrderActivity.OrderActivityFunc;

namespace WebApi.Modules.Utilities.OrderActivity
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "yhYOLhLE92IT")]
    public class OrderActivityController : AppDataController
    {
        public OrderActivityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) {/* logicType = typeof(OrderActivityLogic); */}
        //------------------------------------------------------------------------------------ 
        // GET api/v1/orderactivity/calendardata 
        [HttpGet("calendardata")]
        [FwControllerMethod(Id: "RhaSuoafWaVn0")]
        public async Task<ActionResult<TOrderActivityCalendarResponse>> GetCalendarDataAsync(string WarehouseId, DateTime FromDate, DateTime ToDate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TOrderActivityCalendarResponse response = await OrderActivityFunc.GetOrderActivityCalendarData(AppConfig, UserSession, WarehouseId, FromDate, ToDate);
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
