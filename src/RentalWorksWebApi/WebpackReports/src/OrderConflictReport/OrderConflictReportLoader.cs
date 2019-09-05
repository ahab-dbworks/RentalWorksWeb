using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using WebApi.Modules.Home.InventoryAvailability;
using WebLibrary;
using System;

namespace WebApi.Modules.Reports.OrderConflictReport
{

    //------------------------------------------------------------------------------------ 
    // this is a class used just for sorting the conflicts by Warehouse, Department, Deal, Order Number, I-Code
    public class OrderConflict : List<object>
    {
        public OrderConflict()
        {
            const int COLUMN_COUNT = 29;
            for (int i = 0; i < COLUMN_COUNT; i++)
            {
                Add("");
            }
        }
    }
    //------------------------------------------------------------------------------------ 
    public class OrderConflictComparer : IComparer<OrderConflict>
    {
        public int Compare(OrderConflict a, OrderConflict b)
        {

            const int WAREHOUSE_COLUMN_INDEX = 3;
            const int DEPARTMENT_COLUMN_INDEX = 16;
            const int DEAL_COLUMN_INDEX = 17;
            const int ORDER_NUMBER_COLUMN_INDEX = 18;
            const int ICODE_COLUMN_INDEX = 11;

            string warehouseA = (a[WAREHOUSE_COLUMN_INDEX]).ToString();
            string warehouseB = (b[WAREHOUSE_COLUMN_INDEX]).ToString();
            string departmentA = (a[DEPARTMENT_COLUMN_INDEX]).ToString();
            string departmentB = (b[DEPARTMENT_COLUMN_INDEX]).ToString();
            string dealA = (a[DEAL_COLUMN_INDEX]).ToString();
            string dealB = (b[DEAL_COLUMN_INDEX]).ToString();
            string orderNumberA = (a[ORDER_NUMBER_COLUMN_INDEX]).ToString();
            string orderNumberB = (b[ORDER_NUMBER_COLUMN_INDEX]).ToString();
            string iCodeA = (a[ICODE_COLUMN_INDEX]).ToString();
            string iCodeB = (b[ICODE_COLUMN_INDEX]).ToString();

            string allA = warehouseA.PadRight(255) + departmentA.PadRight(255) + dealA.PadRight(255) + orderNumberA.PadRight(255) + iCodeA.PadRight(255);
            string allB = warehouseB.PadRight(255) + departmentB.PadRight(255) + dealB.PadRight(255) + orderNumberB.PadRight(255) + iCodeB.PadRight(255);
            return allA.CompareTo(allB);
        }
    }
    //------------------------------------------------------------------------------------ 

