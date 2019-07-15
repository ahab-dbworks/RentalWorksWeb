using System;
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using WebLibrary;
using WebApi.Logic;
using System.Collections.Concurrent;
using WebApi.Modules.Settings.AvailabilityKeepFreshLog;

namespace WebApi.Modules.Home.InventoryAvailability
{

    //-------------------------------------------------------------------------------------------------------
    public class AvailabilityRecalcRequest
    {
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public string Classification { get; set; }
    }
    //-------------------------------------------------------------------------------------------------------
    public class AvailabilityInventoryWarehouseRequest
    {
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool RefreshIfNeeded { get; set; }
    }
    //-------------------------------------------------------------------------------------------------------
    public class AvailabilityOrderRequest
    {
        public string SessionId { get; set; }
        public string OrderId { get; set; }
        public bool RefreshIfNeeded { get; set; }
    }
    //-------------------------------------------------------------------------------------------------------
    public class AvailabilityConflictRequest
    {
        public DateTime? ToDate { get; set; }
        public string AvailableFor { get; set; }  // R, S, blank
        public string ConflictType { get; set; }  // P, N, blank
        public string WarehouseId { get; set; }
        public string InventoryTypeId { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string InventoryId { get; set; }
        public string Description { get; set; }
        public string OrderId { get; set; }
        public string DealId { get; set; }
        public SelectedCheckBoxListItems Ranks { get; set; } = new SelectedCheckBoxListItems();
    }
    //-------------------------------------------------------------------------------------------------------
    public class AvailabilityConflictResponseItem
    {
        public string Warehouse { get; set; }
        public string WarehouseCode { get; set; }
        public string InventoryTypeId { get; set; }
        public string InventoryType { get; set; }
        public string CategoryId { get; set; }
        public string Category { get; set; }
        public string SubCategoryId { get; set; }
        public string SubCategory { get; set; }
        public string InventoryId { get; set; }
        public string ICode { get; set; }
        public string ItemDescription { get; set; }
        public string OrderId { get; set; }
        public string OrderType { get; set; }
        public string OrderTypeDescription { get { return AppFunc.GetOrderTypeDescription(OrderType); } }
        public string OrderNumber { get; set; }
        public string OrderDescription { get; set; }
        public string DealId { get; set; }
        public string Deal { get; set; }
        public decimal? QuantityReserved { get; set; }
        public decimal? QuantitySub { get; set; }
        public decimal? QuantityAvailable { get; set; }
        public string AvailabilityState { get; set; } = RwConstants.AVAILABILITY_STATE_STALE;
        public bool AvailabilityIsStale { get; set; }
        public decimal? QuantityLate { get; set; }
        public decimal? QuantityIn { get; set; }
        public decimal? QuantityQc { get; set; }
        public decimal? QuantityInRepair { get; set; }
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
        public string FromDateTimeString { get; set; }
        public string ToDateTimeString { get; set; }
    }
    //-------------------------------------------------------------------------------------------------------
    public class AvailabilityConflictResponse : List<AvailabilityConflictResponseItem> { }
    //-------------------------------------------------------------------------------------------------------
    public class TAvailabilityProgress
    {
        public int PercentComplete { get; set; } = 0;
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityRequestItem
    {
        public string InventoryId { get; set; } = "";
        public string WarehouseId { get; set; } = "";
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
        public TInventoryWarehouseAvailabilityRequestItem(string inventoryId, string warehouseId, DateTime fromDateTime, DateTime toDateTime)
        {
            this.InventoryId = inventoryId;
            this.WarehouseId = warehouseId;
            this.FromDateTime = fromDateTime;
            this.ToDateTime = toDateTime;
        }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityRequestItems : List<TInventoryWarehouseAvailabilityRequestItem> { }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityKey
    {
        public TInventoryWarehouseAvailabilityKey(string inventoryId, string warehouseId)
        {
            this.InventoryId = inventoryId;
            this.WarehouseId = warehouseId;
        }
        public string InventoryId { get; set; } = "";
        public string WarehouseId { get; set; } = "";
        public string CombinedKey { get { return InventoryId + "-" + WarehouseId; } }
        public override string ToString()
        {
            return CombinedKey;
        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj is TInventoryWarehouseAvailabilityKey)
            {
                TInventoryWarehouseAvailabilityKey testObj = (TInventoryWarehouseAvailabilityKey)obj;
                isEqual = testObj.CombinedKey.Equals(CombinedKey);
            }
            return isEqual;

        }
        public override int GetHashCode()
        {
            return CombinedKey.GetHashCode();
        }

    }
    //-------------------------------------------------------------------------------------------------------
    public class AvailabilityKeyEqualityComparer : IEqualityComparer<TInventoryWarehouseAvailabilityKey>
    {
        public bool Equals(TInventoryWarehouseAvailabilityKey k1, TInventoryWarehouseAvailabilityKey k2)
        {
            return ((k1.InventoryId.Equals(k2.InventoryId)) && (k1.WarehouseId.Equals(k2.WarehouseId)));
        }
        public int GetHashCode(TInventoryWarehouseAvailabilityKey k)
        {
            return k.CombinedKey.GetHashCode();
        }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityKeys : List<TInventoryWarehouseAvailabilityKey> { }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityQuantity
    {
        public decimal Owned { get; set; } = 0;
        public decimal Subbed { get; set; } = 0;
        public decimal Consigned { get; set; } = 0;
        public decimal Total { get { return Owned + Subbed + Consigned; } }

        public void CloneFrom(TInventoryWarehouseAvailabilityQuantity source)
        {
            this.Owned = source.Owned;
            this.Subbed = source.Subbed;
            this.Consigned = source.Consigned;
        }

        public static TInventoryWarehouseAvailabilityQuantity operator +(TInventoryWarehouseAvailabilityQuantity q1, TInventoryWarehouseAvailabilityQuantity q2)
        {
            TInventoryWarehouseAvailabilityQuantity q3 = new TInventoryWarehouseAvailabilityQuantity();
            q3.Owned = q1.Owned + q2.Owned;
            q3.Subbed = q1.Subbed + q2.Subbed;
            q3.Consigned = q1.Consigned + q2.Consigned;
            return q3;
        }

        public static TInventoryWarehouseAvailabilityQuantity operator -(TInventoryWarehouseAvailabilityQuantity q1, TInventoryWarehouseAvailabilityQuantity q2)
        {
            TInventoryWarehouseAvailabilityQuantity q3 = new TInventoryWarehouseAvailabilityQuantity();
            q3.Owned = q1.Owned - q2.Owned;
            q3.Subbed = q1.Subbed - q2.Subbed;
            q3.Consigned = q1.Consigned - q2.Consigned;
            return q3;
        }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityReservation
    {
        public string OrderId { get; set; }
        public string OrderItemId { get; set; }
        public string OrderType { get; set; }
        public string OrderTypeDescription { get { return AppFunc.GetOrderTypeDescription(OrderType); } }
        public string OrderNumber { get; set; }
        public string OrderDescription { get; set; }
        public string OrderStatus { get; set; }
        public string DepartmentId { get; set; }
        public string Department { get; set; }
        public string DealId { get; set; }
        public string Deal { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
        public decimal QuantityOrdered { get; set; } = 0;
        public decimal QuantitySub { get; set; } = 0;
        public decimal QuantityConsigned { get; set; } = 0;
        public TInventoryWarehouseAvailabilityQuantity QuantityReserved { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity QuantityStaged { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity QuantityOut { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity QuantityIn { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity QuantityLate { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public bool IsPositiveConflict { get; set; } = false;
        public bool IsNegativeConflict { get; set; } = false;
        [JsonIgnore]
        public bool countedReserved = false;  // used only while calculating future availability
        [JsonIgnore]
        public bool countedLate = false;  // used only while calculating future availability
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityDateTime
    {
        public TInventoryWarehouseAvailabilityDateTime(DateTime availabilityDateTime)
        {
            this.AvailabilityDateTime = availabilityDateTime;
        }
        public DateTime AvailabilityDateTime { get; set; }
        public TInventoryWarehouseAvailabilityQuantity Available { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity Reserved { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity Returning { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityMinimum
    {
        public TInventoryWarehouseAvailabilityQuantity MinimumAvailable { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public DateTime? FirstConfict { get; set; }
        public bool NoAvailabilityCheck { get; set; }
        public bool IsStale { get; set; }
        public string AvailabilityState { get; set; } = RwConstants.AVAILABILITY_STATE_STALE;
        public string Color { get; set; }
        public string TextColor { get; set; }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TPackageAccessory
    {
        public TPackageAccessory(string inventoryId, decimal defaultQuantity)
        {
            this.InventoryId = inventoryId;
            this.DefaultQuantity = defaultQuantity;
        }
        public string InventoryId { get; set; } = "";
        public decimal DefaultQuantity { get; set; } = 0;
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouse
    {
        public TInventoryWarehouse(string inventoryId, string warehouseId)
        {
            this.InventoryId = inventoryId;
            this.WarehouseId = warehouseId;
        }
        public string InventoryTypeId { get; set; } = "";
        public string InventoryType { get; set; } = "";
        public string CategoryId { get; set; } = "";
        public string Category { get; set; } = "";
        public string SubCategoryId { get; set; } = "";
        public string SubCategory { get; set; } = "";
        public string InventoryId { get; set; } = "";
        public string WarehouseId { get; set; } = "";
        public string AvailableFor { get; set; } = "";
        public string ICode { get; set; } = "";
        public string Description { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
        public string Warehouse { get; set; } = "";
        public string Classification { get; set; } = "";
        public bool HourlyAvailability { get; set; } = false;
        public bool NoAvailabilityCheck { get; set; } = false;
        public int LowAvailabilityPercent { get; set; } = 0;
        public int LowAvailabilityQuantity { get; set; } = 0;
        public List<TPackageAccessory> Accessories { get; set; } = new List<TPackageAccessory>();

        public string CombinedKey { get { return InventoryId + "-" + WarehouseId; } }
        public override string ToString()
        {
            return CombinedKey;
        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj is TInventoryWarehouse)
            {
                TInventoryWarehouse testObj = (TInventoryWarehouse)obj;
                isEqual = testObj.CombinedKey.Equals(CombinedKey);
            }
            return isEqual;

        }
        public override int GetHashCode()
        {
            return CombinedKey.GetHashCode();
        }
    }
    //-------------------------------------------------------------------------------------------------------
    public class InventoryWarehouseEqualityComparer : IEqualityComparer<TInventoryWarehouse>
    {
        public bool Equals(TInventoryWarehouse k1, TInventoryWarehouse k2)
        {
            return ((k1.InventoryId.Equals(k2.InventoryId)) && (k1.WarehouseId.Equals(k2.WarehouseId)));
        }
        public int GetHashCode(TInventoryWarehouse k)
        {
            return k.CombinedKey.GetHashCode();
        }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailability
    {
        public TInventoryWarehouseAvailability(string inventoryId, string warehouseId)
        {
            this.InventoryWarehouse = new TInventoryWarehouse(inventoryId, warehouseId);
        }
        public TInventoryWarehouse InventoryWarehouse { get; set; }
        public DateTime CalculatedDateTime { get; set; }
        public TInventoryWarehouseAvailabilityQuantity Total { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity In { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity Staged { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity Out { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity InRepair { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity InTransit { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity InContainer { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity OnTruck { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity QcRequired { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity Late { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public DateTime AvailDataFromDateTime { get; set; }
        public DateTime AvailDataToDateTime { get; set; }
        public List<TInventoryWarehouseAvailabilityReservation> Reservations { get; set; } = new List<TInventoryWarehouseAvailabilityReservation>();
        public ConcurrentDictionary<DateTime, TInventoryWarehouseAvailabilityDateTime> AvailabilityDatesAndTimes { get; set; } = new ConcurrentDictionary<DateTime, TInventoryWarehouseAvailabilityDateTime>();
        public bool HasPositiveConflict { get; set; } = false;
        public bool HasNegativeConflict { get; set; } = false;

        //-------------------------------------------------------------------------------------------------------
        public void CloneFrom(TInventoryWarehouseAvailability source)
        {
            this.InventoryWarehouse = source.InventoryWarehouse;
            this.AvailDataFromDateTime = source.AvailDataFromDateTime;
            this.AvailDataToDateTime = source.AvailDataToDateTime;
            this.Total = source.Total;
            this.In = source.In;
            this.Staged = source.Staged;
            this.Out = source.Out;
            this.InRepair = source.InRepair;
            this.InTransit = source.InTransit;
            this.InContainer = source.InContainer;
            this.OnTruck = source.OnTruck;
            this.QcRequired = source.QcRequired;
            this.Late = source.Late;
            this.CalculatedDateTime = source.CalculatedDateTime;
            this.Reservations.Clear();
            foreach (TInventoryWarehouseAvailabilityReservation reservation in source.Reservations)
            {
                this.Reservations.Add(reservation);
            }
            this.AvailabilityDatesAndTimes.Clear();
            foreach (KeyValuePair<DateTime, TInventoryWarehouseAvailabilityDateTime> date in source.AvailabilityDatesAndTimes)
            {
                this.AvailabilityDatesAndTimes.AddOrUpdate(date.Key, date.Value, (key, existingValue) =>
                {
                    existingValue.AvailabilityDateTime = date.Value.AvailabilityDateTime;
                    existingValue.Available = date.Value.Available;
                    existingValue.Reserved = date.Value.Reserved;
                    existingValue.Returning = date.Value.Returning;
                    return existingValue;
                });
            }
            this.HasNegativeConflict = source.HasNegativeConflict;
            this.HasPositiveConflict = source.HasPositiveConflict;
        }
        //-------------------------------------------------------------------------------------------------------
        public TInventoryWarehouseAvailabilityMinimum GetMinimumAvailableQuantity(DateTime fromDateTime, DateTime toDateTime, decimal additionalQuantity = 0)
        {
            TInventoryWarehouseAvailabilityMinimum minAvail = new TInventoryWarehouseAvailabilityMinimum();
            bool isStale = false;
            bool isHistory = false;
            minAvail.AvailabilityState = RwConstants.AVAILABILITY_STATE_STALE;

            minAvail.NoAvailabilityCheck = InventoryWarehouse.NoAvailabilityCheck;

            if (fromDateTime < DateTime.Today)
            {
                fromDateTime = DateTime.Today;
            }

            if (toDateTime.Equals(InventoryAvailabilityFunc.LateDateTime))
            {
                toDateTime = AvailDataToDateTime;
            }
            else if (toDateTime < DateTime.Today)
            {
                toDateTime = DateTime.Today;
                isHistory = true;
            }

            if (minAvail.NoAvailabilityCheck)
            {
                minAvail.AvailabilityState = RwConstants.AVAILABILITY_STATE_NO_AVAILABILITY_CHECK;
                minAvail.Color = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_NO_AVAILABILITY);
                minAvail.TextColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_NEEDRECALC);
            }
            else if (isHistory)
            {
                minAvail.Color = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_HISTORICAL_DATE);
                minAvail.TextColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_HISTORICAL_DATE);
                minAvail.AvailabilityState = RwConstants.AVAILABILITY_STATE_HISTORY;
            }
            else
            {
                bool firstDateFound = false;
                bool lastDateFound = false;
                //#jhtodo rewrite this without the foreach. create a for loop for each date in the range. perform individual specific reads from "Dates" dictionary using the key
                foreach (KeyValuePair<DateTime, TInventoryWarehouseAvailabilityDateTime> availDate in AvailabilityDatesAndTimes)
                {
                    DateTime theDateTime = availDate.Key;
                    TInventoryWarehouseAvailabilityDateTime inventoryWarehouseAvailabilityDateTime = availDate.Value;

                    TInventoryWarehouseAvailabilityQuantity avail = new TInventoryWarehouseAvailabilityQuantity();
                    avail.CloneFrom(inventoryWarehouseAvailabilityDateTime.Available);

                    if ((fromDateTime <= theDateTime) && (theDateTime <= toDateTime))
                    {
                        avail.Owned = avail.Owned - additionalQuantity;
                        if (theDateTime.Equals(fromDateTime))
                        {
                            firstDateFound = true;
                            minAvail.MinimumAvailable = avail;
                        }
                        minAvail.MinimumAvailable = (minAvail.MinimumAvailable.Total < avail.Total) ? minAvail.MinimumAvailable : avail;

                        if ((minAvail.FirstConfict == null) && (minAvail.MinimumAvailable.Total < 0))
                        {
                            minAvail.FirstConfict = theDateTime;
                        }

                        if (theDateTime.Equals(toDateTime))
                        {
                            lastDateFound = true;
                        }
                    }
                }

                if (!isStale)
                {
                    isStale = InventoryAvailabilityFunc.InventoryWarehouseNeedsRecalc(InventoryWarehouse.InventoryId, InventoryWarehouse.WarehouseId);
                }

                if (!isStale)
                {
                    if ((!firstDateFound) || (!lastDateFound))
                    {
                        isStale = true;
                    }
                }
                minAvail.IsStale = isStale;

                minAvail.Color = null;
                minAvail.TextColor = FwConvert.OleColorToHtmlColor(0); //black

                if (minAvail.IsStale)
                {
                    minAvail.AvailabilityState = RwConstants.AVAILABILITY_STATE_STALE;
                    minAvail.Color = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_NEEDRECALC);
                    minAvail.TextColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_NEEDRECALC);
                }
                else if (minAvail.MinimumAvailable.Total < 0)
                {
                    minAvail.AvailabilityState = RwConstants.AVAILABILITY_STATE_NEGATIVE;
                    minAvail.Color = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_NEGATIVE);
                    minAvail.TextColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_NEGATIVE);
                }
                else if (minAvail.MinimumAvailable.Total == 0)
                {
                    minAvail.AvailabilityState = RwConstants.AVAILABILITY_STATE_ZERO;
                }
                else if ((minAvail.MinimumAvailable.Total >= 0) && (InventoryWarehouse.LowAvailabilityPercent != 0) && (minAvail.MinimumAvailable.Total <= InventoryWarehouse.LowAvailabilityQuantity))
                {
                    minAvail.AvailabilityState = RwConstants.AVAILABILITY_STATE_LOW;
                }
                else if (minAvail.MinimumAvailable.Total > 0)
                {
                    minAvail.AvailabilityState = RwConstants.AVAILABILITY_STATE_ENOUGH;
                }
            }

            return minAvail;
        }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TAvailabilityCache : ConcurrentDictionary<TInventoryWarehouseAvailabilityKey, TInventoryWarehouseAvailability>
    {
        public TAvailabilityCache() : base(new AvailabilityKeyEqualityComparer()) { }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TAvailabilityNeedRecalcDictionary : ConcurrentDictionary<TInventoryWarehouseAvailabilityKey, string>
    {
        public TAvailabilityNeedRecalcDictionary() : base(new AvailabilityKeyEqualityComparer()) { }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryNeedingAvailDictionary : ConcurrentDictionary<TInventoryWarehouseAvailabilityKey, string>
    {
        public TInventoryNeedingAvailDictionary() : base(new AvailabilityKeyEqualityComparer()) { }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryAvailabilityCalendarEvent
    {
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string text { get; set; }
        public string backColor { get; set; }
        public string textColor { get; set; }
        public string id { get; set; } = "";
        public string resource { get; set; }
    }
    //------------------------------------------------------------------------------------ 
    public class TInventoryAvailabilityScheduleResource
    {
        public string name { get; set; } = "";
        public string id { get; set; } = "";
        public string backColor { get; set; } // this will color the background of the resource "name" cell, not the entire row
    }
    //------------------------------------------------------------------------------------ 
    public class TInventoryAvailabilityScheduleEvent
    {
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string text { get; set; }
        public string backColor { get; set; }
        public string barColor { get; set; }
        public string textColor { get; set; }
        public string id { get; set; } = "";
        public string resource { get; set; }
        public string orderNumber { get; set; }
        public string orderStatus { get; set; }
        public string deal { get; set; }
    }
    //------------------------------------------------------------------------------------ 
    public class TInventoryAvailabilityCalendarAndScheduleResponse
    {
        public TInventoryWarehouseAvailability InventoryData { get; set; }
        public List<TInventoryAvailabilityCalendarEvent> InventoryAvailabilityCalendarEvents { get; set; } = new List<TInventoryAvailabilityCalendarEvent>();
        public List<TInventoryAvailabilityScheduleResource> InventoryAvailabilityScheduleResources { get; set; } = new List<TInventoryAvailabilityScheduleResource>();
        public List<TInventoryAvailabilityScheduleEvent> InventoryAvailabilityScheduleEvents { get; set; } = new List<TInventoryAvailabilityScheduleEvent>();
    }

    //------------------------------------------------------------------------------------ 
    //------------------------------------------------------------------------------------ 
    //------------------------------------------------------------------------------------ 
    //------------------------------------------------------------------------------------ 

    public static class InventoryAvailabilityFunc
    {
        //-------------------------------------------------------------------------------------------------------
        private const int AVAILABILITY_DAYS_TO_CACHE = 90;
        //-------------------------------------------------------------------------------------------------------
        private static TAvailabilityNeedRecalcDictionary AvailabilityNeedRecalc = new TAvailabilityNeedRecalcDictionary();
        private static int LastNeedRecalcId = 0;
        public static DateTime LateDateTime = DateTime.MaxValue;  // This is a temporary value.  Actual value gets set in InitializeService
        private static TAvailabilityCache AvailabilityCache = new TAvailabilityCache();
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> InitializeService(FwApplicationConfig appConfig)
        {
            bool success = true;

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select needrecalcid = max(a.id)");
                qry.Add(" from  tmpavailneedrecalc a");
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();
                LastNeedRecalcId = FwConvert.ToInt32(dt.Rows[0][dt.GetColumnNo("needrecalcid")]);
            }

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select latedatetime = dbo.funcmaxavaildate() ");
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();
                LateDateTime = FwConvert.ToDateTime(dt.Rows[0][dt.GetColumnNo("latedatetime")].ToString());
            }

            return success;
        }
        //-------------------------------------------------------------------------------------------------------        
        public static bool InventoryWarehouseNeedsRecalc(TInventoryWarehouseAvailabilityKey availKey)
        {
            return AvailabilityNeedRecalc.ContainsKey(availKey);
        }
        //-------------------------------------------------------------------------------------------------------        
        public static bool InventoryWarehouseNeedsRecalc(string inventoryId, string warehouseId)
        {
            return InventoryWarehouseNeedsRecalc(new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId));
        }
        //-------------------------------------------------------------------------------------------------------        
        public static bool RequestRecalc(string inventoryId, string warehouseId, string classification)
        {
            bool b = true;
            //Console.WriteLine("adding to AvailabilityNeedRecalc - " + inventoryId + ", " + warehouseId + ", " + classification);
            AvailabilityNeedRecalc.AddOrUpdate(new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId), classification, (key, existingValue) =>
            {
                existingValue = classification;
                return existingValue;
            });
            return b;
        }
        //-------------------------------------------------------------------------------------------------------        
        public static async Task<bool> InvalidateHourly(FwApplicationConfig appConfig)
        {
            bool success = true;
            //Console.WriteLine("invalidating cache for all inventory tracked by hourly availbility");
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {

                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select masterid, warehouseid, class   ");
                qry.Add(" from  masterwhforavailview           ");
                qry.Add(" where availbyhour = 'T'              ");
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                foreach (List<object> row in dt.Rows)
                {
                    string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                    string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
                    string classification = row[dt.GetColumnNo("class")].ToString();
                    RequestRecalc(inventoryId, warehouseId, classification);
                }
            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------        
        public static async Task<bool> InvalidateDaily(FwApplicationConfig appConfig)
        {
            bool success = true;
            //Console.WriteLine("invalidating cache for all inventory tracked by daily availbility");
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {

                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select masterid, warehouseid, class   ");
                qry.Add(" from  masterwhforavailview           ");
                qry.Add(" where availbyhour <> 'T'             ");
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                foreach (List<object> row in dt.Rows)
                {
                    string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                    string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
                    string classification = row[dt.GetColumnNo("class")].ToString();
                    RequestRecalc(inventoryId, warehouseId, classification);
                }
            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------        
        public static async Task<bool> CheckNeedRecalc(FwApplicationConfig appConfig)
        {
            bool success = true;

            //Console.WriteLine("about to query the availneedrecalc table");

            //Console.WriteLine("  before: AvailabilityNeedRecalc has " + AvailabilityNeedRecalc.Count.ToString() + " items");
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select a.masterid, a.warehouseid, a.class, a.id   ");
                qry.Add(" from  tmpavailneedrecalcview a with (nolock)     ");
                qry.Add(" where a.id > @lastneedrecalcid                   ");
                qry.Add("order by a.id                                     ");
                qry.AddParameter("@lastneedrecalcid", LastNeedRecalcId);
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                //Console.WriteLine("          found " + dt.TotalRows.ToString() + " records in the availneedrecalc table");

                string needRecalcId = string.Empty;
                foreach (List<object> row in dt.Rows)
                {
                    string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                    string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
                    string classification = row[dt.GetColumnNo("class")].ToString();
                    needRecalcId = row[dt.GetColumnNo("id")].ToString();
                    RequestRecalc(inventoryId, warehouseId, classification);
                }
                if (!needRecalcId.Equals(string.Empty))
                {
                    LastNeedRecalcId = FwConvert.ToInt32(needRecalcId);
                }
            }

            //Console.WriteLine("  after:  AvailabilityNeedRecalc has " + AvailabilityNeedRecalc.Count.ToString() + " items");

            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<TAvailabilityCache> BuildAvailabilityCache(FwApplicationConfig appConfig, FwUserSession userSession, TInventoryWarehouseAvailabilityRequestItems availRequestItems)
        {
            TAvailabilityCache availCache = new TAvailabilityCache();
            string sessionId = AppFunc.GetNextIdAsync(appConfig).Result;
            DateTime fromDateTime = DateTime.Today;
            DateTime toDateTime = DateTime.Today.AddDays(AVAILABILITY_DAYS_TO_CACHE);

            foreach (TInventoryWarehouseAvailabilityRequestItem availRequestItem in availRequestItems)
            {
                TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(availRequestItem.InventoryId, availRequestItem.WarehouseId);
                string classification = "";
                AvailabilityNeedRecalc.TryRemove(availKey, out classification);

                if (availRequestItem.ToDateTime > toDateTime)
                {
                    toDateTime = availRequestItem.ToDateTime;
                }
            }

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("delete                        ");
                qry.Add(" from  tmpsearchsession       ");
                qry.Add(" where sessionid = @sessionid ");
                qry.AddParameter("@sessionid", sessionId);
                await qry.ExecuteNonQueryAsync();
            }

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                int i = 0;
                int totalItemsAddedToSession = 0;
                int totalRequestItems = availRequestItems.Count;
                const int MAX_ITEMS_PER_ITERATION = 300;
                foreach (TInventoryWarehouseAvailabilityRequestItem availRequestItem in availRequestItems)
                {
                    qry.Add("if (not exists (select *                                                                ");
                    qry.Add("                 from  tmpsearchsession t                                               ");
                    qry.Add("                 where t.sessionid   = @sessionid                                       ");
                    qry.Add("                 and   t.masterid    = @masterid" + i.ToString() + "                    ");
                    qry.Add("                 and   t.warehouseid = @warehouseid" + i.ToString() + "))               ");
                    qry.Add("begin                                                                                   ");
                    qry.Add("   insert into tmpsearchsession (sessionid, masterid, warehouseid)                      ");
                    qry.Add("    values (@sessionid, @masterid" + i.ToString() + ", @warehouseid" + i.ToString() + ")");
                    qry.Add("end                                                                                     ");
                    qry.AddParameter("@masterid" + i.ToString(), availRequestItem.InventoryId);
                    qry.AddParameter("@warehouseid" + i.ToString(), availRequestItem.WarehouseId);
                    i++;
                    totalItemsAddedToSession++;

                    if ((i >= MAX_ITEMS_PER_ITERATION) && (totalItemsAddedToSession < totalRequestItems))   // if we have already created a query with 300 items to add, and there are still more to do, then process the 300 and start a new query for the next group. this avoids exceeding the parameter limit on sql server
                    {
                        qry.AddParameter("@sessionid", sessionId);
                        await qry.ExecuteNonQueryAsync();
                        i = 0;
                        qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                    }
                }
                qry.AddParameter("@sessionid", sessionId);
                await qry.ExecuteNonQueryAsync();
            }

            Dictionary<TInventoryWarehouseAvailabilityKey, TInventoryWarehouse> packages = new Dictionary<TInventoryWarehouseAvailabilityKey, TInventoryWarehouse>();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {

                FwSqlCommand qryAcc = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qryAcc.Add("select p.packageid, p.warehouseid, p.masterid, p.defaultqty              ");
                qryAcc.Add(" from  packagemasterwhforavailview p                                     ");
                qryAcc.Add("              join tmpsearchsession a on (p.packageid   = a.masterid and ");
                qryAcc.Add("                                          p.warehouseid = a.warehouseid) ");
                qryAcc.Add(" where a.sessionid = @sessionid                                          ");
                qryAcc.Add("order by p.packageid, p.warehouseid                                      ");
                qryAcc.AddParameter("@sessionid", sessionId);
                FwJsonDataTable dtAcc = await qryAcc.QueryToFwJsonTableAsync();

                if (dtAcc.Rows.Count > 0)
                {
                    string prevPackageId = string.Empty;
                    string prevWarehouseId = string.Empty;
                    string packageId = string.Empty;
                    string warehouseId = string.Empty;
                    TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(packageId, warehouseId);
                    TInventoryWarehouse inventoryWarehouse = new TInventoryWarehouse(packageId, warehouseId);

                    foreach (List<object> rowAcc in dtAcc.Rows)
                    {
                        packageId = rowAcc[dtAcc.GetColumnNo("packageid")].ToString();
                        warehouseId = rowAcc[dtAcc.GetColumnNo("warehouseid")].ToString();
                        if ((!packageId.Equals(prevPackageId)) || (!warehouseId.Equals(prevWarehouseId)))
                        {
                            if (!prevPackageId.Equals(string.Empty))
                            {
                                packages.Add(availKey, inventoryWarehouse);
                            }
                            availKey = new TInventoryWarehouseAvailabilityKey(packageId, warehouseId);
                            inventoryWarehouse = new TInventoryWarehouse(packageId, warehouseId);
                        }

                        string accInventoryId = rowAcc[dtAcc.GetColumnNo("masterid")].ToString();
                        decimal accDefaultQuantity = FwConvert.ToDecimal(rowAcc[dtAcc.GetColumnNo("defaultqty")].ToString());
                        inventoryWarehouse.Accessories.Add(new TPackageAccessory(accInventoryId, accDefaultQuantity));

                        prevPackageId = packageId;
                        prevWarehouseId = warehouseId;
                    }

                    if (!packageId.Equals(string.Empty))
                    {
                        packages.Add(availKey, inventoryWarehouse);
                    }
                }
            }


            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select a.masterid, a.warehouseid, a.availfor,                                                                         ");
                qry.Add("       a.masterno, a.master, a.whcode, a.noavail, a.warehouse, a.class, a.availbyhour, a.availabilitygrace,           ");
                qry.Add("       a.inventorydepartmentid, a.inventorydepartment, a.categoryid, a.category, a.subcategoryid, a.subcategory,      ");
                qry.Add("       a.ownedqty, a.ownedqtyin, a.ownedqtystaged, a.ownedqtyout, a.ownedqtyintransit,                                ");
                qry.Add("       a.ownedqtyinrepair, a.ownedqtyontruck, a.ownedqtyincontainer, a.ownedqtyqcrequired,                            ");
                qry.Add("       a.consignedqty, a.consignedqtyin, a.consignedqtystaged, a.consignedqtyout, a.consignedqtyintransit,            ");
                qry.Add("       a.consignedqtyinrepair, a.consignedqtyontruck, a.consignedqtyincontainer, a.consignedqtyqcrequired             ");
                qry.Add(" from  availabilitymasterwhview a with (nolock)                                                                       ");
                qry.Add("             join tmpsearchsession t with (nolock) on (a.masterid = t.masterid and                                    ");
                qry.Add("                                                       a.warehouseid = t.warehouseid)                                 ");
                qry.Add(" where t.sessionid = @sessionid                                                                                       ");
                qry.AddParameter("@sessionid", sessionId);
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                foreach (List<object> row in dt.Rows)
                {
                    string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                    string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
                    TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);

                    TInventoryWarehouseAvailability availData = new TInventoryWarehouseAvailability(inventoryId, warehouseId);

                    availData.AvailDataFromDateTime = fromDateTime;
                    availData.AvailDataToDateTime = toDateTime;
                    availData.InventoryWarehouse.ICode = row[dt.GetColumnNo("masterno")].ToString();
                    availData.InventoryWarehouse.Description = row[dt.GetColumnNo("master")].ToString();
                    availData.InventoryWarehouse.WarehouseCode = row[dt.GetColumnNo("whcode")].ToString();
                    availData.InventoryWarehouse.Warehouse = row[dt.GetColumnNo("warehouse")].ToString();
                    availData.InventoryWarehouse.Classification = row[dt.GetColumnNo("class")].ToString();
                    availData.InventoryWarehouse.InventoryTypeId = row[dt.GetColumnNo("inventorydepartmentid")].ToString();
                    availData.InventoryWarehouse.InventoryType = row[dt.GetColumnNo("inventorydepartment")].ToString();
                    availData.InventoryWarehouse.CategoryId = row[dt.GetColumnNo("categoryid")].ToString();
                    availData.InventoryWarehouse.Category = row[dt.GetColumnNo("category")].ToString();
                    availData.InventoryWarehouse.SubCategoryId = row[dt.GetColumnNo("subcategoryid")].ToString();
                    availData.InventoryWarehouse.SubCategory = row[dt.GetColumnNo("subcategory")].ToString();
                    availData.InventoryWarehouse.AvailableFor = row[dt.GetColumnNo("availfor")].ToString();

                    if (availData.InventoryWarehouse.Classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE) || availData.InventoryWarehouse.Classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_KIT))
                    {
                        TInventoryWarehouse package = null;
                        if (packages.TryGetValue(availKey, out package))
                        {
                            foreach (TPackageAccessory accessory in package.Accessories)
                            {
                                availData.InventoryWarehouse.Accessories.Add(new TPackageAccessory(accessory.InventoryId, accessory.DefaultQuantity));
                            }
                        }
                    }

                    availData.InventoryWarehouse.HourlyAvailability = FwConvert.ToBoolean(row[dt.GetColumnNo("availbyhour")].ToString());
                    availData.InventoryWarehouse.NoAvailabilityCheck = FwConvert.ToBoolean(row[dt.GetColumnNo("noavail")].ToString());

                    availData.Total.Owned = FwConvert.ToDecimal(row[dt.GetColumnNo("ownedqty")].ToString());
                    availData.Total.Consigned = FwConvert.ToDecimal(row[dt.GetColumnNo("consignedqty")].ToString());

                    availData.In.Owned = FwConvert.ToDecimal(row[dt.GetColumnNo("ownedqtyin")].ToString());
                    availData.In.Consigned = FwConvert.ToDecimal(row[dt.GetColumnNo("consignedqtyin")].ToString());

                    availData.Staged.Owned = FwConvert.ToDecimal(row[dt.GetColumnNo("ownedqtystaged")].ToString());
                    availData.Staged.Consigned = FwConvert.ToDecimal(row[dt.GetColumnNo("consignedqtystaged")].ToString());

                    availData.Out.Owned = FwConvert.ToDecimal(row[dt.GetColumnNo("ownedqtyout")].ToString());
                    availData.Out.Consigned = FwConvert.ToDecimal(row[dt.GetColumnNo("consignedqtyout")].ToString());

                    availData.InTransit.Owned = FwConvert.ToDecimal(row[dt.GetColumnNo("ownedqtyintransit")].ToString());
                    availData.InTransit.Consigned = FwConvert.ToDecimal(row[dt.GetColumnNo("consignedqtyintransit")].ToString());

                    availData.InRepair.Owned = FwConvert.ToDecimal(row[dt.GetColumnNo("ownedqtyinrepair")].ToString());
                    availData.InRepair.Consigned = FwConvert.ToDecimal(row[dt.GetColumnNo("consignedqtyinrepair")].ToString());

                    availData.OnTruck.Owned = FwConvert.ToDecimal(row[dt.GetColumnNo("ownedqtyontruck")].ToString());
                    availData.OnTruck.Consigned = FwConvert.ToDecimal(row[dt.GetColumnNo("consignedqtyontruck")].ToString());

                    availData.InContainer.Owned = FwConvert.ToDecimal(row[dt.GetColumnNo("ownedqtyincontainer")].ToString());
                    availData.InContainer.Consigned = FwConvert.ToDecimal(row[dt.GetColumnNo("consignedqtyincontainer")].ToString());

                    availData.QcRequired.Owned = FwConvert.ToDecimal(row[dt.GetColumnNo("ownedqtyqcrequired")].ToString());
                    availData.QcRequired.Consigned = FwConvert.ToDecimal(row[dt.GetColumnNo("consignedqtyqcrequired")].ToString());

                    availData.InventoryWarehouse.LowAvailabilityPercent = FwConvert.ToInt32(row[dt.GetColumnNo("availabilitygrace")].ToString());
                    availData.InventoryWarehouse.LowAvailabilityQuantity = (int)Math.Floor(((double)availData.Total.Total * (((double)availData.InventoryWarehouse.LowAvailabilityPercent) / 100.00)));


                    availCache.AddOrUpdate(availKey, availData, (key, existingValue) =>
                    {
                        existingValue.CloneFrom(availData);
                        return existingValue;
                    });
                }
            }

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("delete t                                                                ");
                qry.Add(" from  tmpsearchsession t                                               ");
                qry.Add("           join master m with (nolock) on (t.masterid = m.masterid)     ");
                qry.Add(" where t.sessionid = @sessionid                                         ");
                qry.Add(" and   m.noavail   = 'T'                                                ");
                qry.AddParameter("@sessionid", sessionId);
                await qry.ExecuteNonQueryAsync();
            }

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                bool hasConsignment = false;  //jh 02/28/2019 place-holder.  will add system-wide option for consignment here
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select a.masterid, a.warehouseid,                                                      ");
                qry.Add("       a.orderid, a.masteritemid, a.availfromdatetime, a.availtodatetime,              ");
                qry.Add("       a.ordertype, a.orderno, a.orderdesc, a.orderstatus, a.dealid, a.deal,           ");
                qry.Add("       a.departmentid, a.department,                                                   ");
                qry.Add("       a.qtyordered, a.qtystagedowned, a.qtyoutowned, a.qtyinowned,                    ");
                qry.Add("       a.subqty, a.qtystagedsub, a.qtyoutsub, a.qtyinsub,                              ");
                if (hasConsignment)
                {
                    //jh 02/28/2019 this is a bottleneck as the query must join in ordertranextended to get the consignorid.  Consider moving consignorid to the ordetran table
                    qry.Add("       a.consignqty, a.qtystagedconsigned, a.qtyoutconsigned, a.qtyinconsigned ");
                }
                else
                {
                    qry.Add("       consignqty = 0, qtystagedconsigned = 0, qtyoutconsigned = 0, qtyinconsigned = 0 ");
                }
                qry.Add(" from  availabilityitemview a with (nolock)                                             ");
                qry.Add("             join tmpsearchsession t with (nolock) on (a.masterid    = t.masterid and   ");
                qry.Add("                                                       a.warehouseid = t.warehouseid)   ");
                qry.Add(" where t.sessionid = @sessionid                                                         ");
                qry.Add(" and   a.rectype in ('R', 'S')                                                          ");
                qry.Add(" and   (a.ordertype in ('O', 'T', 'R')                                                  ");
                qry.Add("          or                                                                            ");
                qry.Add("        ((a.ordertype = 'Q') and (a.orderstatus = 'RESERVED')))                         ");
                qry.AddParameter("@sessionid", sessionId);
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                // load dt into availData.Reservations
                foreach (List<object> row in dt.Rows)
                {
                    string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                    string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
                    TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);

                    TInventoryWarehouseAvailability availData = null;
                    if (availCache.TryGetValue(availKey, out availData))
                    {
                        TInventoryWarehouseAvailabilityReservation reservation = new TInventoryWarehouseAvailabilityReservation();
                        reservation.OrderId = row[dt.GetColumnNo("orderid")].ToString();
                        reservation.OrderItemId = row[dt.GetColumnNo("masteritemid")].ToString();
                        reservation.OrderType = row[dt.GetColumnNo("ordertype")].ToString();
                        reservation.OrderNumber = row[dt.GetColumnNo("orderno")].ToString();
                        reservation.OrderDescription = row[dt.GetColumnNo("orderdesc")].ToString();
                        reservation.OrderStatus = row[dt.GetColumnNo("orderstatus")].ToString();
                        reservation.DepartmentId = row[dt.GetColumnNo("departmentid")].ToString();
                        reservation.Department = row[dt.GetColumnNo("department")].ToString();
                        reservation.DealId = row[dt.GetColumnNo("dealid")].ToString();
                        reservation.Deal = row[dt.GetColumnNo("deal")].ToString();
                        reservation.FromDateTime = FwConvert.ToDateTime(row[dt.GetColumnNo("availfromdatetime")].ToString());
                        reservation.ToDateTime = FwConvert.ToDateTime(row[dt.GetColumnNo("availtodatetime")].ToString());

                        if (!availData.InventoryWarehouse.HourlyAvailability)
                        {
                            reservation.FromDateTime = reservation.FromDateTime.Date;
                            reservation.ToDateTime = (reservation.ToDateTime.Equals(reservation.ToDateTime.Date) ? reservation.ToDateTime.Date : reservation.ToDateTime.Date.AddDays(1));
                        }

                        reservation.QuantityOrdered = FwConvert.ToDecimal(row[dt.GetColumnNo("qtyordered")].ToString());
                        reservation.QuantitySub = FwConvert.ToDecimal(row[dt.GetColumnNo("subqty")].ToString());
                        reservation.QuantityConsigned = FwConvert.ToDecimal(row[dt.GetColumnNo("consignqty")].ToString());

                        TInventoryWarehouseAvailabilityQuantity reservationStaged = new TInventoryWarehouseAvailabilityQuantity();
                        TInventoryWarehouseAvailabilityQuantity reservationOut = new TInventoryWarehouseAvailabilityQuantity();
                        TInventoryWarehouseAvailabilityQuantity reservationIn = new TInventoryWarehouseAvailabilityQuantity();

                        reservationStaged.Owned = FwConvert.ToDecimal(row[dt.GetColumnNo("qtystagedowned")].ToString());
                        reservationStaged.Subbed = FwConvert.ToDecimal(row[dt.GetColumnNo("qtystagedsub")].ToString());
                        reservationStaged.Consigned = FwConvert.ToDecimal(row[dt.GetColumnNo("qtystagedconsigned")].ToString());

                        reservationOut.Owned = FwConvert.ToDecimal(row[dt.GetColumnNo("qtyoutowned")].ToString());
                        reservationOut.Subbed = FwConvert.ToDecimal(row[dt.GetColumnNo("qtyoutsub")].ToString());
                        reservationOut.Consigned = FwConvert.ToDecimal(row[dt.GetColumnNo("qtyoutconsigned")].ToString());

                        reservationIn.Owned = FwConvert.ToDecimal(row[dt.GetColumnNo("qtyinowned")].ToString());
                        reservationIn.Subbed = FwConvert.ToDecimal(row[dt.GetColumnNo("qtyinsub")].ToString());
                        reservationIn.Consigned = FwConvert.ToDecimal(row[dt.GetColumnNo("qtyinconsigned")].ToString());

                        reservation.QuantityStaged = reservationStaged;
                        reservation.QuantityOut = reservationOut;
                        reservation.QuantityIn = reservationIn;

                        if (reservation.OrderType.Equals(RwConstants.ORDER_TYPE_ORDER) || reservation.OrderType.Equals(RwConstants.ORDER_TYPE_TRANSFER) || (reservation.OrderType.Equals(RwConstants.ORDER_TYPE_QUOTE) && (reservation.OrderStatus.Equals(RwConstants.QUOTE_STATUS_RESERVED))))
                        {
                            reservation.QuantityReserved.Owned = (reservation.QuantityOrdered - reservation.QuantitySub - reservation.QuantityConsigned - reservation.QuantityStaged.Owned - reservation.QuantityOut.Owned - reservation.QuantityIn.Owned);
                            reservation.QuantityReserved.Consigned = (reservation.QuantityConsigned - reservation.QuantityOut.Consigned - reservation.QuantityIn.Consigned);
                        }

                        availData.Reservations.Add(reservation);
                    }
                }
            }
            //#jhtodo copy the loop above for Completes and Kits, joining on parentid.  This will give a list of reservations that reference these packages
            //qry.Add("             join tmpsearchsession t on (a.parentid = t.masterid and a.warehouseid = t.warehouseid)");

            return availCache;
        }
        //-------------------------------------------------------------------------------------------------------
        private static void CalculateFutureAvailability(ref TAvailabilityCache availCache)
        {
            foreach (KeyValuePair<TInventoryWarehouseAvailabilityKey, TInventoryWarehouseAvailability> availEntry in availCache)
            {
                TInventoryWarehouseAvailabilityKey availKey = availEntry.Key;
                TInventoryWarehouseAvailability availData = availEntry.Value;
                DateTime fromDateTime = DateTime.Today;
                DateTime toDateTime = availData.AvailDataToDateTime;
                availData.AvailabilityDatesAndTimes.Clear();
                if (!availData.InventoryWarehouse.NoAvailabilityCheck)
                {
                    if (AppFunc.InventoryClassIsPackage(availData.InventoryWarehouse.Classification))
                    {
                        DateTime theDateTime = fromDateTime;
                        while (theDateTime <= toDateTime)
                        {
                            TInventoryWarehouseAvailabilityQuantity packageAvailable = new TInventoryWarehouseAvailabilityQuantity();
                            bool initialPackageAvailableDetermined = false;
                            bool accessoryAvailabilityDataExists = true;

                            foreach (TPackageAccessory accessory in availData.InventoryWarehouse.Accessories)
                            {
                                if (accessory.DefaultQuantity != 0) // avoid divide-by-zero error.  value should not be zero anyway
                                {
                                    TInventoryWarehouseAvailabilityKey accKey = new TInventoryWarehouseAvailabilityKey(accessory.InventoryId, availKey.WarehouseId);
                                    TInventoryWarehouseAvailability accAvailCache = null;
                                    if (AvailabilityCache.TryGetValue(accKey, out accAvailCache))
                                    {
                                        TInventoryWarehouseAvailabilityDateTime accAvailableDateTime = null;
                                        if (accAvailCache.AvailabilityDatesAndTimes.TryGetValue(theDateTime, out accAvailableDateTime))
                                        {
                                            TInventoryWarehouseAvailabilityQuantity accAvailable = accAvailableDateTime.Available;
                                            if (initialPackageAvailableDetermined)
                                            {
                                                //Compare with available calculation based on other accessories already looked at
                                                packageAvailable.Owned = Math.Min(packageAvailable.Owned, accAvailable.Owned / accessory.DefaultQuantity);
                                                packageAvailable.Consigned = Math.Min(packageAvailable.Consigned, (accAvailable.Consigned / accessory.DefaultQuantity));
                                            }
                                            else
                                            {
                                                packageAvailable.Owned = (accAvailable.Owned / accessory.DefaultQuantity);
                                                packageAvailable.Consigned = (accAvailable.Consigned / accessory.DefaultQuantity);
                                            }
                                            initialPackageAvailableDetermined = true;
                                        }
                                        else
                                        {
                                            // accessory availabiltiy data not found in cache
                                            accessoryAvailabilityDataExists = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        // accessory availabiltiy data not found in cache
                                        accessoryAvailabilityDataExists = false;
                                        break;
                                    }
                                }
                            }

                            //round the package available numbers to the next whole number (important when spare accessories are used)
                            packageAvailable.Owned = Math.Floor(packageAvailable.Owned);
                            packageAvailable.Consigned = Math.Floor(packageAvailable.Consigned);

                            if (accessoryAvailabilityDataExists)
                            {
                                TInventoryWarehouseAvailabilityDateTime inventoryWarehouseAvailabilityDateTime = new TInventoryWarehouseAvailabilityDateTime(theDateTime);
                                inventoryWarehouseAvailabilityDateTime.Available = packageAvailable;
                                availData.AvailabilityDatesAndTimes.TryAdd(theDateTime, inventoryWarehouseAvailabilityDateTime);
                                if (availData.InventoryWarehouse.HourlyAvailability)
                                {
                                    theDateTime = theDateTime.AddHours(1);
                                }
                                else
                                {
                                    theDateTime = theDateTime.AddDays(1);
                                }
                            }
                            else
                            {
                                // availabiltiy data not found in cache for at least one accessory for at least one date in the range
                                break;
                                // exit the date while loop.  leave package availability data incomplete
                            }
                        }
                    }
                    else  // item/accesssory
                    {
                        TInventoryWarehouseAvailabilityQuantity available = new TInventoryWarehouseAvailabilityQuantity();
                        available.CloneFrom(availData.In);   // snapshot the current IN quantity.  use this as a running total
                        TInventoryWarehouseAvailabilityQuantity late = new TInventoryWarehouseAvailabilityQuantity();

                        // use the availData.Reservations to calculate future availability for this Icode
                        DateTime theDateTime = fromDateTime;
                        while (theDateTime <= toDateTime)
                        {
                            TInventoryWarehouseAvailabilityDateTime inventoryWarehouseAvailabilityDateTime = new TInventoryWarehouseAvailabilityDateTime(theDateTime);

                            foreach (TInventoryWarehouseAvailabilityReservation reservation in availData.Reservations)
                            {
                                if ((reservation.FromDateTime <= theDateTime) && (theDateTime <= reservation.ToDateTime))
                                {
                                    inventoryWarehouseAvailabilityDateTime.Reserved += reservation.QuantityReserved;
                                    if (!reservation.countedReserved)
                                    {
                                        available -= reservation.QuantityReserved;
                                    }
                                    reservation.countedReserved = true;
                                }

                                if (availData.InventoryWarehouse.AvailableFor.Equals(RwConstants.INVENTORY_AVAILABLE_FOR_RENT))
                                {
                                    if ((theDateTime == fromDateTime) && (reservation.ToDateTime == LateDateTime))  // items are late
                                    {
                                        if (!reservation.countedLate)
                                        {
                                            reservation.QuantityLate = reservation.QuantityStaged + reservation.QuantityOut;
                                            late += reservation.QuantityLate;
                                        }
                                        reservation.countedLate = true;
                                    }
                                }

                                if (reservation.ToDateTime == theDateTime)
                                {
                                    inventoryWarehouseAvailabilityDateTime.Returning += reservation.QuantityReserved + reservation.QuantityStaged + reservation.QuantityOut;
                                }

                                if ((available.Total < 0) && (reservation.QuantityReserved.Total > 0))
                                {
                                    reservation.IsNegativeConflict = true;
                                    availData.HasNegativeConflict = true;
                                }
                                else if ((reservation.QuantitySub > 0) && (available.Total >= reservation.QuantitySub))
                                {
                                    reservation.IsPositiveConflict = true;
                                    availData.HasPositiveConflict = true;
                                }
                            }

                            inventoryWarehouseAvailabilityDateTime.Available = available;
                            available += inventoryWarehouseAvailabilityDateTime.Returning;  // the amount returning in this date/hour slot will become available for the next date/hour slot

                            availData.AvailabilityDatesAndTimes.TryAdd(theDateTime, inventoryWarehouseAvailabilityDateTime);

                            if (availData.InventoryWarehouse.HourlyAvailability)
                            {
                                theDateTime = theDateTime.AddHours(1);
                            }
                            else
                            {
                                theDateTime = theDateTime.AddDays(1);
                            }
                        }
                        availData.Late = late;
                    }
                    availData.CalculatedDateTime = DateTime.Now;
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<bool> RefreshAvailability(FwApplicationConfig appConfig, FwUserSession userSession, TInventoryWarehouseAvailabilityRequestItems availRequestItems)
        {
            bool success = true;
            if (availRequestItems.Count > 0)
            {
                TAvailabilityCache availCache = await BuildAvailabilityCache(appConfig, userSession, availRequestItems);
                CalculateFutureAvailability(ref availCache);
                foreach (TInventoryWarehouseAvailabilityKey availKey in availCache.Keys)
                {
                    AvailabilityCache.AddOrUpdate(availKey, availCache[availKey], (key, existingValue) =>
                    {
                        existingValue.CloneFrom(availCache[availKey]);
                        return existingValue;
                    });
                }
            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> KeepAvailabilityCacheFresh(FwApplicationConfig appConfig)
        {
            const int AVAILABILITY_REQUEST_BATCH_SIZE = 5000;
            bool success = true;
            //Console.WriteLine("keeping availability fresh");
            DateTime fromDate = DateTime.Today;
            DateTime availabilityThroughDate = DateTime.Today.AddDays(AVAILABILITY_DAYS_TO_CACHE);

            // initialize an empty request
            TInventoryWarehouseAvailabilityRequestItems availRequestItems = new TInventoryWarehouseAvailabilityRequestItems();

            // build up a list of Items and Accessories only from the global AvailabilityNeedRecalc list
            //Console.WriteLine("checking AvailabilityNeedRecalc");
            TAvailabilityNeedRecalcDictionary availNeedRecalcItem = new TAvailabilityNeedRecalcDictionary();
            TAvailabilityNeedRecalcDictionary availNeedRecalcPackage = new TAvailabilityNeedRecalcDictionary();

            lock (AvailabilityNeedRecalc)  // no external adds or deletes of the global AvailabilityNeedRecalc dictonary during this scope. We are emptying the entire dictionary to a local copy
            {
                //Console.WriteLine("locked AvailabilityNeedRecalc");
                if (AvailabilityNeedRecalc.IsEmpty)
                {
                    //Console.WriteLine("no records in AvailabilityNeedRecalc");
                }
                else
                {
                    //Console.WriteLine("pulling " + AvailabilityNeedRecalc.Count.ToString() + " records from AvailabilityNeedRecalc");
                    foreach (KeyValuePair<TInventoryWarehouseAvailabilityKey, string> anc in AvailabilityNeedRecalc)
                    {
                        if (AppFunc.InventoryClassIsPackage(anc.Value))
                        {
                            availNeedRecalcPackage.AddOrUpdate(anc.Key, anc.Value, (key, existingValue) =>
                            {
                                existingValue = anc.Value;
                                return existingValue;
                            });
                        }
                        else
                        {
                            availNeedRecalcItem.AddOrUpdate(anc.Key, anc.Value, (key, existingValue) =>
                            {
                                existingValue = anc.Value;
                                return existingValue;
                            });
                        }
                    }
                    AvailabilityNeedRecalc.Clear();
                }
                //Console.WriteLine("unlocking AvailabilityNeedRecalc");
            }

            if (availNeedRecalcItem.Count > 0)
            {
                AvailabilityKeepFreshLogLogic log = new AvailabilityKeepFreshLogLogic();
                log.SetDependencies(appConfig, null);
                log.StartDateTime = DateTime.Now;
                //log.EndDateTime = DateTime.MaxValue;
                log.BatchSize = availNeedRecalcItem.Count;
                int x = await log.SaveAsync(null);

                // loop through this local list of Items and Accessories in batches
                //Console.WriteLine(availNeedRecalcItem.Count.ToString().PadLeft(7) + " master/warehouse item/accessory records need recalc");
                while (availNeedRecalcItem.Count > 0)
                {
                    // build up a request containing all known items needing recalc
                    availRequestItems.Clear();
                    foreach (KeyValuePair<TInventoryWarehouseAvailabilityKey, string> anc in availNeedRecalcItem)
                    {
                        availRequestItems.Add(new TInventoryWarehouseAvailabilityRequestItem(anc.Key.InventoryId, anc.Key.WarehouseId, fromDate, availabilityThroughDate));
                        if (availRequestItems.Count >= AVAILABILITY_REQUEST_BATCH_SIZE)
                        {
                            break; // break out of this foreach loop
                        }
                    }

                    foreach (TInventoryWarehouseAvailabilityRequestItem availRequestItem in availRequestItems)
                    {
                        string classification = "";
                        availNeedRecalcItem.TryRemove(new TInventoryWarehouseAvailabilityKey(availRequestItem.InventoryId, availRequestItem.WarehouseId), out classification);
                    }

                    // update the global cache of availability data
                    await GetAvailability(appConfig, null, availRequestItems, true);

                    //Console.WriteLine(availNeedRecalcItem.Count.ToString().PadLeft(7) + " master/warehouse item/accessory records need recalc");
                }

                // loop through this local list of Completes and Kits in batches
                //Console.WriteLine(availNeedRecalcPackage.Count.ToString().PadLeft(7) + " master/warehouse complete/kit records need recalc");
                while (availNeedRecalcPackage.Count > 0)
                {
                    // build up a request containing all known items needing recalc
                    availRequestItems.Clear();
                    foreach (KeyValuePair<TInventoryWarehouseAvailabilityKey, string> anc in availNeedRecalcPackage)
                    {
                        availRequestItems.Add(new TInventoryWarehouseAvailabilityRequestItem(anc.Key.InventoryId, anc.Key.WarehouseId, fromDate, availabilityThroughDate));
                        if (availRequestItems.Count >= AVAILABILITY_REQUEST_BATCH_SIZE)
                        {
                            break; // break out of this foreach loop
                        }
                    }

                    foreach (TInventoryWarehouseAvailabilityRequestItem availRequestItem in availRequestItems)
                    {
                        string classification = "";
                        availNeedRecalcPackage.TryRemove(new TInventoryWarehouseAvailabilityKey(availRequestItem.InventoryId, availRequestItem.WarehouseId), out classification);
                    }

                    // update the static cache of availability data
                    await GetAvailability(appConfig, null, availRequestItems, true);

                    //Console.WriteLine(availNeedRecalcPackage.Count.ToString().PadLeft(7) + " master/warehouse complete/kit records need recalc");
                }

                log.EndDateTime = DateTime.Now;
                x = await log.SaveAsync(null);
            }


            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TAvailabilityCache> GetAvailability(FwApplicationConfig appConfig, FwUserSession userSession, TInventoryWarehouseAvailabilityRequestItems availRequestItems, bool refreshIfNeeded = false, bool forceRefresh = false)
        {
            TAvailabilityCache availCache = new TAvailabilityCache();
            TInventoryWarehouseAvailabilityRequestItems availRequestToRefresh = new TInventoryWarehouseAvailabilityRequestItems();

            foreach (TInventoryWarehouseAvailabilityRequestItem availRequestItem in availRequestItems)
            {
                if ((!availRequestItem.InventoryId.Equals("undefined")) && (!availRequestItem.WarehouseId.Equals("undefined")))
                {
                    if (forceRefresh)
                    {
                        availRequestToRefresh.Add(availRequestItem);
                    }
                    else
                    {
                        bool foundInCache = false;
                        bool stale = false;
                        DateTime fromDateTime = availRequestItem.FromDateTime;
                        DateTime toDateTime = availRequestItem.ToDateTime;
                        TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(availRequestItem.InventoryId, availRequestItem.WarehouseId);

                        if (InventoryWarehouseNeedsRecalc(availKey))
                        {
                            stale = true;
                        }

                        if (fromDateTime < DateTime.Today)
                        {
                            fromDateTime = DateTime.Today;
                        }

                        if (toDateTime < DateTime.Today)
                        {
                            toDateTime = DateTime.Today;
                        }

                        TInventoryWarehouseAvailability availData = null;
                        if (AvailabilityCache.TryGetValue(availKey, out availData))
                        {
                            TInventoryWarehouseAvailability tmpAvailData = new TInventoryWarehouseAvailability(availKey.InventoryId, availKey.WarehouseId);
                            tmpAvailData.CloneFrom(availData);
                            foundInCache = true;
                            DateTime theDateTime = tmpAvailData.AvailDataFromDateTime;
                            while (theDateTime <= tmpAvailData.AvailDataToDateTime)
                            {
                                if ((theDateTime < fromDateTime) || (toDateTime < theDateTime))
                                {
                                    TInventoryWarehouseAvailabilityDateTime availDateTime = null;
                                    tmpAvailData.AvailabilityDatesAndTimes.TryRemove(theDateTime, out availDateTime);
                                }

                                if (tmpAvailData.InventoryWarehouse.HourlyAvailability)
                                {
                                    theDateTime = theDateTime.AddHours(1);
                                }
                                else
                                {
                                    theDateTime = theDateTime.AddDays(1);
                                }


                            }

                            theDateTime = fromDateTime;
                            while (theDateTime <= toDateTime)
                            {
                                if (!tmpAvailData.AvailabilityDatesAndTimes.ContainsKey(theDateTime))
                                {
                                    foundInCache = false;
                                    break;
                                }
                                if (tmpAvailData.InventoryWarehouse.HourlyAvailability)
                                {
                                    theDateTime = theDateTime.AddHours(1);
                                }
                                else
                                {
                                    theDateTime = theDateTime.AddDays(1);
                                }
                            }
                            availCache[availKey] = tmpAvailData;
                        }

                        if ((stale) || (!foundInCache))
                        {
                            if (refreshIfNeeded)
                            {
                                availRequestToRefresh.Add(availRequestItem);
                            }
                        }
                    }
                }
            }

            if (availRequestToRefresh.Count > 0)
            {
                await RefreshAvailability(appConfig, userSession, availRequestToRefresh);
                availCache = await GetAvailability(appConfig, userSession, availRequestItems, false);   // this is a recursive call to grab the cache of all items again: originals and refreshes
            }

            return availCache;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TInventoryWarehouseAvailability> GetAvailability(FwApplicationConfig appConfig, FwUserSession userSession, string inventoryId, string warehouseId, DateTime fromDate, DateTime toDate, bool refreshIfNeeded = false, bool forceRefresh = false)

        {
            TInventoryWarehouseAvailability availData = null;

            if ((!inventoryId.Equals("undefined")) && (!warehouseId.Equals("undefined")))
            {
                availData = new TInventoryWarehouseAvailability(inventoryId, warehouseId);

                TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);
                TInventoryWarehouseAvailabilityRequestItems availRequestItems = new TInventoryWarehouseAvailabilityRequestItems();
                availRequestItems.Add(new TInventoryWarehouseAvailabilityRequestItem(inventoryId, warehouseId, fromDate, toDate));

                TAvailabilityCache availCache = await GetAvailability(appConfig, userSession, availRequestItems, refreshIfNeeded, forceRefresh);
                availCache.TryGetValue(availKey, out availData);
            }

            return availData;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TInventoryAvailabilityCalendarAndScheduleResponse> GetCalendarAndScheduleData(FwApplicationConfig appConfig, FwUserSession userSession, string InventoryId, string WarehouseId, DateTime FromDate, DateTime ToDate)

        {
            TInventoryAvailabilityCalendarAndScheduleResponse response = new TInventoryAvailabilityCalendarAndScheduleResponse();

            if (FromDate < DateTime.Today)
            {
                FromDate = DateTime.Today;
            }

            if (ToDate < DateTime.Today)
            {
                ToDate = DateTime.Today;
            }

            TInventoryWarehouseAvailability availData = await GetAvailability(appConfig, userSession, InventoryId, WarehouseId, FromDate, ToDate, true, forceRefresh: true);

            if (availData != null)
            {
                response.InventoryData = availData;
                int eventId = 0;

                // build up the calendar events
                //currently hard-coded for "daily" availability.  will need mods to work for "hourly"  //#jhtodo
                DateTime theDate = FromDate;
                while (theDate <= ToDate)
                {
                    DateTime startDateTime = theDate;
                    DateTime endDateTime = theDate;
                    bool isToday = startDateTime.Equals(DateTime.Today);
                    startDateTime = startDateTime.AddMinutes(1);
                    endDateTime = endDateTime.AddDays(1).AddMinutes(-1);

                    if (availData.InventoryWarehouse.NoAvailabilityCheck)
                    {
                        //no availability check
                        eventId++;
                        TInventoryAvailabilityCalendarEvent iAvail = new TInventoryAvailabilityCalendarEvent();
                        iAvail.id = eventId.ToString();
                        iAvail.InventoryId = InventoryId;
                        iAvail.WarehouseId = WarehouseId;
                        iAvail.start = startDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                        iAvail.end = endDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");
                        iAvail.text = RwConstants.NO_AVAILABILITY_CAPTION;
                        iAvail.backColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_NO_AVAILABILITY);
                        iAvail.textColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_POSITIVE);
                        response.InventoryAvailabilityCalendarEvents.Add(iAvail);
                    }
                    else
                    {
                        TInventoryWarehouseAvailabilityDateTime inventoryWarehouseAvailabilityDateTime = null;
                        if (availData.AvailabilityDatesAndTimes.TryGetValue(theDate, out inventoryWarehouseAvailabilityDateTime))
                        {


                            //late
                            if ((isToday) && (availData.Late.Total != 0))
                            {
                                eventId++;
                                TInventoryAvailabilityCalendarEvent iLate = new TInventoryAvailabilityCalendarEvent();
                                iLate.id = eventId.ToString();
                                iLate.InventoryId = InventoryId;
                                iLate.WarehouseId = WarehouseId;
                                iLate.start = startDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                                iLate.end = endDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");
                                iLate.text = "Late " + ((int)availData.Late.Total).ToString();
                                iLate.backColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_LATE);
                                iLate.textColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_LATE);
                                response.InventoryAvailabilityCalendarEvents.Add(iLate);
                            }

                            //available
                            eventId++;
                            TInventoryAvailabilityCalendarEvent iAvail = new TInventoryAvailabilityCalendarEvent();
                            iAvail.id = eventId.ToString();
                            iAvail.InventoryId = InventoryId;
                            iAvail.WarehouseId = WarehouseId;
                            iAvail.start = startDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                            iAvail.end = endDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");
                            iAvail.text = "Available " + ((int)inventoryWarehouseAvailabilityDateTime.Available.Total).ToString();
                            if (inventoryWarehouseAvailabilityDateTime.Available.Total < 0)
                            {
                                iAvail.backColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_NEGATIVE);
                                iAvail.textColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_NEGATIVE);
                            }
                            else if ((inventoryWarehouseAvailabilityDateTime.Available.Total > 0) && (availData.InventoryWarehouse.LowAvailabilityPercent > 0) && (inventoryWarehouseAvailabilityDateTime.Available.Total <= availData.InventoryWarehouse.LowAvailabilityQuantity))
                            {
                                iAvail.backColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_LOW);
                                iAvail.textColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_LOW);
                            }
                            else
                            {
                                iAvail.backColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_POSITIVE);
                                iAvail.textColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_POSITIVE);
                            }
                            response.InventoryAvailabilityCalendarEvents.Add(iAvail);

                            //reserved
                            if (inventoryWarehouseAvailabilityDateTime.Reserved.Total != 0)
                            {
                                eventId++;
                                TInventoryAvailabilityCalendarEvent iReserve = new TInventoryAvailabilityCalendarEvent();
                                iReserve.id = eventId.ToString();
                                iReserve.InventoryId = InventoryId;
                                iReserve.WarehouseId = WarehouseId;
                                iReserve.start = startDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                                iReserve.end = endDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");
                                iReserve.text = "Reserved " + ((int)inventoryWarehouseAvailabilityDateTime.Reserved.Total).ToString();
                                iReserve.backColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_RESERVED);
                                iReserve.textColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_RESERVED);
                                response.InventoryAvailabilityCalendarEvents.Add(iReserve);
                            }

                            //returning
                            if (inventoryWarehouseAvailabilityDateTime.Returning.Total != 0)
                            {
                                eventId++;
                                TInventoryAvailabilityCalendarEvent iReturn = new TInventoryAvailabilityCalendarEvent();
                                iReturn.id = eventId.ToString();
                                iReturn.InventoryId = InventoryId;
                                iReturn.WarehouseId = WarehouseId;
                                iReturn.start = startDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                                iReturn.end = endDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");
                                iReturn.text = "Returning " + ((int)inventoryWarehouseAvailabilityDateTime.Returning.Total).ToString();
                                iReturn.backColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_RETURNING);
                                iReturn.textColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_RETURNING);
                                response.InventoryAvailabilityCalendarEvents.Add(iReturn);
                            }
                        }
                    }

                    theDate = theDate.AddDays(1); // daily #jhtodo
                }

                // build up the top-line schedule events (available quantity)
                int resourceId = 0;

                resourceId++;
                TInventoryAvailabilityScheduleResource availResource = new TInventoryAvailabilityScheduleResource();
                availResource.id = resourceId.ToString();
                availResource.name = "Available";
                response.InventoryAvailabilityScheduleResources.Add(availResource);

                if (availData.InventoryWarehouse.NoAvailabilityCheck)
                {

                    eventId++;
                    TInventoryAvailabilityScheduleEvent availEvent = new TInventoryAvailabilityScheduleEvent();
                    availEvent.id = eventId.ToString(); ;
                    availEvent.resource = resourceId.ToString();
                    availEvent.InventoryId = InventoryId;
                    availEvent.WarehouseId = WarehouseId;
                    availEvent.start = FromDate.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                    availEvent.end = ToDate.ToString("yyyy-MM-ddTHH:mm:ss tt");
                    availEvent.text = RwConstants.NO_AVAILABILITY_CAPTION;
                    availEvent.backColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_NO_AVAILABILITY);
                    availEvent.textColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_POSITIVE);
                    response.InventoryAvailabilityScheduleEvents.Add(availEvent);
                }
                else
                {
                    theDate = FromDate;
                    while (theDate <= ToDate)
                    {
                        TInventoryWarehouseAvailabilityDateTime inventoryWarehouseAvailabilityDateTime = null;
                        if (availData.AvailabilityDatesAndTimes.TryGetValue(theDate, out inventoryWarehouseAvailabilityDateTime))
                        {
                            eventId++;
                            TInventoryAvailabilityScheduleEvent availEvent = new TInventoryAvailabilityScheduleEvent();
                            availEvent.id = eventId.ToString(); ;
                            availEvent.resource = resourceId.ToString();
                            availEvent.InventoryId = InventoryId;
                            availEvent.WarehouseId = WarehouseId;
                            availEvent.start = theDate.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                            availEvent.end = theDate.ToString("yyyy-MM-ddTHH:mm:ss tt");
                            availEvent.text = ((int)inventoryWarehouseAvailabilityDateTime.Available.Total).ToString();
                            if (inventoryWarehouseAvailabilityDateTime.Available.Total < 0)
                            {
                                availEvent.backColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_NEGATIVE);
                                availEvent.textColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_NEGATIVE);
                            }
                            else if ((inventoryWarehouseAvailabilityDateTime.Available.Total > 0) && (availData.InventoryWarehouse.LowAvailabilityPercent > 0) && (inventoryWarehouseAvailabilityDateTime.Available.Total <= availData.InventoryWarehouse.LowAvailabilityQuantity))
                            {
                                availEvent.backColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_LOW);
                                availEvent.textColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_LOW);
                            }
                            else
                            {
                                availEvent.backColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_POSITIVE);
                                availEvent.textColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_POSITIVE);
                            }
                            response.InventoryAvailabilityScheduleEvents.Add(availEvent);
                        }
                        theDate = theDate.AddDays(1); //daily availability
                    }
                }

                // build up the schedule resources and events
                eventId = 0;

                foreach (TInventoryWarehouseAvailabilityReservation reservation in availData.Reservations)
                {
                    //reserved
                    if (reservation.QuantityReserved.Total != 0)
                    {
                        if ((reservation.FromDateTime <= ToDate) && (reservation.ToDateTime >= FromDate))
                        {
                            resourceId++;
                            TInventoryAvailabilityScheduleResource resource = new TInventoryAvailabilityScheduleResource();
                            resource.id = resourceId.ToString();
                            resource.name = reservation.OrderNumber;
                            response.InventoryAvailabilityScheduleResources.Add(resource);

                            DateTime reservationFromDateTime = reservation.FromDateTime;
                            DateTime reservationToDateTime = reservation.ToDateTime;

                            if (reservationToDateTime.Hour.Equals(0) && reservationToDateTime.Minute.Equals(0) && reservationToDateTime.Second.Equals(0))
                            {
                                reservationToDateTime = reservationToDateTime.AddDays(1).AddSeconds(-1);
                            }

                            eventId++;
                            TInventoryAvailabilityScheduleEvent availScheduleEvent = new TInventoryAvailabilityScheduleEvent();
                            availScheduleEvent.id = eventId.ToString();
                            availScheduleEvent.resource = resourceId.ToString();
                            availScheduleEvent.start = reservationFromDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                            availScheduleEvent.end = reservationToDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");
                            availScheduleEvent.text = ((int)reservation.QuantityReserved.Total).ToString() + " " + reservation.OrderNumber + " " + reservation.OrderDescription + " (" + reservation.Deal + ")";
                            availScheduleEvent.orderNumber = reservation.OrderNumber;
                            availScheduleEvent.orderStatus = reservation.OrderStatus;
                            availScheduleEvent.deal = reservation.Deal;
                            availScheduleEvent.barColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_RESERVED);
                            availScheduleEvent.textColor = FwConvert.OleColorToHtmlColor(0); //black 
                            response.InventoryAvailabilityScheduleEvents.Add(availScheduleEvent);
                        }
                    }

                    //staged
                    if (reservation.QuantityStaged.Total != 0)
                    {
                        if ((reservation.FromDateTime <= ToDate) && (reservation.ToDateTime >= FromDate))
                        {
                            resourceId++;
                            TInventoryAvailabilityScheduleResource resource = new TInventoryAvailabilityScheduleResource();
                            resource.id = resourceId.ToString();
                            resource.name = reservation.OrderNumber;
                            response.InventoryAvailabilityScheduleResources.Add(resource);

                            DateTime reservationFromDateTime = reservation.FromDateTime;
                            DateTime reservationToDateTime = reservation.ToDateTime;

                            if (reservationToDateTime.Hour.Equals(0) && reservationToDateTime.Minute.Equals(0) && reservationToDateTime.Second.Equals(0))
                            {
                                reservationToDateTime = reservationToDateTime.AddDays(1).AddSeconds(-1);
                            }

                            eventId++;
                            TInventoryAvailabilityScheduleEvent availScheduleEvent = new TInventoryAvailabilityScheduleEvent();
                            availScheduleEvent.id = eventId.ToString();
                            availScheduleEvent.resource = resourceId.ToString();
                            availScheduleEvent.start = reservationFromDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                            availScheduleEvent.end = reservationToDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");
                            availScheduleEvent.text = ((int)reservation.QuantityStaged.Total).ToString() + " " + reservation.OrderNumber + " " + reservation.OrderDescription + " (" + reservation.Deal + ")";
                            availScheduleEvent.orderNumber = reservation.OrderNumber;
                            availScheduleEvent.orderStatus = reservation.OrderStatus;
                            availScheduleEvent.deal = reservation.Deal;
                            availScheduleEvent.barColor = RwGlobals.STAGED_COLOR;
                            availScheduleEvent.textColor = FwConvert.OleColorToHtmlColor(0); //black 
                            response.InventoryAvailabilityScheduleEvents.Add(availScheduleEvent);
                        }
                    }

                    //out
                    if (reservation.QuantityOut.Total != 0)
                    {
                        if ((reservation.FromDateTime <= ToDate) && (reservation.ToDateTime >= FromDate))
                        {
                            resourceId++;
                            TInventoryAvailabilityScheduleResource resource = new TInventoryAvailabilityScheduleResource();
                            resource.id = resourceId.ToString();
                            resource.name = reservation.OrderNumber;
                            response.InventoryAvailabilityScheduleResources.Add(resource);

                            DateTime reservationFromDateTime = reservation.FromDateTime;
                            DateTime reservationToDateTime = reservation.ToDateTime;

                            if (reservationToDateTime.Hour.Equals(0) && reservationToDateTime.Minute.Equals(0) && reservationToDateTime.Second.Equals(0))
                            {
                                reservationToDateTime = reservationToDateTime.AddDays(1).AddSeconds(-1);
                            }

                            eventId++;
                            TInventoryAvailabilityScheduleEvent availScheduleEvent = new TInventoryAvailabilityScheduleEvent();
                            availScheduleEvent.id = eventId.ToString();
                            availScheduleEvent.resource = resourceId.ToString();
                            availScheduleEvent.start = reservationFromDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                            availScheduleEvent.end = reservationToDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");
                            availScheduleEvent.text = ((int)reservation.QuantityOut.Total).ToString() + " " + reservation.OrderNumber + " " + reservation.OrderDescription + " (" + reservation.Deal + ")";
                            availScheduleEvent.orderNumber = reservation.OrderNumber;
                            availScheduleEvent.orderStatus = reservation.OrderStatus;
                            availScheduleEvent.deal = reservation.Deal;
                            availScheduleEvent.barColor = RwGlobals.OUT_COLOR;
                            availScheduleEvent.textColor = FwConvert.OleColorToHtmlColor(0); //black 
                            response.InventoryAvailabilityScheduleEvents.Add(availScheduleEvent);
                        }
                    }
                }
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------

        public static async Task<AvailabilityConflictResponse> GetConflicts(FwApplicationConfig appConfig, FwUserSession userSession, AvailabilityConflictRequest request)
        {
            AvailabilityConflictResponse response = new AvailabilityConflictResponse();
            FwJsonDataTable dt = null;

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddColumn("masterid");
                    qry.AddColumn("warehouseid");

                    select.Add("select a.masterid, a.warehouseid                    ");
                    select.Add(" from  availabilitymasterwhview a with (nolock)     ");

                    select.Parse();

                    if ((!string.IsNullOrEmpty(request.AvailableFor)) && (!request.AvailableFor.Equals("ALL")))
                    {
                        select.AddWhere("a.availfor = @availfor");
                        select.AddParameter("@availfor", request.AvailableFor);
                    }

                    select.AddWhereIn("warehouseid", request.WarehouseId);
                    select.AddWhereIn("inventorydepartmentid", request.InventoryTypeId);
                    select.AddWhereIn("categoryid", request.CategoryId);
                    select.AddWhereIn("subcategoryid", request.SubCategoryId);
                    select.AddWhereIn("masterid", request.InventoryId);
                    select.AddWhereIn("rank", request.Ranks);
                    if (!string.IsNullOrEmpty(request.Description))
                    {
                        select.AddWhere("a.master like @description");
                        select.AddParameter("@description", "%" + request.Description.Trim() + "%");
                    }

                    //if (!request.BooleanField.GetValueOrDefault(false)) 
                    //{ 
                    //    select.AddWhere("somefield ^<^> 'T'"); 
                    //} 
                    select.AddOrderBy("warehouse, inventorydepartment, category, subcategory, masterno, master");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }

            }

