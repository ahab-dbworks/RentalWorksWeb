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
        // POST api/v1/inventoryavailability/getavailability
        [HttpPost("getavailability")]
        [FwControllerMethod(Id: "48MUJoECV6L9f")]
        public async Task<ActionResult<Dictionary<DateTime, TInventoryWarehouseAvailabilityDate>>> GetAvailability(string sessionId, string inventoryId, string warehouseId, DateTime fromDate, DateTime toDate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Dictionary<DateTime, TInventoryWarehouseAvailabilityDate> dates = await InventoryAvailabilityFunc.InventoryAvailabilityFunc.GetAvailability(AppConfig, UserSession, sessionId, inventoryId, warehouseId, fromDate, toDate);
                return new OkObjectResult(dates);
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
