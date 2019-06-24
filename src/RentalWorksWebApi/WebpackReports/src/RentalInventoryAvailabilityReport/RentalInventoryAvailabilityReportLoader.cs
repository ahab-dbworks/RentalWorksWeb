using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using WebApi.Modules.Home.InventoryAvailability;
using System.Collections.Generic;
using System;

namespace WebApi.Modules.Reports.RentalInventoryAvailabilityReport
{
    [FwSqlTable("availabilitymasterwhview")]
    public class RentalInventoryAvailabilityReportLoader : AppDataLoadRecord
    {
        const int MAX_AVAILABILITY_DATE_COLUMNS = 30;
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
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string OrderTypeDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.DateTime)]
        public string FromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.DateTime)]
        public string ToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? SubRentQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? LateQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate01 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt01 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString01 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate02 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt02 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString02 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate03 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt03 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString03 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate04 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt04 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString04 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate05 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt05 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString05 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate06 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt06 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString06 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate07 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt07 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString07 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate08 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt08 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString08 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate09 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt09 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString09 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate10 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt10 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString10 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate11 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt11 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString11 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate12 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt12 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString12 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate13 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt13 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString13 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate14 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt14 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString14 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate15 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt15 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString15 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate16 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt16 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString16 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate17 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt17 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString17 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate18 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt18 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString18 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate19 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt19 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString19 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate20 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt20 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString20 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate21 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt21 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString21 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate22 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt22 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString22 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate23 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt23 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString23 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate24 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt24 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString24 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate25 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt25 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString25 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate26 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt26 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString26 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate27 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt27 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString27 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate28 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt28 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString28 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate29 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt29 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString29 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailabilityDate30 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Integer)]
        public int? AvailableInt30 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "''", modeltype: FwDataTypes.Text)]
        public string AvailableString30 { get; set; }
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
                    select.AddWhere("noavail <> 'T'");
                    select.AddWhereIn("warehouseid", request.WarehouseId);
                    select.AddWhereIn("inventorydepartmentid", request.InventoryTypeId);
                    select.AddWhereIn("categoryid", request.CategoryId);
                    select.AddWhereIn("subcategoryid", request.SubCategoryId);
                    select.AddWhereIn("masterid", request.InventoryId);
                    select.AddWhereIn("class", request.Classifications);
                    select.AddWhereIn("trackedby", request.TrackedBys);
                    select.AddWhereIn("rank", request.Ranks);
                    if (!request.IncludeZeroQuantity.GetValueOrDefault(false))
                    {
                        select.AddWhere("totalqty <> 0");
                    }
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

            List<int> rowsToDelete = new List<int>();
            int rowIndex = 0;
            //populate the Availability Date column headings and the AvailableInt columns
            foreach (List<object> row in dt.Rows)
            {
                string inventoryId = row[dt.GetColumnNo("InventoryId")].ToString();
                string warehouseId = row[dt.GetColumnNo("WarehouseId")].ToString();
                TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);
                TInventoryWarehouseAvailability availData = null;
                if (availCache.TryGetValue(availKey, out availData))
                {
                    row[dt.GetColumnNo("LateQuantity")] = availData.Late.Total;
                    DateTime theDate = request.FromDate;
                    bool hasNegative = false;
                    bool hasLow = false;
                    int x = 1;
                    while ((theDate <= request.ToDate) && (x <= MAX_AVAILABILITY_DATE_COLUMNS)) // 30 days max 
                    {
                        row[dt.GetColumnNo("AvailabilityDate" + x.ToString().PadLeft(2, '0'))] = theDate.Month.ToString() + "/" + theDate.Day.ToString();
                        TInventoryWarehouseAvailabilityDateTime availDateTime = null;
                        if (availData.AvailabilityDatesAndTimes.TryGetValue(theDate, out availDateTime))
                        {
                            int availQtyAsInt = (int)Math.Floor(availDateTime.Available.Total);
                            row[dt.GetColumnNo("AvailableInt" + x.ToString().PadLeft(2, '0'))] = availQtyAsInt;

                            if (availQtyAsInt < 0)
                            {
                                hasNegative = true;
                            }
                            else if ((availQtyAsInt >= 0) && (availData.InventoryWarehouse.LowAvailabilityPercent != 0) && (availQtyAsInt <= availData.InventoryWarehouse.LowAvailabilityQuantity))
                            {
                                hasLow = true;
                            }
                        }
                        theDate = theDate.AddDays(1);  // daily inventory   #jhtodo: hourly
                        x++;
                    }

                    if ((request.OnlyIncludeNegative.GetValueOrDefault(false)) && (!hasNegative))
                    {
                        rowsToDelete.Add(rowIndex);
                    }
                    else if ((request.OnlyIncludeLowAndNegative.GetValueOrDefault(false)) && (!hasLow) && (!hasNegative))
                    {
                        rowsToDelete.Add(rowIndex);
                    }
                }
                rowIndex++;
            }

            if (rowsToDelete.Count > 0)
            {
                //traverse dt.Rows in reverse to remove items
                for (int x = dt.Rows.Count - 1; x >= 0; x--)
                {
                    if (rowsToDelete.Contains(x))
                    {
                        dt.Rows.RemoveAt(x);
                    }
                }
            }

            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "AvailableInt01", "AvailableInt02", "AvailableInt03", "AvailableInt04", "AvailableInt05", "AvailableInt06", "AvailableInt07", "AvailableInt08", "AvailableInt09", "AvailableInt10", "AvailableInt11", "AvailableInt12", "AvailableInt13", "AvailableInt14", "AvailableInt15", "AvailableInt16", "AvailableInt17", "AvailableInt18", "AvailableInt19", "AvailableInt20", "AvailableInt21", "AvailableInt22", "AvailableInt23", "AvailableInt24", "AvailableInt25", "AvailableInt26", "AvailableInt27", "AvailableInt28", "AvailableInt29", "AvailableInt30" };
                dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
                dt.InsertSubTotalRows("InventoryType", "RowType", totalFields);
                dt.InsertSubTotalRows("Category", "RowType", totalFields);
                //dt.InsertSubTotalRows("ICode", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }

            // if detail mode, add orders here
            if (request.IsDetail.GetValueOrDefault(false))
            {
                List<List<object>> rowsWithDetail = new List<List<object>>();  // create a new rows list to copy into

                foreach (List<object> row in dt.Rows)
                {
                    rowsWithDetail.Add(row);

                    string inventoryId = row[dt.GetColumnNo("InventoryId")].ToString();
                    string warehouseId = row[dt.GetColumnNo("WarehouseId")].ToString();
                    TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);
                    TInventoryWarehouseAvailability availData = null;
                    if (availCache.TryGetValue(availKey, out availData))
                    {
                        foreach (TInventoryWarehouseAvailabilityReservation reservation in availData.Reservations)
                        {
                            if ((reservation.FromDateTime <= request.ToDate) && (reservation.ToDateTime >= request.FromDate))
                            {
                                List<object> reservationRow = new List<object>();

                                //copy the entire row
                                foreach (object obj in row)
                                {
                                    reservationRow.Add(obj);
                                }
                                reservationRow[dt.GetColumnNo("OrderId")] = reservation.OrderId;
                                reservationRow[dt.GetColumnNo("OrderType")] = reservation.OrderType;
                                reservationRow[dt.GetColumnNo("OrderTypeDescription")] = reservation.OrderTypeDescription;
                                reservationRow[dt.GetColumnNo("OrderNumber")] = reservation.OrderNumber;
                                reservationRow[dt.GetColumnNo("OrderDescription")] = reservation.OrderDescription;
                                reservationRow[dt.GetColumnNo("Deal")] = reservation.Deal;
                                reservationRow[dt.GetColumnNo("FromDate")] = reservation.FromDateTime;
                                reservationRow[dt.GetColumnNo("ToDate")] = reservation.ToDateTime;
                                reservationRow[dt.GetColumnNo("SubRentQuantity")] = reservation.QuantitySub;
                                reservationRow[dt.GetColumnNo("LateQuantity")] = reservation.QuantityLate.Total;

                                DateTime theDate = request.FromDate;
                                int x = 1;
                                while ((theDate <= request.ToDate) && (x <= MAX_AVAILABILITY_DATE_COLUMNS)) // 30 days max 
                                {

                                    if ((reservation.FromDateTime <= theDate) && (theDate <= reservation.ToDateTime))
                                    {
                                        int reservedQtyAsInt = (int)Math.Floor(reservation.QuantityReserved.Owned);
                                        reservationRow[dt.GetColumnNo("AvailableInt" + x.ToString().PadLeft(2, '0'))] = reservedQtyAsInt;
                                    }
                                    theDate = theDate.AddDays(1);  // daily inventory   #jhtodo: hourly
                                    x++;
                                }

                                rowsWithDetail.Add(reservationRow);
                            }
                        }
                    }
                }
                dt.Rows = rowsWithDetail;
            }

            //populate AvailableString columns
            foreach (List<object> row in dt.Rows)
            {
                DateTime theDate = request.FromDate;
                int x = 1;
                while ((theDate <= request.ToDate) && (x <= MAX_AVAILABILITY_DATE_COLUMNS)) // 30 days max 
                {
                    string availQtyAsString = "";
                    object availQtyAsObj = row[dt.GetColumnNo("AvailableInt" + x.ToString().PadLeft(2, '0'))];
                    if (availQtyAsObj != null)
                    {
                        availQtyAsString = availQtyAsObj.ToString();
                    }
                    row[dt.GetColumnNo("AvailableString" + x.ToString().PadLeft(2, '0'))] = availQtyAsString;
                    theDate = theDate.AddDays(1);  // daily inventory   #jhtodo: hourly
                    x++;
                }
            }

            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