            DateTime fromDateTime = DateTime.Today;
            DateTime toDateTime = request.ToDate ?? fromDateTime.AddDays(AVAILABILITY_DAYS_TO_CACHE);
            TInventoryWarehouseAvailabilityRequestItems availRequestItems = new TInventoryWarehouseAvailabilityRequestItems();
            foreach (List<object> row in dt.Rows)
            {
                string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
                availRequestItems.Add(new TInventoryWarehouseAvailabilityRequestItem(inventoryId, warehouseId, fromDateTime, toDateTime));
            }
            bool refreshIfNeeded = true; // user may want to make this true/false in some cases
            TAvailabilityCache availCache = InventoryAvailabilityFunc.GetAvailability(appConfig, userSession, availRequestItems, refreshIfNeeded).Result;


            foreach (List<object> row in dt.Rows)
            {
                string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();

                TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);

                TInventoryWarehouseAvailability availData = null;
                if (availCache.TryGetValue(availKey, out availData))
                {
                    bool hasConflict = (((string.IsNullOrEmpty(request.ConflictType) || (request.ConflictType.Equals("ALL"))) && (availData.HasNegativeConflict || availData.HasPositiveConflict)) ||
                                       (!string.IsNullOrEmpty(request.ConflictType) && request.ConflictType.Equals(RwConstants.INVENTORY_CONFLICT_TYPE_NEGATIVE) && availData.HasNegativeConflict) ||
                                       (!string.IsNullOrEmpty(request.ConflictType) && request.ConflictType.Equals(RwConstants.INVENTORY_CONFLICT_TYPE_POSITIVE) && availData.HasPositiveConflict));

                    if (hasConflict)
                    {
                        foreach (TInventoryWarehouseAvailabilityReservation reservation in availData.Reservations)
                        {
                            if (reservation.QuantityReserved.Total != 0)
                            {

                                bool isConflict = (((string.IsNullOrEmpty(request.ConflictType) || (request.ConflictType.Equals("ALL"))) && (reservation.IsNegativeConflict || reservation.IsPositiveConflict)) ||
                                                   (!string.IsNullOrEmpty(request.ConflictType) && request.ConflictType.Equals(RwConstants.INVENTORY_CONFLICT_TYPE_NEGATIVE) && reservation.IsNegativeConflict) ||
                                                   (!string.IsNullOrEmpty(request.ConflictType) && request.ConflictType.Equals(RwConstants.INVENTORY_CONFLICT_TYPE_POSITIVE) && reservation.IsPositiveConflict));

                                bool showOrderOrDeal = true;
                                if (showOrderOrDeal)
                                {
                                    if (!string.IsNullOrEmpty(request.OrderId))
                                    {
                                        showOrderOrDeal = reservation.OrderId.Equals(request.OrderId);
                                    }
                                }
                                if (showOrderOrDeal)
                                {
                                    if (!string.IsNullOrEmpty(request.DealId))
                                    {
                                        showOrderOrDeal = reservation.DealId.Equals(request.DealId);
                                    }
                                }

                                if (isConflict && showOrderOrDeal)
                                {
                                    AvailabilityConflictResponseItem responseItem = new AvailabilityConflictResponseItem();
                                    responseItem.Warehouse = availData.InventoryWarehouse.Warehouse;
                                    responseItem.WarehouseCode = availData.InventoryWarehouse.WarehouseCode;
                                    responseItem.InventoryTypeId = availData.InventoryWarehouse.InventoryTypeId;
                                    responseItem.InventoryType = availData.InventoryWarehouse.InventoryType;
                                    responseItem.CategoryId = availData.InventoryWarehouse.CategoryId;
                                    responseItem.Category = availData.InventoryWarehouse.Category;
                                    responseItem.SubCategoryId = availData.InventoryWarehouse.SubCategoryId;
                                    responseItem.SubCategory = availData.InventoryWarehouse.SubCategory;
                                    responseItem.InventoryId = availData.InventoryWarehouse.InventoryId;
                                    responseItem.ICode = availData.InventoryWarehouse.ICode;
                                    responseItem.ItemDescription = availData.InventoryWarehouse.Description;
                                    responseItem.QuantityIn = availData.In.Total;
                                    responseItem.QuantityInRepair = availData.InRepair.Total;
                                    responseItem.OrderId = reservation.OrderId;
                                    responseItem.OrderType = reservation.OrderType;
                                    responseItem.OrderNumber = reservation.OrderNumber;
                                    responseItem.OrderDescription = reservation.OrderDescription;
                                    responseItem.DealId = reservation.DealId;
                                    responseItem.Deal = reservation.Deal;
                                    responseItem.QuantityReserved = reservation.QuantityReserved.Total;
                                    responseItem.QuantitySub = reservation.QuantitySub;
                                    responseItem.FromDateTime = reservation.FromDateTime;
                                    responseItem.ToDateTime = reservation.ToDateTime;

                                    responseItem.FromDateTimeString = FwConvert.ToString(reservation.FromDateTime);
                                    responseItem.ToDateTimeString = (reservation.ToDateTime.Equals(LateDateTime) ? "LATE" : FwConvert.ToString(reservation.ToDateTime));

                                    TInventoryWarehouseAvailabilityMinimum minAvail = availData.GetMinimumAvailableQuantity(reservation.FromDateTime, reservation.ToDateTime);
                                    if ((!minAvail.IsStale) && (reservation.QuantitySub > 0) && (minAvail.MinimumAvailable.Total > reservation.QuantitySub))
                                    {
                                        minAvail.AvailabilityState = RwConstants.AVAILABILITY_STATE_POSITIVE_CONFLICT;
                                    }

                                    responseItem.QuantityAvailable = minAvail.MinimumAvailable.Total;
                                    responseItem.AvailabilityIsStale = minAvail.IsStale;
                                    responseItem.AvailabilityState = minAvail.AvailabilityState;
                                    responseItem.QuantityLate = availData.Late.Total;
                                    responseItem.QuantityQc = availData.QcRequired.Total;
                                    response.Add(responseItem);
                                }
                            }
                        }
                    }
                }
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------


