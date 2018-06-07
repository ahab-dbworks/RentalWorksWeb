using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Home.OrderItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class OrderItemController : AppDataController
    {
        public OrderItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderitem/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/orderitem 
        //[HttpGet]
        //public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<OrderItemLogic>(pageno, pagesize, sort, typeof(OrderItemLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        // GET api/v1/orderitem/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderItemLogic>(id, typeof(OrderItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderitem 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]OrderItemLogic l)
        {
            return await DoPostAsync<OrderItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/orderitem/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderitem/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 

        public class OrderItemExtended
        {
            public string RateType;
            public DateTime? FromDate;
            public DateTime? ToDate;
            public Decimal? Quantity;
            public Decimal? Rate;
            public Decimal? DaysPerWeek;
            public Decimal? DiscountPercent;
            public Decimal? WeeklyExtended;
            public Decimal? WeeklyDiscount;
            public Decimal? MonthlyExtended;
            public Decimal? MonthlyDiscount;
            public Decimal? PeriodExtended;
            public Decimal? PeriodDiscount;
        }

        // GET api/v1/orderitem/calculateextended
        [HttpGet("calculateextended")]
        public IActionResult CalculateExtended(string RateType, DateTime? FromDate, DateTime? ToDate, Decimal? Quantity, Decimal? Rate, Decimal? DaysPerWeek, Decimal? DiscountPercent)
        {
            try
            {
                OrderItemExtended extended = new OrderItemExtended();
                extended.RateType = RateType;
                extended.FromDate = FromDate;
                extended.ToDate = ToDate;
                extended.Quantity = Quantity;
                extended.Rate = Rate;
                extended.DaysPerWeek = DaysPerWeek;
                extended.DiscountPercent = DiscountPercent;

                //calculate extendeds here
                extended.WeeklyExtended = 1;
                extended.WeeklyDiscount = 1.5M;
                extended.MonthlyExtended = 2;
                extended.MonthlyDiscount = 2.5M;
                extended.PeriodExtended = 3;
                extended.PeriodDiscount = 3.5M;


                return new OkObjectResult(extended);
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------ 

        // GET api/v1/orderitem/calculatediscountpercent
        [HttpGet("calculatediscountpercent")]
        public IActionResult CalculateDiscountPercent(string RateType, DateTime? FromDate, DateTime? ToDate, Decimal? Quantity, Decimal? Rate, Decimal? DaysPerWeek, Decimal? WeeklyExtended, Decimal? MonthlyExtended, Decimal? PeriodExtended)
        {
            try
            {
                OrderItemExtended extended = new OrderItemExtended();
                extended.RateType = RateType;
                extended.FromDate = FromDate;
                extended.ToDate = ToDate;
                extended.Quantity = Quantity;
                extended.Rate = Rate;
                extended.DaysPerWeek = DaysPerWeek;
                extended.WeeklyExtended = WeeklyExtended;
                extended.MonthlyExtended = MonthlyExtended;
                extended.PeriodExtended = PeriodExtended;

                //calculate discount percent here
                extended.DiscountPercent = 99;


                return new OkObjectResult(extended);
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}