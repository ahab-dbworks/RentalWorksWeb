using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using FwStandard.BusinessLogic;
using WebApi.Logic;

namespace WebApi.Modules.HomeControls.OrderItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"RFgCJpybXoEb")]
    public class OrderItemController : AppDataController
    {
        public OrderItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"Yx7zfT0uv9r4", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"1hyN9Ooa1Jzs", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/orderitem/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"g4Z0ZkzW6VM7", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<OrderItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderitem 
        [HttpPost]
        [FwControllerMethod(Id:"DbB7badcbBLq", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<OrderItemLogic>> NewAsync([FromBody]OrderItemLogic l)
        {
            return await DoNewAsync<OrderItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/orderitem/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "pQX2uahR0foBi", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<OrderItemLogic>> EditAsync([FromRoute] string id, [FromBody]OrderItemLogic l)
        {
            return await DoEditAsync<OrderItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderitem/many
        [HttpPost("many")]
        [FwControllerMethod(Id: "MqUlSwrmvxSAK")]
        public async Task<List<ActionResult<OrderItemLogic>>> PostAsync([FromBody]List<OrderItemLogic> l)
        {
            FwBusinessLogicList l2 = new FwBusinessLogicList();
            l2.AddRange(l);
            return await DoPostAsync<OrderItemLogic>(l2);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/orderitem/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"042zT8NJ4EW8", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<OrderItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 

        // GET api/v1/orderitem/calculateextended
        [HttpGet("calculateextended")]
        [FwControllerMethod(Id:"Ef2C7Iuxu0tA")]
        public IActionResult CalculateExtended(string RateType, string RecType, DateTime? FromDate, DateTime? ToDate, DateTime? BillingFromDate, DateTime? BillingToDate, Decimal? Quantity, Decimal? Rate, Decimal? Rate2, Decimal? Rate3, Decimal? Rate4, Decimal? Rate5, Decimal? DaysPerWeek, Decimal? DiscountPercent)
        {
            try
            {
                OrderItemExtended e = new OrderItemExtended();
                e.RateType = RateType;
                e.RecType = RecType;
                e.FromDate = FromDate;
                e.ToDate = ToDate;
                e.BillingFromDate = BillingFromDate;
                e.BillingToDate = BillingToDate;
                e.Quantity = Quantity;
                e.Rate = Rate;
                e.Rate2 = Rate2;
                e.Rate3 = Rate3;
                e.Rate4 = Rate4;
                e.Rate5 = Rate5;
                e.DaysPerWeek = DaysPerWeek;
                e.DiscountPercent = DiscountPercent;
                e.CalculateExtendeds();
                return new OkObjectResult(e);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 

        // GET api/v1/orderitem/calculatediscountpercent
        [HttpGet("calculatediscountpercent")]
        [FwControllerMethod(Id:"lhAMnv7k6hlY")]
        public IActionResult CalculateDiscountPercent(string RateType, string RecType, DateTime? FromDate, DateTime? ToDate, DateTime? BillingFromDate, DateTime? BillingToDate, Decimal? Quantity, Decimal? Rate, Decimal? Rate2, Decimal? Rate3, Decimal? Rate4, Decimal? Rate5, Decimal? DaysPerWeek, Decimal? DiscountPercent,
                                                      Decimal? UnitDiscountAmount, Decimal? UnitExtended, Decimal? WeeklyDiscountAmount, Decimal? WeeklyExtended, Decimal? MonthlyDiscountAmount, Decimal? MonthlyExtended, Decimal? PeriodDiscountAmount, Decimal? PeriodExtended)
        {
            try
            {
                OrderItemExtended e = new OrderItemExtended();
                e.RateType = RateType;
                e.RecType = RecType;
                e.FromDate = FromDate;
                e.ToDate = ToDate;
                e.BillingFromDate = BillingFromDate;
                e.BillingToDate = BillingToDate;
                e.Quantity = Quantity;
                e.Rate = Rate;
                e.Rate2 = Rate2;
                e.Rate3 = Rate3;
                e.Rate4 = Rate4;
                e.Rate5 = Rate5;
                e.DaysPerWeek = DaysPerWeek;
                e.DiscountPercent = DiscountPercent;
                e.UnitDiscountAmount = UnitDiscountAmount;
                e.UnitExtended = UnitExtended;
                e.WeeklyDiscountAmount = WeeklyDiscountAmount;
                e.WeeklyExtended = WeeklyExtended;
                e.MonthlyDiscountAmount = MonthlyDiscountAmount;
                e.MonthlyExtended = MonthlyExtended;
                e.PeriodDiscountAmount = PeriodDiscountAmount;
                e.PeriodExtended = PeriodExtended;
                e.CalculateDiscountPercent();
                return new OkObjectResult(e);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderitem/sort
        [HttpPost("sort")]
        [FwControllerMethod(Id: "4enRLJuJAEjeO", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<SortItemsResponse>> SortOrderItemsAsync([FromBody]SortOrderItemsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return await OrderItemFunc.SortOrderItems(AppConfig, UserSession, request);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
