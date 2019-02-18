using System;
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebLibrary;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Modules.Home.InventoryAvailabilityFunc
{
    //-------------------------------------------------------------------------------------------------------
    public class TAvailabilityProgress
    {
        public int PercentComplete { get; set; } = 0;
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityReservation
    {
        public string OrderId { get; set; }
        public string OrderItemId { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
        public decimal ResevedQuantity { get; set; } = 0;
        public decimal QuantityOrdered { get; set; } = 0;
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityDate
    {
        public DateTime AvailabilityDate { get; set; }
        public decimal AvailableQuantityOwned { get; set; } = 0;
        public decimal AvailableQuantityConsigned { get; set; } = 0;
        public decimal AvailableQuantityTotal { get; set; } = 0;


        public decimal ReservedQuantityOwned { get; set; } = 0;
        public decimal ReservedQuantityConsigned { get; set; } = 0;
        public decimal ReservedQuantityTotal { get; set; } = 0;

        public decimal ReturningQuantityOwned { get; set; } = 0;
        public decimal ReturningQuantityConsigned { get; set; } = 0;
        public decimal ReturningQuantityTotal { get; set; } = 0;
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailability
    {
        public string InventoryId { get; set; } = "";
        public string WarehouseId { get; set; } = "";
        public decimal CurrentOwnedQuantity { get; set; } = 0;
        public decimal CurrentConsignedQuantity { get; set; } = 0;
        public decimal CurrentTotalQuantity { get; set; } = 0;
        public List<TInventoryWarehouseAvailabilityReservation> Reservations = new List<TInventoryWarehouseAvailabilityReservation>();
        public Dictionary<DateTime, TInventoryWarehouseAvailabilityDate> Dates = new Dictionary<DateTime, TInventoryWarehouseAvailabilityDate>();
    }
    //-------------------------------------------------------------------------------------------------------

    public static class InventoryAvailabilityFunc
    {
        //-------------------------------------------------------------------------------------------------------
        private static Dictionary<string, TAvailabilityProgress> AvailabilitySessions = new Dictionary<string, TAvailabilityProgress>();
        private static Dictionary<string, TInventoryWarehouseAvailability> AvailabilityCache = new Dictionary<string, TInventoryWarehouseAvailability>();
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
        public static bool DumpAvailabilityToFile(string InventoryId = "", string WarehouseId = "", DateTime? theDate = null)
        {
            bool success = true;
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, TInventoryWarehouseAvailability> availEntry in AvailabilityCache)
            {
                TInventoryWarehouseAvailability inventoryWarehouseAvailability = (TInventoryWarehouseAvailability)availEntry.Value;
                sb.Append("InventoryId: ");
                sb.Append(inventoryWarehouseAvailability.InventoryId);
                sb.Append(", ");
                sb.Append("WarehouseId: ");
                sb.Append(inventoryWarehouseAvailability.WarehouseId);
                sb.Append(", ");
                sb.AppendLine();
                sb.Append("   ");
                sb.Append("Current Owned Quantity: ");
                sb.Append(inventoryWarehouseAvailability.CurrentOwnedQuantity.ToString());
                sb.Append(", ");
                sb.Append("Current Consigned Quantity: ");
                sb.Append(inventoryWarehouseAvailability.CurrentConsignedQuantity.ToString());
                sb.Append(", ");
                sb.Append("Current Total Quantity: ");
                sb.Append(inventoryWarehouseAvailability.CurrentTotalQuantity.ToString());
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
                    sb.Append(inventoryWarehouseAvailabilityDate.AvailableQuantityOwned.ToString());
                    sb.Append(" ");
                    sb.Append("Consigned Available: ");
                    sb.Append(inventoryWarehouseAvailabilityDate.AvailableQuantityConsigned.ToString());
                    sb.Append(" ");
                    sb.Append("Total Available: ");
                    sb.Append(inventoryWarehouseAvailabilityDate.AvailableQuantityTotal.ToString());
                    sb.AppendLine();
                }
                sb.AppendLine();
            }
            System.IO.File.WriteAllText(@"C:\temp\availability.txt", sb.ToString());
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<bool> RefreshAvailability(FwApplicationConfig appConfig, FwUserSession userSession, string sessionId, string inventoryId, string warehouseId, DateTime toDate)
        {
            bool success = false;
            TInventoryWarehouseAvailability availData = new TInventoryWarehouseAvailability();
            availData.InventoryId = inventoryId;
            availData.WarehouseId = warehouseId;

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
                    availData.CurrentTotalQuantity = FwConvert.ToDecimal(dt.Rows[0][dt.GetColumnNo("qty")].ToString());
                    availData.CurrentConsignedQuantity = 0;  // temporary
                    availData.CurrentOwnedQuantity = (availData.CurrentTotalQuantity - availData.CurrentConsignedQuantity);
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

                decimal availableQuantityOwned = availData.CurrentOwnedQuantity;
                decimal availableQuantityConsigned = availData.CurrentConsignedQuantity;
                decimal availableQuantityTotal = availData.CurrentTotalQuantity;

                decimal reservedQuantityOwned = 0;
                decimal reservedQuantityConsigned = 0;
                decimal reservedQuantityTotal = 0;

                decimal returningQuantityOwned = 0;
                decimal returningQuantityConsigned = 0;
                decimal returningQuantityTotal = 0;

                foreach (TInventoryWarehouseAvailabilityReservation reservation in availData.Reservations)
                {
                    if ((reservation.FromDateTime <= theDate) && (theDate <= reservation.ToDateTime))
                    {
                        availableQuantityOwned = (availableQuantityOwned - reservation.QuantityOrdered);
                        availableQuantityConsigned = 0;
                        availableQuantityTotal = availableQuantityOwned;

                        reservedQuantityOwned = (reservedQuantityOwned + reservation.QuantityOrdered);
                        reservedQuantityConsigned = 0;
                        reservedQuantityTotal = reservedQuantityOwned;


                    }
                    if (reservation.ToDateTime == theDate)
                    {
                        returningQuantityOwned = (returningQuantityOwned - reservation.QuantityOrdered);
                        returningQuantityConsigned = 0;
                        returningQuantityTotal = returningQuantityOwned;
                    }
                }

                //available
                inventoryWarehouseAvailabilityDate.AvailableQuantityOwned = availableQuantityOwned;
                inventoryWarehouseAvailabilityDate.AvailableQuantityConsigned = availableQuantityConsigned;
                inventoryWarehouseAvailabilityDate.AvailableQuantityTotal = availableQuantityTotal;

                //reserved
                inventoryWarehouseAvailabilityDate.ReservedQuantityOwned = reservedQuantityOwned;
                inventoryWarehouseAvailabilityDate.ReservedQuantityConsigned = reservedQuantityConsigned;
                inventoryWarehouseAvailabilityDate.ReservedQuantityTotal = reservedQuantityTotal;

                //returning
                inventoryWarehouseAvailabilityDate.ReturningQuantityOwned = returningQuantityOwned;
                inventoryWarehouseAvailabilityDate.ReturningQuantityConsigned = returningQuantityConsigned;
                inventoryWarehouseAvailabilityDate.ReturningQuantityTotal = returningQuantityTotal;


                availData.Dates.Add(theDate, inventoryWarehouseAvailabilityDate);
                theDate = theDate.AddDays(1);
            }
            //AvailabilityCache.Remove(inventoryId + warehouseId);
            //AvailabilityCache.Add(inventoryId + warehouseId, availData);
            AvailabilityCache[inventoryId + warehouseId] = availData;

            success = true;

            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public static void DeleteAvailability(FwApplicationConfig appConfig, FwUserSession userSession, string inventoryId, string warehouseId)
        {
            AvailabilityCache.Remove(inventoryId + warehouseId);
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

            string availabilityKey = inventoryId + warehouseId;
            TInventoryWarehouseAvailability inventoryWarehouseAvailability = null;
            if (AvailabilityCache.TryGetValue(availabilityKey, out inventoryWarehouseAvailability))
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