        //-------------------------------------------------------------------------------------------------------
        /*
         * This method is only for debugging.  It is not thread-safe because of the foreach loops
         */
        public static bool DumpAvailabilityToFile(string inventoryId = "", string warehouseId = "")
        {
            string InventoryWarehouseAvailabilityToText(TInventoryWarehouseAvailabilityKey availKey, TInventoryWarehouseAvailability inventoryWarehouseAvailability)
            {
                StringBuilder sb1 = new StringBuilder();
                sb1.Append("InventoryId: ");
                sb1.Append(availKey.InventoryId);
                sb1.Append(" ");
                sb1.Append("WarehouseId: ");
                sb1.Append(availKey.WarehouseId);
                sb1.AppendLine();

                sb1.Append("   ");
                sb1.Append("I-Code:".PadRight(16));
                sb1.Append(inventoryWarehouseAvailability.InventoryWarehouse.ICode);
                sb1.AppendLine();
                sb1.Append("   ");
                sb1.Append("Description:".PadRight(16));
                sb1.Append(inventoryWarehouseAvailability.InventoryWarehouse.Description);
                sb1.AppendLine();
                sb1.Append("   ");
                sb1.Append("Warehouse:".PadRight(16));
                sb1.Append(inventoryWarehouseAvailability.InventoryWarehouse.Warehouse);
                sb1.Append("(");
                sb1.Append(inventoryWarehouseAvailability.InventoryWarehouse.WarehouseCode);
                sb1.Append(")");
                sb1.AppendLine();
                sb1.Append("   ");
                sb1.Append("Classification:".PadRight(16));
                sb1.Append(inventoryWarehouseAvailability.InventoryWarehouse.Classification);

                if (inventoryWarehouseAvailability.InventoryWarehouse.Classification.Equals(RwConstants.ITEMCLASS_COMPLETE) || inventoryWarehouseAvailability.InventoryWarehouse.Classification.Equals(RwConstants.ITEMCLASS_KIT))
                {
                    sb1.AppendLine();
                    sb1.Append("   ");
                    sb1.AppendLine("Accessories:");
                    foreach (TPackageAccessory accessory in inventoryWarehouseAvailability.InventoryWarehouse.Accessories)
                    {
                        sb1.AppendLine("   InventoryId: " + accessory.InventoryId + " Default Quantity:" + accessory.DefaultQuantity.ToString());
                    }
                }

                sb1.Append("   ");
                sb1.Append("Calculated:".PadRight(16));
                sb1.Append(inventoryWarehouseAvailability.CalculatedDateTime);
                sb1.AppendLine();

                sb1.AppendLine();
                sb1.Append("   ");
                sb1.AppendLine("------- AVAILABILITY BEHAVIOR ------------------------------------");
                sb1.Append("   ");
                sb1.Append("No Availability Check: ");
                sb1.Append(inventoryWarehouseAvailability.InventoryWarehouse.NoAvailabilityCheck.ToString());
                sb1.AppendLine();
                sb1.Append("   ");
                sb1.Append("Hourly Availability: ");
                sb1.Append(inventoryWarehouseAvailability.InventoryWarehouse.HourlyAvailability.ToString());
                sb1.AppendLine();

                sb1.AppendLine();
                sb1.Append("   ");
                sb1.AppendLine("------- CURRENT QUANTITIES ------------------------------------");
                sb1.Append("   ");
                sb1.Append("Ownership".PadRight(13));
                sb1.Append("Total".PadLeft(13));
                sb1.Append("In".PadLeft(13));
                sb1.Append("Staged".PadLeft(13));
                sb1.Append("Out".PadLeft(13));
                sb1.Append("In Transit".PadLeft(13));
                sb1.Append("In Repair".PadLeft(13));
                sb1.Append("On Truck".PadLeft(13));
                sb1.Append("In Container".PadLeft(13));
                sb1.Append("QC Required".PadLeft(13));
                sb1.AppendLine();

                sb1.Append("   ");
                sb1.Append("Owned".PadRight(13));
                sb1.Append(inventoryWarehouseAvailability.Total.Owned.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.In.Owned.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.Staged.Owned.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.Out.Owned.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.InTransit.Owned.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.InRepair.Owned.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.OnTruck.Owned.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.InContainer.Owned.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.QcRequired.Owned.ToString().PadLeft(13));
                sb1.AppendLine();

                sb1.Append("   ");
                sb1.Append("Consigned".PadRight(13));
                sb1.Append(inventoryWarehouseAvailability.Total.Consigned.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.In.Consigned.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.Staged.Consigned.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.Out.Consigned.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.InTransit.Consigned.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.InRepair.Consigned.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.OnTruck.Consigned.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.InContainer.Consigned.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.QcRequired.Consigned.ToString().PadLeft(13));
                sb1.AppendLine();

                sb1.Append("   ");
                sb1.Append("Total".PadRight(13));
                sb1.Append(inventoryWarehouseAvailability.Total.Total.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.In.Total.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.Staged.Total.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.Out.Total.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.InTransit.Total.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.InRepair.Total.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.OnTruck.Total.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.InContainer.Total.ToString().PadLeft(13));
                sb1.Append(inventoryWarehouseAvailability.QcRequired.Total.ToString().PadLeft(13));
                sb1.AppendLine();

                sb1.AppendLine();
                sb1.Append("   ");
                sb1.AppendLine("------- RESERVATIONS ------------------------------------");
                foreach (TInventoryWarehouseAvailabilityReservation reservation in inventoryWarehouseAvailability.Reservations)
                {
                    sb1.Append("   ");
                    sb1.Append("OrderId: ");
                    sb1.Append(reservation.OrderId.PadLeft(8));
                    sb1.Append(" ");
                    sb1.Append("OrderItemId: ");
                    sb1.Append(reservation.OrderItemId.PadLeft(8));
                    sb1.AppendLine();
                    sb1.Append("   ");
                    sb1.AppendLine("-------------------------------------------");

                    sb1.Append("       ");
                    sb1.Append("Type:".PadRight(15));
                    sb1.Append(reservation.OrderType.ToString());
                    sb1.AppendLine();
                    sb1.Append("       ");
                    sb1.Append("Number:".PadRight(15));
                    sb1.Append(reservation.OrderNumber.ToString());
                    sb1.AppendLine();
                    sb1.Append("       ");
                    sb1.Append("Description:".PadRight(15));
                    sb1.Append(reservation.OrderDescription.ToString());
                    sb1.AppendLine();
                    sb1.Append("       ");
                    sb1.Append("Status:".PadRight(15));
                    sb1.Append(reservation.OrderStatus.ToString());
                    sb1.AppendLine();
                    sb1.Append("       ");
                    sb1.Append("Deal:".PadRight(15));
                    sb1.Append(reservation.Deal.ToString());
                    sb1.Append(" (");
                    sb1.Append(reservation.DealId.ToString());
                    sb1.Append(")");
                    sb1.AppendLine();
                    sb1.Append("       ");
                    sb1.Append("Period:".PadRight(15));
                    sb1.Append(reservation.FromDateTime.ToString());
                    sb1.Append(" - ");
                    sb1.Append(reservation.ToDateTime.ToString());
                    sb1.AppendLine();



                    sb1.AppendLine("       Quantities:");
                    sb1.Append("       ");
                    sb1.AppendLine("-------------------------------------------");

                    sb1.Append("           ");
                    sb1.Append("Ordered:".PadRight(15));
                    sb1.Append(reservation.QuantityOrdered.ToString().PadLeft(10));
                    sb1.AppendLine();
                    sb1.Append("           ");
                    sb1.Append("Sub:".PadRight(15));
                    sb1.Append(reservation.QuantitySub.ToString().PadLeft(10));
                    sb1.AppendLine();
                    sb1.Append("           ");
                    sb1.Append("Consign:".PadRight(15));
                    sb1.Append(reservation.QuantityConsigned.ToString().PadLeft(10));
                    sb1.AppendLine();


                    sb1.AppendLine("       Statuses:");
                    sb1.Append("       ");
                    sb1.AppendLine("-------------------------------------------");
                    sb1.Append("           ");
                    sb1.Append("Ownership".PadRight(13));
                    sb1.Append("Reserved".PadLeft(13));
                    sb1.Append("Staged".PadLeft(13));
                    sb1.Append("Out".PadLeft(13));
                    sb1.Append("In".PadLeft(13));
                    sb1.AppendLine();

                    sb1.Append("           ");
                    sb1.Append("Owned".PadRight(13));
                    sb1.Append(reservation.QuantityReserved.Owned.ToString().PadLeft(13));
                    sb1.Append(reservation.QuantityStaged.Owned.ToString().PadLeft(13));
                    sb1.Append(reservation.QuantityOut.Owned.ToString().PadLeft(13));
                    sb1.Append(reservation.QuantityIn.Owned.ToString().PadLeft(13));
                    sb1.AppendLine();
                    sb1.Append("           ");
                    sb1.Append("Consigned".PadRight(13));
                    sb1.Append(reservation.QuantityReserved.Consigned.ToString().PadLeft(13));
                    sb1.Append(reservation.QuantityStaged.Consigned.ToString().PadLeft(13));
                    sb1.Append(reservation.QuantityOut.Consigned.ToString().PadLeft(13));
                    sb1.Append(reservation.QuantityIn.Consigned.ToString().PadLeft(13));
                    sb1.AppendLine();
                    sb1.Append("           ");
                    sb1.Append("Total".PadRight(13));
                    sb1.Append(reservation.QuantityReserved.Total.ToString().PadLeft(13));
                    sb1.Append(reservation.QuantityStaged.Total.ToString().PadLeft(13));
                    sb1.Append(reservation.QuantityOut.Total.ToString().PadLeft(13));
                    sb1.Append(reservation.QuantityIn.Total.ToString().PadLeft(13));
                    sb1.AppendLine();
                    sb1.AppendLine();



                }
                sb1.AppendLine();
                sb1.Append("   ");
                sb1.AppendLine("-------AVAILABILITY DATES/HOURS ----------------------------------");
                sb1.Append("   ");
                sb1.Append("From:".PadRight(6));
                sb1.Append(inventoryWarehouseAvailability.AvailDataFromDateTime.ToString().PadLeft(26));
                sb1.AppendLine();
                sb1.Append("   ");
                sb1.Append("To:".PadRight(6));
                sb1.Append(inventoryWarehouseAvailability.AvailDataToDateTime.ToString().PadLeft(26));
                sb1.AppendLine();
                sb1.AppendLine("--------------------------------------------");
                sb1.AppendLine();
                foreach (KeyValuePair<DateTime, TInventoryWarehouseAvailabilityDateTime> availDateEntry in inventoryWarehouseAvailability.AvailabilityDatesAndTimes)
                {
                    TInventoryWarehouseAvailabilityDateTime inventoryWarehouseAvailabilityDate = (TInventoryWarehouseAvailabilityDateTime)availDateEntry.Value;
                    sb1.Append("   ");
                    sb1.Append("Date/Time: ");
                    sb1.Append(inventoryWarehouseAvailabilityDate.AvailabilityDateTime.ToString().PadLeft(26));
                    sb1.AppendLine();

                    sb1.Append("      ");
                    sb1.Append("Ownership".PadRight(13));
                    sb1.Append("Available".PadLeft(13));
                    sb1.Append("Reserved".PadLeft(13));
                    sb1.Append("Returning".PadLeft(13));
                    sb1.AppendLine();

                    sb1.Append("      ");
                    sb1.Append("Owned".PadRight(13));
                    sb1.Append(inventoryWarehouseAvailabilityDate.Available.Owned.ToString().PadLeft(13));
                    sb1.Append(inventoryWarehouseAvailabilityDate.Reserved.Owned.ToString().PadLeft(13));
                    sb1.Append(inventoryWarehouseAvailabilityDate.Returning.Owned.ToString().PadLeft(13));
                    sb1.AppendLine();
                    sb1.Append("      ");
                    sb1.Append("Consigned".PadRight(13));
                    sb1.Append(inventoryWarehouseAvailabilityDate.Available.Consigned.ToString().PadLeft(13));
                    sb1.Append(inventoryWarehouseAvailabilityDate.Reserved.Consigned.ToString().PadLeft(13));
                    sb1.Append(inventoryWarehouseAvailabilityDate.Returning.Consigned.ToString().PadLeft(13));
                    sb1.AppendLine();
                    sb1.Append("      ");
                    sb1.Append("Total".PadRight(13));
                    sb1.Append(inventoryWarehouseAvailabilityDate.Available.Total.ToString().PadLeft(13));
                    sb1.Append(inventoryWarehouseAvailabilityDate.Reserved.Total.ToString().PadLeft(13));
                    sb1.Append(inventoryWarehouseAvailabilityDate.Returning.Total.ToString().PadLeft(13));
                    sb1.AppendLine();
                }
                sb1.AppendLine();
                sb1.AppendLine();

                return sb1.ToString();
            }


            bool success = true;
            StringBuilder sb2 = new StringBuilder();

            sb2.AppendLine("------- NEED RECALCULATION  ------------------------------------");

            //foreach (TInventoryWarehouse inventoryWarehouse in AvailabilityNeedRecalc)
            foreach (KeyValuePair<TInventoryWarehouseAvailabilityKey, string> anc in AvailabilityNeedRecalc)
            {
                bool includeInFile = true;

                if (includeInFile)
                {
                    if (!string.IsNullOrEmpty(inventoryId))
                    {
                        includeInFile = (anc.Key.InventoryId.Equals(inventoryId));
                    }
                }

                if (includeInFile)
                {
                    if (!string.IsNullOrEmpty(warehouseId))
                    {
                        includeInFile = (anc.Key.WarehouseId.Equals(warehouseId));
                    }
                }

                if (includeInFile)
                {
                    sb2.Append("InventoryId: ");
                    sb2.Append(anc.Key.InventoryId);
                    sb2.Append(" ");
                    sb2.Append("WarehouseId: ");
                    sb2.Append(anc.Key.WarehouseId);
                    sb2.AppendLine();
                }
            }
            sb2.AppendLine();

            if ((!string.IsNullOrEmpty(inventoryId)) && (!string.IsNullOrEmpty(warehouseId)))
            {
                TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);
                TInventoryWarehouseAvailability inventoryWarehouseAvailability = null;
                if (AvailabilityCache.TryGetValue(availKey, out inventoryWarehouseAvailability))
                {
                    string str = InventoryWarehouseAvailabilityToText(availKey, inventoryWarehouseAvailability);
                    sb2.AppendLine(str);
                }
            }
            else
            {
                foreach (KeyValuePair<TInventoryWarehouseAvailabilityKey, TInventoryWarehouseAvailability> availEntry in AvailabilityCache)
                {
                    TInventoryWarehouseAvailabilityKey availKey = (TInventoryWarehouseAvailabilityKey)availEntry.Key;
                    TInventoryWarehouseAvailability inventoryWarehouseAvailability = (TInventoryWarehouseAvailability)availEntry.Value;

                    string str = InventoryWarehouseAvailabilityToText(availKey, inventoryWarehouseAvailability);
                    sb2.AppendLine(str);
                }
            }

            System.IO.File.WriteAllText(@"C:\temp\availability.txt", sb2.ToString());
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
