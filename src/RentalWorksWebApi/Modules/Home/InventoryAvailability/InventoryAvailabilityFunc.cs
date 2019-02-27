using System;
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace WebApi.Modules.Home.InventoryAvailabilityFunc
{
    //-------------------------------------------------------------------------------------------------------
    public class AvailabilityInventoryWarehouseRequest
    {
        public string SessionId { get; set; }
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
        public decimal MinimumAvailalbe { get; set; }
        public bool IsStale { get; set; }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailability
    {
        public string ICode { get; set; } = "";
        public string Description { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
        public string Warehouse { get; set; } = "";
        public bool HourlyAvailability { get; set; } = false;
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


            if (fromDateTime < DateTime.Today)
            {
                fromDateTime = DateTime.Today;
            }

            if (toDateTime < DateTime.Today)
            {
                toDateTime = DateTime.Today;
            }

            foreach (KeyValuePair<DateTime, TInventoryWarehouseAvailabilityDate> availDate in Dates)
            {
                DateTime theDate = availDate.Key;
                TInventoryWarehouseAvailabilityDate inventoryWarehouseAvailabilityDate = availDate.Value;
                if ((fromDateTime <= theDate) && (theDate <= toDateTime))
                {
                    if (theDate.Equals(fromDateTime))
                    {
                        firstDateFound = true;
                        minAvail.MinimumAvailalbe = inventoryWarehouseAvailabilityDate.Available.Total;
                    }
                    minAvail.MinimumAvailalbe = (minAvail.MinimumAvailalbe < inventoryWarehouseAvailabilityDate.Available.Total) ? minAvail.MinimumAvailalbe : inventoryWarehouseAvailabilityDate.Available.Total;
                    if (theDate.Equals(toDateTime))
                    {
                        lastDateFound = true;
                    }
                }
            }
            if ((!firstDateFound) || (!lastDateFound))
            {
                minAvail.IsStale = true;
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
        private static List<TInventoryWarehouseAvailabilityKey> AvailabilityNeedRecalc = new List<TInventoryWarehouseAvailabilityKey>();
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
        public static bool DumpAvailabilityToFile(string inventoryId = "", string warehouseId = "")
        {
            bool success = true;
            StringBuilder sb = new StringBuilder();

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

                //availData.AvailDataFromDateTime = fromDateTime;
                //availData.AvailDataToDateTime = toDateTime;
                availData.CalculatedDateTime = DateTime.Now;
            }
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<bool> RefreshAvailability(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId, TInventoryWarehouseAvailabilityRequestItems availRequestItems)
        {
            bool success = false;

            TAvailabilityCache availCache = new TAvailabilityCache();
            if (availRequestItems.Count > 0)
            {

                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                    qry.Add("select a.masterid, a.warehouseid,");
                    qry.Add("       a.masterno, a.master, a.whcode, a.warehouse, a.availbyhour,");
                    qry.Add("       a.ownedqty, a.ownedqtyin, a.ownedqtystaged, a.ownedqtyout, a.ownedqtyintransit, a.ownedqtyinrepair, a.ownedqtyontruck, a.ownedqtyincontainer,");
                    qry.Add("       a.consignedqty, a.consignedqtyin, a.consignedqtystaged, a.consignedqtyout, a.consignedqtyintransit, a.consignedqtyinrepair, a.consignedqtyontruck, a.consignedqtyincontainer");
                    qry.Add(" from  availabilitymasterwhview a");
                    if (availRequestItems.Count == 1)
                    {
                        TInventoryWarehouseAvailabilityRequestItem availRequestItem = availRequestItems[0];
                        qry.Add(" where a.masterid    = @masterid");
                        qry.Add(" and   a.warehouseid = @warehouseid");
                        qry.AddParameter("@masterid", availRequestItem.InventoryId);
                        qry.AddParameter("@warehouseid", availRequestItem.WarehouseId);
                    }
                    else if (availRequestItems.Count > 1)
                    {
                        qry.Add(" where ");
                        int i = 0;
                        foreach (TInventoryWarehouseAvailabilityRequestItem availRequestItem in availRequestItems)
                        {
                            if (i > 0)
                            {
                                qry.Add(" or ");
                            }
                            qry.Add(" (a.masterid = @masterid" + i.ToString() + " and a.warehouseid = @warehouseid" + i.ToString() + ")");
                            qry.AddParameter("@masterid" + i.ToString(), availRequestItem.InventoryId);
                            qry.AddParameter("@warehouseid" + i.ToString(), availRequestItem.WarehouseId);
                            i++;
                        }
                    }

                    FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();
                    foreach (List<object> row in dt.Rows)
                    {
                        TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(row[dt.GetColumnNo("masterid")].ToString(), row[dt.GetColumnNo("warehouseid")].ToString());

                        TInventoryWarehouseAvailability availData = new TInventoryWarehouseAvailability();

                        availData.ICode = row[dt.GetColumnNo("masterno")].ToString();
                        availData.Description = row[dt.GetColumnNo("master")].ToString();
                        availData.WarehouseCode = row[dt.GetColumnNo("whcode")].ToString();
                        availData.Warehouse = row[dt.GetColumnNo("warehouse")].ToString();
                        availData.HourlyAvailability = FwConvert.ToBoolean(row[dt.GetColumnNo("availbyhour")].ToString());

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
                    qry.Add("select a.masterid, a.warehouseid,");
                    qry.Add("       a.orderid, a.masteritemid, a.availfromdatetime, a.availtodatetime, ");
                    qry.Add("       a.ordertype, a.orderno, a.orderdesc, a.orderstatus, a.dealid, a.deal, ");
                    qry.Add("       a.qtyordered, a.subqty, a.consignqty, ");
                    qry.Add("       a.qtystagedowned, a.qtyoutowned, a.qtyinowned, ");
                    qry.Add("       a.qtystagedconsigned, a.qtyoutconsigned, a.qtyinconsigned ");
                    qry.Add(" from  availabilityitemview a");
                    if (availRequestItems.Count == 1)
                    {
                        TInventoryWarehouseAvailabilityRequestItem availRequestItem = availRequestItems[0];
                        qry.Add(" where a.masterid    = @masterid");
                        qry.Add(" and   a.warehouseid = @warehouseid");
                        qry.AddParameter("@masterid", availRequestItem.InventoryId);
                        qry.AddParameter("@warehouseid", availRequestItem.WarehouseId);
                    }
                    else if (availRequestItems.Count > 1)
                    {
                        qry.Add(" where ");
                        int i = 0;
                        foreach (TInventoryWarehouseAvailabilityRequestItem availRequestItem in availRequestItems)
                        {
                            if (i > 0)
                            {
                                qry.Add(" or ");
                            }
                            qry.Add(" (a.masterid = @masterid" + i.ToString() + " and a.warehouseid = @warehouseid" + i.ToString() + ")");
                            qry.AddParameter("@masterid" + i.ToString(), availRequestItem.InventoryId);
                            qry.AddParameter("@warehouseid" + i.ToString(), availRequestItem.WarehouseId);
                            i++;
                        }
                    }

                    FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                    // load dt into availData.Reservations
                    foreach (List<object> row in dt.Rows)
                    {
                        TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(row[dt.GetColumnNo("masterid")].ToString(), row[dt.GetColumnNo("warehouseid")].ToString());

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

                        //availData.Reservations.Add(reservation);
                        availCache[availKey].Reservations.Add(reservation);
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
                    AvailabilityCache[availKey] = availCache[availKey];
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
        public static async Task<TAvailabilityCache> GetAvailability(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId, TInventoryWarehouseAvailabilityRequestItems availRequestItems, bool refreshIfNeeded)
        {
            TAvailabilityCache availCache = new TAvailabilityCache();
            TInventoryWarehouseAvailabilityRequestItems availRequestToRefresh = new TInventoryWarehouseAvailabilityRequestItems();

            foreach (TInventoryWarehouseAvailabilityRequestItem availRequestItem in availRequestItems)
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

            if (availRequestToRefresh.Count > 0)
            {
                await RefreshAvailability(appConfig, userSession, sessionId, availRequestToRefresh);
                availCache = await GetAvailability(appConfig, userSession, sessionId, availRequestItems, false);
            }


            return availCache;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TInventoryWarehouseAvailability> GetAvailability(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId, string inventoryId, string warehouseId, DateTime fromDate, DateTime toDate, bool refreshIfNeeded)

        {
            TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);
            TInventoryWarehouseAvailabilityRequestItems availRequestItems = new TInventoryWarehouseAvailabilityRequestItems();
            availRequestItems.Add(new TInventoryWarehouseAvailabilityRequestItem(inventoryId, warehouseId, fromDate, toDate));

            TAvailabilityCache availCache = await GetAvailability(appConfig, userSession, sessionId, availRequestItems, refreshIfNeeded);
            TInventoryWarehouseAvailability availData = new TInventoryWarehouseAvailability();
            availCache.TryGetValue(availKey, out availData);
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




        public static async Task<TInventoryAvailabilityCalendarAndScheduleResponse> GetCalendarAndScheduleData(FwApplicationConfig appConfig, FwUserSession userSession, string SessionId, string InventoryId, string WarehouseId, DateTime FromDate, DateTime ToDate)

        {
            TInventoryAvailabilityCalendarAndScheduleResponse response = new TInventoryAvailabilityCalendarAndScheduleResponse();

            TInventoryWarehouseAvailability availData = await GetAvailability(appConfig, userSession, SessionId, InventoryId, WarehouseId, FromDate, ToDate, true);


            // build up the calendar events
            int eventId = 0;
            foreach (KeyValuePair<DateTime, TInventoryWarehouseAvailabilityDate> d in availData.Dates)
            {
                //available
                eventId++;
                TInventoryAvailabilityCalendarEvent iAvail = new TInventoryAvailabilityCalendarEvent();
                iAvail.id = eventId.ToString(); ;
                iAvail.InventoryId = InventoryId;
                iAvail.WarehouseId = WarehouseId;
                iAvail.start = d.Key.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                iAvail.end = d.Key.ToString("yyyy-MM-ddTHH:mm:ss tt");
                iAvail.text = "Available " + ((int)d.Value.Available.Total).ToString();
                if (d.Value.Available.Total < 0)
                {
                    iAvail.backColor = FwConvert.OleColorToHtmlColor(16711680); //red
                    iAvail.textColor = FwConvert.OleColorToHtmlColor(16777215); //white
                }
                else
                {
                    iAvail.backColor = FwConvert.OleColorToHtmlColor(1176137); //green
                    iAvail.textColor = FwConvert.OleColorToHtmlColor(0); //black
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
                    iReserve.start = d.Key.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                    iReserve.end = d.Key.ToString("yyyy-MM-ddTHH:mm:ss tt");
                    iReserve.text = "Reserved " + ((int)d.Value.Reserved.Total).ToString();
                    iReserve.backColor = FwConvert.OleColorToHtmlColor(15132390); //gray
                    iReserve.textColor = FwConvert.OleColorToHtmlColor(0); //black
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
                    iReturn.start = d.Key.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                    iReturn.end = d.Key.ToString("yyyy-MM-ddTHH:mm:ss tt");
                    iReturn.text = "Returning " + ((int)d.Value.Returning.Total).ToString();
                    iReturn.backColor = FwConvert.OleColorToHtmlColor(618726); //blue
                    iReturn.textColor = FwConvert.OleColorToHtmlColor(16777215); //white
                    response.InventoryAvailabilityCalendarEvents.Add(iReturn);
                }

            }

            // build up the schedule resources and events
            int resourceId = 0;
            eventId = 0;
            foreach (TInventoryWarehouseAvailabilityReservation reservation in availData.Reservations)
            {
                if ((reservation.FromDateTime <= ToDate) && (reservation.ToDateTime >= FromDate))
                {
                    resourceId++;
                    TInventoryAvailabilityScheduleResource resource = new TInventoryAvailabilityScheduleResource();
                    resource.id = resourceId.ToString();
                    resource.name = reservation.OrderNumber;
                    response.InventoryAvailabilityScheduleResources.Add(resource);


                    eventId++;
                    TInventoryAvailabilityScheduleEvent availScheduleEvent = new TInventoryAvailabilityScheduleEvent();
                    availScheduleEvent.id = eventId.ToString();
                    availScheduleEvent.resource = resourceId.ToString();
                    availScheduleEvent.start = reservation.FromDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                    availScheduleEvent.end = reservation.ToDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");
                    availScheduleEvent.text = "";
                    availScheduleEvent.orderNumber = reservation.OrderNumber;
                    availScheduleEvent.orderStatus = reservation.OrderStatus;
                    availScheduleEvent.deal = reservation.Deal;
                    availScheduleEvent.backColor = FwConvert.OleColorToHtmlColor(618726); //blue   (temporary)
                    availScheduleEvent.textColor = FwConvert.OleColorToHtmlColor(16777215); //white  (temporary)
                    response.InventoryAvailabilityScheduleEvents.Add(availScheduleEvent);


                }
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
