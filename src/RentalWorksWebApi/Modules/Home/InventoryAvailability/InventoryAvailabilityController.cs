using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using WebApi.Modules.Home.InventoryAvailabilityFunc;

namespace WebApi.Modules.Home.InventoryAvailability
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "BDwvPyfcT8iY9")]
    public class InventoryAvailabilityController : AppDataController
    {
        public InventoryAvailabilityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) {/* logicType = typeof(InventoryAvailabilityDateLogic);*/ }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryavailability/dumptofile
        [HttpPost("dumptofile")]
        [FwControllerMethod(Id: "VFmuS509if2Vc")]
        public async Task<ActionResult<bool>> DumpToFile(string inventoryId, string warehouseId)  // inventoryId and warehouseId are optional filters here
        {
            return InventoryAvailabilityFunc.InventoryAvailabilityFunc.DumpAvailabilityToFile(inventoryId, warehouseId);
        }
        //------------------------------------------------------------------------------------ 
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
                TInventoryWarehouseAvailability availData = await InventoryAvailabilityFunc.InventoryAvailabilityFunc.GetAvailability(AppConfig, UserSession, request.SessionId, request.InventoryId, request.WarehouseId, request.FromDate, request.ToDate, request.RefreshIfNeeded);
                return new OkObjectResult(availData);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------       
        // POST api/v1/inventoryavailability/getorderavailability
        [HttpPost("getorderavailability")]
        [FwControllerMethod(Id: "5NmGcYnixLAIX")]
        public async Task<ActionResult<TAvailabilityCache>> GetAvailability([FromBody] AvailabilityOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //TAvailabilityCache availData = await InventoryAvailabilityFunc.InventoryAvailabilityFunc.GetAvailability(AppConfig, UserSession, request.SessionId, request.OrderId, request.RefreshIfNeeded);
                //return new OkObjectResult(availData);

                TAvailabilityCache availCache = new TAvailabilityCache();
                return new OkObjectResult(availCache);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------       
        // DELETE api/v1/inventoryavailability
        [HttpDelete()]
        [FwControllerMethod(Id: "FcTkG3wwcgwQQ")]
        public async Task<ActionResult<bool>> DeleteAvailability(string inventoryId, string warehouseId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InventoryAvailabilityFunc.InventoryAvailabilityFunc.DeleteAvailability(AppConfig, UserSession, inventoryId, warehouseId);
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }

        //------------------------------------------------------------------------------------       
    }
}
