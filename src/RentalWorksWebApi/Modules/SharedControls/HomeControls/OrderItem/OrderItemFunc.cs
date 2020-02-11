using FwStandard.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi;
using FwStandard.SqlServer;
using System.Text;
using System.Data;
using WebApi.Modules.HomeControls.DealOrderDetail;

namespace WebApi.Modules.HomeControls.OrderItem
{
    public class SortOrderItemsRequest
    {
        public int? StartAtIndex { get; set; }
        public List<string> OrderItemIds { get; set; } = new List<string>();
    }

    public class OrderLineItem
    {
        public OrderItemLogic Item { get; set; }
        public List<OrderLineItem> Accessories { get; set; } = new List<OrderLineItem>();
        public bool ManuallySortedAccesssory { get; set; }
    }

    public static class OrderItemFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SortItemsResponse> SortOrderItems(FwApplicationConfig appConfig, FwUserSession userSession, SortOrderItemsRequest request)
        {
            SortItemsRequest r2 = new SortItemsRequest();
            r2.TableName = "masteritem";
            r2.IdFieldNames.Add("masteritemid");
            r2.RowNumberFieldName = "itemorder";
            r2.StartAtIndex = request.StartAtIndex;
            r2.RowNumberDigits = 6;

            List<string> itemsToSort = new List<string>();
            List<string> handledOrderItemIds = new List<string>();
            string orderId = "";

