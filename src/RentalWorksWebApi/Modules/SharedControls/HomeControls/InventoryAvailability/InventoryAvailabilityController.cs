using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;

namespace WebApi.Modules.HomeControls.InventoryAvailability
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "BDwvPyfcT8iY9")]
    public class InventoryAvailabilityController : AppDataController
    {
        public InventoryAvailabilityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 

        //// POST api/v1/inventoryavailability/requestrecalc
        //[HttpPost("requestrecalc")]
        //[FwControllerMethod(Id: "Iu0qJbYjk1chs")]
        //public async Task<ActionResult<bool>> RequestRecalc (AvailabilityRecalcRequest request) 
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        bool b = InventoryAvailabilityFunc.RequestRecalc(request.InventoryId, request.WarehouseId, request.Classification);
        //        return new OkObjectResult(b);
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetApiExceptionResult(ex);
        //    }
        //}
        ////------------------------------------------------------------------------------------ 

        // POST api/v1/inventoryavailability/getinventoryavailability
        [HttpPost("getinventoryavailability")]
        [FwControllerMethod(Id: "48MUJoECV6L9f")]
        public async Task<ActionResult<TInventoryWarehouseAvailability>> GetAvailability([FromBody] AvailabilityInventoryWarehouseRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TInventoryWarehouseAvailability availData = await InventoryAvailabilityFunc.GetAvailability(AppConfig, UserSession, request.InventoryId, request.WarehouseId, request.FromDate, request.ToDate, request.RefreshIfNeeded);
                return new OkObjectResult(availData);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------       
        // POST api/v1/inventoryavailability/calendarandscheduledata
        [HttpPost("calendarandscheduledata")]
        [FwControllerMethod(Id: "bi563cSFahD")]
        public async Task<ActionResult<TInventoryAvailabilityCalendarAndScheduleResponse>> GetCalendarDataAsync([FromBody] AvailabilityCalendarAndScheduleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TInventoryAvailabilityCalendarAndScheduleResponse response = await InventoryAvailabilityFunc.GetCalendarAndScheduleData(AppConfig, UserSession, request);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryavailability/conflicts
        [HttpPost("conflicts")]
        [FwControllerMethod(Id: "tU8RitXRpyZLw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<AvailabilityConflictResponse>> GetConflictsAsync([FromBody] AvailabilityConflictRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AvailabilityConflictResponse response = await InventoryAvailabilityFunc.GetConflicts(AppConfig, UserSession, request);
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
