using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using WebApi.Modules.Home.InventoryAvailability;
using System.Collections.Generic;
using System;

namespace WebApi.Modules.Reports.RentalInventoryAvailabilityReport
{
    [FwSqlTable("availabilitymasterwhview")]
    public class RentalInventoryAvailabilityReportLoader : AppDataLoadRecord
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
        public string Description { get; set; }
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
        [FwSqlDataField(column: "totalqty", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalqtyin", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalQuantityIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalqtystaged", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalQuantityStaged { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalqtyout", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalQuantityOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalqtyintransit", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalQuantityInTransit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalqtyinrepair", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalQuantityInRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalqtyontruck", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalQuantityOnTruck { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalqtyincontainer", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalQuantityInContainer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedqty", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedqtyin", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedQuantityIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedqtystaged", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedQuantityStaged { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedqtyout", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedQuantityOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedqtyintransit", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedQuantityInTransit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedqtyinrepair", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedQuantityInRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedqtyontruck", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedQuantityOnTruck { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownedqtyincontainer", modeltype: FwDataTypes.Decimal)]
        public decimal? OwnedQuantityInContainer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignedqty", modeltype: FwDataTypes.Decimal)]
        public decimal? ConsignedQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignedqtyin", modeltype: FwDataTypes.Decimal)]
        public decimal? ConsignedQuantityIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignedqtystaged", modeltype: FwDataTypes.Decimal)]
        public decimal? ConsignedQuantityStaged { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignedqtyout", modeltype: FwDataTypes.Decimal)]
        public decimal? ConsignedQuantityOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignedqtyintransit", modeltype: FwDataTypes.Decimal)]
        public decimal? ConsignedQuantityInTransit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignedqtyinrepair", modeltype: FwDataTypes.Decimal)]
        public decimal? ConsignedQuantityInRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignedqtyontruck", modeltype: FwDataTypes.Decimal)]
        public decimal? ConsignedQuantityOnTruck { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignedqtyincontainer", modeltype: FwDataTypes.Decimal)]
        public decimal? ConsignedQuantityInContainer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date01 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available01 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date02 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available02 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date03 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available03 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date04 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available04 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date05 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available05 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date06 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available06 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date07 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available07 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date08 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available08 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date09 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available09 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date10 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available10 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date11 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available11 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date12 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available12 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date13 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available13 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date14 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available14 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date15 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available15 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date16 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available16 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date17 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available17 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date18 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available18 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date19 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available19 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date20 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available20 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date21 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available21 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date22 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available22 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date23 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available23 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date24 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available24 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date25 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available25 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date26 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available26 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date27 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available27 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date28 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available28 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date29 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available29 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Date30 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Available30 { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(RentalInventoryAvailabilityReportRequest request)
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
                    //select.AddWhere("(xxxxid ^> ')"); 
                    select.AddWhereIn("warehouseid", request.WarehouseId);
                    select.AddWhereIn("inventorydepartmentid", request.InventoryTypeId);
                    select.AddWhereIn("categoryid", request.CategoryId);
                    select.AddWhereIn("subcategoryid", request.SubCategoryId);
                    select.AddWhereIn("masterid", request.InventoryId);
                    select.AddWhereIn("class", request.Classifications);
                    select.AddWhereIn("trackedby", request.TrackedBys);
                    select.AddWhereIn("rank", request.Ranks);


                    //if (!request.BooleanField.GetValueOrDefault(false)) 
                    //{ 
                    //    select.AddWhere("somefield ^<^> 'T'"); 
                    //} 
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
            bool refreshIfNeeded = true; // user may want to make this true/false in some cases
            TAvailabilityCache availCache = InventoryAvailabilityFunc.GetAvailability(AppConfig, UserSession, availRequestItems, refreshIfNeeded).Result;

            foreach (List<object> row in dt.Rows)
            {
                string inventoryId = row[dt.GetColumnNo("InventoryId")].ToString();
                string warehouseId = row[dt.GetColumnNo("WarehouseId")].ToString();
                TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);
                TInventoryWarehouseAvailability availData = null;
                if (availCache.TryGetValue(availKey, out availData))
                {
                    DateTime theDate = request.FromDate;
                    for (int x = 1; x <= 30; x++)  // date slots
                    {
                        string availQtyAsString = "";
                        TInventoryWarehouseAvailabilityDateTime availDateTime = null;
                        if (availData.AvailabilityDatesAndTimes.TryGetValue(theDate, out availDateTime))
                        {
                            availQtyAsString = availDateTime.Available.Total.ToString();
                        }
                        row[dt.GetColumnNo("Date" + x.ToString().PadLeft(2, '0'))] = FwConvert.ToString(theDate);
                        row[dt.GetColumnNo("Available" + x.ToString().PadLeft(2, '0'))] = availQtyAsString;
                        theDate = theDate.AddDays(1);  // daily inventory
                    }
                }
            }



            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "Available01", "Available02", "Available03", "Available04", "Available05", "Available06", "Available07", "Available08", "Available09", "Available10", "Available11", "Available12", "Available13", "Available14", "Available15", "Available16", "Available17", "Available18", "Available19", "Available20", "Available21", "Available22", "Available23", "Available24", "Available25", "Available26", "Available27", "Available28", "Available29", "Available30" };
                dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
                dt.InsertSubTotalRows("InventoryType", "RowType", totalFields);
                dt.InsertSubTotalRows("Category", "RowType", totalFields);
                if (!request.IsSummary.GetValueOrDefault(false))
                {
                    dt.InsertSubTotalRows("ICode", "RowType", totalFields);
                }
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
