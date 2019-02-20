using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using System;
using WebApi.Modules.Home.InventoryAvailabilityFunc;

namespace WebApi.Modules.Home.InventoryAvailabilityDate
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "kfQBdYkQaEn")]
    public class InventoryAvailabilityDateController : AppDataController
    {
        public InventoryAvailabilityDateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryAvailabilityDateLogic); }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryavailabilitydate?SessionId=ABCDEFG&InventoryId=F010F3BN&WarehouseId=B0029AY5&FromDate=11/01/2018&Todate=11/30/2018
        [HttpGet("")]
        [FwControllerMethod(Id: "bi563cSFahD")]
        public async Task<ActionResult<InventoryAvailabilityDateLogic>> GetCalendarAsync(string SessionId, string InventoryId, string WarehouseId, DateTime FromDate, DateTime ToDate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TInventoryWarehouseAvailability availData = await InventoryAvailabilityFunc.InventoryAvailabilityFunc.GetAvailability(AppConfig, UserSession, SessionId, InventoryId, WarehouseId, FromDate, ToDate, true);
                List<InventoryAvailabilityDateLogic> data = new List<InventoryAvailabilityDateLogic>();
                int id = 0;
                foreach (KeyValuePair<DateTime, TInventoryWarehouseAvailabilityDate> d in availData.Dates)
                {
                    //available
                    id++;
                    InventoryAvailabilityDateLogic iAvail = new InventoryAvailabilityDateLogic();
                    iAvail.id = id.ToString(); ;
                    iAvail.InventoryId = InventoryId;
                    iAvail.WarehouseId = WarehouseId;
                    iAvail.start = d.Key.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                    iAvail.end = d.Key.ToString("yyyy-MM-ddTHH:mm:ss tt");
                    iAvail.text = "Available " + d.Value.Available.Total.ToString();
                    if (d.Value.Available.Total < 0)
                    {
                        iAvail.backColor = FwConvert.OleColorToHtmlColor(16711680); //red
                    }
                    else
                    {
                        iAvail.backColor = FwConvert.OleColorToHtmlColor(1176137); //green
                    }
                    iAvail.fontColor = FwConvert.OleColorToHtmlColor(16777215); //white
                    data.Add(iAvail);

                    //reserved
                    if (d.Value.Reserved.Total != 0)
                    {
                        id++;
                        InventoryAvailabilityDateLogic iReserve = new InventoryAvailabilityDateLogic();
                        iReserve.id = id.ToString(); ;
                        iReserve.InventoryId = InventoryId;
                        iReserve.WarehouseId = WarehouseId;
                        iReserve.start = d.Key.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                        iReserve.end = d.Key.ToString("yyyy-MM-ddTHH:mm:ss tt");
                        iReserve.text = "Reserved " + d.Value.Reserved.Total.ToString();
                        iReserve.backColor = FwConvert.OleColorToHtmlColor(15132390); //gray
                        iReserve.fontColor = FwConvert.OleColorToHtmlColor(0); //black
                        data.Add(iReserve);
                    }

                    //returning
                    if (d.Value.Returning.Total != 0)
                    {
                        id++;
                        InventoryAvailabilityDateLogic iReturn = new InventoryAvailabilityDateLogic();
                        iReturn.id = id.ToString(); ;
                        iReturn.InventoryId = InventoryId;
                        iReturn.WarehouseId = WarehouseId;
                        iReturn.start = d.Key.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                        iReturn.end = d.Key.ToString("yyyy-MM-ddTHH:mm:ss tt");
                        iReturn.text = "Returning " + d.Value.Returning.Total.ToString();
                        iReturn.backColor = FwConvert.OleColorToHtmlColor(618726); //blue
                        iReturn.fontColor = FwConvert.OleColorToHtmlColor(16777215); //white
                        data.Add(iReturn);
                    }

                }
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
