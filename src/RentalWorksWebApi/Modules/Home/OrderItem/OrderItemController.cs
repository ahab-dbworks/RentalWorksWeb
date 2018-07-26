using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using WebLibrary;

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
        //public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<OrderItemLogic>(pageno, pagesize, sort, typeof(OrderItemLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        // GET api/v1/orderitem/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
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

        private class OrderItemExtended
        {

            private Decimal? FirstWeekBillableDays = 0;
            private Decimal? PeriodBillableDays = 0;

            public string RateType;
            public string RecType;
            public DateTime? FromDate;
            public DateTime? ToDate;
            public DateTime? BillingFromDate;
            public DateTime? BillingToDate;
            public Decimal? Quantity;
            public Decimal? Rate;
            public Decimal? Rate2;
            public Decimal? Rate3;
            public Decimal? Rate4;
            public Decimal? Rate5;
            public Decimal? DaysPerWeek;
            public Decimal? DiscountPercent;
            public int? Days;
            public int? Weeks;
            public int? Months;
            public Decimal? BillablePeriods;
            public Decimal? UnitDiscountAmount;
            public Decimal? UnitExtended;
            public Decimal? WeeklyDiscountAmount;
            public Decimal? WeeklyExtended;
            public Decimal? MonthlyDiscountAmount;
            public Decimal? MonthlyExtended;
            public Decimal? PeriodDiscountAmount;
            public Decimal? PeriodExtended;

            //------------------------------------------------------------------------------------ 
            private void UpdateDaysWeeksMonths()
            {
                Days = null;
                FirstWeekBillableDays = null;
                PeriodBillableDays = null;
                Weeks = null;
                Months = null;
                BillablePeriods = 1;

                DateTime? fromDate = (BillingFromDate > FromDate ? BillingFromDate : FromDate);
                DateTime? toDate = (BillingToDate < ToDate ? BillingToDate : ToDate);

                if ((fromDate != null) && (fromDate != DateTime.MinValue) && (toDate != null) && (toDate != DateTime.MinValue))
                {
                    Days = ((((toDate.Value) - (fromDate.Value)).Days) + 1);
                    Weeks = (int)Math.Ceiling((decimal)Days / 7);
                    Months = 0;

                    DateTime tmpDate = toDate.Value;
                    while (tmpDate > fromDate.Value)
                    {
                        Months++;
                        tmpDate = tmpDate.AddMonths(-1);
                    }

                    if (RateType.Equals(RwConstants.RATE_TYPE_DAILY))
                    {
                        if (Days > 0)
                        {
                            bool isFirstWeek = true;
                            tmpDate = fromDate.Value;
                            Decimal? daysThisWeek = 0;
                            while (tmpDate <= toDate.Value)
                            {
                                daysThisWeek = (((toDate.Value - tmpDate).Days) + 1);
                                daysThisWeek = Math.Min(DaysPerWeek.Value, daysThisWeek.Value);
                                if (isFirstWeek)
                                {
                                    FirstWeekBillableDays = daysThisWeek.Value;
                                }
                                PeriodBillableDays += daysThisWeek.Value;
                                tmpDate = tmpDate.AddDays(7);
                                isFirstWeek = false;
                            }
                        }
                    }

                    if (RecType.Equals(RwConstants.RECTYPE_RENTAL))
                    {
                        if (RateType.Equals(RwConstants.RATE_TYPE_DAILY))
                        {
                            BillablePeriods = PeriodBillableDays;
                        }
                        else if (RateType.Equals(RwConstants.RATE_TYPE_WEEKLY))
                        {
                            BillablePeriods = Weeks;
                        }
                        else if (RateType.Equals(RwConstants.RATE_TYPE_3WEEK))
                        {
                            throw new Exception($"RateType: {RateType} not programmed yet.");
                        }
                        else if (RateType.Equals(RwConstants.RATE_TYPE_MONTHLY))
                        {
                            BillablePeriods = Months;
                        }
                        else
                        {
                            throw new Exception($"Invalid RateType: {RateType}.");
                        }
                    }
                    else if ((RecType.Equals(RwConstants.RECTYPE_SALE)) || (RecType.Equals(RwConstants.RECTYPE_MISCELLANEOUS)) || (RecType.Equals(RwConstants.RECTYPE_LABOR)))
                    {
                        BillablePeriods = 1;
                    }
                }
            }
            //------------------------------------------------------------------------------------ 
            public void CalculateExtendeds()
            {
                UpdateDaysWeeksMonths();

                UnitDiscountAmount = Rate * (DiscountPercent / 100);
                UnitExtended = Rate * ((100 - DiscountPercent) / 100);

                if (RecType.Equals(RwConstants.RECTYPE_RENTAL))
                {
                    if (RateType.Equals(RwConstants.RATE_TYPE_DAILY))
                    {
                        BillablePeriods = PeriodBillableDays;

                        WeeklyDiscountAmount = Quantity * Rate * FirstWeekBillableDays * (DiscountPercent / 100);
                        WeeklyExtended = Quantity * Rate * FirstWeekBillableDays * ((100 - DiscountPercent) / 100);

                        PeriodDiscountAmount = Quantity * Rate * PeriodBillableDays * (DiscountPercent / 100);
                        PeriodExtended = Quantity * Rate * PeriodBillableDays * ((100 - DiscountPercent) / 100);

                    }
                    else if (RateType.Equals(RwConstants.RATE_TYPE_WEEKLY))
                    {
                        BillablePeriods = Weeks;

                        WeeklyDiscountAmount = Quantity * Rate * (DiscountPercent / 100);
                        WeeklyExtended = Quantity * Rate * ((100 - DiscountPercent) / 100);

                        PeriodDiscountAmount = BillablePeriods * WeeklyDiscountAmount;
                        PeriodExtended = BillablePeriods * WeeklyExtended;
                    }
                    else if (RateType.Equals(RwConstants.RATE_TYPE_3WEEK))
                    {
                        throw new Exception($"RateType: {RateType} not programmed yet.");
                    }
                    else if (RateType.Equals(RwConstants.RATE_TYPE_MONTHLY))
                    {
                        BillablePeriods = Months;

                        MonthlyDiscountAmount = Quantity * Rate * (DiscountPercent / 100);
                        MonthlyExtended = Quantity * Rate * ((100 - DiscountPercent) / 100);

                        PeriodDiscountAmount = BillablePeriods * MonthlyDiscountAmount;
                        PeriodExtended = BillablePeriods * MonthlyExtended;
                    }
                    else
                    {
                        throw new Exception($"Invalid RateType: {RateType}.");
                    }
                }
                else if ((RecType.Equals(RwConstants.RECTYPE_SALE)) || (RecType.Equals(RwConstants.RECTYPE_MISCELLANEOUS)) || (RecType.Equals(RwConstants.RECTYPE_LABOR)))
                {
                    WeeklyDiscountAmount = MonthlyDiscountAmount = PeriodDiscountAmount = Quantity * Rate * (DiscountPercent / 100);
                    WeeklyExtended = MonthlyExtended = PeriodExtended = Quantity * Rate * ((100 - DiscountPercent) / 100);
                }


                if (UnitDiscountAmount.HasValue)
                {
                    UnitDiscountAmount = Math.Round(UnitDiscountAmount.Value, 2);
                }
                if (UnitExtended.HasValue)
                {
                    UnitExtended = Math.Round(UnitExtended.Value, 2);
                }
                if (WeeklyDiscountAmount.HasValue)
                {
                    WeeklyDiscountAmount = Math.Round(WeeklyDiscountAmount.Value, 2);
                }
                if (WeeklyExtended.HasValue)
                {
                    WeeklyExtended = Math.Round(WeeklyExtended.Value, 2);
                }
                if (MonthlyDiscountAmount.HasValue)
                {
                    MonthlyDiscountAmount = Math.Round(MonthlyDiscountAmount.Value, 2);
                }
                if (MonthlyExtended.HasValue)
                {
                    MonthlyExtended = Math.Round(MonthlyExtended.Value, 2);
                }
                if (PeriodDiscountAmount.HasValue)
                {
                    PeriodDiscountAmount = Math.Round(PeriodDiscountAmount.Value, 2);
                }
                if (PeriodExtended.HasValue)
                {
                    PeriodExtended = Math.Round(PeriodExtended.Value, 2);
                }


            }
            //------------------------------------------------------------------------------------ 
            public void CalculateDiscountPercent()
            {
                UpdateDaysWeeksMonths();

                if (RecType.Equals(RwConstants.RECTYPE_RENTAL))
                {
                    if (RateType.Equals(RwConstants.RATE_TYPE_DAILY))
                    {
                        if ((Quantity == 0) || (Rate == 0) || (DaysPerWeek == 0))
                        {
                            throw new Exception("Cannot determine Discount Percent because Quantity * Rate * DaysPerWeek is zero.");
                        }
                        else
                        {
                            if (PeriodExtended != null)
                            {
                                DiscountPercent = (100 - ((100 * PeriodExtended) / (Quantity * Rate * BillablePeriods)));
                            }
                            else if (PeriodDiscountAmount != null)
                            {
                                DiscountPercent = ((100 * PeriodDiscountAmount) / (Quantity * Rate * BillablePeriods));
                            }
                            else if (WeeklyExtended != null)
                            {
                                DiscountPercent = (100 - ((100 * WeeklyExtended) / (Quantity * Rate * FirstWeekBillableDays)));
                            }
                            else if (WeeklyDiscountAmount != null)
                            {
                                DiscountPercent = ((100 * WeeklyDiscountAmount) / (Quantity * Rate * FirstWeekBillableDays));
                            }
                            else if (UnitExtended != null)
                            {
                                DiscountPercent = (100 - ((100 * UnitExtended) / (Rate * FirstWeekBillableDays)));
                            }
                            else if (UnitDiscountAmount != null)
                            {
                                DiscountPercent = ((100 * UnitDiscountAmount) / (Rate * FirstWeekBillableDays));
                            }
                            CalculateExtendeds();
                        }
                    }
                    else if (RateType.Equals(RwConstants.RATE_TYPE_WEEKLY))
                    {
                        if ((Quantity == 0) || (Rate == 0))
                        {
                            throw new Exception("Cannot determine Discount Percent because Quantity * Rate is zero.");
                        }
                        else
                        {
                            if (PeriodExtended != null)
                            {
                                DiscountPercent = (100 - ((100 * PeriodExtended) / (Quantity * Rate * BillablePeriods)));
                            }
                            else if (PeriodDiscountAmount != null)
                            {
                                DiscountPercent = ((100 * PeriodDiscountAmount) / (Quantity * Rate * BillablePeriods));
                            }
                            else if (WeeklyExtended != null)
                            {
                                DiscountPercent = (100 - ((100 * WeeklyExtended) / (Quantity * Rate)));
                            }
                            else if (WeeklyDiscountAmount != null)
                            {
                                DiscountPercent = ((100 * WeeklyDiscountAmount) / (Quantity * Rate));
                            }
                            else if (UnitExtended != null)
                            {
                                DiscountPercent = (100 - ((100 * UnitExtended) / Rate));
                            }
                            else if (UnitDiscountAmount != null)
                            {
                                DiscountPercent = ((100 * UnitDiscountAmount) / Rate);
                            }
                            CalculateExtendeds();
                        }
                    }
                    else if (RateType.Equals(RwConstants.RATE_TYPE_3WEEK))
                    {
                        //    throw new Exception($"RateType: {RateType} not programmed yet.");
                    }
                    else if (RateType.Equals(RwConstants.RATE_TYPE_MONTHLY))
                    {
                        if ((Quantity == 0) || (Rate == 0))
                        {
                            throw new Exception("Cannot determine Discount Percent because Quantity * Rate is zero.");
                        }
                        else
                        {
                            if (PeriodExtended != null)
                            {
                                DiscountPercent = (100 - ((100 * PeriodExtended) / (Quantity * Rate * BillablePeriods)));
                            }
                            else if (PeriodDiscountAmount != null)
                            {
                                DiscountPercent = ((100 * PeriodDiscountAmount) / (Quantity * Rate * BillablePeriods));
                            }
                            else if (MonthlyExtended != null)
                            {
                                DiscountPercent = (100 - ((100 * MonthlyExtended) / (Quantity * Rate)));
                            }
                            else if (MonthlyDiscountAmount != null)
                            {
                                DiscountPercent = ((100 * MonthlyDiscountAmount) / (Quantity * Rate));
                            }
                            else if (UnitExtended != null)
                            {
                                DiscountPercent = (100 - ((100 * UnitExtended) / Rate));
                            }
                            else if (UnitDiscountAmount != null)
                            {
                                DiscountPercent = ((100 * UnitDiscountAmount) / Rate);
                            }
                            CalculateExtendeds();
                        }
                    }
                    else
                    {
                        throw new Exception($"Invalid RateType: {RateType}.");
                    }
                }
                else if ((RecType.Equals(RwConstants.RECTYPE_SALE)) || (RecType.Equals(RwConstants.RECTYPE_MISCELLANEOUS)) || (RecType.Equals(RwConstants.RECTYPE_LABOR)))
                {
                    if ((Quantity == 0) || (Rate == 0))
                    {
                        throw new Exception("Cannot determine Discount Percent because Quantity * Rate is zero.");
                    }
                    else
                    {
                        if (PeriodExtended != null)
                        {
                            DiscountPercent = (100 - ((100 * PeriodExtended) / (Quantity * Rate)));
                        }
                        else if (PeriodDiscountAmount != null)
                        {
                            DiscountPercent = ((100 * PeriodDiscountAmount) / (Quantity * Rate));
                        }
                        else if (UnitExtended != null)
                        {
                            DiscountPercent = (100 - ((100 * UnitExtended) / Rate));
                        }
                        else if (UnitDiscountAmount != null)
                        {
                            DiscountPercent = ((100 * UnitDiscountAmount) / Rate);
                        }
                        CalculateExtendeds();
                    }
                }
            }
            //------------------------------------------------------------------------------------ 
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/orderitem/calculateextended
        [HttpGet("calculateextended")]
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