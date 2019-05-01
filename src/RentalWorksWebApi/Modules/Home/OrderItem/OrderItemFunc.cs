using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Home.OrderItem
{


    public class SortOrderItemsRequest
    {
        public string OrderId { get; set; }
        public int? StartAtIndex { get; set; }
        public List<string> OrderItemIds { get; set; } = new List<string>();
    }
    public class SortOrderItemsResponse : TSpStatusReponse
    {
    }


    public static class OrderItemFunc
    {

        //-------------------------------------------------------------------------------------------------------
        public static async Task<SortOrderItemsResponse> SortOrderItems(FwApplicationConfig appConfig, FwUserSession userSession, SortOrderItemsRequest request)
        {
            SortOrderItemsResponse response = new SortOrderItemsResponse();
            if (string.IsNullOrEmpty(request.OrderId))
            {
                response.msg = "OrderId is required.";
            }
            else
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "sortmasteritems", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                    qry.AddParameter("@startatindex", SqlDbType.Int, ParameterDirection.Input, request.StartAtIndex.GetValueOrDefault(1));
                    qry.AddParameter("@masteritemids", SqlDbType.NVarChar, ParameterDirection.Input, string.Join(",", request.OrderItemIds));
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    //response.ContractId = qry.GetParameter("@contractid").ToString();
                    response.status = qry.GetParameter("@status").ToInt32();
                    response.success = (response.status == 0);
                    response.msg = qry.GetParameter("@msg").ToString();
                }
            }
            return response;
        }
    }
    //-------------------------------------------------------------------------------------------------------    

    public class OrderItemExtended
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

            Quantity = Quantity.GetValueOrDefault(0);
            DaysPerWeek = DaysPerWeek.GetValueOrDefault(1);
            DiscountPercent = DiscountPercent.GetValueOrDefault(0);

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
                        PeriodBillableDays = 0;
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
            else if ((RecType.Equals(RwConstants.RECTYPE_SALE)) || (RecType.Equals(RwConstants.RECTYPE_LOSS_AND_DAMAGE)) || (RecType.Equals(RwConstants.RECTYPE_MISCELLANEOUS)) || (RecType.Equals(RwConstants.RECTYPE_LABOR)))
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
            else if ((RecType.Equals(RwConstants.RECTYPE_SALE)) || (RecType.Equals(RwConstants.RECTYPE_LOSS_AND_DAMAGE)) || (RecType.Equals(RwConstants.RECTYPE_MISCELLANEOUS)) || (RecType.Equals(RwConstants.RECTYPE_LABOR)))
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
}

