using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
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
        //// POST api/v1/inventoryavailabilitydate/browse 
        //[HttpPost("browse")]
        //[FwControllerMethod(Id: "rlxdkpyctbA")]
        //public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        //{
        //    return await DoBrowseAsync(browseRequest);
        //}
        ////------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryavailabilitydate?InventoryId=F010F3BN&WarehouseId=B0029AY5&FromDate=11/01/2018&Todate=11/30/2018
        [HttpGet("")]
        [FwControllerMethod(Id: "bi563cSFahD")]
        public async Task<ActionResult<InventoryAvailabilityDateLogic>> GetCalendarAsync(string InventoryId, string WarehouseId, DateTime FromDate, DateTime ToDate)
        {
            //return await DoGetAsync<InventoryAvailabilityDateLogic>(id);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //BrowseRequest request = new BrowseRequest();
                //request.pageno = 0;
                //request.pagesize = 0;
                //request.orderby = string.Empty;

                //IDictionary<string, object> uniqueIds = new Dictionary<string, object>();
                //if (!string.IsNullOrEmpty(InventoryId))
                //{
                //    uniqueIds.Add("InventoryId", InventoryId);
                //}
                //if (!string.IsNullOrEmpty(WarehouseId))
                //{
                //    uniqueIds.Add("WarehouseId", WarehouseId);
                //}
                //if (FromDate != null)
                //{
                //    uniqueIds.Add("FromDate", FromDate);
                //}
                //if (ToDate != null)
                //{
                //    uniqueIds.Add("ToDate", ToDate);
                //}
                //request.uniqueids = uniqueIds;

                //InventoryAvailabilityDateLogic l = new InventoryAvailabilityDateLogic();
                //l.SetDependencies(this.AppConfig, this.UserSession);
                //IEnumerable<InventoryAvailabilityDateLogic> records = await l.SelectAsync<InventoryAvailabilityDateLogic>(request);
                //return new OkObjectResult(records);


                Dictionary<DateTime, TInventoryWarehouseAvailabilityDate> dates = await InventoryAvailabilityFunc.InventoryAvailabilityFunc.GetAvailability(AppConfig, UserSession, "", InventoryId, WarehouseId, FromDate, ToDate);
                List<InventoryAvailabilityDateLogic> data = new List<InventoryAvailabilityDateLogic>();
                int id = 0;
                foreach (KeyValuePair<DateTime, TInventoryWarehouseAvailabilityDate> d in dates)
                {
                    //available
                    id++;
                    InventoryAvailabilityDateLogic iAvail = new InventoryAvailabilityDateLogic();
                    iAvail.id = id.ToString(); ;
                    iAvail.InventoryId = InventoryId;
                    iAvail.WarehouseId = WarehouseId;
                    iAvail.start = d.Key.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                    iAvail.end = d.Key.ToString("yyyy-MM-ddTHH:mm:ss tt");
                    iAvail.text = "Available " + d.Value.AvailableQuantityOwned.ToString();
                    if (d.Value.AvailableQuantityOwned < 0)
                    {
                        iAvail.backColor = FwConvert.OleColorToHtmlColor(16711680); //red
                    }
                    else
                    {
                        iAvail.backColor = FwConvert.OleColorToHtmlColor(1176137); //green
                    }
                    iAvail.textColor = FwConvert.OleColorToHtmlColor(16777215); //white
                    data.Add(iAvail);

                    //reserved
                    if (d.Value.ReservedQuantityOwned != 0)
                    {
                        id++;
                        InventoryAvailabilityDateLogic iReserve = new InventoryAvailabilityDateLogic();
                        iReserve.id = id.ToString(); ;
                        iReserve.InventoryId = InventoryId;
                        iReserve.WarehouseId = WarehouseId;
                        iReserve.start = d.Key.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                        iReserve.end = d.Key.ToString("yyyy-MM-ddTHH:mm:ss tt");
                        iReserve.text = "Reserved " + d.Value.ReservedQuantityOwned.ToString();
                        iReserve.backColor = FwConvert.OleColorToHtmlColor(15132390); //gray
                        iReserve.textColor = FwConvert.OleColorToHtmlColor(0); //black
                        data.Add(iReserve);
                    }

                    //returning
                    if (d.Value.ReturningQuantityOwned != 0)
                    {
                        id++;
                        InventoryAvailabilityDateLogic iReturn = new InventoryAvailabilityDateLogic();
                        iReturn.id = id.ToString(); ;
                        iReturn.InventoryId = InventoryId;
                        iReturn.WarehouseId = WarehouseId;
                        iReturn.start = d.Key.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                        iReturn.end = d.Key.ToString("yyyy-MM-ddTHH:mm:ss tt");
                        iReturn.text = "Returning " + d.Value.ReturningQuantityOwned.ToString();
                        iReturn.backColor = FwConvert.OleColorToHtmlColor(618726); //blue
                        iReturn.textColor = FwConvert.OleColorToHtmlColor(16777215); //white
                        data.Add(iReturn);
                    }

                }
                return new OkObjectResult(data);



            }
            catch (Exception ex)
            {
                //FwApiException jsonException = new FwApiException();
                //jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                //jsonException.Message = ex.Message;
                //if (ex.InnerException != null)
                //{
                //    jsonException.Message += $"\n\nInnerException: \n{ex.InnerException.Message}";
                //}
                //jsonException.StackTrace = ex.StackTrace;
                //return StatusCode(jsonException.StatusCode, jsonException);
                return GetApiExceptionResult(ex);
            }

        }
        //------------------------------------------------------------------------------------ 
    }
}