            if (request.OrderItemIds.Count > 0)
            {
                orderId = AppFunc.GetStringDataAsync(appConfig, "masteritem", "masteritemid", request.OrderItemIds[0], "orderid").Result;

                //gather sorted detail data for this Order in a single query
                BrowseRequest itemBrowseRequest = new BrowseRequest();
                itemBrowseRequest.uniqueids = new Dictionary<string, object>();
                itemBrowseRequest.uniqueids.Add("OrderId", orderId);
                OrderItemLogic l = new OrderItemLogic();
                l.SetDependencies(appConfig, userSession);
                List<OrderItemLogic> items = l.SelectAsync<OrderItemLogic>(itemBrowseRequest).Result;

                List<OrderLineItem> lines = new List<OrderLineItem>();
                //build logical representation of items and pacakges on this Order
                foreach (OrderItemLogic item in items)
                {
                    OrderLineItem thisLine = new OrderLineItem();
                    thisLine.Item = item;
                    lines.Add(thisLine);

                    if (!string.IsNullOrEmpty(item.NestedOrderItemId))
                    {
                        if (item.ItemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_KIT) || item.ItemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE))
                        {
                            // this line is the main-line of a nested package. find parent
                            foreach (OrderLineItem line in lines)
                            {
                                if (line.Item.OrderItemId.Equals(item.NestedOrderItemId))
                                {
                                    line.Accessories.Add(thisLine);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // this line is a nested accessory, find parent 
                            foreach (OrderLineItem line in lines)
                            {
                                if (line.Item.OrderItemId.Equals(item.ParentId))
                                {
                                    line.Accessories.Add(thisLine);
                                    break;
                                }
                            }
                        }
                    }
                    else if (!string.IsNullOrEmpty(item.ParentId))
                    {
                        // this line is a regular accessory, find parent 
                        foreach (OrderLineItem line in lines)
                        {
                            if (line.Item.OrderItemId.Equals(item.ParentId))
                            {
                                line.Accessories.Add(thisLine);
                                break;
                            }
                        }
                    }
                }

                // now that we have a logical representation of all of the packages on the Order, let's adjust the sorting for correctness to keep completes and kits together
                //foreach (string itemId in request.OrderItemIds)
                for (int i = 0; i <= request.OrderItemIds.Count - 1; i++)
                {
                    string itemId = request.OrderItemIds[i];
                    if (!handledOrderItemIds.Contains(itemId))  // if this item's sorting has not already been handled
                    {
                        itemsToSort.Add(itemId);  // add the id to the sorted list

                        foreach (OrderLineItem line in lines)  // find this item in the "lines" collection
                        {
                            if (line.Item.OrderItemId.Equals(itemId))  // found our line
                            {
                                if (line.Item.ItemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_KIT) || line.Item.ItemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE) || line.Item.ItemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_CONTAINER))
                                {
                                    // check to see if user has inentionally re-positioned accessories of this Complete/Kit immediately after our current line.  If so, we want to honor those sortings

                                    //initailize these values to prepare for the loop
                                    foreach (OrderLineItem accLine in line.Accessories)
                                    {
                                        accLine.ManuallySortedAccesssory = false;
                                    }

                                    for (int t = i + 1; t <= request.OrderItemIds.Count - 1; t++)  // peek at the next few lines to see if they are accessories to our complete/kit
                                    {
                                        string testItemId = request.OrderItemIds[t];
                                        bool isValidAcc = false;
                                        foreach (OrderLineItem accLine in line.Accessories)
                                        {
                                            if (testItemId.Equals(accLine.Item.OrderItemId))
                                            {
                                                isValidAcc = true;
                                                accLine.ManuallySortedAccesssory = true;
                                                break;
                                            }
                                        }
                                        if (!isValidAcc)  // we reached an item that is not an accessory to this complete/kit
                                        {
                                            break;
                                        }
                                    }
                                    bool accessoriesSortedManually = true;
                                    foreach (OrderLineItem accLine in line.Accessories)
                                    {
                                        if (!accLine.ManuallySortedAccesssory)
                                        {
                                            accessoriesSortedManually = false;
                                            break;
                                        }
                                    }

                                    if (!accessoriesSortedManually)  // user has abandoned accessories of this Complete/Kit.  We want to rescue those items and fix their sorting to here
                                    {
                                        // if the line is a Complete, Kit, or Container, then find all of its accessories and put them where they should go
                                        foreach (OrderLineItem accLine in line.Accessories)
                                        {
                                            handledOrderItemIds.Add(accLine.Item.OrderItemId);
                                            if (itemsToSort.Contains(accLine.Item.OrderItemId))
                                            {
                                                itemsToSort.Remove(accLine.Item.OrderItemId);
                                            }
                                            itemsToSort.Add(accLine.Item.OrderItemId);

                                            // if this accessory is really a nested Completes or Kit header line, then find all of its accessories and put them where they should go
                                            foreach (OrderLineItem nestedLine in accLine.Accessories)
                                            {
                                                handledOrderItemIds.Add(nestedLine.Item.OrderItemId);
                                                if (itemsToSort.Contains(nestedLine.Item.OrderItemId))
                                                {
                                                    itemsToSort.Remove(nestedLine.Item.OrderItemId);
                                                }
                                                itemsToSort.Add(nestedLine.Item.OrderItemId);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            foreach (string itemId in itemsToSort)
            {
                List<string> idCombo = new List<string>();
                idCombo.Add(itemId);
                r2.Ids.Add(idCombo);
            }

            if (!string.IsNullOrEmpty(orderId))
            {
                DealOrderDetailRecord o = new DealOrderDetailRecord();
                o.SetDependencies(appConfig, userSession);
                o.OrderId = orderId;
                o.IsManualSort = true;
                await o.SaveAsync(null);
            }

            SortItemsResponse response = await AppFunc.SortItems(appConfig, userSession, r2);
            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
        public static async Task<TSpStatusResponse> InsertHeaderOrderItems(FwApplicationConfig appConfig, FwUserSession userSession, List<OrderItemLogic> items)
        {
            TSpStatusResponse response = new TSpStatusResponse();

            bool inputsValid = true;
            string orderId = "";
            StringBuilder orderItemIds = new StringBuilder();
            foreach (OrderItemLogic oi in items)
            {
                if (string.IsNullOrEmpty(orderId))
                {
                    orderId = oi.OrderId;
                }
                else
                {
                    if (!oi.OrderId.Equals(orderId))
                    {
                        response.msg = "OrderId must be the same for all headings being added.";
                        response.success = false;
                        inputsValid = false;
                    }
                }
                if (orderItemIds.Length > 0)
                {
                    orderItemIds.Append(",");
                }
                orderItemIds.Append(oi.OrderItemId);
            }

            if (inputsValid)
            {

                if (!string.IsNullOrEmpty(orderId))
                {
                    DealOrderDetailRecord o = new DealOrderDetailRecord();
                    o.SetDependencies(appConfig, userSession);
                    o.OrderId = orderId;
                    o.IsManualSort = true;
                    await o.SaveAsync(null);
                }


                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "insertorderheadingsweb", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                    qry.AddParameter("@masteritemids", SqlDbType.NVarChar, ParameterDirection.Input, orderItemIds.ToString());
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.status = qry.GetParameter("@status").ToInt32();
                    response.success = (response.status == 0);
                    response.msg = qry.GetParameter("@msg").ToString();
                }
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
        public static async Task<TSpStatusResponse> InsertSubTotalOrderItems(FwApplicationConfig appConfig, FwUserSession userSession, List<OrderItemLogic> items)
        {
            TSpStatusResponse response = new TSpStatusResponse();

            bool inputsValid = true;
            string orderId = "";
            StringBuilder orderItemIds = new StringBuilder();
            foreach (OrderItemLogic oi in items)
            {
                if (string.IsNullOrEmpty(orderId))
                {
                    orderId = oi.OrderId;
                }
                else
                {
                    if (!oi.OrderId.Equals(orderId))
                    {
                        response.msg = "OrderId must be the same for all sub-totals being added.";
                        response.success = false;
                        inputsValid = false;
                    }
                }
                if (orderItemIds.Length > 0)
                {
                    orderItemIds.Append(",");
                }
                orderItemIds.Append(oi.OrderItemId);
            }

            if (inputsValid)
            {

                if (!string.IsNullOrEmpty(orderId))
                {
                    DealOrderDetailRecord o = new DealOrderDetailRecord();
                    o.SetDependencies(appConfig, userSession);
                    o.OrderId = orderId;
                    o.IsManualSort = true;
                    await o.SaveAsync(null);
                }

                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "insertordersubtotalsweb", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                    qry.AddParameter("@masteritemids", SqlDbType.NVarChar, ParameterDirection.Input, orderItemIds.ToString());
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.status = qry.GetParameter("@status").ToInt32();
                    response.success = (response.status == 0);
                    response.msg = qry.GetParameter("@msg").ToString();
                }
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
        public static async Task<TSpStatusResponse> InsertTextOrderItems(FwApplicationConfig appConfig, FwUserSession userSession, List<OrderItemLogic> items)
        {
            TSpStatusResponse response = new TSpStatusResponse();

            bool inputsValid = true;
            string orderId = "";
            StringBuilder orderItemIds = new StringBuilder();
            foreach (OrderItemLogic oi in items)
            {
                if (string.IsNullOrEmpty(orderId))
                {
                    orderId = oi.OrderId;
                }
                else
                {
                    if (!oi.OrderId.Equals(orderId))
                    {
                        response.msg = "OrderId must be the same for all texts being added.";
                        response.success = false;
                        inputsValid = false;
                    }
                }
                if (orderItemIds.Length > 0)
                {
                    orderItemIds.Append(",");
                }
                orderItemIds.Append(oi.OrderItemId);
            }

            if (inputsValid)
            {

                if (!string.IsNullOrEmpty(orderId))
                {
                    DealOrderDetailRecord o = new DealOrderDetailRecord();
                    o.SetDependencies(appConfig, userSession);
                    o.OrderId = orderId;
                    o.IsManualSort = true;
                    await o.SaveAsync(null);
                }

                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "insertordertextsweb", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                    qry.AddParameter("@masteritemids", SqlDbType.NVarChar, ParameterDirection.Input, orderItemIds.ToString());
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.status = qry.GetParameter("@status").ToInt32();
                    response.success = (response.status == 0);
                    response.msg = qry.GetParameter("@msg").ToString();
                }
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
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

            //if ((fromDate != null) && (fromDate != DateTime.MinValue) && (toDate != null) && (toDate != DateTime.MinValue))
            if ((fromDate != null) && (fromDate != DateTime.MinValue) && (toDate != null) && (toDate != DateTime.MinValue) && (!string.IsNullOrEmpty(RateType))) //justin hoffman #1498
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
                else
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

            //if (RecType.Equals(RwConstants.RECTYPE_RENTAL)) 
            if ((RecType.Equals(RwConstants.RECTYPE_RENTAL)) && (!string.IsNullOrEmpty(RateType)))//justin hoffman #1491
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
            //else if ((RecType.Equals(RwConstants.RECTYPE_SALE)) || (RecType.Equals(RwConstants.RECTYPE_LOSS_AND_DAMAGE)) || (RecType.Equals(RwConstants.RECTYPE_MISCELLANEOUS)) || (RecType.Equals(RwConstants.RECTYPE_LABOR)))
            else
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

            //if (RecType.Equals(RwConstants.RECTYPE_RENTAL)) 
            if ((RecType.Equals(RwConstants.RECTYPE_RENTAL)) && (!string.IsNullOrEmpty(RateType)))//justin hoffman #1491
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
            //else if ((RecType.Equals(RwConstants.RECTYPE_SALE)) || (RecType.Equals(RwConstants.RECTYPE_LOSS_AND_DAMAGE)) || (RecType.Equals(RwConstants.RECTYPE_MISCELLANEOUS)) || (RecType.Equals(RwConstants.RECTYPE_LABOR)))
            else
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

