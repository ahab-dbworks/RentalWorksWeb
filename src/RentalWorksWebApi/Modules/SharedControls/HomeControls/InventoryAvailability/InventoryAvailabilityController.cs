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
        // POST api/v1/inventoryavailability/dumptofile
        [HttpPost("dumptofile")]
        [FwControllerMethod(Id: "VFmuS509if2Vc")]
        public async Task<ActionResult<bool>> DumpToFile([FromRoute] string inventoryId, string warehouseId)  // inventoryId and warehouseId are optional filters here
        {
            await Task.CompletedTask; // get rid of the no async call warning
            return InventoryAvailabilityFunc.DumpAvailabilityToFile(inventoryId, warehouseId);
        }
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
        //// POST api/v1/inventoryavailability/getorderavailability
        //[HttpPost("getorderavailability")]
        //[FwControllerMethod(Id: "5NmGcYnixLAIX")]
        //public async Task<ActionResult<TAvailabilityCache>> GetAvailability([FromBody] AvailabilityOrderRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        //TAvailabilityCache availData = await InventoryAvailabilityFunc.InventoryAvailabilityFunc.GetAvailability(AppConfig, UserSession, request.SessionId, request.OrderId, request.RefreshIfNeeded);
        //        //return new OkObjectResult(availData);

        //        TAvailabilityCache availCache = new TAvailabilityCache();
        //        return new OkObjectResult(availCache);
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetApiExceptionResult(ex);
        //    }
        //}
        ////------------------------------------------------------------------------------------       
        // GET api/v1/inventoryavailability/calendarandscheduledata?InventoryId=F010F3BN&WarehouseId=B0029AY5&FromDate=11/01/2018&Todate=11/30/2018
        [HttpGet("calendarandscheduledata")]
        [FwControllerMethod(Id: "bi563cSFahD")]
        public async Task<ActionResult<TInventoryAvailabilityCalendarAndScheduleResponse>> GetCalendarDataAsync(string InventoryId, string WarehouseId, DateTime FromDate, DateTime ToDate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TInventoryAvailabilityCalendarAndScheduleResponse response = await InventoryAvailabilityFunc.GetCalendarAndScheduleData(AppConfig, UserSession, InventoryId, WarehouseId, FromDate, ToDate);
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