    [FwSqlTable("availabilitymasterwhview")]
    public class OrderConflictReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string ItemDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "class", modeltype: FwDataTypes.Text)]
        public string Classification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availbyhour", modeltype: FwDataTypes.Boolean)]
        public bool? AvailabilityByHour { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "noavail", modeltype: FwDataTypes.Boolean)]
        public bool? NoAvailabilityCheck { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string OrderFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string OrderToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string ItemFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string ItemToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "0", modeltype: FwDataTypes.Text)]
        public int? QuantitySub { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "0", modeltype: FwDataTypes.Text)]
        public int? QuantityReserved { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "0", modeltype: FwDataTypes.Text)]
        public int? QuantityAvailable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "0", modeltype: FwDataTypes.Text)]
        public int? QuantityInRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string ConflictType { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(OrderConflictReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();

                    if (!string.IsNullOrEmpty(request.AvailableFor))
                    {
                        select.AddWhere("availfor = @availfor");
                        select.AddParameter("@availfor", request.AvailableFor);
                    }

                    select.AddWhereIn("warehouseid", request.WarehouseId);
                    select.AddWhereIn("inventorydepartmentid", request.InventoryTypeId);
                    select.AddWhereIn("categoryid", request.CategoryId);
                    select.AddWhereIn("subcategoryid", request.SubCategoryId);
                    select.AddWhereIn("masterid", request.InventoryId);
                    select.AddWhereIn("class", request.Classifications);
                    select.AddWhereIn("trackedby", request.TrackedBys);
                    select.AddWhereIn("rank", request.Ranks);



                    select.AddWhereIn("warehouseid", request.WarehouseId);
                    select.AddOrderBy("warehouse, inventorydepartment, category, subcategory, masterno, master");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }

            TInventoryWarehouseAvailabilityRequestItems availRequestItems = new TInventoryWarehouseAvailabilityRequestItems();
            foreach (List<object> row in dt.Rows)
            {
                string inventoryId = row[dt.GetColumnNo("InventoryId")].ToString();
                string warehouseId = row[dt.GetColumnNo("WarehouseId")].ToString();
                availRequestItems.Add(new TInventoryWarehouseAvailabilityRequestItem(inventoryId, warehouseId, request.FromDate, request.ToDate));
            }
            bool refreshIfNeeded = true; // user may want to make this true in some cases
            TAvailabilityCache availCache = InventoryAvailabilityFunc.GetAvailability(AppConfig, UserSession, availRequestItems, refreshIfNeeded).Result;

            List<OrderConflict> conflictRows = new List<OrderConflict>();

            foreach (List<object> row in dt.Rows)
            {
                string inventoryId = row[dt.GetColumnNo("InventoryId")].ToString();
                string warehouseId = row[dt.GetColumnNo("WarehouseId")].ToString();

                TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);
                TInventoryWarehouseAvailability availData = null;
                if (availCache.TryGetValue(availKey, out availData))
                {
                    bool hasConflict = ((string.IsNullOrEmpty(request.ConflictType) && (availData.HasNegativeConflict || availData.HasPositiveConflict)) ||
                                       (!string.IsNullOrEmpty(request.ConflictType) && request.ConflictType.Equals(RwConstants.INVENTORY_CONFLICT_TYPE_NEGATIVE) && availData.HasNegativeConflict) ||
                                       (!string.IsNullOrEmpty(request.ConflictType) && request.ConflictType.Equals(RwConstants.INVENTORY_CONFLICT_TYPE_POSITIVE) && availData.HasPositiveConflict));

                    if (hasConflict)
                    {
                        if (availData.Reservations.Count > 0)
                        {
                            foreach (TInventoryWarehouseAvailabilityReservation reservation in availData.Reservations)
                            {
                                if (reservation.QuantityReserved.Total != 0)
                                {
                                    bool isConflict = ((string.IsNullOrEmpty(request.ConflictType) && (reservation.IsNegativeConflict || reservation.IsPositiveConflict)) ||
                                                      (!string.IsNullOrEmpty(request.ConflictType) && request.ConflictType.Equals(RwConstants.INVENTORY_CONFLICT_TYPE_NEGATIVE) && reservation.IsNegativeConflict) ||
                                                      (!string.IsNullOrEmpty(request.ConflictType) && request.ConflictType.Equals(RwConstants.INVENTORY_CONFLICT_TYPE_POSITIVE) && reservation.IsPositiveConflict));

                                    bool showOrderOrDeal = true;
                                    if (showOrderOrDeal)
                                    {
                                        if (!string.IsNullOrEmpty(request.OrderId))
                                        {
                                            showOrderOrDeal = request.OrderId.Contains(reservation.OrderId);
                                        }
                                    }
                                    if (showOrderOrDeal)
                                    {
                                        if (!string.IsNullOrEmpty(request.DealId))
                                        {
                                            showOrderOrDeal = request.DealId.Contains(reservation.DealId);
                                        }
                                    }

                                    if (isConflict && showOrderOrDeal)
                                    {
                                        OrderConflict conflictRow = new OrderConflict();
                                        conflictRow[dt.GetColumnNo("RowType")] = "detail";
                                        conflictRow[dt.GetColumnNo("WarehouseId")] = availData.InventoryWarehouse.WarehouseId;
                                        conflictRow[dt.GetColumnNo("WarehouseCode")] = availData.InventoryWarehouse.WarehouseCode;
                                        conflictRow[dt.GetColumnNo("Warehouse")] = availData.InventoryWarehouse.Warehouse;
                                        conflictRow[dt.GetColumnNo("InventoryTypeId")] = availData.InventoryWarehouse.InventoryTypeId;
                                        conflictRow[dt.GetColumnNo("InventoryType")] = availData.InventoryWarehouse.InventoryType;
                                        conflictRow[dt.GetColumnNo("CategoryId")] = availData.InventoryWarehouse.CategoryId;
                                        conflictRow[dt.GetColumnNo("Category")] = availData.InventoryWarehouse.Category;
                                        conflictRow[dt.GetColumnNo("SubCategoryId")] = availData.InventoryWarehouse.SubCategoryId;
                                        conflictRow[dt.GetColumnNo("SubCategory")] = availData.InventoryWarehouse.SubCategory;
                                        conflictRow[dt.GetColumnNo("InventoryId")] = availData.InventoryWarehouse.InventoryId;
                                        conflictRow[dt.GetColumnNo("ICode")] = availData.InventoryWarehouse.ICode;
                                        conflictRow[dt.GetColumnNo("ItemDescription")] = availData.InventoryWarehouse.Description;
                                        conflictRow[dt.GetColumnNo("Classification")] = availData.InventoryWarehouse.Classification;
                                        conflictRow[dt.GetColumnNo("AvailabilityByHour")] = availData.InventoryWarehouse.HourlyAvailability;
                                        conflictRow[dt.GetColumnNo("NoAvailabilityCheck")] = availData.InventoryWarehouse.NoAvailabilityCheck;
                                        conflictRow[dt.GetColumnNo("Department")] = reservation.Department;
                                        conflictRow[dt.GetColumnNo("Deal")] = reservation.Deal;
                                        conflictRow[dt.GetColumnNo("OrderNumber")] = reservation.OrderNumber;
                                        conflictRow[dt.GetColumnNo("OrderDescription")] = reservation.OrderDescription;
                                        conflictRow[dt.GetColumnNo("OrderFromDate")] = FwConvert.ToString(reservation.FromDateTime);
                                        conflictRow[dt.GetColumnNo("OrderToDate")] = (reservation.ToDateTime.Equals(InventoryAvailabilityFunc.LateDateTime) ? "LATE" : FwConvert.ToString(reservation.ToDateTime));
                                        conflictRow[dt.GetColumnNo("ItemFromDate")] = FwConvert.ToString(reservation.FromDateTime);
                                        conflictRow[dt.GetColumnNo("ItemToDate")] = (reservation.ToDateTime.Equals(InventoryAvailabilityFunc.LateDateTime) ? "LATE" : FwConvert.ToString(reservation.ToDateTime));
                                        conflictRow[dt.GetColumnNo("QuantitySub")] = reservation.QuantitySub;
                                        conflictRow[dt.GetColumnNo("QuantityReserved")] = reservation.QuantityReserved.Total;
                                        TInventoryWarehouseAvailabilityMinimum minAvail = availData.GetMinimumAvailableQuantity(reservation.FromDateTime, reservation.ToDateTime);
                                        conflictRow[dt.GetColumnNo("QuantityAvailable")] = minAvail.MinimumAvailable.Total;
                                        conflictRow[dt.GetColumnNo("QuantityInRepair")] = availData.InRepair.Total;
                                        conflictRow[dt.GetColumnNo("ConflictType")] = (reservation.IsNegativeConflict ? RwConstants.INVENTORY_CONFLICT_TYPE_NEGATIVE_DESCRIPTION : reservation.IsPositiveConflict ? RwConstants.INVENTORY_CONFLICT_TYPE_POSITIVE_DESCRIPTION : "");
                                        conflictRows.Add(conflictRow);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            dt.Rows.Clear();
            //OrderConflictComparer conflictComparer = new OrderConflictComparer();
            //conflictRows.Sort(conflictComparer);
            conflictRows.Sort(new OrderConflictComparer());
            dt.Rows.AddRange(conflictRows);

            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "QuantitySub", "QuantityReserved", "QuantityAvailable", "QuantityInRepair" };
                dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
                dt.InsertSubTotalRows("Department", "RowType", totalFields);
                dt.InsertSubTotalRows("Deal", "RowType", totalFields);

                string[] orderHeaderFields = new string[] { "OrderNumber", "OrderDescription", "OrderFromDate", "OrderToDate" };
                dt.InsertSubTotalRows("OrderNumber", "RowType", totalFields, nameHeaderColumns: orderHeaderFields);

                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
