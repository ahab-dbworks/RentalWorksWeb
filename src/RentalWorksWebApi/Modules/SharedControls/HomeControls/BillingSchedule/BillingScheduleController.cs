using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;

namespace WebApi.Modules.HomeControls.BillingSchedule
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "61bIHot9vt0X")]
    public class BillingScheduleController : AppDataController
    {
        public BillingScheduleController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
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
        //[HttpPost("getinventoryavailability")]
        //[FwControllerMethod(Id: "8WohDzjm5J7k")]
        //public async Task<ActionResult<TInventoryWarehouseAvailability>> GetAvailability([FromBody] AvailabilityInventoryWarehouseRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        TInventoryWarehouseAvailability availData = await BillingScheduleFunc.GetAvailability(AppConfig, UserSession, request.InventoryId, request.WarehouseId, request.FromDate, request.ToDate, request.RefreshIfNeeded);
        //        return new OkObjectResult(availData);
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetApiExceptionResult(ex);
        //    }
        //}
        //------------------------------------------------------------------------------------ 
    }
}
