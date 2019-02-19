using System;
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebLibrary;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace WebApi.Modules.Home.InventoryAvailabilityFunc
{
    //-------------------------------------------------------------------------------------------------------
    public class TAvailabilityProgress
    {
        public int PercentComplete { get; set; } = 0;
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityQuantity
    {
        public decimal Owned { get; set; } = 0;
        public decimal Consigned { get; set; } = 0;
        public decimal Total { get; set; } = 0;
    }
    public class TInventoryWarehouseAvailabilityReservation
    {
        public string OrderId { get; set; }
        public string OrderItemId { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
        public decimal QuantityOrdered { get; set; } = 0;
        public decimal QuantitySub { get; set; } = 0;
        public decimal QuantityConsigned { get; set; } = 0;
        public TInventoryWarehouseAvailabilityQuantity QuantityReserved { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity QuantityStaged { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity QuantityOut { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity QuantityIn { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityDate
    {
        public DateTime AvailabilityDate { get; set; }
        public TInventoryWarehouseAvailabilityQuantity Available { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity Reserved { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity Returning { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityKey
    {
        public string InventoryId { get; set; } = "";
        public string WarehouseId { get; set; } = "";
    }
    public class TInventoryWarehouseAvailability
    {
        //public TInventoryWarehouseAvailabilityKey Key = new TInventoryWarehouseAvailabilityKey();
        public TInventoryWarehouseAvailabilityQuantity Current { get; set; } = new TInventoryWarehouseAvailabilityQuantity();

        public List<TInventoryWarehouseAvailabilityReservation> Reservations = new List<TInventoryWarehouseAvailabilityReservation>();
        public Dictionary<DateTime, TInventoryWarehouseAvailabilityDate> Dates = new Dictionary<DateTime, TInventoryWarehouseAvailabilityDate>();
    }
    //-------------------------------------------------------------------------------------------------------

    public static class InventoryAvailabilityFunc
    {
        //-------------------------------------------------------------------------------------------------------
        private static Dictionary<string, TAvailabilityProgress> AvailabilitySessions = new Dictionary<string, TAvailabilityProgress>();
        private static List<TInventoryWarehouseAvailabilityKey> AvailabilityNeedRecalc = new List<TInventoryWarehouseAvailabilityKey>();
        private static Dictionary<TInventoryWarehouseAvailabilityKey, TInventoryWarehouseAvailability> AvailabilityCache = new Dictionary<TInventoryWarehouseAvailabilityKey, TInventoryWarehouseAvailability>();
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
                    if (string.IsNullOrEmpty(inventoryId))
                    {
                        includeInFile = (availEntry.Key.InventoryId.Equals(inventoryId));
                    }
                }

                if (includeInFile)
                {
                    if (string.IsNullOrEmpty(warehouseId))
                    {
                        includeInFile = (availEntry.Key.WarehouseId.Equals(warehouseId));
                    }
                }

                if (includeInFile)
                {
                    TInventoryWarehouseAvailability inventoryWarehouseAvailability = (TInventoryWarehouseAvailability)availEntry.Value;
                    sb.Append("InventoryId: ");
                    sb.Append(availEntry.Key.InventoryId);
                    sb.Append(", ");
                    sb.Append("WarehouseId: ");
                    sb.Append(availEntry.Key.WarehouseId);
                    sb.Append(", ");
                    sb.AppendLine();
                    sb.Append("   ");
                    sb.Append("Current Owned Quantity: ");
                    sb.Append(inventoryWarehouseAvailability.Current.Owned.ToString());
                    sb.Append(", ");
                    sb.Append("Current Consigned Quantity: ");
                    sb.Append(inventoryWarehouseAvailability.Current.Consigned.ToString());
                    sb.Append(", ");
                    sb.Append("Current Total Quantity: ");
                    sb.Append(inventoryWarehouseAvailability.Current.Total.ToString());
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
                        sb.Append(" ");
                        sb.Append("From Date/Time: ");
                        sb.Append(reservation.FromDateTime.ToString().PadLeft(26));
                        sb.Append(" ");
                        sb.Append("To Date/Time: ");
                        sb.Append(reservation.ToDateTime.ToString().PadLeft(26));
                        sb.Append(" ");
                        sb.Append("Quantity Ordered: ");
                        sb.Append(reservation.QuantityOrdered.ToString().PadLeft(26));
                        sb.AppendLine();
                    }
                    sb.AppendLine();
                    sb.Append("   ");
                    sb.AppendLine("------- DATES ------------------------------------");
                    foreach (KeyValuePair<DateTime, TInventoryWarehouseAvailabilityDate> availDateEntry in inventoryWarehouseAvailability.Dates)
                    {
                        TInventoryWarehouseAvailabilityDate inventoryWarehouseAvailabilityDate = (TInventoryWarehouseAvailabilityDate)availDateEntry.Value;
                        sb.Append("   ");
                        sb.Append("Date: ");
                        sb.Append(inventoryWarehouseAvailabilityDate.AvailabilityDate.ToShortDateString().PadLeft(10));
                        sb.Append(" ");
                        sb.Append("Owned Available: ");
                        sb.Append(inventoryWarehouseAvailabilityDate.Available.Owned.ToString());
                        sb.Append(" ");
                        sb.Append("Consigned Available: ");
                        sb.Append(inventoryWarehouseAvailabilityDate.Available.Consigned.ToString());
                        sb.Append(" ");
                        sb.Append("Total Available: ");
                        sb.Append(inventoryWarehouseAvailabilityDate.Available.Total.ToString());
                        sb.AppendLine();
                    }
                    sb.AppendLine();
                }
            }
            System.IO.File.WriteAllText(@"C:\temp\availability.txt", sb.ToString());
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<bool> RefreshAvailability(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId, string inventoryId, string warehouseId, DateTime toDate)
        {
            bool success = false;
            TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey();
            availKey.InventoryId = inventoryId;
            availKey.WarehouseId = warehouseId;

            TInventoryWarehouseAvailability availData = new TInventoryWarehouseAvailability();

            if (toDate < DateTime.Today)
            {
                toDate = DateTime.Today;
            }

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select q.qty ");
                qry.Add(" from  masterwhqty q");
                qry.Add(" where q.masterid    = @masterid");
                qry.Add(" and   q.warehouseid = @warehouseid");
                qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, inventoryId);
                qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, warehouseId);

                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();
                if (dt.Rows.Count > 0)
                {
                    availData.Current.Total = FwConvert.ToDecimal(dt.Rows[0][dt.GetColumnNo("qty")].ToString());
                    availData.Current.Consigned = 0;  // temporary
                    availData.Current.Owned = (availData.Current.Total - availData.Current.Consigned);
                }
            }

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select a.orderid, a.masteritemid, a.availfromdatetime, a.availtodatetime, a.qtyordered ");
                qry.Add(" from  availabilityitemview a");
                qry.Add(" where a.masterid    = @masterid");
                qry.Add(" and   a.warehouseid = @warehouseid");
                qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, inventoryId);
                qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, warehouseId);

                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                // load dt into availData.Reservations
                availData.Reservations = new List<TInventoryWarehouseAvailabilityReservation>();
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    TInventoryWarehouseAvailabilityReservation reservation = new TInventoryWarehouseAvailabilityReservation();
                    reservation.OrderId = dt.Rows[r][dt.GetColumnNo("orderid")].ToString();
                    reservation.OrderItemId = dt.Rows[r][dt.GetColumnNo("masteritemid")].ToString();
                    reservation.FromDateTime = FwConvert.ToDateTime(dt.Rows[r][dt.GetColumnNo("availfromdatetime")].ToString());
                    reservation.ToDateTime = FwConvert.ToDateTime(dt.Rows[r][dt.GetColumnNo("availtodatetime")].ToString());
                    reservation.QuantityOrdered = FwConvert.ToDecimal(dt.Rows[r][dt.GetColumnNo("qtyordered")].ToString());
                    availData.Reservations.Add(reservation);
                }
            }

            // use the Reservations above to calculate future availability for this Icode
            availData.Dates = new Dictionary<DateTime, TInventoryWarehouseAvailabilityDate>();
            DateTime theDate = DateTime.Today;
            while (theDate <= toDate)
            {
                TInventoryWarehouseAvailabilityDate inventoryWarehouseAvailabilityDate = new TInventoryWarehouseAvailabilityDate();
                inventoryWarehouseAvailabilityDate.AvailabilityDate = theDate;

                TInventoryWarehouseAvailabilityQuantity available = new TInventoryWarehouseAvailabilityQuantity();
                TInventoryWarehouseAvailabilityQuantity reserved = new TInventoryWarehouseAvailabilityQuantity();
                TInventoryWarehouseAvailabilityQuantity returning = new TInventoryWarehouseAvailabilityQuantity();

                foreach (TInventoryWarehouseAvailabilityReservation reservation in availData.Reservations)
                {
                    if ((reservation.FromDateTime <= theDate) && (theDate <= reservation.ToDateTime))
                    {
                        available.Owned = (available.Owned - reservation.QuantityOrdered);
                        available.Consigned = 0;
                        available.Total = available.Owned;

                        reserved.Owned = (reserved.Owned + reservation.QuantityOrdered);
                        reserved.Consigned = 0;
                        reserved.Total = reserved.Owned;


                    }
                    if (reservation.ToDateTime == theDate)
                    {
                        returning.Owned = (returning.Owned - reservation.QuantityOrdered);
                        returning.Consigned = 0;
                        returning.Total = returning.Owned;
                    }
                }

                inventoryWarehouseAvailabilityDate.Available = available;
                inventoryWarehouseAvailabilityDate.Reserved = reserved;
                inventoryWarehouseAvailabilityDate.Returning = returning;

                availData.Dates.Add(theDate, inventoryWarehouseAvailabilityDate);
                theDate = theDate.AddDays(1);
            }
            AvailabilityCache[availKey] = availData;

            success = true;

            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public static void DeleteAvailability(FwApplicationConfig appConfig, FwUserSession userSession, string inventoryId, string warehouseId)
        {
            TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey();
            availKey.InventoryId = inventoryId;
            availKey.WarehouseId = warehouseId;
            AvailabilityCache.Remove(availKey);
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<Dictionary<DateTime, TInventoryWarehouseAvailabilityDate>> GetAvailability(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId, string inventoryId, string warehouseId, DateTime fromDate, DateTime toDate)
        {
            bool found = false;
            Dictionary<DateTime, TInventoryWarehouseAvailabilityDate> inventoryWarehouseAvailabilityDates = new Dictionary<DateTime, TInventoryWarehouseAvailabilityDate>();

            if (fromDate < DateTime.Today)
            {
                fromDate = DateTime.Today;
            }

            if (toDate < DateTime.Today)
            {
                toDate = DateTime.Today;
            }

            TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey();
            availKey.InventoryId = inventoryId;
            availKey.WarehouseId = warehouseId;

            TInventoryWarehouseAvailability inventoryWarehouseAvailability = null;
            if (AvailabilityCache.TryGetValue(availKey, out inventoryWarehouseAvailability))
            {
                DateTime theDate = fromDate;
                TInventoryWarehouseAvailabilityDate inventoryWarehouseAvailabilityDate = null;
                while (theDate <= toDate)
                {
                    inventoryWarehouseAvailabilityDate = null;
                    if (inventoryWarehouseAvailability.Dates.TryGetValue(theDate, out inventoryWarehouseAvailabilityDate))
                    {
                        inventoryWarehouseAvailabilityDates.Add(theDate, inventoryWarehouseAvailabilityDate);
                        found = true;
                    }
                    else
                    {
                        found = false;
                        break;
                    }
                    theDate = theDate.AddDays(1);
                }
            }

            if (!found)
            {
                await RefreshAvailability(appConfig, userSession, sessionId, inventoryId, warehouseId, toDate);
                inventoryWarehouseAvailabilityDates = await GetAvailability(appConfig, userSession, sessionId, inventoryId, warehouseId, fromDate, toDate);
            }

            return inventoryWarehouseAvailabilityDates;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
