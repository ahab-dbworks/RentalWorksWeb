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

namespace WebApi.Modules.Home.InventoryAvailability
{
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
        public string InventoryType { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string ICode { get; set; }
        public string ItemDesription { get; set; }
        public string OrderNumber { get; set; }
        public string OrderDescription { get; set; }
        public string Deal { get; set; }
        public decimal? QuantityOrdered { get; set; }
        public decimal? QuantitySub { get; set; }
        public decimal? QuantityAvailable { get; set; }
        public decimal? QuantityLate { get; set; }
        public decimal? QuantityIn { get; set; }
        public decimal? QuantityQc { get; set; }
        public decimal? QuantityInRepair { get; set; }
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
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
        public decimal Consigned { get; set; } = 0;
        public decimal Total { get { return Owned + Consigned; } }

        public static TInventoryWarehouseAvailabilityQuantity operator +(TInventoryWarehouseAvailabilityQuantity q1, TInventoryWarehouseAvailabilityQuantity q2)
        {
            TInventoryWarehouseAvailabilityQuantity q3 = new TInventoryWarehouseAvailabilityQuantity();
            q3.Owned = q1.Owned + q2.Owned;
            q3.Consigned = q1.Consigned + q2.Consigned;
            return q3;
        }

        public static TInventoryWarehouseAvailabilityQuantity operator -(TInventoryWarehouseAvailabilityQuantity q1, TInventoryWarehouseAvailabilityQuantity q2)
        {
            TInventoryWarehouseAvailabilityQuantity q3 = new TInventoryWarehouseAvailabilityQuantity();
            q3.Owned = q1.Owned - q2.Owned;
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
        public string OrderNumber { get; set; }
        public string OrderDescription { get; set; }
        public string OrderStatus { get; set; }
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
        [JsonIgnore]
        public bool countedReserved = false;  // used only while projecting future availability
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityDate
    {
        public TInventoryWarehouseAvailabilityDate(DateTime availabilityDate)
        {
            this.AvailabilityDate = availabilityDate;
        }
        public DateTime AvailabilityDate { get; set; }
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
        public string InventoryId { get; set; } = "";
        public string WarehouseId { get; set; } = "";
        public string ICode { get; set; } = "";
        public string Description { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
        public string Warehouse { get; set; } = "";
        public string Classification { get; set; } = "";
        public bool HourlyAvailability { get; set; } = false;
        public bool NoAvailabilityCheck { get; set; } = false;
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
        public DateTime AvailDataFromDateTime { get; set; }
        public DateTime AvailDataToDateTime { get; set; }
        public List<TInventoryWarehouseAvailabilityReservation> Reservations { get; set; } = new List<TInventoryWarehouseAvailabilityReservation>();
        public ConcurrentDictionary<DateTime, TInventoryWarehouseAvailabilityDate> Dates { get; set; } = new ConcurrentDictionary<DateTime, TInventoryWarehouseAvailabilityDate>();
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
            this.CalculatedDateTime = source.CalculatedDateTime;
            this.Reservations.Clear();
            foreach (TInventoryWarehouseAvailabilityReservation reservation in source.Reservations)
            {
                this.Reservations.Add(reservation);
            }
            this.Dates.Clear();
            foreach (KeyValuePair<DateTime, TInventoryWarehouseAvailabilityDate> date in source.Dates)
            {
                this.Dates.AddOrUpdate(date.Key, date.Value, (key, existingValue) =>
                {
                    existingValue.AvailabilityDate = date.Value.AvailabilityDate;
                    existingValue.Available = date.Value.Available;
                    existingValue.Reserved = date.Value.Reserved;
                    existingValue.Returning = date.Value.Returning;
                    return existingValue;
                });
            }
        }
        //-------------------------------------------------------------------------------------------------------
        public TInventoryWarehouseAvailabilityMinimum GetMinimumAvailableQuantity(DateTime fromDateTime, DateTime toDateTime)
        {
            TInventoryWarehouseAvailabilityMinimum minAvail = new TInventoryWarehouseAvailabilityMinimum();
            bool firstDateFound = false;
            bool lastDateFound = false;
            bool isStale = false;
            bool isHistory = false;

            minAvail.NoAvailabilityCheck = InventoryWarehouse.NoAvailabilityCheck;

            if (fromDateTime < DateTime.Today)
            {
                fromDateTime = DateTime.Today;
            }

            if (toDateTime < DateTime.Today)
            {
                toDateTime = DateTime.Today;
                isHistory = true;
            }

            if (isHistory)
            {
                minAvail.Color = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_HISTORICAL_DATE);
                minAvail.TextColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_HISTORICAL_DATE);
            }
            else
            {
                //#jhtodo rewrite this without the foreach. create a for loop for each date in the range. perform individual specific reads from "Dates" dictionary using the key
                foreach (KeyValuePair<DateTime, TInventoryWarehouseAvailabilityDate> availDate in Dates)
                {
                    DateTime theDate = availDate.Key;
                    TInventoryWarehouseAvailabilityDate inventoryWarehouseAvailabilityDate = availDate.Value;
                    if ((fromDateTime <= theDate) && (theDate <= toDateTime))
                    {
                        if (theDate.Equals(fromDateTime))
                        {
                            firstDateFound = true;
                            minAvail.MinimumAvailable = inventoryWarehouseAvailabilityDate.Available;
                        }
                        minAvail.MinimumAvailable = (minAvail.MinimumAvailable.Total < inventoryWarehouseAvailabilityDate.Available.Total) ? minAvail.MinimumAvailable : inventoryWarehouseAvailabilityDate.Available;

                        if ((minAvail.FirstConfict == null) && (minAvail.MinimumAvailable.Total < 0))
                        {
                            minAvail.FirstConfict = theDate;
                        }

                        if (theDate.Equals(toDateTime))
                        {
                            lastDateFound = true;
                        }
                    }
                }

                if (!isStale)
                {
                    isStale = InventoryAvailabilityFunc.AvailabilityNeedRecalc.Contains(new TInventoryWarehouse(InventoryWarehouse.InventoryId, InventoryWarehouse.WarehouseId));
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

                if (minAvail.NoAvailabilityCheck)
                {
                    minAvail.Color = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_NO_AVAILABILITY);
                    minAvail.TextColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_NEEDRECALC);
                }
                else if (minAvail.IsStale)
                {
                    minAvail.Color = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_NEEDRECALC);
                    minAvail.TextColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_NEEDRECALC);
                }
                else if (minAvail.MinimumAvailable.Total < 0)
                {
                    minAvail.Color = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_NEGATIVE);
                    minAvail.TextColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_NEGATIVE);
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
        public List<TInventoryAvailabilityCalendarEvent> InventoryAvailabilityCalendarEvents { get; set; } = new List<TInventoryAvailabilityCalendarEvent>();
        public List<TInventoryAvailabilityScheduleResource> InventoryAvailabilityScheduleResources { get; set; } = new List<TInventoryAvailabilityScheduleResource>();
        public List<TInventoryAvailabilityScheduleEvent> InventoryAvailabilityScheduleEvents { get; set; } = new List<TInventoryAvailabilityScheduleEvent>();
    }
    //------------------------------------------------------------------------------------ 
    public static class InventoryAvailabilityFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static List<TInventoryWarehouse> AvailabilityNeedRecalc = new List<TInventoryWarehouse>();
        private static int LastNeedRecalcId = 0;
        private static TAvailabilityCache AvailabilityCache = new TAvailabilityCache();
        private const int AVAILABILITY_DAYS_TO_CACHE = 90;
        private static DateTime AvailabilityThroughDate;
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> InitializeService(FwApplicationConfig appConfig)
        {
            bool success = true;
            AvailabilityThroughDate = DateTime.Today.AddDays(AVAILABILITY_DAYS_TO_CACHE);

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select needrecalcid = max(a.id)");
                qry.Add(" from  tmpavailneedrecalc a");
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();
                LastNeedRecalcId = FwConvert.ToInt32(dt.Rows[0][dt.GetColumnNo("needrecalcid")]);
            }

            return success;
        }
        //-------------------------------------------------------------------------------------------------------        
        private static async Task<bool> CheckNeedRecalc(FwApplicationConfig appConfig)
        {
            bool success = true;

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select a.masterid, a.warehouseid, a.class, a.id");
                qry.Add(" from  tmpavailneedrecalcview a");
                qry.Add(" where a.id > @lastneedrecalcid");
                qry.Add("order by a.id");
                qry.AddParameter("@lastneedrecalcid", LastNeedRecalcId);
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                int needRecalcId = 0;
                foreach (List<object> row in dt.Rows)
                {
                    string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                    string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
                    string classification = row[dt.GetColumnNo("class")].ToString();
                    needRecalcId = FwConvert.ToInt32(row[dt.GetColumnNo("id")]);
                    TInventoryWarehouse inventoryWarehouse = new TInventoryWarehouse(inventoryId, warehouseId);
                    inventoryWarehouse.Classification = classification;
                    AvailabilityNeedRecalc.Add(inventoryWarehouse);
                }
                if (needRecalcId > 0)
                {
                    LastNeedRecalcId = needRecalcId;
                }
            }

            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<List<TInventoryWarehouse>> GetInventoryNeedingAvail(FwApplicationConfig appConfig, List<string> classifications)
        {
            List<TInventoryWarehouse> inventoryNeedingAvail = new List<TInventoryWarehouse>();

            AvailabilityThroughDate = DateTime.Today.AddDays(AVAILABILITY_DAYS_TO_CACHE);

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {

                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select masterid, warehouseid, class");
                qry.Add(" from  masterwhforavailview");
                if (classifications.Count > 0)
                {
                    qry.Add(" where class in (");
                    int c = 0;
                    foreach (string classification in classifications)
                    {
                        c++;
                        qry.Add("'" + classification + "'");
                        if (c < classifications.Count)
                        {
                            qry.Add(",");
                        }
                    }
                    qry.Add(")");
                }
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                inventoryNeedingAvail.Clear();
                foreach (List<object> row in dt.Rows)
                {
                    string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                    string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
                    string classification = row[dt.GetColumnNo("class")].ToString();
                    bool foundInCache = false;
                    bool dataIsCached = false;
                    TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);

                    TInventoryWarehouseAvailability availData = null;
                    if (AvailabilityCache.TryGetValue(availKey, out availData))
                    {
                        foundInCache = true;
                        dataIsCached = (availData.AvailDataToDateTime >= AvailabilityThroughDate);
                    }

                    if ((!foundInCache) || (!dataIsCached))
                    {
                        TInventoryWarehouse inventoryWarehouse = new TInventoryWarehouse(inventoryId, warehouseId);
                        inventoryWarehouse.Classification = classification;
                        inventoryNeedingAvail.Add(inventoryWarehouse);
                    }
                }
            }

            return inventoryNeedingAvail;
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<List<TInventoryWarehouse>> GetItemsAccessoriesNeedingAvail(FwApplicationConfig appConfig)
        {
            List<string> classifications = new List<string>();
            classifications.Add(RwConstants.INVENTORY_CLASSIFICATION_ITEM);
            classifications.Add(RwConstants.INVENTORY_CLASSIFICATION_ACCESSORY);
            return await GetInventoryNeedingAvail(appConfig, classifications);
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<List<TInventoryWarehouse>> GetPackageNeedingAvail(FwApplicationConfig appConfig)
        {
            List<string> classifications = new List<string>();
            classifications.Add(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE);
            classifications.Add(RwConstants.INVENTORY_CLASSIFICATION_KIT);
            return await GetInventoryNeedingAvail(appConfig, classifications);
        }
        //-------------------------------------------------------------------------------------------------------
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
                foreach (KeyValuePair<DateTime, TInventoryWarehouseAvailabilityDate> availDateEntry in inventoryWarehouseAvailability.Dates)
                {
                    TInventoryWarehouseAvailabilityDate inventoryWarehouseAvailabilityDate = (TInventoryWarehouseAvailabilityDate)availDateEntry.Value;
                    sb1.Append("   ");
                    sb1.Append("Date: ");
                    sb1.Append(inventoryWarehouseAvailabilityDate.AvailabilityDate.ToString().PadLeft(26));
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

            foreach (TInventoryWarehouse inventoryWarehouse in AvailabilityNeedRecalc)
            {
                bool includeInFile = true;

                if (includeInFile)
                {
                    if (!string.IsNullOrEmpty(inventoryId))
                    {
                        includeInFile = (inventoryWarehouse.InventoryId.Equals(inventoryId));
                    }
                }

                if (includeInFile)
                {
                    if (!string.IsNullOrEmpty(warehouseId))
                    {
                        includeInFile = (inventoryWarehouse.WarehouseId.Equals(warehouseId));
                    }
                }

                if (includeInFile)
                {
                    sb2.Append("InventoryId: ");
                    sb2.Append(inventoryWarehouse.InventoryId);
                    sb2.Append(" ");
                    sb2.Append("WarehouseId: ");
                    sb2.Append(inventoryWarehouse.WarehouseId);
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
        private static void ProjectFutureAvailability(ref TAvailabilityCache availCache)
        {
            foreach (KeyValuePair<TInventoryWarehouseAvailabilityKey, TInventoryWarehouseAvailability> availEntry in availCache)
            {
                TInventoryWarehouseAvailabilityKey availKey = availEntry.Key;
                TInventoryWarehouseAvailability availData = availEntry.Value;
                DateTime fromDateTime = DateTime.Today; //daily availability  //#jhtodo
                DateTime toDateTime = availData.AvailDataToDateTime;
                if (!availData.InventoryWarehouse.NoAvailabilityCheck)
                {
                    if (availData.InventoryWarehouse.Classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE) || availData.InventoryWarehouse.Classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_KIT))
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
                                        TInventoryWarehouseAvailabilityDate accAvailableDate = null;
                                        if (accAvailCache.Dates.TryGetValue(theDateTime, out accAvailableDate))
                                        {
                                            TInventoryWarehouseAvailabilityQuantity accAvailable = accAvailableDate.Available;
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
                                TInventoryWarehouseAvailabilityDate inventoryWarehouseAvailabilityDate = new TInventoryWarehouseAvailabilityDate(theDateTime);
                                inventoryWarehouseAvailabilityDate.Available = packageAvailable;
                                availData.Dates.TryAdd(theDateTime, inventoryWarehouseAvailabilityDate);
                                theDateTime = theDateTime.AddDays(1); //daily availability   //#jhtodo
                                                                      //hourly availability?
                            }
                            else
                            {
                                // availabiltiy data not found in cache for at least one accessory for at least one date in the range
                                break;
                                // exit the date while loop.  leave package availability data incomplete
                            }
                        }
                    }
                    else
                    {
                        TInventoryWarehouseAvailabilityQuantity available = availData.In;  // snapshot the current IN quantity.  use this as a running total

                        // use the availData.Reservations to calculate future availability for this Icode
                        DateTime theDateTime = fromDateTime;
                        while (theDateTime <= toDateTime)
                        {
                            TInventoryWarehouseAvailabilityDate inventoryWarehouseAvailabilityDate = new TInventoryWarehouseAvailabilityDate(theDateTime);

                            foreach (TInventoryWarehouseAvailabilityReservation reservation in availData.Reservations)
                            {
                                if ((reservation.FromDateTime <= theDateTime) && (theDateTime <= reservation.ToDateTime))
                                {
                                    inventoryWarehouseAvailabilityDate.Reserved += reservation.QuantityReserved;
                                    if (!reservation.countedReserved)
                                    {
                                        available -= reservation.QuantityReserved;
                                    }
                                    reservation.countedReserved = true;
                                }
                                if (reservation.ToDateTime == theDateTime)
                                {
                                    inventoryWarehouseAvailabilityDate.Returning += reservation.QuantityReserved + reservation.QuantityStaged + reservation.QuantityOut;
                                }
                            }

                            inventoryWarehouseAvailabilityDate.Available = available;
                            available += inventoryWarehouseAvailabilityDate.Returning;  // the amount returning in this date/hour slot will become available for the next date/hour slot

                            availData.Dates.TryAdd(theDateTime, inventoryWarehouseAvailabilityDate);

                            theDateTime = theDateTime.AddDays(1); //daily availability   //#jhtodo
                                                                  //hourly availability?
                        }
                    }

                    availData.CalculatedDateTime = DateTime.Now;
                }
                AvailabilityNeedRecalc.RemoveAll(k => k.Equals(new TInventoryWarehouse(availKey.InventoryId, availKey.WarehouseId)));
            }
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<bool> RefreshAvailability(FwApplicationConfig appConfig, FwUserSession userSession, TInventoryWarehouseAvailabilityRequestItems availRequestItems)
        {
            bool success = false;
            string sessionId = AppFunc.GetNextIdAsync(appConfig).Result;

            TAvailabilityCache availCache = new TAvailabilityCache();
            if (availRequestItems.Count > 0)
            {

                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                    qry.Add("delete ");
                    qry.Add(" from  tmpsearchsession ");
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
                        qry.Add("if (not exists (select * ");
                        qry.Add("                 from  tmpsearchsession t");
                        qry.Add("                 where t.sessionid   = @sessionid");
                        qry.Add("                 and   t.masterid    = @masterid" + i.ToString());
                        qry.Add("                 and   t.warehouseid = @warehouseid" + i.ToString() + "))");
                        qry.Add("begin");
                        qry.Add("   insert into tmpsearchsession (sessionid, masterid, warehouseid)");
                        qry.Add("    values (@sessionid, @masterid" + i.ToString() + ", @warehouseid" + i.ToString() + ")");
                        qry.Add("end");
                        qry.Add(" ");
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
                    qry.Add("select a.masterid, a.warehouseid,");
                    qry.Add("       a.masterno, a.master, a.whcode, a.noavail, a.warehouse, a.class, a.availbyhour,");
                    qry.Add("       a.ownedqty, a.ownedqtyin, a.ownedqtystaged, a.ownedqtyout, a.ownedqtyintransit, a.ownedqtyinrepair, a.ownedqtyontruck, a.ownedqtyincontainer,");
                    qry.Add("       a.consignedqty, a.consignedqtyin, a.consignedqtystaged, a.consignedqtyout, a.consignedqtyintransit, a.consignedqtyinrepair, a.consignedqtyontruck, a.consignedqtyincontainer");
                    qry.Add(" from  availabilitymasterwhview a");
                    qry.Add("             join tmpsearchsession t on (a.masterid = t.masterid and a.warehouseid = t.warehouseid)");
                    qry.Add(" where t.sessionid = @sessionid");
                    qry.AddParameter("@sessionid", sessionId);
                    FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                    foreach (List<object> row in dt.Rows)
                    {
                        string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                        string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
                        TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);

                        TInventoryWarehouseAvailability availData = new TInventoryWarehouseAvailability(inventoryId, warehouseId);

                        availData.InventoryWarehouse.ICode = row[dt.GetColumnNo("masterno")].ToString();
                        availData.InventoryWarehouse.Description = row[dt.GetColumnNo("master")].ToString();
                        availData.InventoryWarehouse.WarehouseCode = row[dt.GetColumnNo("whcode")].ToString();
                        availData.InventoryWarehouse.Warehouse = row[dt.GetColumnNo("warehouse")].ToString();
                        availData.InventoryWarehouse.Classification = row[dt.GetColumnNo("class")].ToString();

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

                        availCache[availKey] = availData;
                    }
                }

                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                    qry.Add("delete t");
                    qry.Add(" from  tmpsearchsession t ");
                    qry.Add("           join master m on (t.masterid = m.masterid)");
                    qry.Add(" where t.sessionid = @sessionid ");
                    qry.Add(" and   m.noavail = 'T' ");
                    qry.AddParameter("@sessionid", sessionId);
                    await qry.ExecuteNonQueryAsync();
                }

                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    bool hasConsignment = false;  //jh 02/28/2019 place-holder.  will add system-wide option for consignment here
                    FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                    qry.Add("select a.masterid, a.warehouseid,");
                    qry.Add("       a.orderid, a.masteritemid, a.availfromdatetime, a.availtodatetime, ");
                    qry.Add("       a.ordertype, a.orderno, a.orderdesc, a.orderstatus, a.dealid, a.deal, ");
                    qry.Add("       a.qtyordered, a.qtystagedowned, a.qtyoutowned, a.qtyinowned,  ");
                    qry.Add("       a.subqty, a.qtystagedsub, a.qtyoutsub, a.qtyinsub, ");
                    if (hasConsignment)
                    {
                        //jh 02/28/2019 this is a bottleneck as the query must join in ordertranextended to get the consignorid.  Consider moving consignorid to the ordetran table
                        qry.Add("       a.consignqty, a.qtystagedconsigned, a.qtyoutconsigned, a.qtyinconsigned ");
                    }
                    else
                    {
                        qry.Add("       consignqty = 0, qtystagedconsigned = 0, qtyoutconsigned = 0, qtyinconsigned = 0 ");
                    }
                    qry.Add(" from  availabilityitemview a");
                    qry.Add("             join tmpsearchsession t on (a.masterid = t.masterid and a.warehouseid = t.warehouseid)");
                    qry.Add(" where t.sessionid = @sessionid");
                    qry.AddParameter("@sessionid", sessionId);
                    FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                    // load dt into availData.Reservations
                    foreach (List<object> row in dt.Rows)
                    {
                        string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                        string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
                        TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);

                        TInventoryWarehouseAvailabilityReservation reservation = new TInventoryWarehouseAvailabilityReservation();
                        reservation.OrderId = row[dt.GetColumnNo("orderid")].ToString();
                        reservation.OrderItemId = row[dt.GetColumnNo("masteritemid")].ToString();
                        reservation.OrderType = row[dt.GetColumnNo("ordertype")].ToString();
                        reservation.OrderNumber = row[dt.GetColumnNo("orderno")].ToString();
                        reservation.OrderDescription = row[dt.GetColumnNo("orderdesc")].ToString();
                        reservation.OrderStatus = row[dt.GetColumnNo("orderstatus")].ToString();
                        reservation.DealId = row[dt.GetColumnNo("dealid")].ToString();
                        reservation.Deal = row[dt.GetColumnNo("deal")].ToString();
                        reservation.FromDateTime = FwConvert.ToDateTime(row[dt.GetColumnNo("availfromdatetime")].ToString());
                        reservation.ToDateTime = FwConvert.ToDateTime(row[dt.GetColumnNo("availtodatetime")].ToString());
                        reservation.QuantityOrdered = FwConvert.ToDecimal(row[dt.GetColumnNo("qtyordered")].ToString());
                        reservation.QuantitySub = FwConvert.ToDecimal(row[dt.GetColumnNo("subqty")].ToString());
                        reservation.QuantityConsigned = FwConvert.ToDecimal(row[dt.GetColumnNo("consignqty")].ToString());

                        TInventoryWarehouseAvailabilityQuantity reservationStaged = new TInventoryWarehouseAvailabilityQuantity();
                        TInventoryWarehouseAvailabilityQuantity reservationOut = new TInventoryWarehouseAvailabilityQuantity();
                        TInventoryWarehouseAvailabilityQuantity reservationIn = new TInventoryWarehouseAvailabilityQuantity();

                        reservationStaged.Owned = FwConvert.ToDecimal(row[dt.GetColumnNo("qtystagedowned")].ToString());
                        reservationStaged.Consigned = FwConvert.ToDecimal(row[dt.GetColumnNo("qtystagedconsigned")].ToString());

                        reservationOut.Owned = FwConvert.ToDecimal(row[dt.GetColumnNo("qtyoutowned")].ToString());
                        reservationOut.Consigned = FwConvert.ToDecimal(row[dt.GetColumnNo("qtyoutconsigned")].ToString());

                        reservationIn.Owned = FwConvert.ToDecimal(row[dt.GetColumnNo("qtyinowned")].ToString());
                        reservationIn.Consigned = FwConvert.ToDecimal(row[dt.GetColumnNo("qtyinconsigned")].ToString());

                        reservation.QuantityStaged = reservationStaged;
                        reservation.QuantityOut = reservationOut;
                        reservation.QuantityIn = reservationIn;

                        reservation.QuantityReserved.Owned = (reservation.QuantityOrdered - reservation.QuantitySub - reservation.QuantityConsigned - reservation.QuantityStaged.Owned - reservation.QuantityOut.Owned - reservation.QuantityIn.Owned);
                        reservation.QuantityReserved.Consigned = (reservation.QuantityConsigned - reservation.QuantityOut.Consigned - reservation.QuantityIn.Consigned);

                        TInventoryWarehouseAvailability availData = new TInventoryWarehouseAvailability(inventoryId, warehouseId);
                        if (availCache.TryGetValue(availKey, out availData))
                        {
                            availData.Reservations.Add(reservation);
                        }
                    }
                }
                // copy the loop above for Completes and Kits, joining on parentid.  This will give a list of reservations that reference these packages
                //qry.Add("             join tmpsearchsession t on (a.parentid = t.masterid and a.warehouseid = t.warehouseid)");


                foreach (TInventoryWarehouseAvailabilityRequestItem availRequestItem in availRequestItems)
                {
                    // in case of duplicates, may need to use min/max here
                    TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(availRequestItem.InventoryId, availRequestItem.WarehouseId);
                    availCache[availKey].AvailDataFromDateTime = availRequestItem.FromDateTime;
                    availCache[availKey].AvailDataToDateTime = availRequestItem.ToDateTime;
                }

                ProjectFutureAvailability(ref availCache);

                foreach (TInventoryWarehouseAvailabilityKey availKey in availCache.Keys)
                {
                    AvailabilityCache.AddOrUpdate(availKey, availCache[availKey], (key, existingValue) =>
                    {
                        existingValue.CloneFrom(availCache[availKey]);
                        return existingValue;
                    });
                }

                success = true;

            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> KeepFresh(FwApplicationConfig appConfig)
        {
            const int AVAILABILITY_REQUEST_BATCH_SIZE = 5000;
            bool success = true;
            Console.WriteLine("keeping availability fresh");
            DateTime fromDate = DateTime.Today;
            AvailabilityThroughDate = DateTime.Today.AddDays(AVAILABILITY_DAYS_TO_CACHE);

            // initialize an empty request
            TInventoryWarehouseAvailabilityRequestItems availRequestItems = new TInventoryWarehouseAvailabilityRequestItems();

            // update the list of known items needed recalc
            Console.WriteLine("checking the need recalc table");
            await CheckNeedRecalc(appConfig);


            // build up a list of Items and Accessories only from the global AvailabilityNeedRecalc list
            Console.WriteLine("checking for master/warehouse item/accessory records need recalc");
            List<TInventoryWarehouse> availNeedRecalc = new List<TInventoryWarehouse>();
            availNeedRecalc.Clear();
            while (AvailabilityNeedRecalc.Count > 0)
            {
                foreach (TInventoryWarehouse inventoryWarehouse in AvailabilityNeedRecalc)  //#jhtodo remove foreach enumerator here
                {
                    if (inventoryWarehouse.Classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_ITEM) || inventoryWarehouse.Classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_ACCESSORY))
                    {
                        availNeedRecalc.Add(inventoryWarehouse);
                    }
                }
            }

            // loop through this local list of Itema and Accessories in batches
            Console.WriteLine(availNeedRecalc.Count.ToString().PadLeft(7) + " master/warehouse item/accessory records need recalc");
            while (availNeedRecalc.Count > 0)
            {
                // build up a request containing all known items needing recalc
                availRequestItems.Clear();
                foreach (TInventoryWarehouse inventoryWarehouse in availNeedRecalc)  
                {
                    availRequestItems.Add(new TInventoryWarehouseAvailabilityRequestItem(inventoryWarehouse.InventoryId, inventoryWarehouse.WarehouseId, fromDate, AvailabilityThroughDate));
                    if (availRequestItems.Count >= AVAILABILITY_REQUEST_BATCH_SIZE)
                    {
                        break; // break out of this foreach loop
                    }
                }

                // update the static cache of availability data
                await GetAvailability(appConfig, null, availRequestItems, true);

                Console.WriteLine(availNeedRecalc.Count.ToString().PadLeft(7) + " master/warehouse item/accessory records need recalc");
            }

            // get the list of inventory items and accessories that either have no availability data in cache, or the cache is not far enough out in the future
            Console.WriteLine("checking item/accessory inventory that needs availability data in cache");
            List<TInventoryWarehouse> inventoryNeedingAvail = await GetItemsAccessoriesNeedingAvail(appConfig);

            // loop through this list in batches 
            Console.WriteLine(inventoryNeedingAvail.Count.ToString().PadLeft(7) + " master/warehouse item/accessory records need availability data in cache");
            while (inventoryNeedingAvail.Count > 0)
            {
                // build up a request containing all items that need availability
                availRequestItems.Clear();
                foreach (TInventoryWarehouse inventoryWarehouse in inventoryNeedingAvail)
                {
                    availRequestItems.Add(new TInventoryWarehouseAvailabilityRequestItem(inventoryWarehouse.InventoryId, inventoryWarehouse.WarehouseId, fromDate, AvailabilityThroughDate));

                    if (availRequestItems.Count >= AVAILABILITY_REQUEST_BATCH_SIZE)
                    {
                        break; // break out of this foreach loop
                    }
                }

                foreach (TInventoryWarehouseAvailabilityRequestItem availRequestItem in availRequestItems)
                {
                    //inventoryNeedingAvail.RemoveAll(k => k.Equals(new TInventoryWarehouse(availRequestItem.InventoryId, availRequestItem.WarehouseId)));
                    inventoryNeedingAvail.Remove(new TInventoryWarehouse(availRequestItem.InventoryId, availRequestItem.WarehouseId));
                }

                // update the static cache of availability data
                await GetAvailability(appConfig, null, availRequestItems, true);

                Console.WriteLine(inventoryNeedingAvail.Count.ToString().PadLeft(7) + " master/warehouse item/accessory records need availability data in cache");
            }


            // build up a list of Completes and Kits only from the global AvailabilityNeedRecalc list
            Console.WriteLine("checking for master/warehouse complete/kit records need recalc");
            availNeedRecalc.Clear();
            while (AvailabilityNeedRecalc.Count > 0)
            {
                foreach (TInventoryWarehouse inventoryWarehouse in AvailabilityNeedRecalc) //#jhtodo remove foreach enumerator here
                {
                    if (inventoryWarehouse.Classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE) || inventoryWarehouse.Classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_KIT))
                    {
                        availNeedRecalc.Add(inventoryWarehouse);
                    }
                }
            }

            // loop through this local list of Completes and Kits in batches
            Console.WriteLine(availNeedRecalc.Count.ToString().PadLeft(7) + " master/warehouse complete/kit records need recalc");
            while (availNeedRecalc.Count > 0)
            {
                // build up a request containing all known items needing recalc
                availRequestItems.Clear();
                foreach (TInventoryWarehouse inventoryWarehouse in availNeedRecalc)
                {
                    availRequestItems.Add(new TInventoryWarehouseAvailabilityRequestItem(inventoryWarehouse.InventoryId, inventoryWarehouse.WarehouseId, fromDate, AvailabilityThroughDate));
                    if (availRequestItems.Count >= AVAILABILITY_REQUEST_BATCH_SIZE)
                    {
                        break; // break out of this foreach loop
                    }
                }

                // update the static cache of availability data
                await GetAvailability(appConfig, null, availRequestItems, true);

                Console.WriteLine(availNeedRecalc.Count.ToString().PadLeft(7) + " master/warehouse complete/kit records need recalc");
            }


            // get the list of inventory Completes and Kits that either have no availability data in cache, or the cache is not far enough out in the future
            Console.WriteLine("checking complete/kit inventory that needs availability data in cache");
            inventoryNeedingAvail = await GetPackageNeedingAvail(appConfig);

            // loop through this list in batches 
            Console.WriteLine(inventoryNeedingAvail.Count.ToString().PadLeft(7) + " master/warehouse complete/kit records need availability data in cache");
            while (inventoryNeedingAvail.Count > 0)
            {
                // build up a request containing all items that need availability
                availRequestItems.Clear();
                foreach (TInventoryWarehouse inventoryWarehouse in inventoryNeedingAvail)
                {
                    availRequestItems.Add(new TInventoryWarehouseAvailabilityRequestItem(inventoryWarehouse.InventoryId, inventoryWarehouse.WarehouseId, fromDate, AvailabilityThroughDate));

                    if (availRequestItems.Count >= AVAILABILITY_REQUEST_BATCH_SIZE)
                    {
                        break; // break out of this foreach loop
                    }
                }

                foreach (TInventoryWarehouseAvailabilityRequestItem availRequestItem in availRequestItems)
                {
                    //inventoryNeedingAvail.RemoveAll(k => k.Equals(new TInventoryWarehouse(availRequestItem.InventoryId, availRequestItem.WarehouseId)));
                    inventoryNeedingAvail.Remove(new TInventoryWarehouse(availRequestItem.InventoryId, availRequestItem.WarehouseId));
                }

                // update the static cache of availability data
                await GetAvailability(appConfig, null, availRequestItems, true);

                Console.WriteLine(inventoryNeedingAvail.Count.ToString().PadLeft(7) + " master/warehouse complete/kit records need availability data in cache");
            }



            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TAvailabilityCache> GetAvailability(FwApplicationConfig appConfig, FwUserSession userSession, TInventoryWarehouseAvailabilityRequestItems availRequestItems, bool refreshIfNeeded)
        {
            TAvailabilityCache availCache = new TAvailabilityCache();
            TInventoryWarehouseAvailabilityRequestItems availRequestToRefresh = new TInventoryWarehouseAvailabilityRequestItems();

            await CheckNeedRecalc(appConfig);

            foreach (TInventoryWarehouseAvailabilityRequestItem availRequestItem in availRequestItems)
            {
                if ((!availRequestItem.InventoryId.Equals("undefined")) && (!availRequestItem.WarehouseId.Equals("undefined")))
                {
                    bool foundInCache = false;
                    bool stale = false;
                    DateTime fromDate = availRequestItem.FromDateTime;
                    DateTime toDate = availRequestItem.ToDateTime;

                    if (AvailabilityNeedRecalc.Contains(new TInventoryWarehouse(availRequestItem.InventoryId, availRequestItem.WarehouseId)))
                    {
                        stale = true;
                    }

                    if (fromDate < DateTime.Today)
                    {
                        fromDate = DateTime.Today;
                    }

                    if (toDate < DateTime.Today)
                    {
                        toDate = DateTime.Today;
                    }

                    TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(availRequestItem.InventoryId, availRequestItem.WarehouseId);
                    TInventoryWarehouseAvailability availData = null;
                    if (AvailabilityCache.TryGetValue(availKey, out availData))
                    {
                        foundInCache = true;
                        DateTime theDate = availData.AvailDataFromDateTime;
                        while (theDate <= availData.AvailDataToDateTime)
                        {
                            if ((theDate < fromDate) || (toDate < theDate))
                            {
                                TInventoryWarehouseAvailabilityDate availDate = null;
                                availData.Dates.TryRemove(theDate, out availDate);
                            }
                            theDate = theDate.AddDays(1);
                        }

                        theDate = fromDate;
                        while (theDate <= toDate)
                        {
                            if (!availData.Dates.ContainsKey(theDate))
                            {
                                foundInCache = false;
                                break;
                            }
                            theDate = theDate.AddDays(1);
                        }
                        availCache[availKey] = availData;
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

            if (availRequestToRefresh.Count > 0)
            {
                await RefreshAvailability(appConfig, userSession, availRequestToRefresh);
                availCache = await GetAvailability(appConfig, userSession, availRequestItems, false);
            }


            return availCache;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TInventoryWarehouseAvailability> GetAvailability(FwApplicationConfig appConfig, FwUserSession userSession, string inventoryId, string warehouseId, DateTime fromDate, DateTime toDate, bool refreshIfNeeded)

        {
            TInventoryWarehouseAvailability availData = null;

            if ((!inventoryId.Equals("undefined")) && (!warehouseId.Equals("undefined")))
            {
                availData = new TInventoryWarehouseAvailability(inventoryId, warehouseId);

                TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);
                TInventoryWarehouseAvailabilityRequestItems availRequestItems = new TInventoryWarehouseAvailabilityRequestItems();
                availRequestItems.Add(new TInventoryWarehouseAvailabilityRequestItem(inventoryId, warehouseId, fromDate, toDate));

                TAvailabilityCache availCache = await GetAvailability(appConfig, userSession, availRequestItems, refreshIfNeeded);
                availCache.TryGetValue(availKey, out availData);
            }

            return availData;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TInventoryAvailabilityCalendarAndScheduleResponse> GetCalendarAndScheduleData(FwApplicationConfig appConfig, FwUserSession userSession, string InventoryId, string WarehouseId, DateTime FromDate, DateTime ToDate)

        {
            TInventoryAvailabilityCalendarAndScheduleResponse response = new TInventoryAvailabilityCalendarAndScheduleResponse();

            TInventoryWarehouseAvailability availData = await GetAvailability(appConfig, userSession, InventoryId, WarehouseId, FromDate, ToDate, true);

            int eventId = 0;

            if (availData != null)
            {
                // build up the calendar events
                //currently hard-coded for "daily" availability.  will need mods to work for "hourly"  //#jhtodo
                foreach (KeyValuePair<DateTime, TInventoryWarehouseAvailabilityDate> d in availData.Dates)
                {
                    DateTime startDateTime = d.Key;
                    DateTime endDateTime = d.Key;

                    startDateTime = startDateTime.AddMinutes(1);
                    endDateTime = endDateTime.AddDays(1).AddMinutes(-1);
                    //available
                    eventId++;
                    TInventoryAvailabilityCalendarEvent iAvail = new TInventoryAvailabilityCalendarEvent();
                    iAvail.id = eventId.ToString(); ;
                    iAvail.InventoryId = InventoryId;
                    iAvail.WarehouseId = WarehouseId;
                    iAvail.start = startDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                    iAvail.end = endDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");
                    iAvail.text = "Available " + ((int)d.Value.Available.Total).ToString();
                    if (d.Value.Available.Total < 0)
                    {
                        iAvail.backColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_NEGATIVE);
                        iAvail.textColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_NEGATIVE);
                    }
                    else
                    {
                        iAvail.backColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_POSITIVE);
                        iAvail.textColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_POSITIVE);
                    }
                    response.InventoryAvailabilityCalendarEvents.Add(iAvail);

                    //reserved
                    if (d.Value.Reserved.Total != 0)
                    {
                        eventId++;
                        TInventoryAvailabilityCalendarEvent iReserve = new TInventoryAvailabilityCalendarEvent();
                        iReserve.id = eventId.ToString(); ;
                        iReserve.InventoryId = InventoryId;
                        iReserve.WarehouseId = WarehouseId;
                        iReserve.start = startDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                        iReserve.end = endDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");
                        iReserve.text = "Reserved " + ((int)d.Value.Reserved.Total).ToString();
                        iReserve.backColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_RESERVED);
                        iReserve.textColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_RESERVED);
                        response.InventoryAvailabilityCalendarEvents.Add(iReserve);
                    }

                    //returning
                    if (d.Value.Returning.Total != 0)
                    {
                        eventId++;
                        TInventoryAvailabilityCalendarEvent iReturn = new TInventoryAvailabilityCalendarEvent();
                        iReturn.id = eventId.ToString(); ;
                        iReturn.InventoryId = InventoryId;
                        iReturn.WarehouseId = WarehouseId;
                        iReturn.start = startDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                        iReturn.end = endDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");
                        iReturn.text = "Returning " + ((int)d.Value.Returning.Total).ToString();
                        iReturn.backColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_RETURNING);
                        iReturn.textColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_RESERVED);
                        response.InventoryAvailabilityCalendarEvents.Add(iReturn);
                    }

                }
            }


            // build up the top-line schedule events (available quantity)
            int resourceId = 0;

            resourceId++;
            TInventoryAvailabilityScheduleResource availResource = new TInventoryAvailabilityScheduleResource();
            availResource.id = resourceId.ToString();
            availResource.name = "Available";
            response.InventoryAvailabilityScheduleResources.Add(availResource);

            DateTime theDate = FromDate;
            while (theDate <= ToDate)
            {
                TInventoryWarehouseAvailabilityDate inventoryWarehouseAvailabilityDate = null;
                if (availData.Dates.TryGetValue(theDate, out inventoryWarehouseAvailabilityDate))
                {
                    eventId++;
                    TInventoryAvailabilityScheduleEvent availEvent = new TInventoryAvailabilityScheduleEvent();
                    availEvent.id = eventId.ToString(); ;
                    availEvent.resource = resourceId.ToString();
                    availEvent.InventoryId = InventoryId;
                    availEvent.WarehouseId = WarehouseId;
                    availEvent.start = theDate.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                    availEvent.end = theDate.ToString("yyyy-MM-ddTHH:mm:ss tt");
                    availEvent.text = ((int)inventoryWarehouseAvailabilityDate.Available.Total).ToString();
                    if (inventoryWarehouseAvailabilityDate.Available.Total < 0)
                    {
                        availEvent.backColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_NEGATIVE);
                        availEvent.textColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_NEGATIVE);
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

            // build up the schedule resources and events
            eventId = 0;
            foreach (TInventoryWarehouseAvailabilityReservation reservation in availData.Reservations)
            {
                if (reservation.QuantityReserved.Total != 0)
                {
                    if ((reservation.FromDateTime <= ToDate) && (reservation.ToDateTime >= FromDate))
                    {
                        resourceId++;
                        TInventoryAvailabilityScheduleResource resource = new TInventoryAvailabilityScheduleResource();
                        resource.id = resourceId.ToString();
                        resource.name = reservation.OrderNumber;
                        //resource.backColor = FwConvert.OleColorToHtmlColor(618726); //blue
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
                        availScheduleEvent.barColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_ORDER);
                        availScheduleEvent.textColor = FwConvert.OleColorToHtmlColor(0); //black 
                        response.InventoryAvailabilityScheduleEvents.Add(availScheduleEvent);
                    }
                }
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------

        //public static async Task<AvailabilityConflictResponse> GetConflicts (FwApplicationConfig appConfig, FwUserSession userSession, AvailabilityConflictRequest request)
        public static AvailabilityConflictResponse GetConflicts(FwApplicationConfig appConfig, FwUserSession userSession, AvailabilityConflictRequest request)
        {
            AvailabilityConflictResponse response = new AvailabilityConflictResponse();

            int i = 0;
            foreach (KeyValuePair<TInventoryWarehouseAvailabilityKey, TInventoryWarehouseAvailability> availEntry in AvailabilityCache)
            {
                TInventoryWarehouseAvailability inventoryWarehouseAvailability = (TInventoryWarehouseAvailability)availEntry.Value;

                if (inventoryWarehouseAvailability.Reservations.Count > 0)
                {
                    foreach (TInventoryWarehouseAvailabilityReservation reservation in inventoryWarehouseAvailability.Reservations)
                    {
                        AvailabilityConflictResponseItem responseItem = new AvailabilityConflictResponseItem();
                        responseItem.Warehouse = inventoryWarehouseAvailability.InventoryWarehouse.Warehouse;
                        responseItem.WarehouseCode = inventoryWarehouseAvailability.InventoryWarehouse.WarehouseCode;
                        responseItem.ICode = inventoryWarehouseAvailability.InventoryWarehouse.ICode;
                        responseItem.ItemDesription = inventoryWarehouseAvailability.InventoryWarehouse.Description;
                        responseItem.QuantityIn = inventoryWarehouseAvailability.In.Total;
                        responseItem.QuantityInRepair = inventoryWarehouseAvailability.InRepair.Total;
                        responseItem.OrderNumber = reservation.OrderNumber;
                        responseItem.OrderDescription = reservation.OrderDescription;
                        responseItem.Deal = reservation.Deal;
                        responseItem.QuantityOrdered = reservation.QuantityOrdered;
                        responseItem.QuantitySub = reservation.QuantitySub;
                        responseItem.FromDateTime = reservation.FromDateTime;
                        responseItem.ToDateTime = reservation.ToDateTime;
                        response.Add(responseItem);
                    }
                    i++;
                    if (i >= 10) { break; }
                }


            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
