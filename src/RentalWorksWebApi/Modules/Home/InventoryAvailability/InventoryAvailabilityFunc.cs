using System;
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using WebLibrary;
using WebApi.Logic;

namespace WebApi.Modules.Home.InventoryAvailabilityFunc
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
        public decimal MinimumAvailable { get; set; }
        public DateTime? FirstConfict { get; set; }
        public bool NoAvailabilityCheck { get; set; }
        public bool IsStale { get; set; }
        public string Color { get; set; }
        public string TextColor { get; set; }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailability
    {
        public TInventoryWarehouseAvailability(string inventoryId, string warehouseId)
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
        public bool HourlyAvailability { get; set; } = false;
        public bool NoAvailabilityCheck { get; set; } = false;
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
        public Dictionary<DateTime, TInventoryWarehouseAvailabilityDate> Dates { get; set; } = new Dictionary<DateTime, TInventoryWarehouseAvailabilityDate>();
        public TInventoryWarehouseAvailabilityMinimum GetMinimumAvailableQuantity(DateTime fromDateTime, DateTime toDateTime)
        {
            TInventoryWarehouseAvailabilityMinimum minAvail = new TInventoryWarehouseAvailabilityMinimum();
            bool firstDateFound = false;
            bool lastDateFound = false;
            bool isStale = false;
            bool isHistory = false;

            minAvail.NoAvailabilityCheck = NoAvailabilityCheck;

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
                foreach (KeyValuePair<DateTime, TInventoryWarehouseAvailabilityDate> availDate in Dates)
                {
                    DateTime theDate = availDate.Key;
                    TInventoryWarehouseAvailabilityDate inventoryWarehouseAvailabilityDate = availDate.Value;
                    if ((fromDateTime <= theDate) && (theDate <= toDateTime))
                    {
                        if (theDate.Equals(fromDateTime))
                        {
                            firstDateFound = true;
                            minAvail.MinimumAvailable = inventoryWarehouseAvailabilityDate.Available.Total;
                        }
                        minAvail.MinimumAvailable = (minAvail.MinimumAvailable < inventoryWarehouseAvailabilityDate.Available.Total) ? minAvail.MinimumAvailable : inventoryWarehouseAvailabilityDate.Available.Total;

                        if ((minAvail.FirstConfict == null) && (minAvail.MinimumAvailable < 0))
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
                    isStale = InventoryAvailabilityFunc.AvailabilityNeedRecalc.Contains(new TInventoryWarehouseAvailabilityKey(InventoryId, WarehouseId));
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
                else if (minAvail.MinimumAvailable < 0)
                {
                    minAvail.Color = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_NEGATIVE);
                    minAvail.TextColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_NEGATIVE);
                }
            }

            return minAvail;
        }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TAvailabilityCache : Dictionary<TInventoryWarehouseAvailabilityKey, TInventoryWarehouseAvailability>
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
        private static Dictionary<string, TAvailabilityProgress> AvailabilitySessions = new Dictionary<string, TAvailabilityProgress>();
        public static List<TInventoryWarehouseAvailabilityKey> AvailabilityNeedRecalc = new List<TInventoryWarehouseAvailabilityKey>();
        //private static int LastNeedRecalcId = 0;
        private static TAvailabilityCache AvailabilityCache = new TAvailabilityCache();
        //-------------------------------------------------------------------------------------------------------
        public static TAvailabilityProgress GetAvailabilityProgress(string sessionId)
        {
            TAvailabilityProgress availabilityProgress = new TAvailabilityProgress();
            AvailabilitySessions.TryGetValue(sessionId, out availabilityProgress);
            return availabilityProgress;
        }
        //-------------------------------------------------------------------------------------------------------
        public static void SetAvailabilityProgress(string sessionId, int percentComplete)
        {
            TAvailabilityProgress availabilityProgress = new TAvailabilityProgress();
            availabilityProgress.PercentComplete = percentComplete;
            AvailabilitySessions[sessionId] = availabilityProgress;
        }
        //-------------------------------------------------------------------------------------------------------
        public static void RemoveAvailabilityProgress(string sessionId)
        {
            AvailabilitySessions.Remove(sessionId);
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<bool> CheckNeedRecalc(FwApplicationConfig appConfig, FwUserSession userSession)
        {
            bool success = true;

            //temporary
            await Task.CompletedTask; // get rid of the no async call warning

            //using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            //{
            //    FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
            //    qry.Add("select a.masterid, a.warehouseid, a.id");
            //    qry.Add(" from  tmpavailneedrecalc a");
            //    qry.Add(" where a.id > @lastneedrecalcid");
            //    qry.Add("order by a.id");
            //    qry.AddParameter("@lastneedrecalcid", LastNeedRecalcId);
            //    FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

            //    int needRecalcId = 0;
            //    foreach (List<object> row in dt.Rows)
            //    {
            //        string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
            //        string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
            //        needRecalcId = FwConvert.ToInt32(row[dt.GetColumnNo("id")]);
            //        AvailabilityNeedRecalc.Add(new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId));
            //    }
            //    if (needRecalcId > 0)
            //    {
            //        LastNeedRecalcId = needRecalcId;
            //    }
            //}

            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public static bool DumpAvailabilityToFile(string inventoryId = "", string warehouseId = "")
        {
            bool success = true;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("------- NEED RECALCULATION  ------------------------------------");

            foreach (TInventoryWarehouseAvailabilityKey availKey in AvailabilityNeedRecalc)
            {
                bool includeInFile = true;

                if (includeInFile)
                {
                    if (!string.IsNullOrEmpty(inventoryId))
                    {
                        includeInFile = (availKey.InventoryId.Equals(inventoryId));
                    }
                }

                if (includeInFile)
                {
                    if (!string.IsNullOrEmpty(warehouseId))
                    {
                        includeInFile = (availKey.WarehouseId.Equals(warehouseId));
                    }
                }

                if (includeInFile)
                {
                    sb.Append("InventoryId: ");
                    sb.Append(availKey.InventoryId);
                    sb.Append(" ");
                    sb.Append("WarehouseId: ");
                    sb.Append(availKey.WarehouseId);
                    sb.AppendLine();
                }
            }
            sb.AppendLine();

            foreach (KeyValuePair<TInventoryWarehouseAvailabilityKey, TInventoryWarehouseAvailability> availEntry in AvailabilityCache)
            {
                bool includeInFile = true;

                if (includeInFile)
                {
                    if (!string.IsNullOrEmpty(inventoryId))
                    {
                        includeInFile = (availEntry.Key.InventoryId.Equals(inventoryId));
                    }
                }

                if (includeInFile)
                {
                    if (!string.IsNullOrEmpty(warehouseId))
                    {
                        includeInFile = (availEntry.Key.WarehouseId.Equals(warehouseId));
                    }
                }

                if (includeInFile)
                {
                    TInventoryWarehouseAvailability inventoryWarehouseAvailability = (TInventoryWarehouseAvailability)availEntry.Value;
                    sb.Append("InventoryId: ");
                    sb.Append(availEntry.Key.InventoryId);
                    sb.Append(" ");
                    sb.Append("WarehouseId: ");
                    sb.Append(availEntry.Key.WarehouseId);
                    sb.AppendLine();

                    sb.Append("   ");
                    sb.Append("I-Code:".PadRight(14));
                    sb.Append(inventoryWarehouseAvailability.ICode);
                    sb.AppendLine();
                    sb.Append("   ");
                    sb.Append("Description:".PadRight(14));
                    sb.Append(inventoryWarehouseAvailability.Description);
                    sb.AppendLine();
                    sb.Append("   ");
                    sb.Append("Warehouse:".PadRight(14));
                    sb.Append(inventoryWarehouseAvailability.Warehouse);
                    sb.Append("(");
                    sb.Append(inventoryWarehouseAvailability.WarehouseCode);
                    sb.Append(")");
                    sb.AppendLine();
                    sb.Append("   ");
                    sb.Append("Calculated:".PadRight(14));
                    sb.Append(inventoryWarehouseAvailability.CalculatedDateTime);
                    sb.AppendLine();

                    sb.AppendLine();
                    sb.Append("   ");
                    sb.AppendLine("------- AVAILABILITY BEHAVIOR ------------------------------------");
                    sb.Append("   ");
                    sb.Append("No Availability Check: ");
                    sb.Append(inventoryWarehouseAvailability.NoAvailabilityCheck.ToString());
                    sb.AppendLine();
                    sb.Append("   ");
                    sb.Append("Hourly Availability: ");
                    sb.Append(inventoryWarehouseAvailability.HourlyAvailability.ToString());
                    sb.AppendLine();

                    sb.AppendLine();
                    sb.Append("   ");
                    sb.AppendLine("------- CURRENT QUANTITIES ------------------------------------");
                    sb.Append("   ");
                    sb.Append("Ownership".PadRight(13));
                    sb.Append("Total".PadLeft(13));
                    sb.Append("In".PadLeft(13));
                    sb.Append("Staged".PadLeft(13));
                    sb.Append("Out".PadLeft(13));
                    sb.Append("In Transit".PadLeft(13));
                    sb.Append("In Repair".PadLeft(13));
                    sb.Append("On Truck".PadLeft(13));
                    sb.Append("In Container".PadLeft(13));
                    sb.AppendLine();

                    sb.Append("   ");
                    sb.Append("Owned".PadRight(13));
                    sb.Append(inventoryWarehouseAvailability.Total.Owned.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.In.Owned.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.Staged.Owned.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.Out.Owned.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.InTransit.Owned.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.InRepair.Owned.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.OnTruck.Owned.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.InContainer.Owned.ToString().PadLeft(13));
                    sb.AppendLine();

                    sb.Append("   ");
                    sb.Append("Consigned".PadRight(13));
                    sb.Append(inventoryWarehouseAvailability.Total.Consigned.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.In.Consigned.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.Staged.Consigned.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.Out.Consigned.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.InTransit.Consigned.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.InRepair.Consigned.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.OnTruck.Consigned.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.InContainer.Consigned.ToString().PadLeft(13));
                    sb.AppendLine();

                    sb.Append("   ");
                    sb.Append("Total".PadRight(13));
                    sb.Append(inventoryWarehouseAvailability.Total.Total.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.In.Total.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.Staged.Total.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.Out.Total.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.InTransit.Total.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.InRepair.Total.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.OnTruck.Total.ToString().PadLeft(13));
                    sb.Append(inventoryWarehouseAvailability.InContainer.Total.ToString().PadLeft(13));
                    sb.AppendLine();

                    sb.AppendLine();
                    sb.Append("   ");
                    sb.AppendLine("------- RESERVATIONS ------------------------------------");
                    foreach (TInventoryWarehouseAvailabilityReservation reservation in inventoryWarehouseAvailability.Reservations)
                    {
                        sb.Append("   ");
                        sb.Append("OrderId: ");
                        sb.Append(reservation.OrderId.PadLeft(8));
                        sb.Append(" ");
                        sb.Append("OrderItemId: ");
                        sb.Append(reservation.OrderItemId.PadLeft(8));
                        sb.AppendLine();
                        sb.Append("   ");
                        sb.AppendLine("-------------------------------------------");

                        sb.Append("       ");
                        sb.Append("Type:".PadRight(15));
                        sb.Append(reservation.OrderType.ToString());
                        sb.AppendLine();
                        sb.Append("       ");
                        sb.Append("Number:".PadRight(15));
                        sb.Append(reservation.OrderNumber.ToString());
                        sb.AppendLine();
                        sb.Append("       ");
                        sb.Append("Description:".PadRight(15));
                        sb.Append(reservation.OrderDescription.ToString());
                        sb.AppendLine();
                        sb.Append("       ");
                        sb.Append("Status:".PadRight(15));
                        sb.Append(reservation.OrderStatus.ToString());
                        sb.AppendLine();
                        sb.Append("       ");
                        sb.Append("Deal:".PadRight(15));
                        sb.Append(reservation.Deal.ToString());
                        sb.Append(" (");
                        sb.Append(reservation.DealId.ToString());
                        sb.Append(")");
                        sb.AppendLine();
                        sb.Append("       ");
                        sb.Append("Period:".PadRight(15));
                        sb.Append(reservation.FromDateTime.ToString());
                        sb.Append(" - ");
                        sb.Append(reservation.ToDateTime.ToString());
                        sb.AppendLine();



                        sb.AppendLine("       Quantities:");
                        sb.Append("       ");
                        sb.AppendLine("-------------------------------------------");

                        sb.Append("           ");
                        sb.Append("Ordered:".PadRight(15));
                        sb.Append(reservation.QuantityOrdered.ToString().PadLeft(10));
                        sb.AppendLine();
                        sb.Append("           ");
                        sb.Append("Sub:".PadRight(15));
                        sb.Append(reservation.QuantitySub.ToString().PadLeft(10));
                        sb.AppendLine();
                        sb.Append("           ");
                        sb.Append("Consign:".PadRight(15));
                        sb.Append(reservation.QuantityConsigned.ToString().PadLeft(10));
                        sb.AppendLine();


                        sb.AppendLine("       Statuses:");
                        sb.Append("       ");
                        sb.AppendLine("-------------------------------------------");
                        sb.Append("           ");
                        sb.Append("Ownership".PadRight(13));
                        sb.Append("Reserved".PadLeft(13));
                        sb.Append("Staged".PadLeft(13));
                        sb.Append("Out".PadLeft(13));
                        sb.Append("In".PadLeft(13));
                        sb.AppendLine();

                        sb.Append("           ");
                        sb.Append("Owned".PadRight(13));
                        sb.Append(reservation.QuantityReserved.Owned.ToString().PadLeft(13));
                        sb.Append(reservation.QuantityStaged.Owned.ToString().PadLeft(13));
                        sb.Append(reservation.QuantityOut.Owned.ToString().PadLeft(13));
                        sb.Append(reservation.QuantityIn.Owned.ToString().PadLeft(13));
                        sb.AppendLine();
                        sb.Append("           ");
                        sb.Append("Consigned".PadRight(13));
                        sb.Append(reservation.QuantityReserved.Consigned.ToString().PadLeft(13));
                        sb.Append(reservation.QuantityStaged.Consigned.ToString().PadLeft(13));
                        sb.Append(reservation.QuantityOut.Consigned.ToString().PadLeft(13));
                        sb.Append(reservation.QuantityIn.Consigned.ToString().PadLeft(13));
                        sb.AppendLine();
                        sb.Append("           ");
                        sb.Append("Total".PadRight(13));
                        sb.Append(reservation.QuantityReserved.Total.ToString().PadLeft(13));
                        sb.Append(reservation.QuantityStaged.Total.ToString().PadLeft(13));
                        sb.Append(reservation.QuantityOut.Total.ToString().PadLeft(13));
                        sb.Append(reservation.QuantityIn.Total.ToString().PadLeft(13));
                        sb.AppendLine();
                        sb.AppendLine();



                    }
                    sb.AppendLine();
                    sb.Append("   ");
                    sb.AppendLine("-------AVAILABILITY DATES/HOURS ----------------------------------");
                    sb.Append("   ");
                    sb.Append("From:".PadRight(6));
                    sb.Append(inventoryWarehouseAvailability.AvailDataFromDateTime.ToString().PadLeft(26));
                    sb.AppendLine();
                    sb.Append("   ");
                    sb.Append("To:".PadRight(6));
                    sb.Append(inventoryWarehouseAvailability.AvailDataToDateTime.ToString().PadLeft(26));
                    sb.AppendLine();
                    sb.AppendLine("--------------------------------------------");
                    sb.AppendLine();
                    foreach (KeyValuePair<DateTime, TInventoryWarehouseAvailabilityDate> availDateEntry in inventoryWarehouseAvailability.Dates)
                    {
                        TInventoryWarehouseAvailabilityDate inventoryWarehouseAvailabilityDate = (TInventoryWarehouseAvailabilityDate)availDateEntry.Value;
                        sb.Append("   ");
                        sb.Append("Date: ");
                        sb.Append(inventoryWarehouseAvailabilityDate.AvailabilityDate.ToString().PadLeft(26));
                        sb.AppendLine();

                        sb.Append("      ");
                        sb.Append("Ownership".PadRight(13));
                        sb.Append("Available".PadLeft(13));
                        sb.Append("Reserved".PadLeft(13));
                        sb.Append("Returning".PadLeft(13));
                        sb.AppendLine();

                        sb.Append("      ");
                        sb.Append("Owned".PadRight(13));
                        sb.Append(inventoryWarehouseAvailabilityDate.Available.Owned.ToString().PadLeft(13));
                        sb.Append(inventoryWarehouseAvailabilityDate.Reserved.Owned.ToString().PadLeft(13));
                        sb.Append(inventoryWarehouseAvailabilityDate.Returning.Owned.ToString().PadLeft(13));
                        sb.AppendLine();
                        sb.Append("      ");
                        sb.Append("Consigned".PadRight(13));
                        sb.Append(inventoryWarehouseAvailabilityDate.Available.Consigned.ToString().PadLeft(13));
                        sb.Append(inventoryWarehouseAvailabilityDate.Reserved.Consigned.ToString().PadLeft(13));
                        sb.Append(inventoryWarehouseAvailabilityDate.Returning.Consigned.ToString().PadLeft(13));
                        sb.AppendLine();
                        sb.Append("      ");
                        sb.Append("Total".PadRight(13));
                        sb.Append(inventoryWarehouseAvailabilityDate.Available.Total.ToString().PadLeft(13));
                        sb.Append(inventoryWarehouseAvailabilityDate.Reserved.Total.ToString().PadLeft(13));
                        sb.Append(inventoryWarehouseAvailabilityDate.Returning.Total.ToString().PadLeft(13));
                        sb.AppendLine();
                    }
                    sb.AppendLine();
                    sb.AppendLine();
                }
            }
            System.IO.File.WriteAllText(@"C:\temp\availability.txt", sb.ToString());
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        private static void ProjectFutureAvailability(ref TAvailabilityCache availCache)
        {
            foreach (KeyValuePair<TInventoryWarehouseAvailabilityKey, TInventoryWarehouseAvailability> availEntry in availCache)
            {
                TInventoryWarehouseAvailabilityKey availKey = availEntry.Key;
                TInventoryWarehouseAvailability availData = availEntry.Value;
                DateTime fromDateTime = DateTime.Today; //daily availability
                DateTime toDateTime = availData.AvailDataToDateTime;
                if (!availData.NoAvailabilityCheck)
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

                        availData.Dates.Add(theDateTime, inventoryWarehouseAvailabilityDate);

                        theDateTime = theDateTime.AddDays(1); //daily availability
                                                              //hourly availability?
                    }

                    availData.CalculatedDateTime = DateTime.Now;
                }
                AvailabilityNeedRecalc.RemoveAll(k => k.Equals(availKey));
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
                    }
                    qry.AddParameter("@sessionid", sessionId);
                    await qry.ExecuteNonQueryAsync();
                }


                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                    qry.Add("select a.masterid, a.warehouseid,");
                    qry.Add("       a.masterno, a.master, a.whcode, /*a.noavail, */a.warehouse, a.availbyhour,");
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

                        availData.ICode = row[dt.GetColumnNo("masterno")].ToString();
                        availData.Description = row[dt.GetColumnNo("master")].ToString();
                        availData.WarehouseCode = row[dt.GetColumnNo("whcode")].ToString();
                        availData.Warehouse = row[dt.GetColumnNo("warehouse")].ToString();
                        availData.HourlyAvailability = FwConvert.ToBoolean(row[dt.GetColumnNo("availbyhour")].ToString());
                        //availData.NoAvailabilityCheck = FwConvert.ToBoolean(row[dt.GetColumnNo("noavail")].ToString());

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
                        //jh 02/28/2019 this is a bottleneck as the query must join in ordertranextended to get the consignorid.  Consider modving consignorid to the ordetran table
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
                    //AvailabilityCache[availKey] = availCache[availKey];
                    //#jhtodo - need exclusive access to the AvailabilityCache object before adding/updating below
                    if (!AvailabilityCache.TryAdd(availKey, availCache[availKey]))  // try to add the value.  if not allowed, then updated it below
                    {
                        AvailabilityCache[availKey] = availCache[availKey];
                    }
                }

                success = true;

            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public static void DeleteAvailability(FwApplicationConfig appConfig, FwUserSession userSession, string inventoryId, string warehouseId)
        {
            TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);
            AvailabilityCache.Remove(availKey);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TAvailabilityCache> GetAvailability(FwApplicationConfig appConfig, FwUserSession userSession, TInventoryWarehouseAvailabilityRequestItems availRequestItems, bool refreshIfNeeded)
        {
            TAvailabilityCache availCache = new TAvailabilityCache();
            TInventoryWarehouseAvailabilityRequestItems availRequestToRefresh = new TInventoryWarehouseAvailabilityRequestItems();

            await CheckNeedRecalc(appConfig, userSession);

            foreach (TInventoryWarehouseAvailabilityRequestItem availRequestItem in availRequestItems)
            {
                if ((!availRequestItem.InventoryId.Equals("undefined")) && (!availRequestItem.WarehouseId.Equals("undefined")))
                {
                    bool foundInCache = false;
                    bool stale = false;

                    TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(availRequestItem.InventoryId, availRequestItem.WarehouseId);
                    DateTime fromDate = availRequestItem.FromDateTime;
                    DateTime toDate = availRequestItem.ToDateTime;

                    if (AvailabilityNeedRecalc.Contains(availKey))
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

                    TInventoryWarehouseAvailability availData = null;
                    if (AvailabilityCache.TryGetValue(availKey, out availData))
                    {
                        foundInCache = true;
                        DateTime theDate = availData.AvailDataFromDateTime;
                        while (theDate <= availData.AvailDataToDateTime)
                        {
                            if ((theDate < fromDate) || (toDate < theDate))
                            {
                                availData.Dates.Remove(theDate);
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


        //public static async Task<TAvailabilityCache> GetAvailability(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId, string orderId, bool refreshIfNeeded)
        //{
        //    TAvailabilityCache availCache = new TAvailabilityCache();
        //    TInventoryWarehouseAvailabilityKeys availKeys = new TInventoryWarehouseAvailabilityKeys();
        //    TInventoryWarehouseAvailabilityKeys availKeysToRefresh = new TInventoryWarehouseAvailabilityKeys();

        //    DateTime batchFromDateTime = DateTime.MinValue;
        //    DateTime batchToDateTime = DateTime.MaxValue;

        //    using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
        //    {
        //        FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
        //        qry.Add("select a.masterid, a.warehouseid,");
        //        qry.Add("       a.orderid, a.masteritemid, a.availfromdatetime, a.availtodatetime, ");
        //        qry.Add("       a.ordertype, a.orderno, a.orderdesc, a.orderstatus, a.dealid, a.deal, ");
        //        qry.Add("       a.qtyordered, a.subqty, a.consignqty, ");
        //        qry.Add("       a.qtystagedowned, a.qtyoutowned, a.qtyinowned, ");
        //        qry.Add("       a.qtystagedconsigned, a.qtyoutconsigned, a.qtyinconsigned ");
        //        qry.Add(" from  availabilityitemview a");
        //        qry.Add(" where a.orderid = @orderid");
        //        qry.AddParameter("@orderid", orderId);

        //        FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

        //        // load dt into list of keys to calculate
        //        int i = 0;
        //        foreach (List<object> row in dt.Rows)
        //        {
        //            string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
        //            string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
        //            DateTime fromDateTime = FwConvert.ToDateTime(row[dt.GetColumnNo("availfromdatetime")].ToString());
        //            DateTime toDateTime = FwConvert.ToDateTime(row[dt.GetColumnNo("availtodatetime")].ToString());

        //            if (i == 0)
        //            {
        //                batchFromDateTime = fromDateTime;
        //                batchToDateTime = toDateTime;
        //            }
        //            else
        //            {
        //                batchFromDateTime = ((DateTime.Compare(fromDateTime, batchFromDateTime) < 0) ? fromDateTime : batchFromDateTime);
        //                batchToDateTime = ((DateTime.Compare(toDateTime, batchToDateTime) < 0) ? batchToDateTime : toDateTime);
        //            }
        //            TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);
        //            availKeys.Add(availKey);
        //            i++;
        //        }
        //    }


        //    if (batchFromDateTime < DateTime.Today)
        //    {
        //        batchFromDateTime = DateTime.Today;
        //    }

        //    if (batchToDateTime < DateTime.Today)
        //    {
        //        batchToDateTime = DateTime.Today;
        //    }

        //    foreach (TInventoryWarehouseAvailabilityKey availKey in availKeys)
        //    {
        //        bool foundInCache = false;
        //        bool stale = false;

        //        if (AvailabilityNeedRecalc.Contains(availKey))
        //        {
        //            stale = true;
        //        }

        //        TInventoryWarehouseAvailability availData = null;
        //        if (AvailabilityCache.TryGetValue(availKey, out availData))
        //        {
        //            foundInCache = true;
        //            DateTime theDate = availData.AvailDataFromDateTime;
        //            while (theDate <= availData.AvailDataToDateTime)
        //            {
        //                if ((theDate < batchFromDateTime) || (batchToDateTime < theDate))
        //                {
        //                    availData.Dates.Remove(theDate);
        //                }
        //                theDate = theDate.AddDays(1);
        //            }

        //            theDate = batchFromDateTime;
        //            while (theDate <= batchToDateTime)
        //            {
        //                if (!availData.Dates.ContainsKey(theDate))
        //                {
        //                    foundInCache = false;
        //                    break;
        //                }
        //                theDate = theDate.AddDays(1);
        //            }
        //            availCache.Add(availKey, availData);
        //        }

        //        if ((stale) || (!foundInCache))
        //        {
        //            if (refreshIfNeeded)
        //            {
        //                availKeysToRefresh.Add(availKey);
        //            }
        //        }
        //    }

        //    if (availKeysToRefresh.Count > 0)
        //    {
        //        await RefreshAvailability(appConfig, userSession, sessionId, availKeysToRefresh, batchToDateTime);
        //        availCache = await GetAvailability(appConfig, userSession, sessionId, orderId, false);
        //    }

        //    return availCache;
        //}
        ////-------------------------------------------------------------------------------------------------------




        public static async Task<TInventoryAvailabilityCalendarAndScheduleResponse> GetCalendarAndScheduleData(FwApplicationConfig appConfig, FwUserSession userSession, string InventoryId, string WarehouseId, DateTime FromDate, DateTime ToDate)

        {
            TInventoryAvailabilityCalendarAndScheduleResponse response = new TInventoryAvailabilityCalendarAndScheduleResponse();

            TInventoryWarehouseAvailability availData = await GetAvailability(appConfig, userSession, InventoryId, WarehouseId, FromDate, ToDate, true);

            int eventId = 0;

            if (availData != null)
            {
                // build up the calendar events
                //currently hard-coded for "daily" availability.  will need mods to work for "hourly"
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

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
